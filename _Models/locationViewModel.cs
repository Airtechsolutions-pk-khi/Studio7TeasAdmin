using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace StudioAdmin._Models
{
    public class locationViewModel
    {
    }

    public class LocationBLL
    {
        public int LocationID { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string Address { get; set; }
        public string ContactNo { get; set; }
        public string Currency { get; set; }
        public string Email { get; set; }
        public int LicenseID { get; set; }
        public bool DeliveryServices { get; set; }
        public double? DeliveryCharges { get; set; }
        public double? Discounts { get; set; }
        public double? Tax{ get; set; }
        public string DeliveryTime { get; set; }
        public double? MinOrderAmount { get; set; }
        public string Longitude { get; set; }
        public string Latitude { get; set; }
        public string Passcode { get; set; }

        public string Opentime { get; set; }

        public string Closetime{ get; set; }
        public int IsPickupAllowed { get; set; }
        public int IsDeliveryAllowed { get; set; }
        public int IsDineInAllowed { get; set; }
        public string LastUpdatedBy { get; set; }
        public Nullable<System.DateTime> LastUpdatedDate { get; set; }
        public int StatusID { get; set; }
        public string ImageURL { get; set; }
        public Nullable<int> BrandID { get; set; }
    }

}
