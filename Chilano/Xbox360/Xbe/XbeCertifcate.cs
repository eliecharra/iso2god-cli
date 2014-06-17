namespace Chilano.Xbox360.Xbe
{
    using Chilano.Xbox360.IO;
    using System;
    using System.Text;

    public class XbeCertifcate
    {
        public XbeAllowedMedia AllowedMedia;
        public byte[] AltSignatureKeys = new byte[0x100];
        public byte[] AltTitleIDs = new byte[0x40];
        public uint DiskNumber;
        public uint GameRatings;
        public XbeGameRegion GameRegion;
        public byte[] LanKey = new byte[0x10];
        public byte[] SignatureKey = new byte[0x10];
        public uint Size;
        public byte[] TimeData = new byte[4];
        private uint titleID;
        private byte[] titleName = new byte[80];
        public uint Version;

        public XbeCertifcate(CBinaryReader br)
        {
            this.Size = br.ReadUInt32();
            this.TimeData = br.ReadBytes(4);
            this.titleID = br.ReadUInt32();
            this.titleName = br.ReadBytes(80);
            this.AltTitleIDs = br.ReadBytes(0x40);
            this.AllowedMedia = (XbeAllowedMedia) br.ReadUInt32();
            this.GameRegion = (XbeGameRegion) br.ReadUInt32();
            this.GameRatings = br.ReadUInt32();
            this.DiskNumber = br.ReadUInt32();
            this.Version = br.ReadUInt32();
            this.LanKey = br.ReadBytes(0x10);
            this.SignatureKey = br.ReadBytes(0x10);
            this.AltSignatureKeys = br.ReadBytes(0x100);
        }

        public string TitleID
        {
            get
            {
                return this.titleID.ToString("X02");
            }
        }

        public string TitleName
        {
            get
            {
                if (this.titleName == null)
                {
                    return "";
                }
                return Encoding.Unicode.GetString(this.titleName);
            }
        }
    }
}

