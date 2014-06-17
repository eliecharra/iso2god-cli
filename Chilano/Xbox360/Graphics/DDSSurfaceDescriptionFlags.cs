namespace Chilano.Xbox360.Graphics
{
    using System;

    public enum DDSSurfaceDescriptionFlags : uint
    {
        DDSD_CAPS = 1,
        DDSD_DEPTH = 0x800000,
        DDSD_HEIGHT = 2,
        DDSD_LINEARSIZE = 0x80000,
        DDSD_MIPMAPCOUNT = 0x20000,
        DDSD_PITCH = 8,
        DDSD_PIXELFORMAT = 0x1000,
        DDSD_WIDTH = 4
    }
}

