using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace StudioAdmin._Models
{
    public class DeliveryBoyViewModel
    {
    }

    public class DeliveryBoyBLL
    {
        public int DeliveryBoyID { get; set; }
        public string DBName { get; set; }
        public string DBAddress { get; set; }
        public string DBContactNo { get; set; }
        public string DBVehicleNo { get; set; }
        public string DBCNICNo { get; set; }
        public DateTime CreatedOn { get; set; }
        public string CreatedBy { get; set; }
        public DateTime UpdatedOn { get; set; }
        public string UpdatedBy { get; set; }
        public int StatusID { get; set; }
        //public int DBAJuncID { get; set; }
        public int BrandID { get; set; }
        public string DBImage { get; set; }
        public int Amount { get; set; }

    }
    //public class DeliveryBoyBrandJunc
    //{
    //    public int DBAJuncID { get; set; }
    //    public int DeliveryBoyID { get; set; }
    //    public int BrandID { get; set; }
    //}

}
