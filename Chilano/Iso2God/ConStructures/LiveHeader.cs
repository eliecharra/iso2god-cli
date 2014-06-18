namespace Chilano.Iso2God.ConStructures
{
   using System.IO;

    public class LiveHeader
    {
        private byte[] buffer = new byte[0xb000];
        private BinaryWriter bw;
        private MemoryStream ms;

        public LiveHeader()
        {
            ms = new MemoryStream(buffer);
            pad(buffer.Length);
            ms.Seek(0L, SeekOrigin.Begin);
            bw.Write((byte) 0x4c);
            bw.Write((byte) 0x49);
            bw.Write((byte) 0x56);
            bw.Write((byte) 0x45);
            ms.Seek(0x22cL, SeekOrigin.Begin);
            pad(8, 0xff);
            ms.Seek(0x342L, SeekOrigin.Begin);
            bw.Write(0xad);
            bw.Write(14);
            ms.Seek(0x346L, SeekOrigin.Begin);
            bw.Write(0x70);
            ms.Seek(0x34bL, SeekOrigin.Begin);
            bw.Write(2);
            ms.Seek(0x35bL, SeekOrigin.Begin);
            bw.Write(10);
            ms.Seek(0x35fL, SeekOrigin.Begin);
            bw.Write(10);
        }

        private byte[] hexToBytes(string hex)
        {
            byte[] buffer = new byte[hex.Length];
            for (int i = 0; i < buffer.Length; i++)
            {
                char c = char.ToUpper(hex[i]);
                if (char.IsDigit(c))
                {
                    buffer[i] = (byte) (((byte) c) - 0x30);
                }
                if (char.IsLetter(c))
                {
                    buffer[i] = (byte) (((byte) c) - 0x37);
                }
            }
            return buffer;
        }

        private void pad(int count)
        {
            pad(count, 0);
        }

        private void pad(int count, byte value)
        {
            for (int i = 0; i < count; i++)
            {
                bw.Write(value);
            }
        }

        public void WriteMediaID(string MediaID)
        {
            ms.Seek(0x354L, SeekOrigin.Begin);
            byte[] buffer = hexToBytes(MediaID);
            ms.Write(buffer, 0, buffer.Length);
        }

        public void WriteTitleID(string TitleID)
        {
            ms.Seek(0x360L, SeekOrigin.Begin);
            byte[] buffer = hexToBytes(TitleID);
            ms.Write(buffer, 0, buffer.Length);
        }

        public void WriteTitleName(string Name)
        {
        }
    }
}

