using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace StudioAdmin._Models
{
    public class aboutViewModel
    {
    }

    public class AboutBLL
    {
        public int? AppInfoID { get; set; }
        public int ID { get; set; }
        public string BranchName { get; set; }
        public string BranchAddress { get; set; }
        public string BranchTiming { get; set; }
        public int? Discount { get; set; }
        public string DeliveryNo { get; set; }
        public string Discountdescription { get; set; }        
        public string AppDescription { get; set; }
        public int? StatusID { get; set; }
        public Nullable<int> BrandID { get; set; }
        public string Facebook { get; set; }
        public string Twitter { get; set; }

        public string Instagram { get; set; }

        public string WhatsappNo { get; set; }

    }

}
