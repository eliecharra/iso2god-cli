namespace Chilano.Xbox360.Xdbf
{
    using IO;
    using System.IO;

    public class Xdbf
    {
        private CBinaryReader br;
        private uint dataOffset;
        public XdbfTable entries;
        public XdbfHeader header;

        public Xdbf(byte[] Data)
        {
            br = new CBinaryReader(EndianType.BigEndian, new MemoryStream(Data));
            header = new XdbfHeader(br);
            entries = new XdbfTable(br, header);
            dataOffset = (uint) br.BaseStream.Position;
        }

        public byte[] GetResource(XdbfResource Resource, XdbfResourceType Type)
        {
            return GetResource((uint) Resource, (ushort) Type);
        }

        public byte[] GetResource(uint Signature, ushort Type)
        {
            foreach (XdbfTableEntry entry in entries)
            {
                if ((entry.Identifier == Signature) && (entry.Type == Type))
                {
                    br.Seek((long) (dataOffset + entry.Offset), SeekOrigin.Begin);
                    return br.ReadBytes((int) entry.Size);
                }
            }
            return new byte[0];
        }
    }
}

