using BAL.Repositories;
using StudioAdmin._Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;

namespace StudioAdmin.BLL._Services
{
    public class dashboardService : baseService
    {
        dashboardDB _service;
        public dashboardService()
        {
            _service = new dashboardDB();
        }

        public RspDashboard GetDashboard(int LocationID, string Date)
        {
            var rsp = new RspDashboard();
            try
            {
                rsp.summarysales = _service.GetDashboardSummary(LocationID, DateTime.Parse(Date));
            }
            catch (Exception)
            {
                rsp.summarysales = new DashboardSummary();
            }


            try
            {
                rsp.maensales = _service.GetMAENSummary(LocationID, DateTime.Parse(Date));
            }
            catch (Exception)
            {
                rsp.maensales = new DashboardMAEN();
            }


            try
            {
                rsp.todaysales = _service.GetTodaySales(LocationID, DateTime.Parse(Date));
            }
            catch (Exception)
            {
                rsp.todaysales = new DashboardToday();
            }


            return rsp;

        }

        public RspDashboard GetDashboardRange(int LocationID, string FDate, string TDate)
        {
            var rsp = new RspDashboard();
            try
            {
                rsp.summarysales = _service.GetDashboardSummaryRange(LocationID,DateTime.Parse(FDate), DateTime.Parse(TDate));
            }
            catch (Exception)
            {
                rsp.summarysales = new DashboardSummary();
            }


            try
            {
                rsp.maensales = _service.GetMAENSummaryRange(LocationID,DateTime.Parse(FDate));
            }
            catch (Exception)
            {
                rsp.maensales = new DashboardMAEN();
            }


            try
            {
                rsp.todaysales = _service.GetTodaySalesRange(LocationID,DateTime.Parse(FDate));
            }
            catch (Exception)
            {
                rsp.todaysales = new DashboardToday();
            }


            return rsp;

        }
    }
}
