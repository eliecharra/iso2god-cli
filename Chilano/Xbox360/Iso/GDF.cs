namespace Chilano.Xbox360.Iso
{
    using IO;
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Text;

    public class GDF : IDisposable
    {
        private uint dirs;
        public List<Exception> Exceptions = new List<Exception>();
        private FileStream file;
        private uint files;
        private CBinaryReader fr;
        private GDFDirTable rootDir;
        private IsoType type;
        private GDFVolumeDescriptor volDesc;

        public GDF(FileStream File)
        {
            file = File;
            fr = new CBinaryReader(EndianType.LittleEndian, file);
            readVolume();
            try
            {
                rootDir = new GDFDirTable(fr, volDesc, volDesc.RootDirSector, volDesc.RootDirSize);
            }
            catch (Exception exception)
            {
                Exceptions.Add(exception);
            }
        }

        private void calcSectors(GDFDirTable table, GDFStats stats)
        {
            stats.UsedDirSectors += (uint) Math.Ceiling(table.Size / ((double) stats.SectorSize));
            stats.TotalDirs++;
            foreach (GDFDirEntry entry in table)
            {
                if (entry.IsDirectory)
                {
                    if (entry.SubDir == null)
                    {
                        entry.SubDir = new GDFDirTable(fr, volDesc, entry.Sector, entry.Size);
                        entry.SubDir.Parent = entry;
                    }
                    calcSectors(entry.SubDir, stats);
                }
                else
                {
                    uint num = (uint) Math.Ceiling(entry.Size / ((double) stats.SectorSize));
                    stats.TotalFiles++;
                    stats.UsedDataSectors += num;
                    stats.DataBytes += entry.Size;
                }
            }
        }

        public void Close()
        {
            volDesc = new GDFVolumeDescriptor();
            Exceptions.Clear();
            type = IsoType.Gdf;
            rootDir.Clear();
            fr.Close();
            file.Close();
        }

        public void Dispose()
        {
            Close();
            file.Dispose();
        }

        public GDFStats ExamineSectors()
        {
            GDFStats stats = new GDFStats(volDesc);
            for (int i = 0; i < 0x20; i++)
            {
                stats.SetPixel((uint) i, (GDFStats.GDFSectorStatus)unchecked((uint)(-16711936)));
            }
            stats.SetPixel(volDesc.RootDirSector, (GDFStats.GDFSectorStatus)unchecked((uint)(-65536)));
            uint lastSector = 0;
            ParseDirectory(rootDir, true, ref lastSector);
            calcSectors(rootDir, stats);
            return stats;
        }

        public bool Exists(string Path)
        {
            try
            {
                GDFDirTable folder = GetFolder(rootDir, Path);
                string str = Path.Contains(@"\") ? Path.Substring(Path.LastIndexOf(@"\") + 1) : Path;
                foreach (GDFDirEntry entry in folder)
                {
                    if (entry.Name.ToLower() == str.ToLower())
                    {
                        return true;
                    }
                }
            }
            catch (Exception exception)
            {
                Exceptions.Add(exception);
            }
            return false;
        }

        public List<GDFDirEntry> GetDirContents(string Path)
        {
            List<GDFDirEntry> list = new List<GDFDirEntry>();
            try
            {
                GDFDirTable folder = GetFolder(rootDir, Path);
                if (folder == null)
                {
                    return list;
                }
                foreach (GDFDirEntry entry in folder)
                {
                    list.Add(entry);
                }
            }
            catch (Exception exception)
            {
                Exceptions.Add(exception);
            }
            return list;
        }

        public byte[] GetFile(string Path)
        {
            try
            {
                GDFDirTable folder = GetFolder(rootDir, Path);
                string str = Path.Contains(@"\") ? Path.Substring(Path.LastIndexOf(@"\") + 1) : Path;
                foreach (GDFDirEntry entry in folder)
                {
                    if (entry.Name.ToLower() == str.ToLower())
                    {
                        fr.Seek(volDesc.RootOffset + (entry.Sector * volDesc.SectorSize), SeekOrigin.Begin);
                        return fr.ReadBytes((int) entry.Size);
                    }
                }
            }
            catch (Exception exception)
            {
                Exceptions.Add(exception);
            }
            return new byte[0];
        }

        private void getFileSystem(ref Queue<GDFStreamEntry> fs, GDFDirTable root, string path)
        {
            foreach (GDFDirEntry entry in root)
            {
                fs.Enqueue(new GDFStreamEntry(entry, path + @"\" + entry.Name));
                if (entry.IsDirectory)
                {
                    getFileSystem(ref fs, entry.SubDir, path + @"\" + entry.Name);
                }
            }
        }

        public Queue<GDFStreamEntry> GetFileSystem(GDFDirTable Root)
        {
            Queue<GDFStreamEntry> fs = new Queue<GDFStreamEntry>();
            getFileSystem(ref fs, Root, "*");
            return fs;
        }

        public GDFDirTable GetFolder(GDFDirTable Table, string Path)
        {
            try
            {
                if (Path.Length == 0)
                {
                    return Table;
                }
                string[] strArray = new string[1];
                if (Path.Contains(@"\"))
                {
                    strArray = Path.Split(new[] { '\\' });
                }
                else
                {
                    strArray[0] = Path;
                }
                foreach (GDFDirEntry entry in Table)
                {
                    if (entry.Name.ToLower() == strArray[0].ToLower())
                    {
                        if (!entry.IsDirectory)
                        {
                            return Table;
                        }
                        if (entry.SubDir == null)
                        {
                            entry.SubDir = new GDFDirTable(fr, volDesc, entry.Sector, entry.Size);
                        }
                        if (strArray.Length == 1)
                        {
                            return entry.SubDir;
                        }
                        return GetFolder(entry.SubDir, Path.Substring(strArray[0].Length + 1));
                    }
                }
            }
            catch (Exception exception)
            {
                Exceptions.Add(exception);
            }
            return null;
        }

        public void ParseDirectory(GDFDirTable table, bool recursive, ref uint lastSector)
        {
            try
            {
                foreach (GDFDirEntry entry in table)
                {
                    if (entry.Sector >= lastSector)
                    {
                        lastSector = entry.Sector + ((uint) Math.Ceiling(entry.Size / ((double) volDesc.SectorSize)));
                    }
                    if (entry.IsDirectory)
                    {
                        dirs++;
                        entry.SubDir = new GDFDirTable(fr, volDesc, entry.Sector, entry.Size);
                        entry.SubDir.Parent = entry;
                        if (recursive)
                        {
                            ParseDirectory(entry.SubDir, true, ref lastSector);
                        }
                    }
                    else
                    {
                        files++;
                    }
                }
            }
            catch (Exception exception)
            {
                Exceptions.Add(exception);
            }
        }

        private void readVolume()
        {
            volDesc = new GDFVolumeDescriptor();
            volDesc.SectorSize = 0x800;
            fr.Seek(0x20 * volDesc.SectorSize, SeekOrigin.Begin);
            if (Encoding.ASCII.GetString(fr.ReadBytes(20)) == "MICROSOFT*XBOX*MEDIA")
            {
                type = IsoType.Xsf;
                volDesc.RootOffset = (uint) type;
            }
            else
            {
                file.Seek((0x20 * volDesc.SectorSize) + 0xfd90000, SeekOrigin.Begin);
                if (Encoding.ASCII.GetString(fr.ReadBytes(20)) == "MICROSOFT*XBOX*MEDIA")
                {
                    type = IsoType.Gdf;
                    volDesc.RootOffset = (uint) type;
                }
                else
                {
                    type = IsoType.XGD3;
                    volDesc.RootOffset = (uint) type;
                }
            }
            file.Seek((0x20 * volDesc.SectorSize) + volDesc.RootOffset, SeekOrigin.Begin);
            volDesc.Identifier = fr.ReadBytes(20);
            volDesc.RootDirSector = fr.ReadUInt32();
            volDesc.RootDirSize = fr.ReadUInt32();
            volDesc.ImageCreationTime = fr.ReadBytes(8);
            volDesc.VolumeSize = (ulong) (fr.BaseStream.Length - volDesc.RootOffset);
            volDesc.VolumeSectors = (uint) (volDesc.VolumeSize / volDesc.SectorSize);
        }

        public void SaveFileSystem(FileStream File)
        {
            StreamWriter file = new StreamWriter(File);
            file.WriteLine("GDF File System Structure");
            file.WriteLine("-------------------------");
            file.WriteLine("Source ISO: " + this.file.Name);
            file.Write("\nSector\t\tSize (s)\t\tSize (b)\t\tName\n");
            file.WriteLine("---------------------------------------------------------");
            uint lastSector = 0;
            ParseDirectory(rootDir, true, ref lastSector);
            saveFileSystemTable(file, rootDir);
            file.Flush();
        }

        private void saveFileSystemTable(StreamWriter file, GDFDirTable table)
        {
            foreach (GDFDirEntry entry in table)
            {
                if (entry.IsDirectory)
                {
                    saveFileSystemTable(file, entry.SubDir);
                }
                else
                {
                    file.WriteLine(string.Concat(new object[] { entry.Sector, "\t\t", entry.Size, "\t\t", Math.Ceiling(entry.Size / ((double) volDesc.SectorSize)), "\t\t", entry.Name }));
                }
            }
        }

        public long WriteFileToStream(string Path, CBinaryWriter Writer)
        {
            try
            {
                GDFDirTable folder = GetFolder(rootDir, Path);
                string str = Path.Contains(@"\") ? Path.Substring(Path.LastIndexOf(@"\") + 1) : Path;
                foreach (GDFDirEntry entry in folder)
                {
                    if (entry.Name.ToLower() == str.ToLower())
                    {
                        fr.Seek(volDesc.RootOffset + (entry.Sector * volDesc.SectorSize), SeekOrigin.Begin);
                        uint num = (uint) Math.Ceiling(entry.Size / ((double) volDesc.SectorSize));
                        long num2 = 0L;
                        for (uint i = 0; i < num; i++)
                        {
                            byte[] buffer;
                            if ((num2 + volDesc.SectorSize) > entry.Size)
                            {
                                buffer = fr.ReadBytes((int) (entry.Size - num2));
                                Writer.Write(buffer);
                                int num4 = ((int) volDesc.SectorSize) - buffer.Length;
                                for (int j = 0; j < num4; j++)
                                {
                                    Writer.Write((byte) 0);
                                }
                            }
                            else
                            {
                                buffer = fr.ReadBytes((int) volDesc.SectorSize);
                                Writer.Write(buffer);
                            }
                            num2 += volDesc.SectorSize;
                        }
                        return entry.Size;
                    }
                }
            }
            catch (Exception exception)
            {
                Exceptions.Add(exception);
            }
            return -1L;
        }

        public uint DirCount
        {
            get
            {
                return dirs;
            }
        }

        public uint FileCount
        {
            get
            {
                return files;
            }
        }

        public ulong LastOffset
        {
            get
            {
                return (LastSector * volDesc.SectorSize);
            }
        }

        public uint LastSector
        {
            get
            {
                uint lastSector = 0;
                ParseDirectory(rootDir, true, ref lastSector);
                return lastSector;
            }
        }

        public GDFDirTable RootDir
        {
            get
            {
                return rootDir;
            }
        }

        public ulong RootOffset
        {
            get
            {
                return (ulong) Type;
            }
        }

        public IsoType Type
        {
            get
            {
                return type;
            }
        }

        public GDFVolumeDescriptor VolDesc
        {
            get
            {
                return volDesc;
            }
        }
    }
}

