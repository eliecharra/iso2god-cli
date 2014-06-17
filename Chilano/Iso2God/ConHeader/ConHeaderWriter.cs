namespace Chilano.Iso2God.ConHeader
{
    using Chilano.Iso2God.ConStructures;
    using Chilano.Iso2God.Properties;
    using Chilano.Xbox360.IO;
    using System;
    using System.IO;
    using System.Security.Cryptography;
    using System.Text;

    public class ConHeaderWriter
    {
        private byte[] buffer = new byte[Resources.emptyLIVE.Length];
        private CBinaryWriter bw;
        private MemoryStream header;

        public ConHeaderWriter()
        {
            Resources.emptyLIVE.CopyTo(this.buffer, 0);
            this.header = new MemoryStream(this.buffer);
            this.bw = new CBinaryWriter(EndianType.BigEndian, this.header);
        }

        private string bytesToHexString(byte[] value)
        {
            StringBuilder builder = new StringBuilder(value.Length * 2);
            foreach (byte num2 in value)
            {
                builder.Append(num2.ToString("X02"));
            }
            return builder.ToString();
        }

        public void Close()
        {
            this.bw.Close();
        }

        private byte[] hexStringToBytes(string value)
        {
            int num = value.Length / 2;
            byte[] buffer = new byte[num];
            for (int i = 0; i < num; i++)
            {
                buffer[i] = Convert.ToByte(value.Substring(i * 2, 2), 0x10);
            }
            return buffer;
        }

        public void Write(string FilePath)
        {
            this.bw.Seek(0x35bL, SeekOrigin.Begin);
            this.bw.Write((byte) 0);
            this.bw.Seek(0x35fL, SeekOrigin.Begin);
            this.bw.Write((byte) 0);
            this.bw.Seek(0x391L, SeekOrigin.Begin);
            this.bw.Write((byte) 0);
            this.WriteHash();
            FileStream stream = new FileStream(FilePath, FileMode.Create, FileAccess.ReadWrite, FileShare.None);
            stream.Write(this.buffer, 0, this.buffer.Length);
            stream.Close();
        }

        public void WriteBlockCounts(uint TotalNumberToBeStored, ushort TotalNumberNotAllocated)
        {
            this.header.Seek(0x392L, SeekOrigin.Begin);
            this.bw.WriteUint24(TotalNumberToBeStored);
            this.header.Seek(0x395L, SeekOrigin.Begin);
            this.bw.WriteUint16(TotalNumberNotAllocated);
        }

        public void WriteContentType(ContentType Type)
        {
            this.bw.Seek(0x344L, SeekOrigin.Begin);
            this.bw.Endian = EndianType.BigEndian;
            this.bw.WriteUint32((uint) Type);
        }

        public void WriteDataPartsInfo(uint NumberParts, ulong SizeOfDataParts)
        {
            this.bw.Seek(0x3a0L, SeekOrigin.Begin);
            this.bw.Endian = EndianType.LittleEndian;
            this.bw.WriteUint32(NumberParts);
            this.bw.Seek(0x3a4L, SeekOrigin.Begin);
            this.bw.Endian = EndianType.BigEndian;
            this.bw.WriteUint32((uint) (SizeOfDataParts / ((ulong) 0x100L)));
        }

        public void WriteExecutionDetails(byte DiscNumber, byte DiscCount, byte Platform, byte ExType)
        {
            this.header.Seek(0x364L, SeekOrigin.Begin);
            this.bw.Write(Platform);
            this.header.Seek(0x365L, SeekOrigin.Begin);
            this.bw.Write(ExType);
            this.header.Seek(870L, SeekOrigin.Begin);
            this.bw.Write(DiscNumber);
            this.header.Seek(0x367L, SeekOrigin.Begin);
            this.bw.Write(DiscCount);
        }

        public void WriteGameIcon(byte[] PngData)
        {
            if (PngData == null)
            {
                PngData = new byte[20];
            }
            uint length = (uint) PngData.Length;
            this.bw.Seek(0x1712L, SeekOrigin.Begin);
            this.bw.WriteUint32(length);
            this.bw.Seek(0x1716L, SeekOrigin.Begin);
            this.bw.WriteUint32(length);
            this.bw.Seek(0x171aL, SeekOrigin.Begin);
            this.bw.Write(PngData);
            this.bw.Seek(0x571aL, SeekOrigin.Begin);
            this.bw.Write(PngData);
        }

        public void WriteHash()
        {
            byte[] buffer = new SHA1Managed().ComputeHash(this.buffer, 0x344, 0xacbc);
            this.bw.Seek(0x32cL, SeekOrigin.Begin);
            this.bw.Write(buffer);
        }

        public void WriteIDs(string TitleID, string MediaID, string GameTitle)
        {
            this.header.Seek(0x360L, SeekOrigin.Begin);
            this.bw.Write(this.hexStringToBytes(TitleID));
            this.header.Seek(0x354L, SeekOrigin.Begin);
            this.bw.Write(this.hexStringToBytes(MediaID));
            byte[] bytes = Encoding.Unicode.GetBytes(GameTitle);
            for (int i = 0; i < bytes.Length; i += 2)
            {
                byte num2 = bytes[i];
                bytes[i] = bytes[i + 1];
                bytes[i + 1] = num2;
            }
            this.header.Seek(0x411L, SeekOrigin.Begin);
            this.header.Write(bytes, 0, bytes.Length);
            this.header.Seek(0x1691L, SeekOrigin.Begin);
            this.header.Write(bytes, 0, bytes.Length);
        }

        public void WriteMhtHash(byte[] hash)
        {
            this.header.Seek(0x37dL, SeekOrigin.Begin);
            this.header.Write(hash, 0, hash.Length);
        }
    }
}

