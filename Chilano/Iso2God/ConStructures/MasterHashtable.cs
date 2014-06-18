namespace Chilano.Iso2God.ConStructures
{
   using System.Collections.Generic;
    using System.IO;

    public class MasterHashtable : List<byte[]>
    {
        public MasterHashtable() : base(0xcc)
        {
        }

        public void ReadMHT(FileStream f)
        {
            byte[] buffer = new byte[0x1000];
            f.Read(buffer, 0, buffer.Length);
            for (int i = 0; i < 0xcc; i++)
            {
                uint num2 = 0;
                byte[] item = new byte[20];
                for (int j = 0; j < 20; j++)
                {
                    item[j] = buffer[(i * 20) + j];
                    num2 += item[j];
                }
                if (num2 <= 0)
                {
                    break;
                }
                base.Add(item);
            }
        }

        public byte[] ToByteArray()
        {
            byte[] array = new byte[0x1000];
            uint num = 0;
            for (int i = 0; i < array.Length; i++)
            {
                array[i] = 0;
            }
            foreach (byte[] buffer2 in this)
            {
                buffer2.CopyTo(array, (long) num);
                num += 20;
            }
            return array;
        }

        public void Write(FileStream f)
        {
            BinaryWriter writer = new BinaryWriter(f);
            foreach (byte[] buffer in this)
            {
                writer.Write(buffer, 0, buffer.Length);
            }
        }

        public void WriteBlank(FileStream f)
        {
            BinaryWriter writer = new BinaryWriter(f);
            for (int i = 0; i < 0x1000; i++)
            {
                writer.Write((byte) 0);
            }
        }
    }
}

