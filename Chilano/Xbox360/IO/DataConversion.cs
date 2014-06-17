namespace Chilano.Xbox360.IO
{
    using System;
    using System.Text;

    public static class DataConversion
    {
        public static string BytesToHexString(byte[] value)
        {
            StringBuilder builder = new StringBuilder(value.Length * 2);
            foreach (byte num2 in value)
            {
                builder.Append(num2.ToString("X02"));
            }
            return builder.ToString();
        }
    }
}

