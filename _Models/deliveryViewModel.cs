using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace StudioAdmin._Models
{
    public class deliveryViewModel
    {
    }

    public class DeliveryBLL
    {
        public int DeliveryAreaID { get; set; }
        
        public string Name { get; set; }
        public double Amount { get; set; }
        public int StatusID { get; set; }
        public string brands { get; set; }

    }
    public class BrandSettingsBLL
    {
        public int BrandID { get; set; }
        public string Brands { get; set; }
    }

}
