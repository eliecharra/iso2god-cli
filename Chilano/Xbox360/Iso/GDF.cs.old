namespace Chilano.Xbox360.Iso
{
    using Chilano.Xbox360.IO;
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
            this.file = File;
            this.fr = new CBinaryReader(EndianType.LittleEndian, this.file);
            this.readVolume();
            try
            {
                this.rootDir = new GDFDirTable(this.fr, this.volDesc, this.volDesc.RootDirSector, this.volDesc.RootDirSize);
            }
            catch (Exception exception)
            {
                this.Exceptions.Add(exception);
            }
        }

        private void calcSectors(GDFDirTable table, GDFStats stats)
        {
            stats.UsedDirSectors += (uint) Math.Ceiling((double) (((double) table.Size) / ((double) stats.SectorSize)));
            stats.TotalDirs++;
            foreach (GDFDirEntry entry in table)
            {
                if (entry.IsDirectory)
                {
                    if (entry.SubDir == null)
                    {
                        entry.SubDir = new GDFDirTable(this.fr, this.volDesc, entry.Sector, entry.Size);
                        entry.SubDir.Parent = entry;
                    }
                    this.calcSectors(entry.SubDir, stats);
                }
                else
                {
                    uint num = (uint) Math.Ceiling((double) (((double) entry.Size) / ((double) stats.SectorSize)));
                    stats.TotalFiles++;
                    stats.UsedDataSectors += num;
                    stats.DataBytes += entry.Size;
                }
            }
        }

        public void Close()
        {
            this.volDesc = new GDFVolumeDescriptor();
            this.Exceptions.Clear();
            this.type = IsoType.Gdf;
            this.rootDir.Clear();
            this.fr.Close();
            this.file.Close();
        }

        public void Dispose()
        {
            this.Close();
            this.file.Dispose();
        }

        public GDFStats ExamineSectors()
        {
            GDFStats stats = new GDFStats(this.volDesc);
            for (int i = 0; i < 0x20; i++)
            {
                stats.SetPixel((uint) i, (GDFStats.GDFSectorStatus) (-16711936));
            }
            stats.SetPixel(this.volDesc.RootDirSector, (GDFStats.GDFSectorStatus) (-65536));
            uint lastSector = 0;
            this.ParseDirectory(this.rootDir, true, ref lastSector);
            this.calcSectors(this.rootDir, stats);
            return stats;
        }

        public bool Exists(string Path)
        {
            try
            {
                GDFDirTable folder = this.GetFolder(this.rootDir, Path);
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
                this.Exceptions.Add(exception);
            }
            return false;
        }

        public List<GDFDirEntry> GetDirContents(string Path)
        {
            List<GDFDirEntry> list = new List<GDFDirEntry>();
            try
            {
                GDFDirTable folder = this.GetFolder(this.rootDir, Path);
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
                this.Exceptions.Add(exception);
            }
            return list;
        }

        public byte[] GetFile(string Path)
        {
            try
            {
                GDFDirTable folder = this.GetFolder(this.rootDir, Path);
                string str = Path.Contains(@"\") ? Path.Substring(Path.LastIndexOf(@"\") + 1) : Path;
                foreach (GDFDirEntry entry in folder)
                {
                    if (entry.Name.ToLower() == str.ToLower())
                    {
                        this.fr.Seek((long) (this.volDesc.RootOffset + (entry.Sector * this.volDesc.SectorSize)), SeekOrigin.Begin);
                        return this.fr.ReadBytes((int) entry.Size);
                    }
                }
            }
            catch (Exception exception)
            {
                this.Exceptions.Add(exception);
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
                    this.getFileSystem(ref fs, entry.SubDir, path + @"\" + entry.Name);
                }
            }
        }

        public Queue<GDFStreamEntry> GetFileSystem(GDFDirTable Root)
        {
            Queue<GDFStreamEntry> fs = new Queue<GDFStreamEntry>();
            this.getFileSystem(ref fs, Root, "*");
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
                    strArray = Path.Split(new char[] { '\\' });
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
                            entry.SubDir = new GDFDirTable(this.fr, this.volDesc, entry.Sector, entry.Size);
                        }
                        if (strArray.Length == 1)
                        {
                            return entry.SubDir;
                        }
                        return this.GetFolder(entry.SubDir, Path.Substring(strArray[0].Length + 1));
                    }
                }
            }
            catch (Exception exception)
            {
                this.Exceptions.Add(exception);
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
                        lastSector = entry.Sector + ((uint) Math.Ceiling((double) (((double) entry.Size) / ((double) this.volDesc.SectorSize))));
                    }
                    if (entry.IsDirectory)
                    {
                        this.dirs++;
                        entry.SubDir = new GDFDirTable(this.fr, this.volDesc, entry.Sector, entry.Size);
                        entry.SubDir.Parent = entry;
                        if (recursive)
                        {
                            this.ParseDirectory(entry.SubDir, true, ref lastSector);
                        }
                    }
                    else
                    {
                        this.files++;
                    }
                }
            }
            catch (Exception exception)
            {
                this.Exceptions.Add(exception);
            }
        }

        private void readVolume()
        {
            this.volDesc = new GDFVolumeDescriptor();
            this.volDesc.SectorSize = 0x800;
            this.fr.Seek((long) (0x20 * this.volDesc.SectorSize), SeekOrigin.Begin);
            if (Encoding.ASCII.GetString(this.fr.ReadBytes(20)) == "MICROSOFT*XBOX*MEDIA")
            {
                this.type = IsoType.Xsf;
                this.volDesc.RootOffset = (uint) this.type;
            }
            else
            {
                this.file.Seek((long) ((0x20 * this.volDesc.SectorSize) + 0xfd90000), SeekOrigin.Begin);
                if (Encoding.ASCII.GetString(this.fr.ReadBytes(20)) == "MICROSOFT*XBOX*MEDIA")
                {
                    this.type = IsoType.Gdf;
                    this.volDesc.RootOffset = (uint) this.type;
                }
                else
                {
                    this.type = IsoType.XGD3;
                    this.volDesc.RootOffset = (uint) this.type;
                }
            }
            this.file.Seek((long) ((0x20 * this.volDesc.SectorSize) + this.volDesc.RootOffset), SeekOrigin.Begin);
            this.volDesc.Identifier = this.fr.ReadBytes(20);
            this.volDesc.RootDirSector = this.fr.ReadUInt32();
            this.volDesc.RootDirSize = this.fr.ReadUInt32();
            this.volDesc.ImageCreationTime = this.fr.ReadBytes(8);
            this.volDesc.VolumeSize = (ulong) (this.fr.BaseStream.Length - this.volDesc.RootOffset);
            this.volDesc.VolumeSectors = (uint) (this.volDesc.VolumeSize / ((ulong) this.volDesc.SectorSize));
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
            this.ParseDirectory(this.rootDir, true, ref lastSector);
            this.saveFileSystemTable(file, this.rootDir);
            file.Flush();
        }

        private void saveFileSystemTable(StreamWriter file, GDFDirTable table)
        {
            foreach (GDFDirEntry entry in table)
            {
                if (entry.IsDirectory)
                {
                    this.saveFileSystemTable(file, entry.SubDir);
                }
                else
                {
                    file.WriteLine(string.Concat(new object[] { entry.Sector, "\t\t", entry.Size, "\t\t", Math.Ceiling((double) (((double) entry.Size) / ((double) this.volDesc.SectorSize))), "\t\t", entry.Name }));
                }
            }
        }

        public long WriteFileToStream(string Path, CBinaryWriter Writer)
        {
            try
            {
                GDFDirTable folder = this.GetFolder(this.rootDir, Path);
                string str = Path.Contains(@"\") ? Path.Substring(Path.LastIndexOf(@"\") + 1) : Path;
                foreach (GDFDirEntry entry in folder)
                {
                    if (entry.Name.ToLower() == str.ToLower())
                    {
                        this.fr.Seek((long) (this.volDesc.RootOffset + (entry.Sector * this.volDesc.SectorSize)), SeekOrigin.Begin);
                        uint num = (uint) Math.Ceiling((double) (((double) entry.Size) / ((double) this.volDesc.SectorSize)));
                        long num2 = 0L;
                        for (uint i = 0; i < num; i++)
                        {
                            byte[] buffer;
                            if ((num2 + this.volDesc.SectorSize) > entry.Size)
                            {
                                buffer = this.fr.ReadBytes((int) (entry.Size - num2));
                                Writer.Write(buffer);
                                int num4 = ((int) this.volDesc.SectorSize) - buffer.Length;
                                for (int j = 0; j < num4; j++)
                                {
                                    Writer.Write((byte) 0);
                                }
                            }
                            else
                            {
                                buffer = this.fr.ReadBytes((int) this.volDesc.SectorSize);
                                Writer.Write(buffer);
                            }
                            num2 += this.volDesc.SectorSize;
                        }
                        return (long) entry.Size;
                    }
                }
            }
            catch (Exception exception)
            {
                this.Exceptions.Add(exception);
            }
            return -1L;
        }

        public uint DirCount
        {
            get
            {
                return this.dirs;
            }
        }

        public uint FileCount
        {
            get
            {
                return this.files;
            }
        }

        public ulong LastOffset
        {
            get
            {
                return (this.LastSector * this.volDesc.SectorSize);
            }
        }

        public uint LastSector
        {
            get
            {
                uint lastSector = 0;
                this.ParseDirectory(this.rootDir, true, ref lastSector);
                return lastSector;
            }
        }

        public GDFDirTable RootDir
        {
            get
            {
                return this.rootDir;
            }
        }

        public ulong RootOffset
        {
            get
            {
                return (ulong) this.Type;
            }
        }

        public IsoType Type
        {
            get
            {
                return this.type;
            }
        }

        public GDFVolumeDescriptor VolDesc
        {
            get
            {
                return this.volDesc;
            }
        }
    }
}

