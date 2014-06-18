namespace Chilano.Xbox360.Xex
{
    using IO;
    using System.IO;

    public class XexExecutionInfo : XexInfoField
    {
        public uint BaseVersion;
        public byte DiscCount;
        public byte DiscNumber;
        public byte ExecutableType;
        public byte[] MediaID;
        public byte Platform;
        public static byte[] Signature;
        public byte[] TitleID;
        public uint Version;

        static XexExecutionInfo()
        {
            byte[] buffer = new byte[4];
            buffer[1] = 4;
            buffer[3] = 6;
            Signature = buffer;
        }

        public XexExecutionInfo(uint Address) : base(Address)
        {
            TitleID = new byte[0];
            MediaID = new byte[0];
        }

        public override void Parse(CBinaryReader br)
        {
            br.Seek(Address, SeekOrigin.Begin);
            br.Endian = EndianType.BigEndian;
            MediaID = br.ReadBytes(4);
            Version = br.ReadUInt32();
            BaseVersion = br.ReadUInt32();
            TitleID = br.ReadBytes(4);
            Platform = br.ReadByte();
            ExecutableType = br.ReadByte();
            DiscNumber = br.ReadByte();
            DiscCount = br.ReadByte();
        }
    }
}

