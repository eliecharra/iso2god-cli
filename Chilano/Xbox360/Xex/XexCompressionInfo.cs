namespace Chilano.Xbox360.Xex
{
    using System;

    public class XexCompressionInfo : XexInfoField
    {
        public static byte[] Signature;

        static XexCompressionInfo()
        {
            byte[] buffer = new byte[4];
            buffer[2] = 3;
            buffer[3] = 0xff;
            Signature = buffer;
        }

        public XexCompressionInfo(uint Address) : base(Address)
        {
        }
    }
}

