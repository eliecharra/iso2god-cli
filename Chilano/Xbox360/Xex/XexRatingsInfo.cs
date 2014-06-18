namespace Chilano.Xbox360.Xex
{
   public class XexRatingsInfo : XexInfoField
    {
        public static byte[] Signature = { 0, 4, 3, 0x10 };

        public XexRatingsInfo(uint Address) : base(Address)
        {
        }
    }
}

