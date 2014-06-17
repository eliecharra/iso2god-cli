namespace Chilano.Xbox360.Iso
{
    using System;

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
                return this.Entry.Sector;
            }
            set
            {
                this.Entry.Sector = value;
            }
        }

        public uint Size
        {
            get
            {
                return this.Entry.Size;
            }
        }
    }
}

