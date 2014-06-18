namespace Chilano.Iso2God.ConHeader
{
    using ConStructures;
    using Properties;
    using Xbox360.IO;
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
            Resources.emptyLIVE.CopyTo(buffer, 0);
            header = new MemoryStream(buffer);
            bw = new CBinaryWriter(EndianType.BigEndian, header);
        }

        public void Close()
        {
            bw.Close();
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
            bw.Seek(0x35bL, SeekOrigin.Begin);
            bw.Write((byte) 0);
            bw.Seek(0x35fL, SeekOrigin.Begin);
            bw.Write((byte) 0);
            bw.Seek(0x391L, SeekOrigin.Begin);
            bw.Write((byte) 0);
            WriteHash();
            FileStream stream = new FileStream(FilePath, FileMode.Create, FileAccess.ReadWrite, FileShare.None);
            stream.Write(buffer, 0, buffer.Length);
            stream.Close();
        }

        public void WriteBlockCounts(uint TotalNumberToBeStored, ushort TotalNumberNotAllocated)
        {
            header.Seek(0x392L, SeekOrigin.Begin);
            bw.WriteUint24(TotalNumberToBeStored);
            header.Seek(0x395L, SeekOrigin.Begin);
            bw.WriteUint16(TotalNumberNotAllocated);
        }

        public void WriteContentType(ContentType Type)
        {
            bw.Seek(0x344L, SeekOrigin.Begin);
            bw.Endian = EndianType.BigEndian;
            bw.WriteUint32((uint) Type);
        }

        public void WriteDataPartsInfo(uint NumberParts, ulong SizeOfDataParts)
        {
            bw.Seek(0x3a0L, SeekOrigin.Begin);
            bw.Endian = EndianType.LittleEndian;
            bw.WriteUint32(NumberParts);
            bw.Seek(0x3a4L, SeekOrigin.Begin);
            bw.Endian = EndianType.BigEndian;
            bw.WriteUint32((uint) (SizeOfDataParts / 0x100L));
        }

        public void WriteExecutionDetails(byte DiscNumber, byte DiscCount, byte Platform, byte ExType)
        {
            header.Seek(0x364L, SeekOrigin.Begin);
            bw.Write(Platform);
            header.Seek(0x365L, SeekOrigin.Begin);
            bw.Write(ExType);
            header.Seek(870L, SeekOrigin.Begin);
            bw.Write(DiscNumber);
            header.Seek(0x367L, SeekOrigin.Begin);
            bw.Write(DiscCount);
        }

        public void WriteGameIcon(byte[] PngData)
        {
            if (PngData == null)
            {
                PngData = new byte[20];
            }
            uint length = (uint) PngData.Length;
            bw.Seek(0x1712L, SeekOrigin.Begin);
            bw.WriteUint32(length);
            bw.Seek(0x1716L, SeekOrigin.Begin);
            bw.WriteUint32(length);
            bw.Seek(0x171aL, SeekOrigin.Begin);
            bw.Write(PngData);
            bw.Seek(0x571aL, SeekOrigin.Begin);
            bw.Write(PngData);
        }

        public void WriteHash()
        {
            byte[] buffer = new SHA1Managed().ComputeHash(this.buffer, 0x344, 0xacbc);
            bw.Seek(0x32cL, SeekOrigin.Begin);
            bw.Write(buffer);
        }

        public void WriteIDs(string TitleID, string MediaID, string GameTitle)
        {
            header.Seek(0x360L, SeekOrigin.Begin);
            bw.Write(hexStringToBytes(TitleID));
            header.Seek(0x354L, SeekOrigin.Begin);
            bw.Write(hexStringToBytes(MediaID));
            byte[] bytes = Encoding.Unicode.GetBytes(GameTitle);
            for (int i = 0; i < bytes.Length; i += 2)
            {
                byte num2 = bytes[i];
                bytes[i] = bytes[i + 1];
                bytes[i + 1] = num2;
            }
            header.Seek(0x411L, SeekOrigin.Begin);
            header.Write(bytes, 0, bytes.Length);
            header.Seek(0x1691L, SeekOrigin.Begin);
            header.Write(bytes, 0, bytes.Length);
        }

        public void WriteMhtHash(byte[] hash)
        {
            header.Seek(0x37dL, SeekOrigin.Begin);
            header.Write(hash, 0, hash.Length);
        }
    }
}

