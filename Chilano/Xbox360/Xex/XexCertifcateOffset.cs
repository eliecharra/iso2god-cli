namespace Chilano.Xbox360.Xex
{
    using System;

    public class XexCertifcateOffset : XexInfoField
    {
        public static byte[] Signature;

        static XexCertifcateOffset()
        {
            byte[] buffer = new byte[4];
            buffer[2] = 3;
            buffer[3] = 0xff;
            Signature = buffer;
        }

        public XexCertifcateOffset(uint Address) : base(Address)
        {
        }
    }
}

