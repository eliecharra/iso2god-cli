namespace Chilano.Xbox360.Xbe
{
    using IO;

   public class XbeSectionHeader
    {
        public XbeSectionFlags Flags;
        public uint HeadSharedPageRefCountAddress;
        public uint RawAddress;
        public uint RawSize;
        public byte[] SectionDigest;
        public uint SectionNameAddress;
        public uint SectionNameRefCount;
        public uint TailSharedPageRefCountAddress;
        public uint VirtualAddress;
        public uint VirtualSize;

        public XbeSectionHeader()
        {
            SectionDigest = new byte[20];
        }

        public XbeSectionHeader(CBinaryReader bw)
        {
            SectionDigest = new byte[20];
            bw.Endian = EndianType.LittleEndian;
            Flags = (XbeSectionFlags) bw.ReadUInt32();
            VirtualAddress = bw.ReadUInt32();
            VirtualSize = bw.ReadUInt32();
            RawAddress = bw.ReadUInt32();
            RawSize = bw.ReadUInt32();
            SectionNameAddress = bw.ReadUInt32();
            SectionNameRefCount = bw.ReadUInt32();
            HeadSharedPageRefCountAddress = bw.ReadUInt32();
            TailSharedPageRefCountAddress = bw.ReadUInt32();
            SectionDigest = bw.ReadBytes(20);
        }
    }
}

