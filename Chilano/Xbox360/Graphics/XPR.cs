namespace Chilano.Xbox360.Graphics
{
    using Chilano.Xbox360.IO;
    using System;
    using System.IO;

    public class XPR
    {
        public XPRHeader Header;
        public byte[] Image;

        public XPR()
        {
        }

        public XPR(CBinaryReader br)
        {
            this.init(br);
        }

        public XPR(byte[] data)
        {
            this.init(new CBinaryReader(EndianType.LittleEndian, new MemoryStream(data)));
        }

        public DDS ConvertToDDS(int Width, int Height)
        {
            DDS dds = new DDS(DDSType.ARGB);
            switch (this.Format)
            {
                case XPRFormat.ARGB:
                    dds = new DDS(DDSType.ARGB);
                    break;

                case XPRFormat.DXT1:
                    dds = new DDS(DDSType.DXT1);
                    break;
            }
            dds.SetDetails(Height, Width, 1);
            dds.Data = this.Image;
            return dds;
        }

        private void init(CBinaryReader br)
        {
            br.Seek(0L, SeekOrigin.Begin);
            this.Header = new XPRHeader(br);
            if (this.IsValid)
            {
                this.readImageData(br);
            }
        }

        private void readImageData(CBinaryReader br)
        {
            br.Seek((long) this.Header.HeaderSize, SeekOrigin.Begin);
            int count = (int) (this.Header.FileSize - this.Header.HeaderSize);
            this.Image = new byte[count];
            this.Image = br.ReadBytes(count);
        }

        public XPRFormat Format
        {
            get
            {
                if (this.Header == null)
                {
                    return XPRFormat.None;
                }
                return (XPRFormat) this.Header.TextureFormat;
            }
        }

        public int Height
        {
            get
            {
                if (this.Header == null)
                {
                    return -1;
                }
                return (int) Math.Pow(2.0, (double) this.Header.TextureRes2);
            }
        }

        public bool IsValid
        {
            get
            {
                if (this.Header == null)
                {
                    return false;
                }
                return this.Header.IsValid;
            }
        }

        public int Width
        {
            get
            {
                if (this.Header == null)
                {
                    return -1;
                }
                return (int) Math.Pow(2.0, (double) this.Header.TextureRes2);
            }
        }
    }
}

