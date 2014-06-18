namespace Chilano.Xbox360.Xex
{
   public class XexOriginalName : XexInfoField
    {
        public static byte[] Signature = { 0, 1, 0x83, 0xff };

        public XexOriginalName(uint Address) : base(Address)
        {
        }
    }
}

