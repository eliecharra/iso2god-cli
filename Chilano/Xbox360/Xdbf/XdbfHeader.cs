namespace Chilano.Xbox360.Xdbf
{
    using IO;
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
            MagicBytes = b.ReadBytes(4);
            Version = b.ReadUInt16();
            Reserved = b.ReadUInt16();
            NumEntries = b.ReadUInt32();
            NumEntriesCopy = b.ReadUInt32();
            UnknownA = b.ReadUInt32();
            UnknownB = b.ReadUInt32();
        }
    }
}

