namespace Chilano.Xbox360.Xex
{
    using System;

    public class XexBaseFileTimestamp : XexInfoField
    {
        public static byte[] Signature = new byte[] { 0, 1, 0x80, 2 };

        public XexBaseFileTimestamp(uint Address) : base(Address)
        {
        }
    }
}

