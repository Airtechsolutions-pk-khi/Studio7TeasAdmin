using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace StudioAdmin._Models
{
    public class brandViewModel
    {
    }

    public class BrandBLL
    {
        public int BrandID { get; set; }
        public string Username { get; set; }
        public string Name { get; set; }
        public string Image { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public string CompanyURl { get; set; }
        public string Address { get; set; }
        public int StatusID { get; set; }
        public string Currency { get; set; }
        public long BusinessKey { get; set; }
        public string LastUpdateBy { get; set; }
        public Nullable<System.DateTime> LastUpdatedDate { get; set; }
    }

}
