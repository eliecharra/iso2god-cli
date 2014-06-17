namespace Chilano.Xbox360.Graphics
{
    using Chilano.Xbox360.IO;
    using System;

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
            this.MagicBytes = br.ReadUInt32();
            if (this.MagicBytes == 0x30525058)
            {
                this.FileSize = br.ReadUInt32();
                this.HeaderSize = br.ReadUInt32();
                this.TextureCommon = br.ReadUInt32();
                this.TextureData = br.ReadUInt32();
                this.TextureLock = br.ReadUInt32();
                this.TextureMisc1 = br.ReadByte();
                this.TextureFormat = br.ReadByte();
                this.TextureRes1 = br.ReadByte();
                this.TextureRes2 = br.ReadByte();
                this.IsValid = true;
            }
        }
    }
}

