namespace Chilano.Xbox360.Xex
{
   public class XexBaseFileTimestamp : XexInfoField
    {
        public static byte[] Signature = { 0, 1, 0x80, 2 };

        public XexBaseFileTimestamp(uint Address) : base(Address)
        {
        }
    }
}

