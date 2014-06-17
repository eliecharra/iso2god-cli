namespace Chilano.Xbox360.Graphics
{
    using Chilano.Xbox360.IO;
    using System;
    using System.Drawing;

    public class DDS
    {
        public byte[] Data;
        public DDSSurfaceDesc2 Header = new DDSSurfaceDesc2();
        public uint magicBytes = 0x20534444;

        public DDS(DDSType Type)
        {
            switch (Type)
            {
                case DDSType.DXT1:
                    this.Header.PixelFormat.Flags |= DDSPixelFormatFlags.DDPF_FOURCC;
                    this.Header.PixelFormat.FourCC = DDSPixelFormatFourCC.DXT1;
                    this.Header.Pitch /= 2;
                    return;

                case DDSType.DXT1a:
                    this.Header.PixelFormat.Flags |= DDSPixelFormatFlags.DDPF_FOURCC;
                    this.Header.PixelFormat.FourCC = DDSPixelFormatFourCC.DXT1;
                    this.Header.Pitch /= 2;
                    return;

                case DDSType.DXT3:
                    this.Header.PixelFormat.Flags |= DDSPixelFormatFlags.DDPF_FOURCC;
                    this.Header.PixelFormat.FourCC = DDSPixelFormatFourCC.DXT3;
                    this.Header.Pitch /= 2;
                    return;

                case DDSType.DXT5:
                    this.Header.PixelFormat.Flags |= DDSPixelFormatFlags.DDPF_FOURCC;
                    this.Header.PixelFormat.FourCC = DDSPixelFormatFourCC.DXT5;
                    this.Header.Pitch /= 2;
                    return;

                case DDSType.b4444:
                case DDSType.b565:
                    break;

                case DDSType.b1555:
                    this.Header.PixelFormat.Flags |= DDSPixelFormatFlags.DDPF_RGB | DDSPixelFormatFlags.DDPF_ALPHAPIXELS;
                    this.Header.PixelFormat.RGBBitCount = 0x10;
                    this.Header.PixelFormat.BitMaskRed = 0x7c00;
                    this.Header.PixelFormat.BitMaskGreen = 0x3e0;
                    this.Header.PixelFormat.BitMaskBlue = 0x1f;
                    this.Header.PixelFormat.BitMaskRGBAlpha = 0x8000;
                    break;

                case DDSType.ARGB:
                    this.Header.PixelFormat.Flags |= DDSPixelFormatFlags.DDPF_RGB | DDSPixelFormatFlags.DDPF_ALPHAPIXELS;
                    this.Header.PixelFormat.RGBBitCount = 0x20;
                    this.Header.PixelFormat.BitMaskRed = 0xff0000;
                    this.Header.PixelFormat.BitMaskGreen = 0xff00;
                    this.Header.PixelFormat.BitMaskBlue = 0xff;
                    this.Header.PixelFormat.BitMaskRGBAlpha = 0xff000000;
                    this.Header.Pitch *= 4;
                    return;

                default:
                    return;
            }
        }

        private void blockDecompressImageDXT1(ulong width, ulong height, byte[] data, Image img)
        {
            ulong num = (width + ((ulong) 3L)) / ((ulong) 4L);
            ulong num2 = (height + ((ulong) 3L)) / ((ulong) 4L);
            long sourceIndex = 0L;
            for (ulong i = 0L; i < num2; i += (ulong) 1L)
            {
                for (ulong j = 0L; j < num; j += (ulong) 1L)
                {
                    byte[] destinationArray = new byte[8];
                    Array.Copy(data, sourceIndex, destinationArray, 0L, 8L);
                    sourceIndex += 8L;
                    this.decompressBlockDXT1(j * ((ulong) 4L), i * ((ulong) 4L), width, destinationArray, img);
                }
            }
        }

        private void decompressBlockDXT1(ulong x, ulong y, ulong width, byte[] blockStorage, Image img)
        {
            ushort num = BitConverter.ToUInt16(blockStorage, 0);
            ushort num2 = BitConverter.ToUInt16(blockStorage, 2);
            ulong num3 = (ulong) (((num >> 11) * 0xff) + 0x10);
            byte r = (byte) (((num3 / ((ulong) 0x20L)) + num3) / ((ulong) 0x20L));
            num3 = (ulong) ((((num & 0x7e0) >> 5) * 0xff) + 0x20);
            byte g = (byte) (((num3 / ((ulong) 0x40L)) + num3) / ((ulong) 0x40L));
            num3 = (ulong) (((num & 0x1f) * 0xff) + 0x10);
            byte b = (byte) (((num3 / ((ulong) 0x20L)) + num3) / ((ulong) 0x20L));
            num3 = (ulong) (((num2 >> 11) * 0xff) + 0x10);
            byte num7 = (byte) (((num3 / ((ulong) 0x20L)) + num3) / ((ulong) 0x20L));
            num3 = (ulong) ((((num2 & 0x7e0) >> 5) * 0xff) + 0x20);
            byte num8 = (byte) (((num3 / ((ulong) 0x40L)) + num3) / ((ulong) 0x40L));
            num3 = (ulong) (((num2 & 0x1f) * 0xff) + 0x10);
            byte num9 = (byte) (((num3 / ((ulong) 0x20L)) + num3) / ((ulong) 0x20L));
            ulong num10 = BitConverter.ToUInt32(blockStorage, 4);
            for (int i = 0; i < 4; i++)
            {
                for (int j = 0; j < 4; j++)
                {
                    Color color = new Color();
                    byte num13 = (byte) ((num10 >> (2 * ((4 * i) + j))) & ((ulong) 3L));
                    if (num > num2)
                    {
                        switch (num13)
                        {
                            case 0:
                                color = this.PackRGBA(r, g, b, 0xff);
                                break;

                            case 1:
                                color = this.PackRGBA(num7, num8, num9, 0xff);
                                break;

                            case 2:
                                color = this.PackRGBA((byte) (((2 * r) + num7) / 3), (byte) (((2 * g) + num8) / 3), (byte) (((2 * b) + num9) / 3), 0xff);
                                break;

                            case 3:
                                color = this.PackRGBA((byte) ((r + (2 * num7)) / 3), (byte) ((g + (2 * num8)) / 3), (byte) ((b + (2 * num9)) / 3), 0xff);
                                break;
                        }
                    }
                    else
                    {
                        switch (num13)
                        {
                            case 0:
                                color = this.PackRGBA(r, g, b, 0xff);
                                break;

                            case 1:
                                color = this.PackRGBA(num7, num8, num9, 0xff);
                                break;

                            case 2:
                                color = this.PackRGBA((byte) ((r + num7) / 2), (byte) ((g + num8) / 2), (byte) ((b + num9) / 2), 0xff);
                                break;

                            case 3:
                                color = this.PackRGBA(0, 0, 0, 0xff);
                                break;
                        }
                    }
                    if ((x + Convert.ToUInt64(j)) < width)
                    {
                        ((Bitmap) img).SetPixel(((int) x) + j, ((int) y) + i, color);
                    }
                }
            }
        }

        public Image GetImage(DDSType Type)
        {
            Image img = new Bitmap(this.Header.Width, this.Header.Height);
            DDSType type = Type;
            if (type != DDSType.DXT1)
            {
                if (type != DDSType.ARGB)
                {
                    return img;
                }
            }
            else
            {
                this.blockDecompressImageDXT1((ulong) this.Header.Width, (ulong) this.Header.Height, this.Data, img);
                return img;
            }
            this.rgbaDecompressImage(img);
            return img;
        }

        private Color PackRGBA(byte r, byte g, byte b, byte a)
        {
            return Color.FromArgb(a, r, g, b);
        }

        private void rgbaDecompressImage(Image img)
        {
            int width = this.Header.Width;
            int height = this.Header.Height;
            int index = 0;
            for (int i = 0; i < height; i++)
            {
                for (int j = 0; j < width; j++)
                {
                    Color color = Color.FromArgb(this.Data[index + 3], this.Data[index + 2], this.Data[index + 1], this.Data[index]);
                    ((Bitmap) img).SetPixel(j, i, color);
                    index += 4;
                }
            }
        }

        public void SetDetails(int Height, int Width, uint MipMapCount)
        {
            this.Header.Height = Height;
            this.Header.Width = Width;
            this.Header.Pitch = (uint) (Height * Width);
            this.Header.MipMapCount = MipMapCount;
            this.Header.Caps.Caps1 |= (MipMapCount > 1) ? DDSCaps1Flags.DDSCAPS_MIPMAP : DDSCaps1Flags.DDSCAPS_TEXTURE;
        }

        public void Write(CBinaryWriter bw)
        {
            bw.Endian = EndianType.LittleEndian;
            bw.WriteUint32(this.magicBytes);
            this.Header.Write(bw);
            if (this.Data != null)
            {
                bw.Write(this.Data);
            }
        }
    }
}

