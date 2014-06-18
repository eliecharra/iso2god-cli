namespace Chilano.Xbox360.Iso
{
    using System;
    using System.Drawing;
    using System.Drawing.Imaging;
    using System.IO;

    public class GDFStats
    {
        private Bitmap Bmp;
        public ulong DataBytes;
        public ulong MaxBytes;
        public uint MaxSectors;
        public uint SectorSize;
        public uint TotalDirs;
        public uint TotalFiles;
        public uint UsedDataSectors;
        public uint UsedDirSectors;
        public uint UsedGDFSectors;

        public GDFStats(GDFVolumeDescriptor volDesc)
        {
            SectorSize = volDesc.SectorSize;
            MaxSectors = volDesc.VolumeSectors;
            MaxBytes = volDesc.VolumeSize;
            Bmp = new Bitmap((int) (MaxSectors / 0x800), 0x800);
            for (int i = 0; i < Bmp.Width; i++)
            {
                for (int j = 0; j < Bmp.Height; j++)
                {
                    Bmp.SetPixel(i, j, Color.Black);
                }
            }
        }

        public void SaveSectorBitmap(FileStream File)
        {
            Bmp.Save(File, ImageFormat.Png);
        }

        public void SetPixel(uint Sector, GDFSectorStatus Status)
        {
            int y = (int) Math.Floor((double) (((double) Sector) / ((double) Bmp.Width)));
            int x = ((int) Sector) - (Bmp.Width * y);
            Bmp.SetPixel(x, y, Color.FromArgb((int) Status));
        }

        public override string ToString()
        {
            string str = "GDF Statistics\n---------------------------";
            uint num = TotalDirs - 1;
            return ((((((((((str + "\nTotal Bytes: " + MaxBytes) + "\nTotal Sectors: " + MaxSectors) + "\n\nFiles: " + TotalFiles) + "\nDirs: " + num.ToString()) + "\nTotal Used Sectors: " + UsedSectors) + "\nTotal Used Bytes: " + DataBytes) + "\n\nUsed Data Sectors: " + UsedDataSectors) + "\nUsed Dir Sectors: " + UsedDirSectors) + "\nUsed GDF Sectors: " + UsedGDFSectors) + "\nFree Sectors: " + FreeSectors);
        }

        public uint FreeSectors
        {
            get
            {
                return (MaxSectors - ((UsedDataSectors + UsedDirSectors) + UsedGDFSectors));
            }
        }

        public uint UsedSectors
        {
            get
            {
                return ((UsedDataSectors + UsedDirSectors) + UsedGDFSectors);
            }
        }

        public enum GDFSectorStatus : uint
        {
            DirTable = 0xffff0000,
            Empty = 0xff000000,
            FileData = 0xffdedede,
            Gdf = 0xff00ff00
        }
    }
}

