using System;
using System.Text;

namespace PDF.ExifUtils.Exif.IO
{
    /// <summary>
    /// Encodes the GDI+ representation of EXIF properties.
    /// </summary>
    internal static class EngExifEncoder
    {
        #region Byte Encoding

        /// <summary>
        /// 
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        private static byte[] WriteBytes(object value)
        {
            if (value == null)
            {
                return new byte[0];
            }

            if (value is Array)
            {
                Array array = value as Array;
                int count = array.Length;
                byte[] data = new byte[count];

                for (int i = 0; i < count; i++)
                {
                    data[i] = Convert.ToByte(array.GetValue(i));
                }

                return data;
            }

            if (value.GetType().IsValueType || value is IConvertible)
            {
                return new byte[]
                {
                    Convert.ToByte(value)
                };
            }

            throw new ArgumentException(String.Format("Error converting {0} to byte[].", value.GetType().Name));
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        private static byte[] WriteUInt16(object value)
        {
            if (value == null)
            {
                return new byte[0];
            }

            if (value is Array)
            {
                Array array = value as Array;
                int count = array.Length;
                byte[] data = new byte[count * EngExifDecoder.UInt16Size];

                for (int i = 0; i < count; i++)
                {
                    byte[] item = BitConverter.GetBytes(Convert.ToUInt16(array.GetValue(i)));
                    item.CopyTo(data, i * EngExifDecoder.UInt16Size);
                }

                return data;
            }

            if (value.GetType().IsValueType || value is IConvertible)
            {
                return BitConverter.GetBytes(Convert.ToUInt16(value));
            }

            throw new ArgumentException(String.Format("Error converting {0} to UInt16[].", value.GetType().Name));
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        private static byte[] WriteInt32(object value)
        {
            if (value == null)
            {
                return new byte[0];
            }

            if (value is Array)
            {
                Array array = value as Array;
                int count = array.Length;
                byte[] data = new byte[count * EngExifDecoder.Int32Size];

                for (int i = 0; i < count; i++)
                {
                    byte[] item = BitConverter.GetBytes(Convert.ToInt32(array.GetValue(i)));
                    item.CopyTo(data, i * EngExifDecoder.Int32Size);
                }

                return data;
            }

            if (value.GetType().IsValueType || value is IConvertible)
            {
                return BitConverter.GetBytes(Convert.ToInt32(value));
            }

            throw new ArgumentException(String.Format("Error converting {0} to Int32[].", value.GetType().Name));
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        private static byte[] WriteUInt32(object value)
        {
            if (value == null)
            {
                return new byte[0];
            }

            if (value is Array)
            {
                Array array = value as Array;
                int count = array.Length;
                byte[] data = new byte[count * EngExifDecoder.UInt32Size];

                for (int i = 0; i < count; i++)
                {
                    byte[] item = BitConverter.GetBytes(Convert.ToUInt32(array.GetValue(i)));
                    item.CopyTo(data, i * EngExifDecoder.UInt32Size);
                }

                return data;
            }

            if (value.GetType().IsValueType || value is IConvertible)
            {
                return BitConverter.GetBytes(Convert.ToUInt32(value));
            }

            throw new ArgumentException(String.Format("Error converting {0} to UInt32[].", value.GetType().Name));
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        private static byte[] WriteRational(object value)
        {
            if (value == null)
            {
                return new byte[0];
            }

            if (value is Array)
            {
                Array array = value as Array;
                int count = array.Length;
                byte[] data = new byte[count * EngExifDecoder.RationalSize];

                for (int i = 0; i < count; i++)
                {
                    Rational<int> item = (Rational<int>)Convert.ChangeType(array.GetValue(i), typeof(Rational<int>));
                    BitConverter.GetBytes(item.Numerator).CopyTo(data, i * EngExifDecoder.RationalSize);
                    BitConverter.GetBytes(item.Denominator).CopyTo(data, i * EngExifDecoder.RationalSize + EngExifDecoder.Int32Size);
                }

                return data;
            }

            if (value.GetType().IsValueType || value is IConvertible)
            {
                byte[] data = new byte[EngExifDecoder.RationalSize];

                Rational<int> item = (Rational<int>)Convert.ChangeType(value, typeof(Rational<int>));
                BitConverter.GetBytes(item.Numerator).CopyTo(data, 0);
                BitConverter.GetBytes(item.Denominator).CopyTo(data, EngExifDecoder.UInt32Size);

                return data;
            }

            throw new ArgumentException(String.Format("Error converting {0} to Rational<int>[].", value.GetType().Name));
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        private static byte[] WriteURational(object value)
        {
            if (value == null)
            {
                return new byte[0];
            }

            if (value is Array)
            {
                Array array = value as Array;
                int count = array.Length;
                byte[] data = new byte[count * EngExifDecoder.URationalSize];

                for (int i = 0; i < count; i++)
                {
                    Rational<uint> item = (Rational<uint>)Convert.ChangeType(array.GetValue(i), typeof(Rational<uint>));
                    BitConverter.GetBytes(item.Numerator).CopyTo(data, i * EngExifDecoder.URationalSize);
                    BitConverter.GetBytes(item.Denominator).CopyTo(data, i * EngExifDecoder.URationalSize + EngExifDecoder.UInt32Size);
                }

                return data;
            }

            if (value.GetType().IsValueType || value is IConvertible)
            {
                byte[] data = new byte[EngExifDecoder.RationalSize];

                Rational<uint> item = (Rational<uint>)Convert.ChangeType(value, typeof(Rational<uint>));
                BitConverter.GetBytes(item.Numerator).CopyTo(data, 0);
                BitConverter.GetBytes(item.Denominator).CopyTo(data, EngExifDecoder.UInt32Size);

                return data;
            }

            throw new ArgumentException(String.Format("Error converting {0} to Rational<uint>[].", value.GetType().Name));
        }

        #endregion Byte Encoding

        #region Data Conversion

        public static byte[] ConvertData(Type dataType, EngExifType targetType, object value)
        {
            switch (targetType)
            {
                case EngExifType.Ascii:
                    {
                        return Encoding.ASCII.GetBytes(Convert.ToString(value) + '\0');
                    }
                case EngExifType.Byte:
                case EngExifType.Raw:
                    {
                        if (dataType == typeof(UnicodeEncoding))
                        {
                            return Encoding.Unicode.GetBytes(Convert.ToString(value) + '\0');
                        }

                        return EngExifEncoder.WriteBytes(value);
                    }
                case EngExifType.Int32:
                    {
                        return EngExifEncoder.WriteInt32(value);
                    }
                case EngExifType.Rational:
                    {
                        return EngExifEncoder.WriteRational(value);
                    }
                case EngExifType.UInt16:
                    {
                        return EngExifEncoder.WriteUInt16(value);
                    }
                case EngExifType.UInt32:
                    {
                        return EngExifEncoder.WriteUInt32(value);
                    }
                case EngExifType.URational:
                    {
                        return EngExifEncoder.WriteURational(value);
                    }
                default:
                    {
                        throw new NotImplementedException(String.Format("Encoding for EXIF type \"{0}\" has not yet been implemented.", targetType));
                    }
            }
        }

        #endregion Data Conversion
    }
}
