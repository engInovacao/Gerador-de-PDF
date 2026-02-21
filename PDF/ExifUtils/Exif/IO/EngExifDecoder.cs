using System;
using System.Drawing;
using System.Drawing.Imaging;
using System.Globalization;
using System.IO;
using System.Runtime.InteropServices;
using System.Text;

namespace PDF.ExifUtils.Exif.IO
{
    /// <summary>
	/// Decodes the GDI+ representation of EXIF properties.
	/// </summary>
	internal static class EngExifDecoder
    {
        #region Constants

        private static readonly string[] ExifDateTimeFormats = new string[]{
            "yyyy:MM:dd HH:mm:ss",
            "yyyy:MM:dd   :  :  ",
            "    :  :   HH:mm:ss",
        };

        internal static readonly int UInt16Size = Marshal.SizeOf(typeof(ushort));
        internal static readonly int Int32Size = Marshal.SizeOf(typeof(int));
        internal static readonly int UInt32Size = Marshal.SizeOf(typeof(uint));
        internal static readonly int RationalSize = 2 * Marshal.SizeOf(typeof(int));
        internal static readonly int URationalSize = 2 * Marshal.SizeOf(typeof(uint));

        #endregion Constants

        #region Static Methods

        /// <summary>
        /// Converts a property item to an object or array of objects.
        /// </summary>
        /// <param name="propertyItem">the property item to convert</param>
        /// <returns>the property value</returns>
        public static object FromPropertyItem(PropertyItem propertyItem)
        {
            if (propertyItem == null)
            {
                return null;
            }

            object data = null;

            EngExifTag tag = EngExifTag.Unknown;
            Type dataType = null;
            if (Enum.IsDefined(typeof(EngExifTag), propertyItem.Id))
            {
                tag = (EngExifTag)propertyItem.Id;
                dataType = ExifDataTypeAttribute.GetDataType(tag);
            }

            EngExifType type = (EngExifType)propertyItem.Type;
            switch (type)
            {
                case EngExifType.Ascii:
                    {
                        // The value represents an array of chars terminated with null ('\0') char
                        data = Encoding.ASCII.GetString(propertyItem.Value).TrimEnd('\0');
                        break;
                    }

                case EngExifType.Byte:
                    {
                        switch (tag)
                        {
                            case EngExifTag.MSTitle:
                            case EngExifTag.MSSubject:
                            case EngExifTag.MSAuthor:
                            case EngExifTag.MSKeywords:
                            case EngExifTag.MSComments:
                                {
                                    // The value represents an array of unicode bytes terminated with null ('\0') char
                                    data = Encoding.Unicode.GetString(propertyItem.Value).TrimEnd('\0');
                                    break;
                                }
                            default:
                                {
                                    // The value represents an array of bytes
                                    data = propertyItem.Value;
                                    break;
                                }
                        }
                        break;
                    }

                case EngExifType.Raw:
                    {
                        // The value represents an array of bytes
                        data = propertyItem.Value;
                        break;
                    }

                case EngExifType.UInt16:
                    {
                        // The value represents an array of unsigned 16-bit integers.
                        int count = propertyItem.Len / UInt16Size;

                        ushort[] result = new ushort[count];
                        for (int i = 0; i < count; i++)
                        {
                            result[i] = ReadUInt16(propertyItem.Value, i * UInt16Size);
                        }
                        data = result;
                        break;
                    }

                case EngExifType.Int32:
                    {
                        // The value represents an array of signed 32-bit integers.
                        int count = propertyItem.Len / Int32Size;

                        int[] result = new int[count];
                        for (int i = 0; i < count; i++)
                        {
                            result[i] = ReadInt32(propertyItem.Value, i * Int32Size);
                        }
                        data = result;
                        break;
                    }

                case EngExifType.UInt32:
                    {
                        // The value represents an array of unsigned 32-bit integers.
                        int count = propertyItem.Len / UInt32Size;

                        uint[] result = new uint[count];
                        for (int i = 0; i < count; i++)
                        {
                            result[i] = ReadUInt32(propertyItem.Value, i * UInt32Size);
                        }
                        data = result;
                        break;
                    }

                case EngExifType.Rational:
                    {
                        // The value represents an array of signed rational numbers
                        // Numerator is an Int32 value, denominator a UInt32 value.
                        int count = propertyItem.Len / RationalSize;

                        Rational<int>[] result = new Rational<int>[count];
                        for (int i = 0; i < count; i++)
                        {
                            result[i] = new Rational<int>(
                                ReadInt32(propertyItem.Value, i * RationalSize),
                                ReadInt32(propertyItem.Value, i * RationalSize + Int32Size));
                        }
                        data = result;
                        break;
                    }

                case EngExifType.URational:
                    {
                        // The value represents an array of signed rational numbers
                        // Numerator and denominator are UInt32 values.
                        int count = propertyItem.Len / URationalSize;

                        Rational<uint>[] result = new Rational<uint>[count];
                        for (int i = 0; i < count; i++)
                        {
                            result[i] = new Rational<uint>(
                                ReadUInt32(propertyItem.Value, i * URationalSize),
                                ReadUInt32(propertyItem.Value, i * URationalSize + UInt32Size));
                        }
                        data = result;
                        break;
                    }
                default:
                    {
                        data = propertyItem.Value;
                        break;
                    }
            }

            return EngExifDecoder.ConvertData(dataType, data);
        }

        #endregion Static Methods

        #region Byte Decoding

        /// <summary>
        /// 
        /// </summary>
        /// <param name="buffer"></param>
        /// <param name="offset"></param>
        /// <returns></returns>
        private static ushort ReadUInt16(byte[] buffer, int offset)
        {
            return BitConverter.ToUInt16(buffer, offset);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="buffer"></param>
        /// <param name="offset"></param>
        /// <returns></returns>
        private static int ReadInt32(byte[] buffer, int offset)
        {
            return BitConverter.ToInt32(buffer, offset);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="buffer"></param>
        /// <param name="offset"></param>
        /// <returns></returns>
        private static uint ReadUInt32(byte[] buffer, int offset)
        {
            return BitConverter.ToUInt32(buffer, offset);
        }

        #endregion Byte Decoding

        #region Data Conversion

        private static object ConvertData(Type targetType, object value)
        {
            if (value is Array)
            {
                int length = ((Array)value).Length;
                if (length < 1)
                {
                    value = null;
                }
                else if (length == 1)
                {
                    value = ((Array)value).GetValue(0);
                }
            }

            if (value is String)
            {
                value = ((String)value).TrimEnd('\0').Trim();
            }

            if (targetType == null || value == null)
            {
                return value;
            }

            if (targetType == typeof(DateTime) && value is String)
            {
                DateTime dateTime;
                if (DateTime.TryParseExact((string)value, EngExifDecoder.ExifDateTimeFormats,
                    DateTimeFormatInfo.InvariantInfo,
                    DateTimeStyles.AllowWhiteSpaces, out dateTime))
                {
                    return dateTime;
                }
            }

            if (targetType.IsEnum)
            {
                Type underlyingType = Enum.GetUnderlyingType(targetType);
                if (value.GetType() != underlyingType)
                {
                    value = Convert.ChangeType(value, underlyingType);
                }

                if (Enum.IsDefined(targetType, value) || FlagsAttribute.IsDefined(targetType, typeof(FlagsAttribute)))
                {
                    try
                    {
                        return Enum.ToObject(targetType, value);
                    }
                    catch { }
                }
            }

            if (targetType == typeof(UnicodeEncoding) && value is byte[])
            {
                byte[] bytes = (byte[])value;
                if (bytes.Length <= 1)
                {
                    return String.Empty;
                }

                return Encoding.Unicode.GetString(bytes, 0, bytes.Length - 1);
            }

            if (targetType == typeof(Bitmap) && value is byte[])
            {
                byte[] bytes = (byte[])value;
                if (bytes.Length < 1)
                {
                    return null;
                }

                using (MemoryStream stream = new MemoryStream(bytes))
                {
                    return Bitmap.FromStream(stream);
                }
            }

            return value;
        }

        #endregion Data Conversion
    }
}
