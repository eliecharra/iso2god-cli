namespace Chilano.Iso2God.ConStructures
{
   using System.Collections.Generic;
    using System.IO;

    public class SubHashTable : List<byte[]>
    {
        public SubHashTable() : base(0xcc)
        {
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
                buffer2.CopyTo(array, num);
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

