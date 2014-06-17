namespace Chilano.Xbox360.Iso
{
    using System;

    public class AVLTree
    {
        public GDFEntryNode Root;

        public void Insert(GDFDirEntry Value)
        {
            if (this.Root == null)
            {
                this.Root = new GDFEntryNode(Value);
            }
            else
            {
                GDFEntryNode.Insert(this.Root, Value);
            }
            while (this.Root.Parent != null)
            {
                this.Root = this.Root.Parent;
            }
        }

        public override string ToString()
        {
            string str = "";
            if (this.Root != null)
            {
                object obj2 = str;
                str = string.Concat(new object[] { obj2, "Root = ", this.Root.Key, "," });
                if (this.Root.Left != null)
                {
                    object obj3 = str;
                    str = string.Concat(new object[] { obj3, "Root.Left = ", this.Root.Left.Key, "," });
                }
                if (this.Root.Right != null)
                {
                    str = str + "Root.Right = " + this.Root.Right.Key;
                }
            }
            return str;
        }
    }
}

