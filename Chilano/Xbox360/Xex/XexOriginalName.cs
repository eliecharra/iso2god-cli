namespace Chilano.Xbox360.Xex
{
    using System;

    public class XexOriginalName : XexInfoField
    {
        public static byte[] Signature = new byte[] { 0, 1, 0x83, 0xff };

        public XexOriginalName(uint Address) : base(Address)
        {
        }
    }
}

