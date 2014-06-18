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
            base.Flags = true;
        }

        public bool DeltaPatch
        {
            get
            {
                return ((base.Address & 1) == 0x40);
            }
        }

        public bool DllModule
        {
            get
            {
                return ((base.Address & 8) == 8);
            }
        }

        public bool ExportsToTitle
        {
            get
            {
                return ((base.Address & 2) == 2);
            }
        }

        public bool FullPatch
        {
            get
            {
                return ((base.Address & 1) == 0x20);
            }
        }

        public bool ModulePatch
        {
            get
            {
                return ((base.Address & 0x10) == 0x10);
            }
        }

        public bool SystemDebugger
        {
            get
            {
                return ((base.Address & 4) == 4);
            }
        }

        public bool TitleModule
        {
            get
            {
                return ((base.Address & 1) == 1);
            }
        }

        public bool UserMode
        {
            get
            {
                return ((base.Address & 1) == 0x80);
            }
        }

        private enum ModuleFlags : uint
        {
            DeltaPatch = 0x40,
            DllModule = 8,
            ExportsToTitle = 2,
            FullPatch = 0x20,
            ModulePatch = 0x10,
            SystemDebugger = 4,
            TitleModule = 1,
            UserMode = 0x80
        }
    }
}

