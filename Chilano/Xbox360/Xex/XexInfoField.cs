namespace Chilano.Xbox360.Xex
{
    using Chilano.Xbox360.IO;
    using System;

    public class XexInfoField
    {
        public uint Address;
        private bool flags;

        public XexInfoField(uint address)
        {
            this.Address = address;
        }

        public virtual void Parse(CBinaryReader br)
        {
        }

        public bool Flags
        {
            get
            {
                return this.flags;
            }
            set
            {
                this.flags = value;
            }
        }

        public bool Found
        {
            get
            {
                return (this.Address != 0);
            }
        }
    }
}

