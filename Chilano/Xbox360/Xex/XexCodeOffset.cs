namespace Chilano.Xbox360.Xex
{
   public class XexCodeOffset : XexInfoField
    {
        public static byte[] Signature;

        static XexCodeOffset()
        {
            byte[] buffer = new byte[4];
            buffer[2] = 3;
            buffer[3] = 0xff;
            Signature = buffer;
        }

        public XexCodeOffset(uint Address) : base(Address)
        {
        }
    }
}

