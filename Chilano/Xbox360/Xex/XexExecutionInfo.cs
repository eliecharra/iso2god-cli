namespace Chilano.Xbox360.Xex
{
    using Chilano.Xbox360.IO;
    using System;
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
            this.TitleID = new byte[0];
            this.MediaID = new byte[0];
        }

        public override void Parse(CBinaryReader br)
        {
            br.Seek((long) base.Address, SeekOrigin.Begin);
            br.Endian = EndianType.BigEndian;
            this.MediaID = br.ReadBytes(4);
            this.Version = br.ReadUInt32();
            this.BaseVersion = br.ReadUInt32();
            this.TitleID = br.ReadBytes(4);
            this.Platform = br.ReadByte();
            this.ExecutableType = br.ReadByte();
            this.DiscNumber = br.ReadByte();
            this.DiscCount = br.ReadByte();
        }
    }
}

