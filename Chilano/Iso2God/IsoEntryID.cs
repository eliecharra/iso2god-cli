namespace Chilano.Iso2God
{
    using System;

    public class IsoEntryID
    {
        public string ContainerID;
        public byte DiscCount;
        public byte DiscNumber;
        public byte ExType;
        public string MediaID;
        public byte Platform;
        public string TitleID;

        public IsoEntryID()
        {
        }

        public IsoEntryID(string TitleID, string MediaID, byte DiscNumber, byte DiscCount, byte Platform, byte ExType)
        {
            this.TitleID = TitleID;
            this.MediaID = MediaID;
            this.DiscNumber = DiscNumber;
            this.DiscCount = DiscCount;
            this.Platform = Platform;
            this.ExType = ExType;
        }
    }
}

