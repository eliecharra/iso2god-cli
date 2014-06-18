namespace Chilano.Xbox360.Xbe
{
    using IO;
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
            Size = br.ReadUInt32();
            TimeData = br.ReadBytes(4);
            titleID = br.ReadUInt32();
            titleName = br.ReadBytes(80);
            AltTitleIDs = br.ReadBytes(0x40);
            AllowedMedia = (XbeAllowedMedia) br.ReadUInt32();
            GameRegion = (XbeGameRegion) br.ReadUInt32();
            GameRatings = br.ReadUInt32();
            DiskNumber = br.ReadUInt32();
            Version = br.ReadUInt32();
            LanKey = br.ReadBytes(0x10);
            SignatureKey = br.ReadBytes(0x10);
            AltSignatureKeys = br.ReadBytes(0x100);
        }

        public string TitleID
        {
            get
            {
                return titleID.ToString("X02");
            }
        }

        public string TitleName
        {
            get
            {
                if (titleName == null)
                {
                    return "";
                }
                return Encoding.Unicode.GetString(titleName);
            }
        }
    }
}

