using System;

namespace SLK.Common
{
    public class ComponentAttribute : Attribute
    {
        #region Member Fields
        #endregion

        #region Constructors
        public ComponentAttribute()
        {
        }
        #endregion

        #region Properties

        public string Type { get; set; }
        public string PathPostfix { get; set; }
        public string ComptName { get; set; }
        public string NodeType { get; set; }
        public string PageType { get; set; } = "single";
        #endregion
    }
}