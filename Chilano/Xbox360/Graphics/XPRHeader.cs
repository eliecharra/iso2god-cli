namespace Chilano.Xbox360.Graphics
{
    using IO;

   public class XPRHeader
    {
        public uint FileSize;
        public uint HeaderSize;
        public bool IsValid;
        public uint MagicBytes;
        public uint TextureCommon;
        public uint TextureData;
        public byte TextureFormat;
        public uint TextureLock;
        public byte TextureMisc1;
        public byte TextureRes1;
        public byte TextureRes2;

        public XPRHeader()
        {
        }

        public XPRHeader(CBinaryReader br)
        {
            br.Endian = EndianType.LittleEndian;
            MagicBytes = br.ReadUInt32();
            if (MagicBytes == 0x30525058)
            {
                FileSize = br.ReadUInt32();
                HeaderSize = br.ReadUInt32();
                TextureCommon = br.ReadUInt32();
                TextureData = br.ReadUInt32();
                TextureLock = br.ReadUInt32();
                TextureMisc1 = br.ReadByte();
                TextureFormat = br.ReadByte();
                TextureRes1 = br.ReadByte();
                TextureRes2 = br.ReadByte();
                IsValid = true;
            }
        }
    }
}

