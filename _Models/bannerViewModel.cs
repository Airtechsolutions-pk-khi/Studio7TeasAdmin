using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace StudioAdmin._Models
{
    public class bannerViewModel
    {
    }

    public class BannerBLL
    {
        public int BannerID { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        
        public string Image { get; set; }
        public int StatusID { get; set; }
        public string LastUpdatedBy { get; set; }
        public Nullable<System.DateTime> LastUpdatedDate { get; set; }
        public Nullable<int> BrandID { get; set; }
    }

}
