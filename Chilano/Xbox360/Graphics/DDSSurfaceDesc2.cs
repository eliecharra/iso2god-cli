namespace Chilano.Xbox360.Graphics
{
    using Chilano.Xbox360.IO;
    using System;

    public class DDSSurfaceDesc2
    {
        public DDSCaps2 Caps;
        public uint Depth;
        public DDSSurfaceDescriptionFlags Flags;
        public int Height;
        public uint MipMapCount;
        public uint Pitch;
        public DDSPixelFormat PixelFormat;
        public byte[] Reserved;
        public uint Reserved2;
        public uint Size;
        public int Width;

        public DDSSurfaceDesc2()
        {
            this.Size = 0x7c;
            this.Reserved = new byte[0x2c];
            this.Flags = DDSSurfaceDescriptionFlags.DDSD_MIPMAPCOUNT | DDSSurfaceDescriptionFlags.DDSD_PIXELFORMAT | DDSSurfaceDescriptionFlags.DDSD_LINEARSIZE | DDSSurfaceDescriptionFlags.DDSD_CAPS | DDSSurfaceDescriptionFlags.DDSD_WIDTH | DDSSurfaceDescriptionFlags.DDSD_HEIGHT;
            this.PixelFormat = new DDSPixelFormat();
            this.Caps = new DDSCaps2();
        }

        public DDSSurfaceDesc2(DDSSurfaceDescriptionFlags Flags, int Height, int Width, uint Pitch, uint Depth, uint MipMapCount)
        {
            this.Size = 0x7c;
            this.Reserved = new byte[0x2c];
            this.Flags = Flags;
            this.Height = Height;
            this.Width = Width;
            this.Pitch = Pitch;
            this.Depth = Depth;
            this.MipMapCount = MipMapCount;
            this.PixelFormat = new DDSPixelFormat();
            this.Caps = new DDSCaps2();
        }

        public DDSSurfaceDesc2(DDSSurfaceDescriptionFlags Flags, int Height, int Width, uint Pitch, uint Depth, uint MipMapCount, DDSPixelFormat PixelFormat, DDSCaps2 Caps)
        {
            this.Size = 0x7c;
            this.Reserved = new byte[0x2c];
            this.Flags = Flags;
            this.Height = Height;
            this.Width = Width;
            this.Pitch = Pitch;
            this.Depth = Depth;
            this.MipMapCount = MipMapCount;
            this.PixelFormat = PixelFormat;
            this.Caps = Caps;
        }

        public void Write(CBinaryWriter bw)
        {
            bw.Write(this.Size);
            bw.Write((uint) this.Flags);
            bw.Write(this.Height);
            bw.Write(this.Width);
            bw.Write(this.Depth);
            bw.Write(this.Pitch);
            bw.Write(this.MipMapCount);
            bw.Write(this.Reserved);
            this.PixelFormat.Write(bw);
            this.Caps.Write(bw);
            bw.Write(this.Reserved2);
        }
    }
}

