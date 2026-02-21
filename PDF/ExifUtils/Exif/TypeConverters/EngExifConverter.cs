using System;
using System.ComponentModel;
using System.Globalization;

namespace PDF.ExifUtils.Exif.TypeConverters
{
    internal class EngExifConverter : ExpandableObjectConverter
    {
        #region Methods

        public override object ConvertTo(ITypeDescriptorContext context, CultureInfo culture, object value, Type destinationType)
        {
            if (value is EngExifProperty && destinationType == typeof(string))
            {
                return ((EngExifProperty)value).DisplayValue;
            }

            return base.ConvertTo(context, culture, value, destinationType);
        }

        #endregion Methods
    }
}
