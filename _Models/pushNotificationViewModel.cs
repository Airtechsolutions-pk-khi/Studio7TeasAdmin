using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace StudioAdmin._Models
{
    public class pushNotificationViewModel
    {
    }

    public class PushNotificationBLL
    {
        public string DeviceID { get; set; }
        public string Type { get; set; }
        public string Title { get; set; }
        public string Message { get; set; }
    }
    public class PushTokenBLL
    {
        public int TokenID { get; set; }
        public Nullable<int> CustomerID { get; set; }
        public string Token { get; set; }
        public Nullable<int> Device { get; set; }
        public Nullable<int> StatusID { get; set; }
    }

}
