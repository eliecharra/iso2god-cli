namespace Chilano.Iso2God.Properties
{
    using System;
    using System.CodeDom.Compiler;
    using System.ComponentModel;
    using System.Diagnostics;
    using System.Drawing;
    using System.Globalization;
    using System.Resources;
    using System.Runtime.CompilerServices;

    [GeneratedCode("System.Resources.Tools.StronglyTypedResourceBuilder", "2.0.0.0"), CompilerGenerated, DebuggerNonUserCode]
    internal class Resources
    {
        private static CultureInfo resourceCulture;
        private static System.Resources.ResourceManager resourceMan;

        internal Resources()
        {
        }

        internal static Icon AppIcon
        {
            get
            {
                return (Icon) ResourceManager.GetObject("AppIcon", resourceCulture);
            }
        }

        internal static Bitmap Application
        {
            get
            {
                return (Bitmap) ResourceManager.GetObject("Application", resourceCulture);
            }
        }

        internal static Bitmap Create
        {
            get
            {
                return (Bitmap) ResourceManager.GetObject("Create", resourceCulture);
            }
        }

        [EditorBrowsable(EditorBrowsableState.Advanced)]
        internal static CultureInfo Culture
        {
            get
            {
                return resourceCulture;
            }
            set
            {
                resourceCulture = value;
            }
        }

        internal static byte[] emptyLIVE
        {
            get
            {
                return (byte[]) ResourceManager.GetObject("emptyLIVE", resourceCulture);
            }
        }

        internal static Bitmap Go
        {
            get
            {
                return (Bitmap) ResourceManager.GetObject("Go", resourceCulture);
            }
        }

        internal static Bitmap Hint
        {
            get
            {
                return (Bitmap) ResourceManager.GetObject("Hint", resourceCulture);
            }
        }

        internal static Bitmap Info
        {
            get
            {
                return (Bitmap) ResourceManager.GetObject("Info", resourceCulture);
            }
        }

        internal static Bitmap LogoAbout
        {
            get
            {
                return (Bitmap) ResourceManager.GetObject("LogoAbout", resourceCulture);
            }
        }

        internal static Bitmap LogoToolbar
        {
            get
            {
                return (Bitmap) ResourceManager.GetObject("LogoToolbar", resourceCulture);
            }
        }

        internal static Bitmap No_entry
        {
            get
            {
                return (Bitmap) ResourceManager.GetObject("No_entry", resourceCulture);
            }
        }

        [EditorBrowsable(EditorBrowsableState.Advanced)]
        internal static System.Resources.ResourceManager ResourceManager
        {
            get
            {
                if (object.ReferenceEquals(resourceMan, null))
                {
                    System.Resources.ResourceManager manager = new System.Resources.ResourceManager("Chilano.Iso2God.Properties.Resources", typeof(Resources).Assembly);
                    resourceMan = manager;
                }
                return resourceMan;
            }
        }

        internal static Bitmap ToolbarBg
        {
            get
            {
                return (Bitmap) ResourceManager.GetObject("ToolbarBg", resourceCulture);
            }
        }
    }
}

