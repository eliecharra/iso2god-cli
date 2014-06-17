namespace Chilano.Iso2God.ConStructures
{
    using System;
    using System.IO;

    public class LiveHeader
    {
        private byte[] buffer = new byte[0xb000];
        private BinaryWriter bw;
        private MemoryStream ms;

        public LiveHeader()
        {
            this.ms = new MemoryStream(this.buffer);
            this.pad(this.buffer.Length);
            this.ms.Seek(0L, SeekOrigin.Begin);
            this.bw.Write((byte) 0x4c);
            this.bw.Write((byte) 0x49);
            this.bw.Write((byte) 0x56);
            this.bw.Write((byte) 0x45);
            this.ms.Seek(0x22cL, SeekOrigin.Begin);
            this.pad(8, 0xff);
            this.ms.Seek(0x342L, SeekOrigin.Begin);
            this.bw.Write(0xad);
            this.bw.Write(14);
            this.ms.Seek(0x346L, SeekOrigin.Begin);
            this.bw.Write(0x70);
            this.ms.Seek(0x34bL, SeekOrigin.Begin);
            this.bw.Write(2);
            this.ms.Seek(0x35bL, SeekOrigin.Begin);
            this.bw.Write(10);
            this.ms.Seek(0x35fL, SeekOrigin.Begin);
            this.bw.Write(10);
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
            this.pad(count, 0);
        }

        private void pad(int count, byte value)
        {
            for (int i = 0; i < count; i++)
            {
                this.bw.Write(value);
            }
        }

        public void WriteMediaID(string MediaID)
        {
            this.ms.Seek(0x354L, SeekOrigin.Begin);
            byte[] buffer = this.hexToBytes(MediaID);
            this.ms.Write(buffer, 0, buffer.Length);
        }

        public void WriteTitleID(string TitleID)
        {
            this.ms.Seek(0x360L, SeekOrigin.Begin);
            byte[] buffer = this.hexToBytes(TitleID);
            this.ms.Write(buffer, 0, buffer.Length);
        }

        public void WriteTitleName(string Name)
        {
        }
    }
}

