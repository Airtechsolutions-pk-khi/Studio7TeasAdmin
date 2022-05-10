using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace StudioAdmin._Models
{
    public class offersViewModel
    {
    }

    public class OffersBLL
    {
        public int OfferID { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string FromDate { get; set; }
        public string ToDate { get; set; }
        public int BrandID { get; set; }
        public string Image { get; set; }
        public Nullable<int> ItemID { get; set; }
        public int StatusID { get; set; }
        public Nullable<System.DateTime> LastUpdatedDate { get; set; }
        public string LastUpdatedBy { get; set; }

    }

}
