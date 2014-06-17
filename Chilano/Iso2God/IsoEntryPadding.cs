namespace Chilano.Iso2God
{
    using System;

    public class IsoEntryPadding
    {
        public string IsoPath;
        public bool KeepIso;
        public string TempPath;
        public IsoEntryPaddingRemoval Type;

        public IsoEntryPadding()
        {
            this.KeepIso = true;
        }

        public IsoEntryPadding(IsoEntryPaddingRemoval Type, string TempPath, string IsoPath, bool KeepIso)
        {
            this.KeepIso = true;
            this.Type = Type;
            this.TempPath = TempPath;
            this.IsoPath = IsoPath;
            this.KeepIso = KeepIso;
        }
    }
}

