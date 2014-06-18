namespace Chilano.Xbox360.Xdbf
{
    using IO;

   public class XdbfTableEntry
    {
        public uint Identifier;
        public uint Offset;
        public uint Padding;
        public uint Size;
        public ushort Type;

        public XdbfTableEntry(CBinaryReader b)
        {
            Identifier = b.ReadUInt32();
            Offset = b.ReadUInt32();
            Size = b.ReadUInt32();
            Type = b.ReadUInt16();
            Padding = b.ReadUInt32();
        }

        public override string ToString()
        {
            string str = "XdbfTableEntry: { ";
            return (((((str + "Identifier = " + Identifier.ToString()) + ", Offset = " + Offset.ToString()) + ", Size = " + Size.ToString()) + ", Type = " + Type.ToString()) + " }");
        }
    }
}

