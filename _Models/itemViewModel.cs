using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace StudioAdmin._Models
{
    public class ItemViewModel
    {
    }
    public class ItemSettingsBLL
    {
        public int BrandID{ get; set; }
        public string Items { get; set; }

        public string ItemSettingTitle { get; set; }

        public bool? IsItemSetting { get; set; }
    }
    public class ItemBLL
    {
        public int ItemID { get; set; }
        public int CategoryID { get; set; }
        public Nullable<int> UnitID { get; set; }
        public string Name { get; set; }
        public string ArabicName { get; set; }
        public string Description { get; set; }
        public string CategoryName  { get; set; }
        public string Image { get; set; }
        public string Barcode { get; set; }
        public string SKU { get; set; }
        public int DisplayOrder { get; set; }
        public double Price { get; set; }
        public double Cost { get; set; }
        public string ItemType { get; set; }
        public string LastUpdatedBy { get; set; }
        public Nullable<System.DateTime> LastUpdatedDate { get; set; }
        public int StatusID { get; set; }
        public bool IsFeatured { get; set; }
        public bool? IsApplyDiscount { get; set; }
        public double Calories { get; set; }
        public string Modifiers { get; set; }
        public string Addons { get; set; }
    }

}
