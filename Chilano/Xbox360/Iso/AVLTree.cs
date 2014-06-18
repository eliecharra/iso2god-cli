namespace Chilano.Xbox360.Iso
{
   public class AVLTree
    {
        public GDFEntryNode Root;

        public void Insert(GDFDirEntry Value)
        {
            if (Root == null)
            {
                Root = new GDFEntryNode(Value);
            }
            else
            {
                GDFEntryNode.Insert(Root, Value);
            }
            while (Root.Parent != null)
            {
                Root = Root.Parent;
            }
        }

        public override string ToString()
        {
            string str = "";
            if (Root != null)
            {
                object obj2 = str;
                str = string.Concat(new[] { obj2, "Root = ", Root.Key, "," });
                if (Root.Left != null)
                {
                    object obj3 = str;
                    str = string.Concat(new[] { obj3, "Root.Left = ", Root.Left.Key, "," });
                }
                if (Root.Right != null)
                {
                    str = str + "Root.Right = " + Root.Right.Key;
                }
            }
            return str;
        }
    }
}

