using System;
using System.Text;
using System.ComponentModel;
using System.Globalization;
using System.Xml.Serialization;
using System.Drawing;
using System.Drawing.Imaging;
using PDF.ExifUtils.Exif.IO;
using PDF.ExifUtils.Exif.TagValues;
using PDF.ExifUtils.Exif.TypeConverters;


namespace PDF.ExifUtils.Exif
{
    /// <summary>
    /// Represents a single EXIF property.
    /// </summary>
    /// <remarks>
    /// Should try to serialize as EXIF+RDF http://www.w3.org/2003/12/exif/
    /// </remarks>
    [Serializable]
    [TypeConverter(typeof(EngExifConverter))]
    public class EngExifProperty
    {
        #region Fields

        private int id = (int)EngExifTag.Unknown;
        private EngExifType type = EngExifType.Unknown;
        private object value = null;

        #endregion Fields

        #region Init

        /// <summary>
        /// Ctor
        /// </summary>
        public EngExifProperty()
        {
        }

        /// <summary>
        /// Ctor
        /// </summary>
        public EngExifProperty(EngExifTag tag, object value)
        {
            this.Tag = tag;
            this.value = value;
        }

        /// <summary>
        /// Ctor
        /// </summary>
        /// <param name="property"></param>
        public EngExifProperty(PropertyItem property)
        {
            this.id = property.Id;
            this.type = (EngExifType)property.Type;
            this.value = EngExifDecoder.FromPropertyItem(property);
        }

        #endregion Init

        #region Properties

        /// <summary>
        /// Gets and sets the Property ID according to the Exif specification for DCF images.
        /// </summary>
        [Category("Key")]
        [DisplayName("Exif ID")]
        [Description("The Property ID according to the Exif specification for DCF images.")]
        [XmlAttribute("ExifID"), DefaultValue((int)EngExifTag.Unknown)]
        public int ID
        {
            get { return this.id; }
            set { this.id = value; }
        }

        /// <summary>
        /// Gets and sets the property name according to the Exif specification for DCF images.
        /// </summary>
        /// <remarks>
        /// Note: If the ExifTag value specifies the ExifType then ExifProperty.Type is automatically set.
        /// If the ExifTag does not specify the ExifType then ExifProperty.Type is left unchanged.
        /// </remarks>
        [Category("Key")]
        [DisplayName("Exif Tag")]
        [Description("The property name according to the Exif specification for DCF images.")]
        [XmlAttribute("ExifTag"), DefaultValue(EngExifTag.Unknown)]
        public EngExifTag Tag
        {
            get
            {
                if (Enum.IsDefined(typeof(EngExifTag), this.id))
                    return (EngExifTag)this.id;
                else
                    return EngExifTag.Unknown;
            }
            set
            {
                if (value != EngExifTag.Unknown)
                {
                    this.id = (int)value;
                    EngExifType exifType = ExifDataTypeAttribute.GetExifType(value);
                    if (exifType != EngExifType.Unknown)
                    {
                        this.Type = exifType;
                    }
                }
            }
        }

        /// <summary>
        /// Gets and sets the EXIF data type.
        /// </summary>
        [Category("Value")]
        [Browsable(false)]
        [XmlAttribute("ExifType"), DefaultValue(EngExifType.Unknown)]
        public EngExifType Type
        {
            get { return this.type; }
            set { this.type = value; }
        }

        /// <summary>
        /// Gets and sets the EXIF value.
        /// </summary>
        [XmlElement(typeof(Byte))]
        [XmlElement(typeof(Byte[]))]
        [XmlElement(typeof(UInt16))]
        [XmlElement(typeof(UInt16[]))]
        [XmlElement(typeof(UInt32))]
        [XmlElement(typeof(UInt32[]))]
        [XmlElement(typeof(Int32))]
        [XmlElement(typeof(Int32[]))]
        [XmlElement(typeof(String))]
        [XmlElement(typeof(String[]))]
        [XmlElement(typeof(Rational<int>))]
        [XmlElement(typeof(Rational<int>[]))]
        [XmlElement(typeof(Rational<uint>))]
        [XmlElement(typeof(Rational<uint>[]))]
        [XmlElement(typeof(DateTime))]
        [XmlElement(typeof(UnicodeEncoding))]
        [XmlElement(typeof(ExifTagColorSpace))]
        [XmlElement(typeof(ExifTagCleanFaxData))]
        [XmlElement(typeof(ExifTagCompression))]
        [XmlElement(typeof(ExifTagContrast))]
        [XmlElement(typeof(ExifTagCustomRendered))]
        [XmlElement(typeof(ExifTagExposureMode))]
        [XmlElement(typeof(ExifTagExposureProgram))]
        [XmlElement(typeof(ExifTagFileSource))]
        [XmlElement(typeof(ExifTagFillOrder))]
        [XmlElement(typeof(ExifTagFlash))]
        [XmlElement(typeof(ExifTagGainControl))]
        [XmlElement(typeof(ExifTagGpsAltitudeRef))]
        [XmlElement(typeof(ExifTagGpsDifferential))]
        [XmlElement(typeof(ExifTagInkSet))]
        [XmlElement(typeof(ExifTagJPEGProc))]
        [XmlElement(typeof(ExifTagLightSource))]
        [XmlElement(typeof(ExifTagMeteringMode))]
        [XmlElement(typeof(ExifTagOrientation))]
        [XmlElement(typeof(ExifTagPhotometricInterpretation))]
        [XmlElement(typeof(ExifTagPlanarConfiguration))]
        [XmlElement(typeof(ExifTagPredictor))]
        [XmlElement(typeof(ExifTagResolutionUnit))]
        [XmlElement(typeof(ExifTagSampleFormat))]
        [XmlElement(typeof(ExifTagSaturation))]
        [XmlElement(typeof(ExifTagSceneCaptureType))]
        [XmlElement(typeof(ExifTagSceneType))]
        [XmlElement(typeof(ExifTagSensingMethod))]
        [XmlElement(typeof(ExifTagSharpness))]
        [XmlElement(typeof(ExifTagSubjectDistanceRange))]
        [XmlElement(typeof(ExifTagThreshholding))]
        [XmlElement(typeof(ExifTagWhiteBalance))]
        [XmlElement(typeof(ExifTagYCbCrPositioning))]
        [Category("Value")]
        public object Value
        {
            get { return this.value; }
            set { this.value = value; }
        }

        /// <summary>
        /// Gets the formatted text representation of the value.
        /// </summary>
        [DisplayName("Display Value")]
        [Category("Value")]
        public string DisplayValue
        {
            get { return this.FormatValue(); }
        }

        /// <summary>
        /// Gets the formatted text representation of the label.
        /// </summary>
        [DisplayName("Display Name")]
        [Category("Value")]
        public string DisplayName
        {
            get
            {
                if (this.Tag != EngExifTag.Unknown)
                {
                    string label = Utility.GetDescription(this.Tag);
                    if (String.IsNullOrEmpty(label))
                    {
                        label = this.Tag.ToString("g");
                    }
                    return label;
                }
                else
                {
                    return String.Format("Exif_0x{0:x4}", this.ID);
                }
            }
        }

        #endregion Properties

        #region Methods

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        /// <remarks>
        /// References:
        /// http://www.media.mit.edu/pia/Research/deepview/exif.html
        /// http://en.wikipedia.org/wiki/APEX_system
        /// http://en.wikipedia.org/wiki/Exposure_value
        /// </remarks>
        protected string FormatValue()
        {
            object rawValue = this.Value;
            switch (this.Tag)
            {
                case EngExifTag.ISOSpeed:
                    {
                        if (rawValue is Array)
                        {
                            Array array = (Array)rawValue;
                            if (array.Length < 1 || !(array.GetValue(0) is IConvertible))
                            {
                                goto default;
                            }
                            rawValue = array.GetValue(0);
                        }

                        if (!(rawValue is IConvertible))
                        {
                            goto default;
                        }

                        return String.Format("ISO-{0:###0}", Convert.ToDecimal(rawValue));
                    }
                case EngExifTag.Aperture:
                case EngExifTag.MaxAperture:
                    {
                        // The actual aperture value of lens when the image was taken.
                        // To convert this value to ordinary F-number(F-stop),
                        // calculate this value's power of root 2 (=1.4142).
                        // For example, if value is '5', F-number is 1.4142^5 = F5.6.
                        double fStop = Math.Pow(2.0, Convert.ToDouble(rawValue) / 2.0);
                        return String.Format("f/{0:#0.0}", fStop);
                    }
                case EngExifTag.FNumber:
                    {
                        // The actual F-number (F-stop) of lens when the image was taken.
                        return String.Format("f/{0:#0.0}", Convert.ToDecimal(rawValue));
                    }
                case EngExifTag.FocalLength:
                case EngExifTag.FocalLengthIn35mmFilm:
                    {
                        return String.Format("{0:#0.#} mm", Convert.ToDecimal(rawValue));
                    }
                case EngExifTag.ShutterSpeed:
                    {
                        if (!(rawValue is Rational<int>))
                        {
                            goto default;
                        }

                        // To convert this value to ordinary 'Shutter Speed';
                        // calculate this value's power of 2, then reciprocal.
                        // For example, if value is '4', shutter speed is 1/(2^4)=1/16 second.
                        Rational<int> shutter = (Rational<int>)rawValue;

                        if (shutter.Numerator > 0)
                        {
                            double speed = Math.Pow(2.0, Convert.ToDouble(shutter));
                            return String.Format("1/{0:####0} sec", speed);
                        }
                        else
                        {
                            double speed = Math.Pow(2.0, -Convert.ToDouble(shutter));
                            return String.Format("{0:####0.##} sec", speed);
                        }
                    }
                case EngExifTag.ExposureTime:
                    {
                        if (!(rawValue is Rational<uint>))
                        {
                            goto default;
                        }

                        // Exposure time (reciprocal of shutter speed). Unit is second.
                        Rational<uint> exposure = (Rational<uint>)rawValue;

                        if (exposure.Numerator < exposure.Denominator)
                        {
                            exposure.Reduce();
                            return String.Format("{0} sec", exposure);
                        }
                        else
                        {
                            return String.Format("{0:####0.##} sec", Convert.ToDecimal(rawValue));
                        }
                    }
                case EngExifTag.XResolution:
                case EngExifTag.YResolution:
                case EngExifTag.ThumbnailXResolution:
                case EngExifTag.ThumbnailYResolution:
                case EngExifTag.FocalPlaneXResolution:
                case EngExifTag.FocalPlaneYResolution:
                    {
                        return String.Format("{0:###0} dpi", Convert.ToDecimal(rawValue));
                    }
                case EngExifTag.ImageHeight:
                case EngExifTag.ImageWidth:
                case EngExifTag.CompressedImageHeight:
                case EngExifTag.CompressedImageWidth:
                case EngExifTag.ThumbnailHeight:
                case EngExifTag.ThumbnailWidth:
                    {
                        return String.Format("{0:###0} pixels", Convert.ToDecimal(rawValue));
                    }
                case EngExifTag.SubjectDistance:
                    {
                        return String.Format("{0:###0.#} m", Convert.ToDecimal(rawValue));
                    }
                case EngExifTag.ExposureBias:
                case EngExifTag.Brightness:
                    {
                        string val;
                        if (rawValue is Rational<int>)
                        {
                            val = ((Rational<int>)rawValue).Numerator != 0 ? rawValue.ToString() : "0";
                        }
                        else
                        {
                            val = Convert.ToDecimal(rawValue).ToString();
                        }

                        return String.Format("{0} EV", val);
                    }
                case EngExifTag.CompressedBitsPerPixel:
                    {
                        return String.Format("{0:###0.0} bits", Convert.ToDecimal(rawValue));
                    }
                case EngExifTag.DigitalZoomRatio:
                    {
                        return Convert.ToString(rawValue).Replace('/', ':');
                    }
                case EngExifTag.GpsLatitude:
                case EngExifTag.GpsLongitude:
                case EngExifTag.GpsDestLatitude:
                case EngExifTag.GpsDestLongitude:
                    {
                        if (!(rawValue is Array))
                        {
                            goto default;
                        }

                        Array array = (Array)rawValue;
                        if (array.Length < 1)
                        {
                            return String.Empty;
                        }
                        else if (array.Length != 3)
                        {
                            goto default;
                        }

                        // attempt to use the GPSCoordinate XMP formatting guidelines
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

                        return gps.ToString();
                    }
                case EngExifTag.GpsTimeStamp:
                    {
                        Array array = (Array)rawValue;
                        if (array.Length < 1)
                        {
                            return String.Empty;
                        }

                        string[] time = new string[array.Length];
                        for (int i = 0; i < array.Length; i++)
                        {
                            time[i] = Convert.ToDecimal(array.GetValue(i)).ToString();
                        }
                        return String.Join(":", time);
                    }
                default:
                    {
                        if (rawValue is Enum)
                        {
                            string description = Utility.GetDescription((Enum)rawValue);
                            if (!String.IsNullOrEmpty(description))
                            {
                                return description;
                            }
                        }
                        else if (rawValue is Array)
                        {
                            Array array = (Array)rawValue;
                            if (array.Length < 1)
                            {
                                return String.Empty;
                            }

                            Type type = array.GetValue(0).GetType();
                            if (!type.IsPrimitive || type == typeof(char) || type == typeof(float) || type == typeof(double))
                            {
                                return Convert.ToString(rawValue);
                            }

                            //const int ElemsPerRow = 40;
                            int charSize = 2 * System.Runtime.InteropServices.Marshal.SizeOf(type);
                            string format = "{0:X" + (charSize) + "}";
                            StringBuilder builder = new StringBuilder(((charSize + 1) * array.Length)/*+(2*array.Length/ElemsPerRow)*/);
                            for (int i = 0; i < array.Length; i++)
                            {
                                if (i > 0)
                                {
                                    //if ((i+1)%ElemsPerRow == 0)
                                    //{
                                    //    builder.AppendLine();
                                    //}
                                    //else
                                    {
                                        builder.Append(" ");
                                    }
                                }

                                builder.AppendFormat(format, array.GetValue(i));
                            }
                            return builder.ToString();
                        }

                        return Convert.ToString(rawValue);
                    }
            }
        }

        #endregion Methods

        #region Object Overrides

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            return String.Format("{0}: {1}", this.DisplayName, this.DisplayValue);
        }

        #endregion Object Overrides
    }
}
