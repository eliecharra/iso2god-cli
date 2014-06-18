namespace Chilano.Xbox360.Xex
{
    using IO;
    using System;
    using System.IO;

    public class XexInfo : IDisposable
    {
        private CBinaryReader br;
       public XexHeader Header;
        private MemoryStream ms;

        public XexInfo(byte[] Xex)
        {
           ms = new MemoryStream(Xex);
            br = new CBinaryReader(EndianType.BigEndian, ms);
            Header = new XexHeader(br);
            foreach (XexInfoField field in Header.Values)
            {
                if (!field.Flags)
                {
                    field.Parse(br);
                }
            }
        }

        public void Dispose()
        {
            br.Close();
            ms.Close();
        }

        public bool IsValid
        {
            get
            {
                return (Header.Count > 0);
            }
        }
    }
}

