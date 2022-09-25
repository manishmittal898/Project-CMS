using CMS.Core.Attribute;
using System;
using System.Collections.Generic;
using System.Text;

namespace CMS.Core.FixedValue
{
    public class Enums
    {

        public enum LookupTypeEnum
        {
            [StringValue("GENDER")]
            GENDER,
            [StringValue("Product_Category")]
            Product_Category,
            [StringValue("Caption_Tag")]
            Caption_Tag,
            [StringValue("Product_Size")]
            Product_Size,
            [StringValue("CMS_Page")]
            CMS_Page
        }
    }
}
