namespace Chilano.Xbox360.Graphics
{
    using IO;

   public class DDSPixelFormat
    {
        public uint BitMaskBlue;
        public uint BitMaskGreen;
        public uint BitMaskRed;
        public uint BitMaskRGBAlpha;
        public DdsPixelFormatFlags Flags;
        public DDSPixelFormatFourCC FourCC;
        public uint RGBBitCount;
        public uint Size;

        public DDSPixelFormat()
        {
            Size = 0x20;
        }

        public DDSPixelFormat(DdsPixelFormatFlags Flags, DDSPixelFormatFourCC FourCC, uint RGBBitCount, uint BitMaskRed, uint BitMaskGreen, uint BitMaskBlue, uint BitMaskRGBAlpha)
        {
            Size = 0x20;
            this.Flags = Flags;
            this.FourCC = FourCC;
            this.RGBBitCount = RGBBitCount;
            this.BitMaskRed = BitMaskRed;
            this.BitMaskGreen = BitMaskGreen;
            this.BitMaskBlue = BitMaskBlue;
            this.BitMaskRGBAlpha = BitMaskRGBAlpha;
        }

        public void Write(CBinaryWriter bw)
        {
            bw.Write(Size);
            bw.Write((uint) Flags);
            bw.Write((uint) FourCC);
            bw.Write(RGBBitCount);
            bw.Write(BitMaskRed);
            bw.Write(BitMaskGreen);
            bw.Write(BitMaskBlue);
            bw.Write(BitMaskRGBAlpha);
        }
    }
}

