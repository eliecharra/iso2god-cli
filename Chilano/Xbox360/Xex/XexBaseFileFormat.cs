namespace Chilano.Xbox360.Xex
{
   public class XexBaseFileFormat : XexInfoField
    {
        public static byte[] Signature;

        static XexBaseFileFormat()
        {
            byte[] buffer = new byte[4];
            buffer[2] = 3;
            buffer[3] = 0xff;
            Signature = buffer;
        }

        public XexBaseFileFormat(uint Address) : base(Address)
        {
        }
    }
}

