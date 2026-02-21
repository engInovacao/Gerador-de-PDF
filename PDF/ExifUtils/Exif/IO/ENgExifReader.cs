using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;


namespace PDF.ExifUtils.Exif.IO
{
    /// <summary>
    /// Utility class for reading EXIF data 
    /// </summary>
    public static class EngExifReader
    {
        #region Methods

        /// <summary>
        /// Creates a ExifPropertyCollection from an image file path.
        /// Minimally loads image only enough to get PropertyItems.
        /// </summary>
        /// <param name="imagePath"></param>
        /// <param name="exifTags">filter of EXIF tags to include</param>
        /// <returns>Collection of ExifProperty items</returns>
        public static EngExifPropertyCollection GetExifData(string imagePath, params EngExifTag[] exifTags)
        {
            return EngExifReader.GetExifData(imagePath, (ICollection<EngExifTag>)exifTags);
        }

        /// <summary>
        /// Creates a ExifPropertyCollection from an image file path.
        /// Minimally loads image only enough to get PropertyItems.
        /// </summary>
        /// <param name="imagePath"></param>
        /// <param name="exifTags">collection of EXIF tags to include</param>
        /// <returns>Collection of ExifProperty items</returns>
        public static EngExifPropertyCollection GetExifData(string imagePath, ICollection<EngExifTag> exifTags)
        {
            PropertyItem[] propertyItems;

            // minimally load image
            Image image;
            using (EngExifReader.LoadImage(imagePath, out image))
            {
                using (image)
                {
                    propertyItems = image.PropertyItems;
                }
            }

            return new EngExifPropertyCollection(propertyItems, exifTags);
        }

        /// <summary>
        /// Creates a ExifPropertyCollection from the PropertyItems of a Bitmap.
        /// </summary>
        /// <param name="image"></param>
        /// <param name="exifTags">filter of EXIF tags to include</param>
        /// <returns></returns>
        public static EngExifPropertyCollection GetExifData(Image image, params EngExifTag[] exifTags)
        {
            return EngExifReader.GetExifData(image, (ICollection<EngExifTag>)exifTags);
        }

        /// <summary>
        /// Creates a ExifPropertyCollection from the PropertyItems of a Bitmap.
        /// </summary>
        /// <param name="image"></param>
        /// <param name="exifTags">filter of EXIF tags to include</param>
        /// <returns></returns>
        public static EngExifPropertyCollection GetExifData(Image image, ICollection<EngExifTag> exifTags)
        {
            if (image == null)
            {
                throw new NullReferenceException("image");
            }

            return new EngExifPropertyCollection(image.PropertyItems, exifTags);
        }

        #endregion Methods

        #region Utility Methods

        /// <summary>
        /// Minimally load image without verifying image data.
        /// </summary>
        /// <param name="imagePath"></param>
        /// <param name="image">the loaded image object</param>
        /// <returns>the stream object to dispose of when finished</returns>
        internal static IDisposable LoadImage(string imagePath, out Image image)
        {
            FileStream stream = new FileStream(imagePath, FileMode.Open, FileAccess.Read);
            image = Image.FromStream(stream, false, false);
            return stream;
        }

        #endregion Utility Methods
    }
}
