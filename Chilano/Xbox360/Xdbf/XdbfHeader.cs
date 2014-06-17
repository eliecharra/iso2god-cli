namespace Chilano.Xbox360.Xdbf
{
    using Chilano.Xbox360.IO;
    using System;
    using System.IO;

    public class XdbfHeader
    {
        public byte[] MagicBytes;
        public uint NumEntries;
        public uint NumEntriesCopy;
        public ushort Reserved;
        public uint UnknownA;
        public uint UnknownB;
        public ushort Version;

        public XdbfHeader(CBinaryReader b)
        {
            b.Seek(0L, SeekOrigin.Begin);
            this.MagicBytes = b.ReadBytes(4);
            this.Version = b.ReadUInt16();
            this.Reserved = b.ReadUInt16();
            this.NumEntries = b.ReadUInt32();
            this.NumEntriesCopy = b.ReadUInt32();
            this.UnknownA = b.ReadUInt32();
            this.UnknownB = b.ReadUInt32();
        }
    }
}

