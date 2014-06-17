namespace Chilano.Xbox360.Xdbf
{
    using Chilano.Xbox360.IO;
    using System;
    using System.IO;

    public class Xdbf
    {
        private CBinaryReader br;
        private uint dataOffset;
        public XdbfTable entries;
        public XdbfHeader header;

        public Xdbf(byte[] Data)
        {
            this.br = new CBinaryReader(EndianType.BigEndian, new MemoryStream(Data));
            this.header = new XdbfHeader(this.br);
            this.entries = new XdbfTable(this.br, this.header);
            this.dataOffset = (uint) this.br.BaseStream.Position;
        }

        public byte[] GetResource(XdbfResource Resource, XdbfResourceType Type)
        {
            return this.GetResource((uint) Resource, (ushort) Type);
        }

        public byte[] GetResource(uint Signature, ushort Type)
        {
            foreach (XdbfTableEntry entry in this.entries)
            {
                if ((entry.Identifier == Signature) && (entry.Type == Type))
                {
                    this.br.Seek((long) (this.dataOffset + entry.Offset), SeekOrigin.Begin);
                    return this.br.ReadBytes((int) entry.Size);
                }
            }
            return new byte[0];
        }
    }
}

