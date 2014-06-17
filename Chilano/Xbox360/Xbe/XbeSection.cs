namespace Chilano.Xbox360.Xbe
{
    using Chilano.Xbox360.IO;
    using System;
    using System.IO;

    public class XbeSection
    {
        public byte[] Data;
        public XbeSectionHeader Header;
        public string Name;

        public XbeSection()
        {
        }

        public XbeSection(CBinaryReader br)
        {
            this.Header = new XbeSectionHeader(br);
            this.Name = "";
        }

        public void Read(CBinaryReader br, uint BaseAddress)
        {
            br.Seek((long) (this.Header.SectionNameAddress - BaseAddress), SeekOrigin.Begin);
            while (br.PeekChar() != 0)
            {
                this.Name = this.Name + br.ReadChar();
            }
            br.Seek((long) this.Header.RawAddress, SeekOrigin.Begin);
            this.Data = br.ReadBytes((int) this.Header.RawSize);
        }
    }
}

