using System;
using System.ComponentModel;
using System.Globalization;

namespace PDF.ExifUtils.Exif.TypeConverters
{
    internal class EngExifCollectionConverter : CollectionConverter
    {
        #region Methods

        public override PropertyDescriptorCollection GetProperties(
            ITypeDescriptorContext context,
            object value,
            Attribute[] attributes)
        {
            PropertyDescriptor[] descriptors = null;
            EngExifPropertyCollection exifs = value as EngExifPropertyCollection;
            if (exifs != null)
            {
                descriptors = new PropertyDescriptor[exifs.Count];
                int i = 0;
                foreach (EngExifProperty exif in (((EngExifPropertyCollection)value)))
                {
                    descriptors[i++] = new EngExifCollectionConverter.ExifPropertyDescriptor(exif.Tag, exif.DisplayName);
                }
            }
            return new PropertyDescriptorCollection(descriptors);
        }

        public override bool GetPropertiesSupported(ITypeDescriptorContext context)
        {
            return true;
        }

        public override object ConvertTo(
            ITypeDescriptorContext context,
            CultureInfo culture,
            object value,
            Type destinationType)
        {
            if (value is EngExifPropertyCollection && destinationType == typeof(string))
            {
                return ((EngExifPropertyCollection)value).Count + " EXIF Properties";
            }

            return base.ConvertTo(context, culture, value, destinationType);
        }

        #endregion Methods

        #region Nested Types

        // Nested Types
        private class ExifPropertyDescriptor : System.ComponentModel.TypeConverter.SimplePropertyDescriptor
        {
            #region Fields

            private EngExifTag id;

            #endregion Fields

            #region Methods

            public ExifPropertyDescriptor(EngExifTag id, string label) :
                base(typeof(EngExifPropertyCollection), label, typeof(EngExifProperty))
            {
                this.id = id;
            }

            public override object GetValue(object instance)
            {
                if (instance is EngExifPropertyCollection)
                {
                    EngExifPropertyCollection exifs = (EngExifPropertyCollection)instance;
                    return exifs[this.id];
                }
                return null;
            }

            public override void SetValue(object instance, object value)
            {
                if (instance is EngExifPropertyCollection &&
                    value is EngExifProperty)
                {
                    EngExifPropertyCollection exifs = (EngExifPropertyCollection)instance;
                    exifs[this.id] = (EngExifProperty)value;
                    this.OnValueChanged(instance, EventArgs.Empty);
                }
            }

            #endregion Methods
        }

        #endregion Nested Types
    }
}
