namespace Chilano.Xbox360.Graphics
{
    using IO;

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
            Size = 0x7c;
            Reserved = new byte[0x2c];
            Flags = DDSSurfaceDescriptionFlags.DDSD_MIPMAPCOUNT | DDSSurfaceDescriptionFlags.DDSD_PIXELFORMAT | DDSSurfaceDescriptionFlags.DDSD_LINEARSIZE | DDSSurfaceDescriptionFlags.DDSD_CAPS | DDSSurfaceDescriptionFlags.DDSD_WIDTH | DDSSurfaceDescriptionFlags.DDSD_HEIGHT;
            PixelFormat = new DDSPixelFormat();
            Caps = new DDSCaps2();
        }

        public DDSSurfaceDesc2(DDSSurfaceDescriptionFlags Flags, int Height, int Width, uint Pitch, uint Depth, uint MipMapCount)
        {
            Size = 0x7c;
            Reserved = new byte[0x2c];
            this.Flags = Flags;
            this.Height = Height;
            this.Width = Width;
            this.Pitch = Pitch;
            this.Depth = Depth;
            this.MipMapCount = MipMapCount;
            PixelFormat = new DDSPixelFormat();
            Caps = new DDSCaps2();
        }

        public DDSSurfaceDesc2(DDSSurfaceDescriptionFlags Flags, int Height, int Width, uint Pitch, uint Depth, uint MipMapCount, DDSPixelFormat PixelFormat, DDSCaps2 Caps)
        {
            Size = 0x7c;
            Reserved = new byte[0x2c];
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
            bw.Write(Size);
            bw.Write((uint) Flags);
            bw.Write(Height);
            bw.Write(Width);
            bw.Write(Depth);
            bw.Write(Pitch);
            bw.Write(MipMapCount);
            bw.Write(Reserved);
            PixelFormat.Write(bw);
            Caps.Write(bw);
            bw.Write(Reserved2);
        }
    }
}

