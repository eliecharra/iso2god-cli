namespace Chilano.Iso2God
{
   using System.Runtime.InteropServices;

    [StructLayout(LayoutKind.Sequential)]
    public struct IsoEntry
    {
        public IsoEntryPlatform Platform;
        public string Path;
        public string Destination;
        public string TitleName;
        public long Size;
        public uint Parts;
        public IsoEntryID ID;
        public IsoEntry(IsoEntryPlatform Platform, string Path, string Destination, long Size, string TitleName, IsoEntryID ID)
        {
            this.Platform = Platform;
            this.Path = Path;
            this.Destination = Destination;
            this.Size = Size;
            this.TitleName = TitleName;
            Parts = 0;
            this.ID = ID;
        }
    }
}

