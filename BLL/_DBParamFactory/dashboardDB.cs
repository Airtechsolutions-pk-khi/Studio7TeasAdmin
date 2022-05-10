

using StudioAdmin._Models;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.SqlClient;
using System.Linq;
using System.Runtime.ExceptionServices;
using System.Text;
using WebAPICode.Helpers;

namespace BAL.Repositories
{

    public class dashboardDB : baseDB
    {
        public static DataTable _dt;
        public static DataSet _ds;
        public dashboardDB()
           : base()
        {
            _dt = new DataTable();
            _ds = new DataSet();
        }

        public DashboardSummary GetDashboardSummary(int LocationID, DateTime Date)
        {
            var obj = new DashboardSummary();

            try
            {
                SqlParameter[] p = new SqlParameter[2];
                p[0] = new SqlParameter("@LocationID", LocationID);
                p[1] = new SqlParameter("@BusinessDate", Date);

                _dt = (new DBHelper().GetTableFromSP)("sp_SalesDashboard_admin", p);

                obj.TotalOrders = Convert.ToDouble(_dt.Rows[0]["TotalOrders"].ToString());
                obj.TotalTax = Convert.ToDouble(_dt.Rows[0]["TotalTax"].ToString());
                obj.NetSales = Convert.ToDouble(_dt.Rows[0]["NetSales"].ToString());
                obj.Sales = Convert.ToDouble(_dt.Rows[0]["Sales"].ToString());
                return obj;
            }
            catch (Exception ex)
            {
                obj.NetSales = 0;
                obj.TotalOrders = 0;
                obj.TotalTax = 0;
                obj.Sales = 0;
                return obj;
            }
        }

        public DashboardMAEN GetMAENSummary(int LocationID, DateTime Date)
        {
            var obj = new DashboardMAEN();

            try
            {
                SqlParameter[] p = new SqlParameter[2];
                p[0] = new SqlParameter("@LocationID", LocationID);
                p[1] = new SqlParameter("@BusinessDate", Date);

                _dt = (new DBHelper().GetTableFromSP)("sp_SalesMAEN", p);

                obj.Morning = Convert.ToDouble(_dt.Rows[0]["Sales"].ToString());
                obj.AfterNoon = Convert.ToDouble(_dt.Rows[1]["Sales"].ToString());
                obj.Evening = Convert.ToDouble(_dt.Rows[2]["Sales"].ToString());
                obj.Night = Convert.ToDouble(_dt.Rows[3]["Sales"].ToString());
                return obj;
            }
            catch (Exception ex)
            {
                obj.Morning = 0;
                obj.AfterNoon = 0;
                obj.Evening = 0;
                obj.Night = 0;
                return obj;
            }
        }
        public DashboardToday GetTodaySales(int LocationID, DateTime Date)
        {
            var rsp = new DashboardToday();
            var lstS = new List<string>();
            var lstTS = new List<string>();

            try
            {
                SqlParameter[] p = new SqlParameter[2];
                p[0] = new SqlParameter("@LocationID", LocationID);
                p[1] = new SqlParameter("@BusinessDate", Date);

                _dt = (new DBHelper().GetTableFromSP)("sp_SaleaToday_admin", p);

                for (int i = 0; i < _dt.Rows.Count; i++)
                {
                    lstS.Add(_dt.Rows[i]["Sales"].ToString());
                }
                for (int i = 0; i < _dt.Rows.Count; i++)
                {
                    lstTS.Add(_dt.Rows[i]["TimeSlot"].ToString());
                }
                rsp.Sales = lstS;
                rsp.TimeSlot = lstTS;
            }
            catch (Exception ex)
            {
                rsp.Sales = new List<string>();
                rsp.TimeSlot = new List<string>();
            }

            return rsp;
        }


        #region dashboard range
        public DashboardSummary GetDashboardSummaryRange(int LocationID, DateTime FDate, DateTime TDate)
        {
            var obj = new DashboardSummary();

            try
            {
                SqlParameter[] p = new SqlParameter[3];
                p[0] = new SqlParameter("@LocationID", LocationID);
                p[1] = new SqlParameter("@FromBusinessDate", FDate);
                p[2] = new SqlParameter("@ToBusinessDate", TDate);

                _dt = (new DBHelper().GetTableFromSP)("sp_SalesDashboardRange_admin", p);

                obj.TotalOrders = Convert.ToDouble(_dt.Rows[0]["TotalOrders"].ToString());
                obj.TotalTax = Convert.ToDouble(_dt.Rows[0]["TotalTax"].ToString());
                obj.NetSales = Convert.ToDouble(_dt.Rows[0]["NetSales"].ToString());
                obj.Sales = Convert.ToDouble(_dt.Rows[0]["Sales"].ToString());
                return obj;
            }
            catch (Exception ex)
            {
                obj.NetSales = 0;
                obj.TotalOrders = 0;
                obj.TotalTax = 0;
                obj.Sales = 0;
                return obj;
            }
        }

        public DashboardMAEN GetMAENSummaryRange(int LocationID, DateTime Date)
        {
            var obj = new DashboardMAEN();

            try
            {
                SqlParameter[] p = new SqlParameter[2];
                p[0] = new SqlParameter("@LocationID", LocationID);
                p[1] = new SqlParameter("@BusinessDate", Date);
                _dt = (new DBHelper().GetTableFromSP)("sp_SalesMAEN", p);

                obj.Morning = Convert.ToDouble(_dt.Rows[0]["Sales"].ToString());
                obj.AfterNoon = Convert.ToDouble(_dt.Rows[1]["Sales"].ToString());
                obj.Evening = Convert.ToDouble(_dt.Rows[2]["Sales"].ToString());
                obj.Night = Convert.ToDouble(_dt.Rows[3]["Sales"].ToString());
                return obj;
            }
            catch (Exception ex)
            {
                obj.Morning = 0;
                obj.AfterNoon = 0;
                obj.Evening = 0;
                obj.Night = 0;
                return obj;
            }
        }
        public DashboardToday GetTodaySalesRange(int LocationID, DateTime Date)
        {
            var rsp = new DashboardToday();
            var lstS = new List<string>();
            var lstTS = new List<string>();

            try
            {
                SqlParameter[] p = new SqlParameter[1];
                p[0] = new SqlParameter("@LocationID", LocationID);
                p[0] = new SqlParameter("@BusinessDate", Date);
                _dt = (new DBHelper().GetTableFromSP)("sp_SaleaToday_admin", p);

                for (int i = 0; i < _dt.Rows.Count; i++)
                {
                    lstS.Add(_dt.Rows[i]["Sales"].ToString());
                }
                for (int i = 0; i < _dt.Rows.Count; i++)
                {
                    lstTS.Add(_dt.Rows[i]["TimeSlot"].ToString());
                }
                rsp.Sales = lstS;
                rsp.TimeSlot = lstTS;
            }
            catch (Exception ex)
            {
                rsp.Sales = new List<string>();
                rsp.TimeSlot = new List<string>();
            }

            return rsp;
        }
        #endregion
    }
}
