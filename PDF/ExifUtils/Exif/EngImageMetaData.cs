using System;
using System.Drawing;
using PDF.ExifUtils.Exif.IO;
using PDF.ExifUtils.Exif.TagValues;

namespace PDF.ExifUtils.Exif
{
    /// <summary>
	/// A strongly-typed adapter for common EXIF properties
	/// </summary>
	public class EngImageMetaData
    {
        #region Constants

        private static readonly EngExifTag[] StandardTags =
        {
            EngExifTag.Aperture,
            EngExifTag.Artist,
            EngExifTag.ColorSpace,
            EngExifTag.CompressedImageHeight,
            EngExifTag.CompressedImageWidth,
            EngExifTag.Copyright,
            EngExifTag.DateTime,
            EngExifTag.DateTimeDigitized,
            EngExifTag.DateTimeOriginal,
            EngExifTag.ExposureBias,
            EngExifTag.ExposureMode,
            EngExifTag.ExposureProgram,
            EngExifTag.ExposureTime,
            EngExifTag.Flash,
            EngExifTag.FNumber,
            EngExifTag.FocalLength,
            EngExifTag.FocalLengthIn35mmFilm,
            EngExifTag.GpsAltitude,
            EngExifTag.GpsDestLatitude,
            EngExifTag.GpsDestLatitudeRef,
            EngExifTag.GpsDestLongitude,
            EngExifTag.GpsDestLongitudeRef,
            EngExifTag.GpsLatitude,
            EngExifTag.GpsLatitudeRef,
            EngExifTag.GpsLongitude,
            EngExifTag.GpsLongitudeRef,
            EngExifTag.ImageDescription,
            EngExifTag.ImageTitle,
            EngExifTag.ImageWidth,
            EngExifTag.ISOSpeed,
            EngExifTag.Make,
            EngExifTag.MeteringMode,
            EngExifTag.Model,
            EngExifTag.MSAuthor,
            EngExifTag.MSComments,
            EngExifTag.MSKeywords,
            EngExifTag.MSSubject,
            EngExifTag.MSTitle,
            EngExifTag.Orientation,
            EngExifTag.ShutterSpeed,
            EngExifTag.WhiteBalance
        };

        #endregion Constants

        #region Fields

        private decimal aperture;
        private string artist;
        private ExifTagColorSpace colorSpace;
        private string copyright;
        private DateTime dateTaken;
        private Rational<int> exposureBias;
        private ExifTagExposureMode exposureMode;
        private ExifTagExposureProgram exposureProgram;
        private ExifTagFlash flash;
        private decimal focalLength;
        private decimal gpsAltitude;
        private GpsCoordinate gpsLatitude;
        private GpsCoordinate gpsLongitude;
        private string imageDescription;
        private int imageHeight;
        private string imageTitle;
        private int imageWidth;
        private int isoSpeed;
        private ExifTagMeteringMode meteringMode;
        private string make;
        private string model;
        private string msAuthor;
        private string msComments;
        private string msKeywords;
        private string msSubject;
        private string msTitle;
        private ExifTagOrientation orientation;
        private Rational<uint> shutterSpeed;
        private ExifTagWhiteBalance whiteBalance;

        #endregion Fields

        #region Init

        /// <summary>
        /// Ctor
        /// </summary>
        /// <param name="image">image from which to populate properties</param>
        public EngImageMetaData(Bitmap image)
            : this(EngExifReader.GetExifData(image, EngImageMetaData.StandardTags))
        {
            if (image != null)
            {
                // override EXIF with actual values
                if (image.Height > 0)
                {
                    this.ImageHeight = image.Height;
                }

                if (image.Width > 0)
                {
                    this.ImageWidth = image.Width;
                }
            }
        }

        /// <summary>
        /// Ctor
        /// </summary>
        /// <param name="properties">EXIF properties from which to populate</param>
        public EngImageMetaData(EngExifPropertyCollection properties)
        {
            if (properties == null)
            {
                throw new ArgumentNullException("properties");
            }

            // References:
            // http://www.media.mit.edu/pia/Research/deepview/exif.html
            // http://en.wikipedia.org/wiki/APEX_system
            // http://en.wikipedia.org/wiki/Exposure_value

            object rawValue;

            #region Aperture

            rawValue = properties[EngExifTag.FNumber].Value;
            if (rawValue is IConvertible)
            {
                // f/x.x
                this.Aperture = Convert.ToDecimal(rawValue);
            }
            else
            {
                rawValue = properties[EngExifTag.Aperture].Value;
                if (rawValue is IConvertible)
                {
                    // f/x.x
                    this.Aperture = (decimal)Math.Pow(2.0, Convert.ToDouble(rawValue) / 2.0);
                }
            }

            #endregion Aperture

            this.Artist = Convert.ToString(properties[EngExifTag.Artist].Value);

            #region ColorSpace

            rawValue = properties[EngExifTag.ColorSpace].Value;
            if (rawValue is Enum)
            {
                this.ColorSpace = (ExifTagColorSpace)rawValue;
            }

            #endregion ColorSpace

            this.Copyright = Convert.ToString(properties[EngExifTag.Copyright].Value);

            #region DateTaken

            rawValue = properties[EngExifTag.DateTimeOriginal].Value;
            if (rawValue is DateTime)
            {
                this.DateTaken = (DateTime)rawValue;
            }
            else
            {
                rawValue = properties[EngExifTag.DateTimeDigitized].Value;
                if (rawValue is DateTime)
                {
                    this.DateTaken = (DateTime)rawValue;
                }
                else
                {
                    rawValue = properties[EngExifTag.DateTime].Value;
                    if (rawValue is DateTime)
                    {
                        this.DateTaken = (DateTime)rawValue;
                    }
                }
            }

            #endregion DateTaken

            #region ExposureBias

            rawValue = properties[EngExifTag.ExposureBias].Value;
            if (rawValue is Rational<int>)
            {
                this.ExposureBias = (Rational<int>)rawValue;
            }

            #endregion ExposureBias

            #region ExposureMode

            rawValue = properties[EngExifTag.ExposureMode].Value;
            if (rawValue is Enum)
            {
                this.ExposureMode = (ExifTagExposureMode)rawValue;
            }

            #endregion ExposureMode

            #region ExposureProgram

            rawValue = properties[EngExifTag.ExposureProgram].Value;
            if (rawValue is Enum)
            {
                this.ExposureProgram = (ExifTagExposureProgram)rawValue;
            }

            #endregion ExposureProgram

            #region Flash

            rawValue = properties[EngExifTag.Flash].Value;
            if (rawValue is Enum)
            {
                this.Flash = (ExifTagFlash)rawValue;
            }

            #endregion Flash

            #region FocalLength

            rawValue = properties[EngExifTag.FocalLength].Value;
            if (rawValue is IConvertible)
            {
                this.FocalLength = Convert.ToDecimal(rawValue);
            }
            else
            {
                rawValue = properties[EngExifTag.FocalLengthIn35mmFilm].Value;
                if (rawValue is IConvertible)
                {
                    this.FocalLength = Convert.ToDecimal(rawValue);
                }
            }

            #endregion FocalLength

            #region GpsAltitude

            rawValue = properties[EngExifTag.GpsAltitude].Value;
            if (rawValue is IConvertible)
            {
                this.GpsAltitude = Convert.ToDecimal(rawValue);
            }

            #endregion GpsAltitude

            string gpsDir;

            #region GpsLatitude

            gpsDir = Convert.ToString(properties[EngExifTag.GpsLatitudeRef].Value);
            rawValue = properties[EngExifTag.GpsLatitude].Value;
            if (!(rawValue is Array))
            {
                gpsDir = Convert.ToString(properties[EngExifTag.GpsDestLatitudeRef].Value);
                rawValue = properties[EngExifTag.GpsDestLatitude].Value;
            }
            if (rawValue is Array)
            {
                this.GpsLatitude = this.AsGps((Array)rawValue, gpsDir);
            }

            #endregion GpsLatitude

            #region GpsLongitude

            gpsDir = Convert.ToString(properties[EngExifTag.GpsLongitudeRef].Value);
            rawValue = properties[EngExifTag.GpsLongitude].Value;
            if (!(rawValue is Array))
            {
                gpsDir = Convert.ToString(properties[EngExifTag.GpsDestLongitudeRef].Value);
                rawValue = properties[EngExifTag.GpsDestLongitude].Value;
            }
            if (rawValue is Array)
            {
                this.GpsLongitude = this.AsGps((Array)rawValue, gpsDir);
            }

            #endregion GpsLongitude

            this.ImageDescription = Convert.ToString(properties[EngExifTag.ImageDescription].Value);

            #region ImageHeight

            rawValue = properties[EngExifTag.ImageHeight].Value;
            if (rawValue is IConvertible)
            {
                this.ImageHeight = Convert.ToInt32(rawValue);
            }
            else
            {
                rawValue = properties[EngExifTag.CompressedImageHeight].Value;
                if (rawValue is IConvertible)
                {
                    this.ImageHeight = Convert.ToInt32(rawValue);
                }
            }

            #endregion ImageHeight

            #region ImageWidth

            rawValue = properties[EngExifTag.ImageWidth].Value;
            if (rawValue is IConvertible)
            {
                this.ImageWidth = Convert.ToInt32(rawValue);
            }
            else
            {
                rawValue = properties[EngExifTag.CompressedImageWidth].Value;
                if (rawValue is IConvertible)
                {
                    this.ImageWidth = Convert.ToInt32(rawValue);
                }
            }

            #endregion ImageWidth

            this.ImageTitle = Convert.ToString(properties[EngExifTag.ImageTitle].Value);

            #region ISOSpeed

            rawValue = properties[EngExifTag.ISOSpeed].Value;
            if (rawValue is Array)
            {
                Array array = (Array)rawValue;
                if (array.Length > 0)
                {
                    rawValue = array.GetValue(0);
                }
            }
            if (rawValue is IConvertible)
            {
                this.ISOSpeed = Convert.ToInt32(rawValue);
            }

            #endregion ISOSpeed

            this.Make = Convert.ToString(properties[EngExifTag.Make].Value);
            this.Model = Convert.ToString(properties[EngExifTag.Model].Value);

            #region MeteringMode

            rawValue = properties[EngExifTag.MeteringMode].Value;
            if (rawValue is Enum)
            {
                this.MeteringMode = (ExifTagMeteringMode)rawValue;
            }

            #endregion MeteringMode

            this.MSAuthor = Convert.ToString(properties[EngExifTag.MSAuthor].Value);
            this.MSComments = Convert.ToString(properties[EngExifTag.MSComments].Value);
            this.MSKeywords = Convert.ToString(properties[EngExifTag.MSKeywords].Value);
            this.MSSubject = Convert.ToString(properties[EngExifTag.MSSubject].Value);
            this.MSTitle = Convert.ToString(properties[EngExifTag.MSTitle].Value);

            #region Orientation

            rawValue = properties[EngExifTag.Orientation].Value;
            if (rawValue is Enum)
            {
                this.Orientation = (ExifTagOrientation)rawValue;
            }

            #endregion Orientation

            #region ShutterSpeed

            rawValue = properties[EngExifTag.ExposureTime].Value;
            if (rawValue is Rational<uint>)
            {
                this.ShutterSpeed = (Rational<uint>)rawValue;
            }
            else
            {
                rawValue = properties[EngExifTag.ShutterSpeed].Value;
                if (rawValue is Rational<int>)
                {
                    this.ShutterSpeed = Rational<uint>.Approximate((decimal)Math.Pow(2.0, -Convert.ToDouble(rawValue)));
                }
            }

            #endregion ShutterSpeed

            #region WhiteBalance

            rawValue = properties[EngExifTag.WhiteBalance].Value;
            if (rawValue is Enum)
            {
                this.WhiteBalance = (ExifTagWhiteBalance)rawValue;
            }

            #endregion WhiteBalance
        }

        private GpsCoordinate AsGps(Array array, string gpsDir)
        {
            if (array.Length != 3)
            {
                return null;
            }

            GpsCoordinate gps = new GpsCoordinate();

            if (array.GetValue(0) is Rational<uint>)
            {
                gps.Degrees = (Rational<uint>)array.GetValue(0);
            }
            else
            {
                gps.SetDegrees(Convert.ToDecimal(array.GetValue(0)));
            }

            if (array.GetValue(1) is Rational<uint>)
            {
                gps.Minutes = (Rational<uint>)array.GetValue(1);
            }
            else
            {
                gps.SetMinutes(Convert.ToDecimal(array.GetValue(1)));
            }

            if (array.GetValue(2) is Rational<uint>)
            {
                gps.Seconds = (Rational<uint>)array.GetValue(2);
            }
            else
            {
                gps.SetSeconds(Convert.ToDecimal(array.GetValue(2)));
            }

            try
            {
                gps.Direction = gpsDir;
            }
            catch { }

            return gps;
        }

        #endregion Init

        #region Properties

        /// <summary>
        /// Gets and sets the aperture
        /// </summary>
        public decimal Aperture
        {
            get { return this.aperture; }
            set { this.aperture = value; }
        }

        /// <summary>
        /// Gets and sets the artist
        /// </summary>
        public string Artist
        {
            get { return this.artist; }
            set { this.artist = value; }
        }

        /// <summary>
        /// Gets and sets the color space
        /// </summary>
        public ExifTagColorSpace ColorSpace
        {
            get { return this.colorSpace; }
            set { this.colorSpace = value; }
        }

        /// <summary>
        /// Gets and sets the copyright
        /// </summary>
        public string Copyright
        {
            get { return this.copyright; }
            set { this.copyright = value; }
        }

        /// <summary>
        /// Gets and sets the date the photo was taken
        /// </summary>
        public DateTime DateTaken
        {
            get { return this.dateTaken; }
            set { this.dateTaken = value; }
        }

        /// <summary>
        /// Gets and sets the exposure bias
        /// </summary>
        public Rational<int> ExposureBias
        {
            get { return this.exposureBias; }
            set { this.exposureBias = value; }
        }

        /// <summary>
        /// Gets and sets the exposure mode
        /// </summary>
        public ExifTagExposureMode ExposureMode
        {
            get { return this.exposureMode; }
            set { this.exposureMode = value; }
        }

        /// <summary>
        /// Gets and sets the exposure program
        /// </summary>
        public ExifTagExposureProgram ExposureProgram
        {
            get { return this.exposureProgram; }
            set { this.exposureProgram = value; }
        }

        /// <summary>
        /// Gets and sets the flash
        /// </summary>
        public ExifTagFlash Flash
        {
            get { return this.flash; }
            set { this.flash = value; }
        }

        /// <summary>
        /// Gets and sets the focal length
        /// </summary>
        public decimal FocalLength
        {
            get { return this.focalLength; }
            set { this.focalLength = value; }
        }

        /// <summary>
        /// Gets and sets the GPS altitude
        /// </summary>
        public decimal GpsAltitude
        {
            get { return this.gpsAltitude; }
            set { this.gpsAltitude = value; }
        }

        /// <summary>
        /// Gets and sets the GPS latitude
        /// </summary>
        public GpsCoordinate GpsLatitude
        {
            get { return this.gpsLatitude; }
            set { this.gpsLatitude = value; }
        }

        /// <summary>
        /// Gets and sets the GPS longitude
        /// </summary>
        public GpsCoordinate GpsLongitude
        {
            get { return this.gpsLongitude; }
            set { this.gpsLongitude = value; }
        }

        /// <summary>
        /// Gets and sets the image description
        /// </summary>
        public string ImageDescription
        {
            get { return this.imageDescription; }
            set { this.imageDescription = value; }
        }

        /// <summary>
        /// Gets and sets the image height
        /// </summary>
        public int ImageHeight
        {
            get { return this.imageHeight; }
            set { this.imageHeight = value; }
        }

        /// <summary>
        /// Gets and sets the image title
        /// </summary>
        public string ImageTitle
        {
            get { return this.imageTitle; }
            set { this.imageTitle = value; }
        }

        /// <summary>
        /// Gets and sets the image width
        /// </summary>
        public int ImageWidth
        {
            get { return this.imageWidth; }
            set { this.imageWidth = value; }
        }

        /// <summary>
        /// Gets and sets the ISO speed
        /// </summary>
        public int ISOSpeed
        {
            get { return this.isoSpeed; }
            set { this.isoSpeed = value; }
        }

        /// <summary>
        /// Gets and sets the metering mode
        /// </summary>
        public ExifTagMeteringMode MeteringMode
        {
            get { return this.meteringMode; }
            set { this.meteringMode = value; }
        }

        /// <summary>
        /// Gets and sets the camera make
        /// </summary>
        public string Make
        {
            get { return this.make; }
            set { this.make = value; }
        }

        /// <summary>
        /// Gets and sets the camera model
        /// </summary>
        public string Model
        {
            get { return this.model; }
            set { this.model = value; }
        }

        /// <summary>
        /// Gets and sets the author
        /// </summary>
        public string MSAuthor
        {
            get { return this.msAuthor; }
            set { this.msAuthor = value; }
        }

        /// <summary>
        /// Gets and sets comments
        /// </summary>
        public string MSComments
        {
            get { return this.msComments; }
            set { this.msComments = value; }
        }

        /// <summary>
        /// Gets and sets keywords
        /// </summary>
        public string MSKeywords
        {
            get { return this.msKeywords; }
            set { this.msKeywords = value; }
        }

        /// <summary>
        /// Gets and sets the subject
        /// </summary>
        public string MSSubject
        {
            get { return this.msSubject; }
            set { this.msSubject = value; }
        }

        /// <summary>
        /// Gets and sets the title
        /// </summary>
        public string MSTitle
        {
            get { return this.msTitle; }
            set { this.msTitle = value; }
        }

        /// <summary>
        /// Gets and sets the orientation
        /// </summary>
        public ExifTagOrientation Orientation
        {
            get { return this.orientation; }
            set { this.orientation = value; }
        }

        /// <summary>
        /// Gets and sets the shutter speed in seconds
        /// </summary>
        public Rational<uint> ShutterSpeed
        {
            get { return this.shutterSpeed; }
            set { this.shutterSpeed = value; }
        }

        /// <summary>
        /// Gets and sets the white balance
        /// </summary>
        public ExifTagWhiteBalance WhiteBalance
        {
            get { return this.whiteBalance; }
            set { this.whiteBalance = value; }
        }

        #endregion Properties
    }
}
