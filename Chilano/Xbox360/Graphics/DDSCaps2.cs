namespace Chilano.Xbox360.Graphics
{
    using IO;

   public class DDSCaps2
    {
        public DDSCaps1Flags Caps1;
        public DDSCaps2Flags Caps2;
        public byte[] Reserved;

        public DDSCaps2()
        {
            Reserved = new byte[8];
        }

        public DDSCaps2(DDSCaps1Flags Caps1, DDSCaps2Flags Caps2)
        {
            Reserved = new byte[8];
            this.Caps1 = Caps1;
            this.Caps2 = Caps2;
        }

        public void Write(CBinaryWriter bw)
        {
            bw.Write((uint) Caps1);
            bw.Write((uint) Caps2);
            bw.Write(Reserved);
        }
    }
}

