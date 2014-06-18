namespace Chilano.Xbox360.Xbe
{
   public enum XbeSectionFlags : uint
    {
        Executable = 4,
        HeadPageReadOnly = 0x10,
        InsertedFile = 8,
        Preload = 2,
        TailPageReadOnly = 0x20,
        Writable = 1
    }
}

