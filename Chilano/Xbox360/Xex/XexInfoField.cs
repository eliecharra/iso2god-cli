namespace Chilano.Xbox360.Xex
{
    using IO;

   public class XexInfoField
    {
        public uint Address;
        private bool flags;

        public XexInfoField(uint address)
        {
            Address = address;
        }

        public virtual void Parse(CBinaryReader br)
        {
        }

        public bool Flags
        {
            get
            {
                return flags;
            }
            set
            {
                flags = value;
            }
        }

        public bool Found
        {
            get
            {
                return (Address != 0);
            }
        }
    }
}

