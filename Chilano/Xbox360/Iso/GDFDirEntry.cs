namespace Chilano.Xbox360.Iso
{
    using IO;
    using System;
    using System.IO;

    public class GDFDirEntry : ICloneable, IComparable<GDFDirEntry>
    {
        public GDFDirEntryAttrib Attributes;
        public string Name;
        public byte NameLength;
        public byte[] Padding;
        public GDFDirTable Parent;
        public uint Sector;
        public uint Size;
        public GDFDirTable SubDir;
        public ushort SubTreeL;
        public ushort SubTreeR;

        private void calcPadding()
        {
            int num = 4 - ((14 + NameLength) % 4);
            num = (num == 4) ? 0 : num;
            Padding = new byte[num];
            for (int i = 0; i < Padding.Length; i++)
            {
                Padding[i] = 0xff;
            }
        }

        public object Clone()
        {
            return new GDFDirEntry { SubTreeL = SubTreeL, SubTreeR = SubTreeR, Sector = Sector, Size = Size, Attributes = Attributes, NameLength = NameLength, Name = (string) Name.Clone(), Padding = (Padding == null) ? null : ((byte[]) Padding.Clone()), SubDir = (SubDir == null) ? null : ((GDFDirTable) SubDir.Clone()) };
        }

        public int CompareTo(GDFDirEntry Entry)
        {
            return Name.CompareTo(Entry.Name);
        }

        public byte[] ToByteArray()
        {
            if (Padding == null)
            {
                calcPadding();
            }
            byte[] buffer = new byte[EntrySize];
            CBinaryWriter writer = new CBinaryWriter(EndianType.LittleEndian, new MemoryStream(buffer));
            writer.Write(SubTreeL);
            writer.Write(SubTreeR);
            writer.Write(Sector);
            writer.Write(Size);
            writer.Write((byte) Attributes);
            writer.Write(NameLength);
            writer.Write(Name.ToCharArray(), 0, Name.Length);
            writer.Write(Padding);
            writer.Close();
            return buffer;
        }

        public override string ToString()
        {
            string str = "";
            object obj2 = str;
            return ((((string.Concat(new[] { obj2, "XDFDirEntry '", Name, "' at Sector: ", Sector, ", Size: {2}\n" }) + "---------------------------------------------------\n") + "STL = " + SubTreeL.ToString()) + "\nSTR = " + SubTreeR.ToString()) + "\nAtt = " + Attributes.ToString() + "\n\n");
        }

        public uint EntrySize
        {
            get
            {
                if (Padding == null)
                {
                    calcPadding();
                }
                return (uint) ((14 + Name.Length) + Padding.Length);
            }
        }

        public bool IsDirectory
        {
            get
            {
                return (((byte) (Attributes & GDFDirEntryAttrib.Directory)) == 0x10);
            }
        }
    }
}

