namespace Chilano.Xbox360.Xex
{
    using System;

    public class XexRatingsInfo : XexInfoField
    {
        public static byte[] Signature = new byte[] { 0, 4, 3, 0x10 };

        public XexRatingsInfo(uint Address) : base(Address)
        {
        }
    }
}

