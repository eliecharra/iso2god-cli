namespace Chilano.Xbox360.Xbe
{
    using Chilano.Xbox360.IO;
    using System;
    using System.Collections.Generic;
    using System.IO;

    public class XbeInfo : IDisposable
    {
        private CBinaryReader br;
        public XbeCertifcate Certifcate;
        private byte[] data;
        public XbeHeader Header;
        public List<XbeSection> Sections = new List<XbeSection>();

        public XbeInfo(byte[] Xbe)
        {
            this.data = Xbe;
            this.br = new CBinaryReader(EndianType.LittleEndian, new MemoryStream(this.data));
            this.Header = new XbeHeader(this.br);
            if (this.Header.IsValid)
            {
                this.br.Seek((long) (this.Header.CertificateAddress - this.Header.BaseAddress), SeekOrigin.Begin);
                this.Certifcate = new XbeCertifcate(this.br);
                this.br.Seek((long) (this.Header.SectionHeadersAddress - this.Header.BaseAddress), SeekOrigin.Begin);
                for (uint i = 0; i < this.Header.NumberOfSections; i++)
                {
                    this.Sections.Add(new XbeSection(this.br));
                }
                foreach (XbeSection section in this.Sections)
                {
                    section.Read(this.br, this.Header.BaseAddress);
                }
            }
        }

        public void Dispose()
        {
            this.br.Close();
        }

        public bool IsValid
        {
            get
            {
                if (this.Header == null)
                {
                    return false;
                }
                return this.Header.IsValid;
            }
        }
    }
}

