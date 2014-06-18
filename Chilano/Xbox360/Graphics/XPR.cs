namespace Chilano.Xbox360.Graphics
{
    using IO;
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
            init(br);
        }

        public XPR(byte[] data)
        {
            init(new CBinaryReader(EndianType.LittleEndian, new MemoryStream(data)));
        }

        public DDS ConvertToDDS(int Width, int Height)
        {
            DDS dds = new DDS(DDSType.ARGB);
            switch (Format)
            {
                case XPRFormat.ARGB:
                    dds = new DDS(DDSType.ARGB);
                    break;

                case XPRFormat.DXT1:
                    dds = new DDS(DDSType.DXT1);
                    break;
            }
            dds.SetDetails(Height, Width, 1);
            dds.Data = Image;
            return dds;
        }

        private void init(CBinaryReader br)
        {
            br.Seek(0L, SeekOrigin.Begin);
            Header = new XPRHeader(br);
            if (IsValid)
            {
                readImageData(br);
            }
        }

        private void readImageData(CBinaryReader br)
        {
            br.Seek((long) Header.HeaderSize, SeekOrigin.Begin);
            int count = (int) (Header.FileSize - Header.HeaderSize);
            Image = new byte[count];
            Image = br.ReadBytes(count);
        }

        public XPRFormat Format
        {
            get
            {
                if (Header == null)
                {
                    return XPRFormat.None;
                }
                return (XPRFormat) Header.TextureFormat;
            }
        }

        public int Height
        {
            get
            {
                if (Header == null)
                {
                    return -1;
                }
                return (int) Math.Pow(2.0, (double) Header.TextureRes2);
            }
        }

        public bool IsValid
        {
            get
            {
                if (Header == null)
                {
                    return false;
                }
                return Header.IsValid;
            }
        }

        public int Width
        {
            get
            {
                if (Header == null)
                {
                    return -1;
                }
                return (int) Math.Pow(2.0, (double) Header.TextureRes2);
            }
        }
    }
}

