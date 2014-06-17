namespace Chilano.Xbox360.Xbe
{
    using Chilano.Xbox360.IO;
    using System;

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
            this.SectionDigest = new byte[20];
        }

        public XbeSectionHeader(CBinaryReader bw)
        {
            this.SectionDigest = new byte[20];
            bw.Endian = EndianType.LittleEndian;
            this.Flags = (XbeSectionFlags) bw.ReadUInt32();
            this.VirtualAddress = bw.ReadUInt32();
            this.VirtualSize = bw.ReadUInt32();
            this.RawAddress = bw.ReadUInt32();
            this.RawSize = bw.ReadUInt32();
            this.SectionNameAddress = bw.ReadUInt32();
            this.SectionNameRefCount = bw.ReadUInt32();
            this.HeadSharedPageRefCountAddress = bw.ReadUInt32();
            this.TailSharedPageRefCountAddress = bw.ReadUInt32();
            this.SectionDigest = bw.ReadBytes(20);
        }
    }
}

