using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace StudioAdmin._Models
{
    public class dashboardViewModel
    {
    }
    public class RspDashboard
    {
        public DashboardSummary summarysales { get; set; }
        public DashboardToday todaysales{ get; set; }
        public DashboardMAEN maensales { get; set; }
    }
    public class DashboardSummary
    {
        public double TotalOrders { get; set; }
        public double TotalTax { get; set; }
        public double NetSales { get; set; }
        public double Sales { get; set; }
    }
    public class DashboardToday
    {
        public List<string> Sales { get; set; }
        public List<string> TimeSlot { get; set; }
    }
    public class DashboardMAEN
    {
        public double Morning { get; set; }
        public double Evening { get; set; }
        public double AfterNoon { get; set; }
        public double Night { get; set; }
    }
}
