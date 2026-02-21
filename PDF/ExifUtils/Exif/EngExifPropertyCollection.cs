using System;
using System.ComponentModel;
using System.Collections;
using System.Collections.Generic;
using System.Drawing.Imaging;
using System.IO;
using PDF.ExifUtils.Exif.TypeConverters;

namespace PDF.ExifUtils.Exif
{
    /// <summary>
    /// Collection of ExifProperty items
    /// </summary>
    [Serializable]
    [TypeConverter(typeof(EngExifCollectionConverter))]
    public class EngExifPropertyCollection : ICollection<EngExifProperty>, ICollection
    {
        #region Fields

        private SortedDictionary<Int32, EngExifProperty> items = new SortedDictionary<int, EngExifProperty>();

        #endregion Fields

        #region Init

        /// <summary>
        /// Ctor
        /// </summary>
        public EngExifPropertyCollection()
        {
        }

        /// <summary>
        /// Creates a ExifPropertyCollection from a collection of ExifProperties.
        /// Also works as a copy constructor.
        /// </summary>
        /// <param name="properties"></param>
        public EngExifPropertyCollection(IEnumerable<EngExifProperty> properties)
        {
            if (properties == null)
            {
                return;
            }

            // add all the Exif properties
            foreach (EngExifProperty property in properties)
            {
                this.Add(property);
            }
        }

        /// <summary>
        /// Creates a ExifPropertyCollection from a collection of PropertyItems.
        /// </summary>
        /// <param name="propertyItems"></param>
        /// <param name="exifTags">filter of EXIF tags to include</param>
        public EngExifPropertyCollection(IEnumerable<PropertyItem> propertyItems, params EngExifTag[] exifTags)
            : this(propertyItems, (ICollection<EngExifTag>)exifTags)
        {
        }

        /// <summary>
        /// Creates a ExifPropertyCollection from a collection of PropertyItems only including explicitly named ExifTags.
        /// </summary>
        /// <param name="propertyItems"></param>
        /// <param name="exifTags">filter of EXIF tags to include</param>
        public EngExifPropertyCollection(IEnumerable<PropertyItem> propertyItems, ICollection<EngExifTag> exifTags)
        {
            if (propertyItems == null)
            {
                return;
            }

            // copy all the Exif properties
            foreach (PropertyItem property in propertyItems)
            {
                if (exifTags != null && exifTags.Count > 0 &&
                    (!Enum.IsDefined(typeof(EngExifTag), property.Id) || !exifTags.Contains((EngExifTag)property.Id)))
                {
                    // filter those not in the set
                    continue;
                }

                if (property.Value != null)
                {
                    this.Add(new EngExifProperty(property));
                }
            }
        }

        #endregion Init

        #region Properties

        /// <summary>
        /// 
        /// </summary>
        /// <param name="tagID"></param>
        /// <returns></returns>
        public EngExifProperty this[EngExifTag tagID]
        {
            get
            {
                if (!this.items.ContainsKey((int)tagID))
                {
                    EngExifProperty property = new EngExifProperty();
                    property.Tag = tagID;
                    property.Type = ExifDataTypeAttribute.GetExifType(tagID);
                    this.items[(int)tagID] = property;
                }
                return this.items[(int)tagID];
            }
            set { this.items[(int)tagID] = value; }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="index"></param>
        /// <returns></returns>
        /// <remarks>
        /// Warning: inefficient, used only for serialization
        /// </remarks>
        public EngExifProperty this[int index]
        {
            get
            {
                int[] keys = new int[this.items.Keys.Count];
                this.items.Keys.CopyTo(keys, 0);
                return this.items[keys[index]];
            }
            set { throw new NotSupportedException("This operation is not supported."); }
        }

        #endregion Properties

        #region Methods

        /// <summary>
        /// 
        /// </summary>
        /// <param name="tag"></param>
        /// <returns></returns>
        public bool Remove(EngExifTag tag)
        {
            if (!this.items.ContainsKey((int)tag))
                return false;

            this.items.Remove((int)tag);
            return true;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="tag"></param>
        /// <returns></returns>
        public bool Contains(EngExifTag tag)
        {
            return this.items.ContainsKey((int)tag);
        }

        #endregion Methods

        #region ICollection Members

        /// <summary>
        /// 
        /// </summary>
        /// <param name="array"></param>
        /// <param name="index"></param>
        public void CopyTo(Array array, int index)
        {
            ((ICollection)this.items).CopyTo(array, index);
        }

        /// <summary>
        /// 
        /// </summary>
        public int Count
        {
            get { return ((ICollection)this.items).Count; }
        }

        bool ICollection.IsSynchronized
        {
            get { return ((ICollection)this.items).IsSynchronized; }
        }

        object ICollection.SyncRoot
        {
            get { return ((ICollection)this.items).SyncRoot; }
        }

        #endregion ICollection Members

        #region IEnumerable Members

        IEnumerator IEnumerable.GetEnumerator()
        {
            return ((IEnumerable)this.items.Values).GetEnumerator();
        }

        #endregion IEnumerable Members

        #region ICollection<ExifProperty> Members

        /// <summary>
        /// 
        /// </summary>
        /// <param name="item"></param>
        public void Add(EngExifProperty item)
        {
            if (item == null)
                return;

            if (item.Value == null)
            {
                if (this.Contains(item.Tag))
                    this.Remove(item.Tag);
                return;
            }

            this.items[item.ID] = item;
        }

        /// <summary>
        /// 
        /// </summary>
        public void Clear()
        {
            this.items.Clear();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="item"></param>
        /// <returns></returns>
        public bool Contains(EngExifProperty item)
        {
            return this.items.ContainsValue(item);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="array"></param>
        /// <param name="index"></param>
        public void CopyTo(EngExifProperty[] array, int index)
        {
            this.items.Values.CopyTo(array, index);
        }

        /// <summary>
        /// 
        /// </summary>
        [EditorBrowsable(EditorBrowsableState.Never)]
        public bool IsReadOnly
        {
            get { return ((ICollection<KeyValuePair<int, EngExifProperty>>)this.items).IsReadOnly; }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="item"></param>
        /// <returns></returns>
        public bool Remove(EngExifProperty item)
        {
            if (!this.items.ContainsKey(item.ID))
                return false;

            this.items.Remove(item.ID);
            return true;
        }

        #endregion ICollection<ExifProperty> Members

        #region IEnumerable<ExifProperty> Members

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public IEnumerator<EngExifProperty> GetEnumerator()
        {
            return ((IEnumerable<EngExifProperty>)this.items.Values).GetEnumerator();
        }

        #endregion IEnumerable<ExifProperty> Members
    }
}
