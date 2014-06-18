namespace Chilano.Xbox360.Xex
{
   public class XexSystemFlags : XexInfoField
    {
        public static byte[] Signature;

        static XexSystemFlags()
        {
            byte[] buffer = new byte[4];
            buffer[1] = 3;
            Signature = buffer;
        }

        public XexSystemFlags(uint Address) : base(Address)
        {
            base.Flags = true;
        }
    }
}

