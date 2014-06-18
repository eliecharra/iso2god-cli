namespace Chilano.Xbox360.Xbe
{
    using IO;
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
            data = Xbe;
            br = new CBinaryReader(EndianType.LittleEndian, new MemoryStream(data));
            Header = new XbeHeader(br);
            if (Header.IsValid)
            {
                br.Seek((long) (Header.CertificateAddress - Header.BaseAddress), SeekOrigin.Begin);
                Certifcate = new XbeCertifcate(br);
                br.Seek((long) (Header.SectionHeadersAddress - Header.BaseAddress), SeekOrigin.Begin);
                for (uint i = 0; i < Header.NumberOfSections; i++)
                {
                    Sections.Add(new XbeSection(br));
                }
                foreach (XbeSection section in Sections)
                {
                    section.Read(br, Header.BaseAddress);
                }
            }
        }

        public void Dispose()
        {
            br.Close();
        }

        public bool IsValid
        {
            get
            {
                if (Header == null)
                {
                    return false;
                }
                return Header.IsValid;
            }
        }
    }
}

