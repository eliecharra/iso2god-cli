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
            this.SectorSize = volDesc.SectorSize;
            this.MaxSectors = volDesc.VolumeSectors;
            this.MaxBytes = volDesc.VolumeSize;
            this.Bmp = new Bitmap((int) (this.MaxSectors / 0x800), 0x800);
            for (int i = 0; i < this.Bmp.Width; i++)
            {
                for (int j = 0; j < this.Bmp.Height; j++)
                {
                    this.Bmp.SetPixel(i, j, Color.Black);
                }
            }
        }

        public void SaveSectorBitmap(FileStream File)
        {
            this.Bmp.Save(File, ImageFormat.Png);
        }

        public void SetPixel(uint Sector, GDFSectorStatus Status)
        {
            int y = (int) Math.Floor((double) (((double) Sector) / ((double) this.Bmp.Width)));
            int x = ((int) Sector) - (this.Bmp.Width * y);
            this.Bmp.SetPixel(x, y, Color.FromArgb((int) Status));
        }

        public override string ToString()
        {
            string str = "GDF Statistics\n---------------------------";
            uint num = this.TotalDirs - 1;
            return ((((((((((str + "\nTotal Bytes: " + this.MaxBytes) + "\nTotal Sectors: " + this.MaxSectors) + "\n\nFiles: " + this.TotalFiles) + "\nDirs: " + num.ToString()) + "\nTotal Used Sectors: " + this.UsedSectors) + "\nTotal Used Bytes: " + this.DataBytes) + "\n\nUsed Data Sectors: " + this.UsedDataSectors) + "\nUsed Dir Sectors: " + this.UsedDirSectors) + "\nUsed GDF Sectors: " + this.UsedGDFSectors) + "\nFree Sectors: " + this.FreeSectors);
        }

        public uint FreeSectors
        {
            get
            {
                return (this.MaxSectors - ((this.UsedDataSectors + this.UsedDirSectors) + this.UsedGDFSectors));
            }
        }

        public uint UsedSectors
        {
            get
            {
                return ((this.UsedDataSectors + this.UsedDirSectors) + this.UsedGDFSectors);
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

