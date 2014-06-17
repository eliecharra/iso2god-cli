namespace Chilano.Xbox360.Xex
{
    using System;

    public class XexResourceInfo : XexInfoField
    {
        public static byte[] Signature;

        static XexResourceInfo()
        {
            byte[] buffer = new byte[4];
            buffer[2] = 2;
            buffer[3] = 0xff;
            Signature = buffer;
        }

        public XexResourceInfo(uint Address) : base(Address)
        {
        }
    }
}

