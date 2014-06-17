namespace Chilano.Xbox360.Xbe
{
    using System;

    public enum XbeInitFlags : uint
    {
        DontSetupHardDisk = 4,
        FormatUtilityDrive = 2,
        Limit64Megabytes = 3,
        MountUtilityDrive = 1
    }
}

