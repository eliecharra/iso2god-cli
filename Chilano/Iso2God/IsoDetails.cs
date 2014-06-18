using Chilano.Xbox360.Xdbf;

namespace Chilano.Iso2God
{
   using Xbox360.IO;
    using Xbox360.Iso;
   using Xbox360.Xex;
    using System;
    using System.ComponentModel;
    using System.Diagnostics;
   using System.IO;
    using System.Text;

    internal class IsoDetails
    {
        private IsoDetailsArgs args;
        private FileStream f;
        private GDF iso;

        public IsoDetails(IsoDetailsArgs arguments)
        {
            args = arguments;
        }

        public IsoDetailsResults IsoDetails_DoWork()
        {
           if (openIso())
            {
               if (iso.Exists("default.xex"))
                {
                    return readXex();
                }
               throw new Exception("Could not locate default.xex.");
            }
           throw new Exception("Could not open iso file.");
        }

       private bool openIso()
        {
            try
            {
                f = new FileStream(args.PathISO, FileMode.Open, FileAccess.Read, FileShare.ReadWrite);
                iso = new GDF(f);
            }
            catch (IOException exception)
            {
                Console.WriteLine("- Failed to open ISO image. Reason:\n\n" + exception.Message);
                return false;
            }
            catch (Exception exception2)
            {
                Console.WriteLine("- Unhandled exception occured when opening ISO image. Reason:\n\n" + exception2.Message);
                return false;
            }
            return true;
        }

        private IsoDetailsResults readXex()
        {
            IsoDetailsResults results = null;
            byte[] bytes = null;
            string path = null;
            string pathTemp = null;
            Console.WriteLine("+ Locating default.xex...");
            try
            {
                bytes = iso.GetFile("default.xex");
                pathTemp = args.PathTemp;
                path = pathTemp + "default.xex";
                Console.WriteLine("+ Extracting default.xex...");
                if ((bytes == null) || (bytes.Length == 0))
                {
                    throw new Exception("Couldn't locate default.xex. Please check this ISO is valid.");
                }
                File.WriteAllBytes(path, bytes);
            }
            catch (Exception exception)
            {
                throw new Exception("A problem occured when reading the contents of the ISO image.\n\nPlease ensure this is a valid Xbox 360 ISO by running it through ABGX360.\n\n" + exception.Message);
            }
            Console.WriteLine("+ Found! Reading default.xex...");
            using (XexInfo info = new XexInfo(bytes))
            {
                if (!info.IsValid)
                {
                    throw new Exception("Default.xex is not valid.");
                }
                if (info.Header.ContainsKey(XexInfoFields.ExecutionInfo))
                {
                    XexExecutionInfo info2 = (XexExecutionInfo) info.Header[XexInfoFields.ExecutionInfo];
                    results = new IsoDetailsResults("", DataConversion.BytesToHexString(info2.TitleID), DataConversion.BytesToHexString(info2.MediaID), info2.Platform.ToString(), info2.ExecutableType.ToString(), info2.DiscNumber.ToString(), info2.DiscCount.ToString());
                }
            }
            Console.WriteLine("+ Extracting resources...");
            Process process = new Process {
                EnableRaisingEvents = false
            };
            process.StartInfo.FileName = args.PathXexTool;
            if (File.Exists(process.StartInfo.FileName))
            {
                process.StartInfo.WorkingDirectory = pathTemp;
                process.StartInfo.Arguments = "-d . default.xex";
                process.StartInfo.UseShellExecute = false;
                process.StartInfo.RedirectStandardOutput = false;
                process.StartInfo.CreateNoWindow = true;
                try
                {
                    process.Start();
                    process.WaitForExit();
                    process.Close();
                }
                catch (Win32Exception)
                {
                    throw new Exception("Could not launch XexTool!");
                }
                if (results != null && File.Exists(pathTemp + results.TitleID))
                {
                    Xdbf xdbf = new Xdbf(File.ReadAllBytes(pathTemp + results.TitleID));                    
                    try
                    {
                        MemoryStream stream3 = new MemoryStream(xdbf.GetResource(1, 3));
                        stream3.Seek(0x11L, SeekOrigin.Begin);
                        int count = stream3.ReadByte();
                        results.Name = Encoding.UTF8.GetString(stream3.ToArray(), 0x12, count);
                        stream3.Close();
                    }
                    catch (Exception)
                    {
                        try
                        {
                            MemoryStream stream4 = new MemoryStream(xdbf.GetResource(1, 0));
                            stream4.Seek(0x11L, SeekOrigin.Begin);
                            int num2 = stream4.ReadByte();
                            results.Name = Encoding.UTF8.GetString(stream4.ToArray(), 0x12, num2);
                            stream4.Close();
                        }
                        catch (Exception)
                        {
                            results.Name = "Unable to read name.";
                        }
                    }
                }                
            }
            else
            {
               Console.WriteLine("- Couldn't locate XexTool. Expected location was:\n" + process.StartInfo.FileName + "\n\nTry disabling User Access Control if it's enabled.");
            }
            return results;
        }
    }
}

