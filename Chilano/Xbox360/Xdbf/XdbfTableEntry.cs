namespace Chilano.Xbox360.Xdbf
{
    using Chilano.Xbox360.IO;
    using System;

    public class XdbfTableEntry
    {
        public uint Identifier;
        public uint Offset;
        public uint Padding;
        public uint Size;
        public ushort Type;

        public XdbfTableEntry(CBinaryReader b)
        {
            this.Identifier = b.ReadUInt32();
            this.Offset = b.ReadUInt32();
            this.Size = b.ReadUInt32();
            this.Type = b.ReadUInt16();
            this.Padding = b.ReadUInt32();
        }

        public override string ToString()
        {
            string str = "XdbfTableEntry: { ";
            return (((((str + "Identifier = " + this.Identifier.ToString()) + ", Offset = " + this.Offset.ToString()) + ", Size = " + this.Size.ToString()) + ", Type = " + this.Type.ToString()) + " }");
        }
    }
}

