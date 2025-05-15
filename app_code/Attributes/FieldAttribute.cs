using System;

namespace SLK.Common
{
    public class FieldAttribute : Attribute
    {
        #region Member Fields
        #endregion

        #region Constructors
        public FieldAttribute()
        {
        }
        #endregion

        #region Properties

        public bool Required { get; set; }
        public string Title { get; set; }
        public string ChildTitle { get; set; }
        public string Control { get; set; }
        public string PlaceHolder { get; set; }
        public string InputMask { get; set; }
        public string[] Options { get; set; }
        #endregion
    }
}