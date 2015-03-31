using System.Diagnostics;
using System.Reflection;

namespace Chilano.Iso2God
{
    using System;
    using System.IO;

    internal static class Program
    {
        [STAThread]
        private static void Main()
        {
           Console.WriteLine("Iso2god-cli " + FileVersionInfo.GetVersionInfo(Assembly.GetExecutingAssembly().Location).FileVersion + " - " + FileVersionInfo.GetVersionInfo(Assembly.GetExecutingAssembly().Location).LegalCopyright);
            Console.WriteLine("Ported to CLI by Elie CHARRA <elie [dot] charra [at] gmail.com>");
            Console.WriteLine("Usage : iso2god <source iso> <destination folder>");
            Console.WriteLine("");

            String[] arguments = Environment.GetCommandLineArgs();

            if (arguments.Length == 3)
            {
                
                String isoPath = arguments[1];
                String destinationPath = arguments[2];

                Console.WriteLine("+ Computing ISO metadata ...");                
                bool IsRunningOnMono = (Type.GetType ("Mono.Runtime") != null);
		String xexToolExecutable = IsRunningOnMono ? "mono xextool.exe" : "xextool.exe";
		IsoDetails iso = new IsoDetails(new IsoDetailsArgs(isoPath, Path.GetTempPath(), Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location) + Path.DirectorySeparatorChar + xexToolExecutable));
                IsoDetailsResults isoDetailsResults = iso.IsoDetails_DoWork();
                IsoEntryID isoEntryID = new IsoEntryID(isoDetailsResults.TitleID, isoDetailsResults.MediaID, Convert.ToByte(isoDetailsResults.DiscNumber[0]), Convert.ToByte(isoDetailsResults.DiscCount[0]), Convert.ToByte(isoDetailsResults.Platform[0]), Convert.ToByte(isoDetailsResults.ExType[0]));
                IsoEntry isoEntry = new IsoEntry(IsoEntryPlatform.Xbox360, isoPath, destinationPath, new FileInfo(isoPath).Length, isoDetailsResults.Name, isoEntryID);

                Console.WriteLine("-----------------------------------------------------------");
                Console.WriteLine("> Title    : " + isoEntry.TitleName);
                Console.WriteLine("> Title ID : " + isoEntry.ID.TitleID);
                Console.WriteLine("> Disc     : " + Char.ConvertFromUtf32(isoEntry.ID.DiscNumber) + " / " + Char.ConvertFromUtf32(isoEntry.ID.DiscCount));
                Console.WriteLine("> Media ID : " + isoEntry.ID.MediaID);
                Console.WriteLine("> Platform : " + isoEntry.Platform);
                Console.WriteLine("> Ex       : " + Char.ConvertFromUtf32(isoEntry.ID.ExType));
                Console.WriteLine("-----------------------------------------------------------");

                Console.WriteLine("+ Launching GOD conversion ...");
                Iso2God iso2god = new Iso2God();
                iso2god.Run(isoEntry);
            }

        }
    }
}

