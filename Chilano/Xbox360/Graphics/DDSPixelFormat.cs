namespace Chilano.Xbox360.Graphics
{
    using Chilano.Xbox360.IO;
    using System;

    public class DDSPixelFormat
    {
        public uint BitMaskBlue;
        public uint BitMaskGreen;
        public uint BitMaskRed;
        public uint BitMaskRGBAlpha;
        public DDSPixelFormatFlags Flags;
        public DDSPixelFormatFourCC FourCC;
        public uint RGBBitCount;
        public uint Size;

        public DDSPixelFormat()
        {
            this.Size = 0x20;
        }

        public DDSPixelFormat(DDSPixelFormatFlags Flags, DDSPixelFormatFourCC FourCC, uint RGBBitCount, uint BitMaskRed, uint BitMaskGreen, uint BitMaskBlue, uint BitMaskRGBAlpha)
        {
            this.Size = 0x20;
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
            bw.Write(this.Size);
            bw.Write((uint) this.Flags);
            bw.Write((uint) this.FourCC);
            bw.Write(this.RGBBitCount);
            bw.Write(this.BitMaskRed);
            bw.Write(this.BitMaskGreen);
            bw.Write(this.BitMaskBlue);
            bw.Write(this.BitMaskRGBAlpha);
        }
    }
}

