namespace Chilano.Xbox360.Xex
{
   public class XexModuleFlags : XexInfoField
    {
        public static byte[] Signature;

        static XexModuleFlags()
        {
            byte[] buffer = new byte[4];
            buffer[1] = 3;
            Signature = buffer;
        }

        public XexModuleFlags(uint Address) : base(Address)
        {
            Flags = true;
        }

        public bool DeltaPatch
        {
            get
            {
                return ((Address & 1) == 0x40);
            }
        }

        public bool DllModule
        {
            get
            {
                return ((Address & 8) == 8);
            }
        }

        public bool ExportsToTitle
        {
            get
            {
                return ((Address & 2) == 2);
            }
        }

        public bool FullPatch
        {
            get
            {
                return ((Address & 1) == 0x20);
            }
        }

        public bool ModulePatch
        {
            get
            {
                return ((Address & 0x10) == 0x10);
            }
        }

        public bool SystemDebugger
        {
            get
            {
                return ((Address & 4) == 4);
            }
        }

        public bool TitleModule
        {
            get
            {
                return ((Address & 1) == 1);
            }
        }

        public bool UserMode
        {
            get
            {
                return ((Address & 1) == 0x80);
            }
        }
    }
}

