namespace Chilano.Xbox360.Iso
{
   public class GDFStreamEntry
    {
        public GDFDirEntry Entry;
        public string Path;

        public GDFStreamEntry(GDFDirEntry Entry, string Path)
        {
            this.Entry = Entry;
            this.Path = Path;
        }

        public uint Sector
        {
            get
            {
                return Entry.Sector;
            }
            set
            {
                Entry.Sector = value;
            }
        }

        public uint Size
        {
            get
            {
                return Entry.Size;
            }
        }
    }
}

