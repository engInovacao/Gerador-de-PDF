using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Reflection;
using System.Text;

namespace PDF.ExifUtils.Exif.IO
{
    /// <summary>
    /// Utility class for writing EXIF data
    /// </summary>
    public static class EngExifWriter
    {
        #region Fields

        private static ConstructorInfo PropertyItem_Ctor = null;

        #endregion Fields

        #region Write Methods

        /// <summary>
        /// Adds a collection of EXIF properties to an image.
        /// </summary>
        /// <param name="inputPath">file path of original image</param>
        /// <param name="outputPath">file path of modified image</param>
        /// <param name="properties"></param>
        public static void AddExifData(string inputPath, string outputPath, EngExifPropertyCollection properties)
        {
            // minimally load image
            Image image;
            using (EngExifReader.LoadImage(inputPath, out image))
            {
                using (image)
                {
                    EngExifWriter.AddExifData(image, properties);
                    image.Save(outputPath);
                }
            }
        }

        /// <summary>
        /// Adds an EXIF property to an image.
        /// </summary>
        /// <param name="inputPath">file path of original image</param>
        /// <param name="outputPath">file path of modified image</param>
        /// <param name="property"></param>
        public static void AddExifData(string inputPath, string outputPath, EngExifProperty property)
        {
            // minimally load image
            Image image;
            using (EngExifReader.LoadImage(inputPath, out image))
            {
                using (image)
                {
                    EngExifWriter.AddExifData(image, property);
                    image.Save(outputPath);
                }
            }
        }

        /// <summary>
        /// Adds a collection of EXIF properties to an image.
        /// </summary>
        /// <param name="image"></param>
        /// <param name="properties"></param>
        public static void AddExifData(Image image, EngExifPropertyCollection properties)
        {
            if (image == null)
            {
                throw new ArgumentNullException("image");
            }

            if (properties == null || properties.Count < 1)
            {
                return;
            }

            foreach (EngExifProperty property in properties)
            {
                EngExifWriter.AddExifData(image, property);
            }
        }

        /// <summary>
        /// Adds an EXIF property to an image.
        /// </summary>
        /// <param name="image"></param>
        /// <param name="property"></param>
        public static void AddExifData(Image image, EngExifProperty property)
        {
            if (image == null)
            {
                throw new ArgumentNullException("image");
            }
            if (property == null)
            {
                return;
            }

            PropertyItem propertyItem;

            // The .NET interface for GDI+ does not allow instantiation of the
            // PropertyItem class. Therefore one must be stolen off the Image
            // and repurposed.  GDI+ uses PropertyItem by value so there is no
            // side effect when changing the values and reassigning to the image.
            if (image.PropertyItems == null || image.PropertyItems.Length < 1)
            {
                propertyItem = EngExifWriter.CreatePropertyItem();
            }
            else
            {
                propertyItem = image.PropertyItems[0];
            }

            propertyItem.Id = (int)property.Tag;
            propertyItem.Type = (short)property.Type;

            Type dataType = ExifDataTypeAttribute.GetDataType(property.Tag);

            propertyItem.Value = EngExifEncoder.ConvertData(dataType, property.Type, property.Value);
            propertyItem.Len = propertyItem.Value.Length;

            // This appears to not be necessary
            EngExifWriter.RemoveExifData(image, property.Tag);
            image.SetPropertyItem(propertyItem);
        }

        /// <summary>
        /// Remvoes EXIF properties from an image.
        /// </summary>
        /// <param name="inputPath">file path of original image</param>
        /// <param name="outputPath">file path of modified image</param>
        /// <param name="exifTags">tags to remove</param>
        public static void RemoveExifData(string inputPath, string outputPath, params EngExifTag[] exifTags)
        {
            EngExifWriter.RemoveExifData(inputPath, outputPath, (IEnumerable<EngExifTag>)exifTags);
        }

        /// <summary>
        /// Remvoes EXIF properties from an image.
        /// </summary>
        /// <param name="inputPath">file path of original image</param>
        /// <param name="outputPath">file path of modified image</param>
        /// <param name="exifTags">tags to remove</param>
        public static void RemoveExifData(string inputPath, string outputPath, IEnumerable<EngExifTag> exifTags)
        {
            // minimally load image
            Image image;
            using (EngExifReader.LoadImage(inputPath, out image))
            {
                using (image)
                {
                    EngExifWriter.RemoveExifData(image, exifTags);
                    image.Save(outputPath);
                }
            }
        }

        /// <summary>
        /// Remvoes EXIF properties from an image.
        /// </summary>
        /// <param name="image"></param>
        /// <param name="exifTags">tags to remove</param>
        public static void RemoveExifData(Image image, params EngExifTag[] exifTags)
        {
            EngExifWriter.RemoveExifData(image, (IEnumerable<EngExifTag>)exifTags);
        }

        /// <summary>
        /// Remvoes EXIF properties from an image.
        /// </summary>
        /// <param name="image"></param>
        /// <param name="exifTags">tags to remove</param>
        public static void RemoveExifData(Image image, IEnumerable<EngExifTag> exifTags)
        {
            if (image == null)
            {
                throw new ArgumentNullException("image");
            }
            if (exifTags == null)
            {
                return;
            }

            foreach (EngExifTag tag in exifTags)
            {
                int propertyID = (int)tag;
                foreach (int id in image.PropertyIdList)
                {
                    if (id == propertyID)
                    {
                        image.RemovePropertyItem(propertyID);
                        break;
                    }
                }
            }
        }

        #endregion Write Methods

        #region Copy Methods

        /// <summary>
        /// Copies EXIF data from one image to another
        /// </summary>
        /// <param name="source"></param>
        /// <param name="dest"></param>
        public static void CloneExifData(Image source, Image dest)
        {
            EngExifWriter.CloneExifData(source, dest, -1);
        }

        /// <summary>
        /// Copies EXIF data from one image to another
        /// </summary>
        /// <param name="source"></param>
        /// <param name="dest"></param>
        /// <param name="maxPropertyBytes">setting to filter properties</param>
        public static void CloneExifData(Image source, Image dest, int maxPropertyBytes)
        {
            bool filter = (maxPropertyBytes > 0);

            // preserve EXIF
            foreach (PropertyItem prop in source.PropertyItems)
            {
                if (filter && prop.Len > maxPropertyBytes)
                {
                    // skip large sections
                    continue;
                }

                dest.SetPropertyItem(prop);
            }
        }

        #endregion Copy Methods

        #region Utility Methods

        /// <summary>
        /// Uses Reflection to instantiate a PropertyItem
        /// </summary>
        /// <returns></returns>
        internal static PropertyItem CreatePropertyItem()
        {
            if (EngExifWriter.PropertyItem_Ctor == null)
            {
                // Must use Reflection to get access to PropertyItem constructor
                EngExifWriter.PropertyItem_Ctor = typeof(PropertyItem).GetConstructor(BindingFlags.NonPublic | BindingFlags.Instance, null, Type.EmptyTypes, null);
                if (EngExifWriter.PropertyItem_Ctor == null)
                {
                    throw new NotSupportedException("Unable to instantiate a " + typeof(PropertyItem).FullName);
                }
            }

            return (PropertyItem)EngExifWriter.PropertyItem_Ctor.Invoke(null);
        }

        #endregion Utility Methods
    }
}
