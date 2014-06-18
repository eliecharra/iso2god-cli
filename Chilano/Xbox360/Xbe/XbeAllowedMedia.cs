namespace Chilano.Xbox360.Xbe
{
   public enum XbeAllowedMedia : uint
    {
        Cd = 8,
        Dongle = 0x100,
        Dvd5Ro = 0x10,
        Dvd5Rw = 0x40,
        Dvd9Ro = 0x20,
        Dvd9Rw = 0x80,
        DvdCd = 4,
        DvdX2 = 2,
        HardDisk = 1,
        MediaBoard = 0x200,
        MediaMask = 0xffffff,
        NonSecureHD = 0x40000000,
        NonSecureMode = 0x80000000
    }
}

