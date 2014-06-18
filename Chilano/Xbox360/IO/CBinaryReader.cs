namespace Chilano.Xbox360.IO
{
    using System;
    using System.IO;
    using System.Text;

    public class CBinaryReader : BinaryReader
    {
        public EndianType Endian;

        public CBinaryReader(EndianType e, Stream s) : base(s)
        {
            Endian = e;
        }

        private object readBigEndian(DataType dt)
        {
            byte[] array = null;
            switch (dt)
            {
                case DataType.Double:
                    array = BitConverter.GetBytes(base.ReadDouble());
                    break;

                case DataType.Int16:
                    array = BitConverter.GetBytes(base.ReadInt16());
                    break;

                case DataType.Int32:
                    array = BitConverter.GetBytes(base.ReadInt32());
                    break;

                case DataType.Int64:
                    array = BitConverter.GetBytes(base.ReadInt64());
                    break;

                case DataType.Single:
                    array = BitConverter.GetBytes(base.ReadSingle());
                    break;

                case DataType.UInt16:
                    array = BitConverter.GetBytes(base.ReadUInt16());
                    break;

                case DataType.UInt32:
                    array = BitConverter.GetBytes(base.ReadUInt32());
                    break;

                case DataType.UInt64:
                    array = BitConverter.GetBytes(base.ReadUInt64());
                    break;
            }
            Array.Reverse(array);
            switch (dt)
            {
                case DataType.Double:
                    return BitConverter.ToDouble(array, 0);

                case DataType.Int16:
                    return BitConverter.ToInt16(array, 0);

                case DataType.Int32:
                    return BitConverter.ToInt32(array, 0);

                case DataType.Int64:
                    return BitConverter.ToInt64(array, 0);

                case DataType.Single:
                    return BitConverter.ToSingle(array, 0);

                case DataType.UInt16:
                    return BitConverter.ToUInt16(array, 0);

                case DataType.UInt32:
                    return BitConverter.ToUInt32(array, 0);

                case DataType.UInt64:
                    return BitConverter.ToUInt64(array, 0);
            }
            return array;
        }

        public override double ReadDouble()
        {
            if (Endian == EndianType.BigEndian)
            {
                return (double) readBigEndian(DataType.Double);
            }
            return base.ReadDouble();
        }

        public override short ReadInt16()
        {
            if (Endian == EndianType.BigEndian)
            {
                return (short) readBigEndian(DataType.Int16);
            }
            return base.ReadInt16();
        }

        public override int ReadInt32()
        {
            if (Endian == EndianType.BigEndian)
            {
                return (int) readBigEndian(DataType.Int32);
            }
            return base.ReadInt32();
        }

        public override long ReadInt64()
        {
            if (Endian == EndianType.BigEndian)
            {
                return (long) readBigEndian(DataType.Int64);
            }
            return base.ReadInt64();
        }

        public override float ReadSingle()
        {
            if (Endian == EndianType.BigEndian)
            {
                return (float) readBigEndian(DataType.Single);
            }
            return base.ReadSingle();
        }

        public string ReadStringUTF16(int Characters)
        {
            if (Endian != EndianType.BigEndian)
            {
                return Encoding.Unicode.GetString(base.ReadBytes(Characters * 2));
            }
            byte[] array = base.ReadBytes(Characters * 2);
            for (int i = 0; i < array.Length; i += 2)
            {
                Array.Reverse(array, i, 2);
            }
            return Encoding.Unicode.GetString(array);
        }

        public override ushort ReadUInt16()
        {
            if (Endian == EndianType.BigEndian)
            {
                return (ushort) readBigEndian(DataType.UInt16);
            }
            return base.ReadUInt16();
        }

        public override uint ReadUInt32()
        {
            if (Endian == EndianType.BigEndian)
            {
                return (uint) readBigEndian(DataType.UInt32);
            }
            return base.ReadUInt32();
        }

        public override ulong ReadUInt64()
        {
            if (Endian == EndianType.BigEndian)
            {
                return (ulong) readBigEndian(DataType.UInt64);
            }
            return base.ReadUInt64();
        }

        public void Seek(long Offset, SeekOrigin Origin)
        {
            base.BaseStream.Seek(Offset, Origin);
        }
    }
}

