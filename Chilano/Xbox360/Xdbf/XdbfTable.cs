namespace Chilano.Xbox360.Xdbf
{
    using Chilano.Xbox360.IO;
    using System;
    using System.Collections.Generic;
    using System.IO;

    public class XdbfTable : List<XdbfTableEntry>
    {
        public XdbfTable(CBinaryReader b, XdbfHeader header)
        {
            b.Seek(30L, SeekOrigin.Begin);
            for (int i = 0; i < header.NumEntries; i++)
            {
                base.Add(new XdbfTableEntry(b));
            }
            while (b.PeekChar() == 0)
            {
                b.ReadByte();
            }
        }
    }
}

