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
            CMS_Page,
            [StringValue("Address_Type")]
            Address_Type,
            [StringValue("State")]
            State,
            [StringValue("Product_View_Section")]
            Product_View_Section,
        }

        public enum ContentTypeEnum
        {
            [StringValue("None")]
            None = 0,
            [StringValue("Single Photo")]
            Photo = 1,
            [StringValue("Multiple Images")]
            MultipleImages = 2,
            [StringValue("Video")]
            Video = 3,
            [StringValue("Documents")]
            Document = 4,


        }

        public enum RoleEnum
        {
            SuperAdmin=1,
            Admin=2,
            Customer=3
        }

        public enum PlatformEnum
        {
            [StringValue("Admin")]
            Admin,
            [StringValue("Customer")]
            Customer
        }
    }
}
