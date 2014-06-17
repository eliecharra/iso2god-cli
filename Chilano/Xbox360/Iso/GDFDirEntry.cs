namespace Chilano.Xbox360.Iso
{
    using Chilano.Xbox360.IO;
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
            int num = 4 - ((14 + this.NameLength) % 4);
            num = (num == 4) ? 0 : num;
            this.Padding = new byte[num];
            for (int i = 0; i < this.Padding.Length; i++)
            {
                this.Padding[i] = 0xff;
            }
        }

        public object Clone()
        {
            return new GDFDirEntry { SubTreeL = this.SubTreeL, SubTreeR = this.SubTreeR, Sector = this.Sector, Size = this.Size, Attributes = this.Attributes, NameLength = this.NameLength, Name = (string) this.Name.Clone(), Padding = (this.Padding == null) ? null : ((byte[]) this.Padding.Clone()), SubDir = (this.SubDir == null) ? null : ((GDFDirTable) this.SubDir.Clone()) };
        }

        public int CompareTo(GDFDirEntry Entry)
        {
            return this.Name.CompareTo(Entry.Name);
        }

        public byte[] ToByteArray()
        {
            if (this.Padding == null)
            {
                this.calcPadding();
            }
            byte[] buffer = new byte[this.EntrySize];
            CBinaryWriter writer = new CBinaryWriter(EndianType.LittleEndian, new MemoryStream(buffer));
            writer.Write(this.SubTreeL);
            writer.Write(this.SubTreeR);
            writer.Write(this.Sector);
            writer.Write(this.Size);
            writer.Write((byte) this.Attributes);
            writer.Write(this.NameLength);
            writer.Write(this.Name.ToCharArray(), 0, this.Name.Length);
            writer.Write(this.Padding);
            writer.Close();
            return buffer;
        }

        public override string ToString()
        {
            string str = "";
            object obj2 = str;
            return ((((string.Concat(new object[] { obj2, "XDFDirEntry '", this.Name, "' at Sector: ", this.Sector, ", Size: {2}\n" }) + "---------------------------------------------------\n") + "STL = " + this.SubTreeL.ToString()) + "\nSTR = " + this.SubTreeR.ToString()) + "\nAtt = " + this.Attributes.ToString() + "\n\n");
        }

        public uint EntrySize
        {
            get
            {
                if (this.Padding == null)
                {
                    this.calcPadding();
                }
                return (uint) ((14 + this.Name.Length) + this.Padding.Length);
            }
        }

        public bool IsDirectory
        {
            get
            {
                return (((byte) (this.Attributes & GDFDirEntryAttrib.Directory)) == 0x10);
            }
        }
    }
}

