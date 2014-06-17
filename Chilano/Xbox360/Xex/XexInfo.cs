namespace Chilano.Xbox360.Xex
{
    using Chilano.Xbox360.IO;
    using System;
    using System.IO;

    public class XexInfo : IDisposable
    {
        private CBinaryReader br;
        private byte[] data;
        public XexHeader Header;
        private MemoryStream ms;

        public XexInfo(byte[] Xex)
        {
            this.data = Xex;
            this.ms = new MemoryStream(this.data);
            this.br = new CBinaryReader(EndianType.BigEndian, this.ms);
            this.Header = new XexHeader(this.br);
            foreach (XexInfoField field in this.Header.Values)
            {
                if (!field.Flags)
                {
                    field.Parse(this.br);
                }
            }
        }

        public void Dispose()
        {
            this.br.Close();
            this.ms.Close();
        }

        public bool IsValid
        {
            get
            {
                return (this.Header.Count > 0);
            }
        }
    }
}

