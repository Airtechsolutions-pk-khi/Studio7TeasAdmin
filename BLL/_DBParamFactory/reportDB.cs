

using StudioAdmin._Models;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using WebAPICode.Helpers;

namespace BAL.Repositories
{

    public class reportDB : baseDB
    {
        public static DataTable _dt;
        public static DataSet _ds;
        public reportDB()
           : base()
        {
            _dt = new DataTable();
            _ds = new DataSet();
        }

        public List<salesSummarytBLL> GetSalesSummaryRpt(int brandID, DateTime FromDate, DateTime ToDate)
        {
            try
            {
                var lst = new List<salesSummarytBLL>();

                SqlParameter[] p = new SqlParameter[3];
                p[0] = new SqlParameter("@brandid", brandID);
                p[1] = new SqlParameter("@fromdate", FromDate);
                p[2] = new SqlParameter("@todate", ToDate);


                _dt = (new DBHelper().GetTableFromSP)("sp_rptSalesSummaryAdmin", p);
                if (_dt != null)
                {
                    if (_dt.Rows.Count > 0)
                    {
                        lst= JArray.Parse(Newtonsoft.Json.JsonConvert.SerializeObject(_dt)).ToObject<List<salesSummarytBLL>>();
                        //lst = _dt.DataTableToList<salesSummarytBLL>();
                    }
                }
                return lst;
            }
            catch (Exception ex)
            {
                return new List<salesSummarytBLL>();
            }
        }
        public List<SalesDetailBLL> GetSalesDetailRpt(int brandID, string locationID, DateTime FromDate, DateTime ToDate)
        {
            try
            {
                var lst = new List<SalesDetailBLL>();

                SqlParameter[] p = new SqlParameter[5];
                p[0] = new SqlParameter("@brandid", brandID);
                p[1] = new SqlParameter("@locationid", locationID);
                p[2] = new SqlParameter("@fromdate", FromDate);
                p[3] = new SqlParameter("@todate", ToDate);
                p[4] = new SqlParameter("@customerid", DBNull.Value);


                _dt = (new DBHelper().GetTableFromSP)("sp_rptSalesDetailsReport", p);
                if (_dt != null)
                {
                    if (_dt.Rows.Count > 0)
                    {
                        lst = JArray.Parse(Newtonsoft.Json.JsonConvert.SerializeObject(_dt)).ToObject<List<SalesDetailBLL>>();
                    }
                }
                return lst;
            }
            catch (Exception ex)
            {
                return new List<SalesDetailBLL>();
            }
        }
        public List<SalesItemwiseBLL> GetSalesItemwiseRpt(int brandID, string locationID, DateTime FromDate, DateTime ToDate)
        {
            try
            {
                var lst = new List<SalesItemwiseBLL>();

                SqlParameter[] p = new SqlParameter[4];
                p[0] = new SqlParameter("@brandid", brandID);
                p[1] = new SqlParameter("@locationid", locationID);
                p[2] = new SqlParameter("@fromdate", FromDate);
                p[3] = new SqlParameter("@todate", ToDate);


                _dt = (new DBHelper().GetTableFromSP)("sp_rptSalesItemwiseAdmin", p);
                if (_dt != null)
                {
                    if (_dt.Rows.Count > 0)
                    {
                        lst = JArray.Parse(Newtonsoft.Json.JsonConvert.SerializeObject(_dt)).ToObject<List<SalesItemwiseBLL>>();

                    }
                }
                return lst;
            }
            catch (Exception ex)
            {
                return new List<SalesItemwiseBLL>();
            }
        }
        public List<SalesCategorywiseBLL> GetSalesCategorywiseRpt(int brandID, string locationID, DateTime FromDate, DateTime ToDate)
        {
            try
            {
                var lst = new List<SalesCategorywiseBLL>();

                SqlParameter[] p = new SqlParameter[4];
                p[0] = new SqlParameter("@brandid", brandID);
                p[1] = new SqlParameter("@locationid", locationID);
                p[2] = new SqlParameter("@fromdate", FromDate);
                p[3] = new SqlParameter("@todate", ToDate);


                _dt = (new DBHelper().GetTableFromSP)("sp_rptSalesCategorywiseAdmin", p);
                if (_dt != null)
                {
                    if (_dt.Rows.Count > 0)
                    {
                        lst = JArray.Parse(Newtonsoft.Json.JsonConvert.SerializeObject(_dt)).ToObject<List<SalesCategorywiseBLL>>();
                        
                    }
                }
                return lst;
            }
            catch (Exception ex)
            {
                return new List<SalesCategorywiseBLL>();
            }
        }
        public List<DeliveryBoyDetailBLL> GetDeliveryBoyDetailRpt(int brandID, DateTime FromDate, DateTime ToDate)
        {
            try
            {
                var lst = new List<DeliveryBoyDetailBLL>();

                SqlParameter[] p = new SqlParameter[3];
                p[0] = new SqlParameter("@brandid", brandID);
                p[1] = new SqlParameter("@fromdate", FromDate);
                p[2] = new SqlParameter("@todate", ToDate);


                _dt = (new DBHelper().GetTableFromSP)("sp_rptDeliveryBoysDetailReport", p);
                if (_dt != null)
                {
                    if (_dt.Rows.Count > 0)
                    {
                        lst = JArray.Parse(Newtonsoft.Json.JsonConvert.SerializeObject(_dt)).ToObject<List<DeliveryBoyDetailBLL>>();
                    }
                }
                return lst;
            }
            catch (Exception ex)
            {
                return new List<DeliveryBoyDetailBLL>();
            }
        }
        public List<OrderReportBLL> OrderReportByType(int brandId, int orderType, int locationId, DateTime FromDate, DateTime ToDate)
        {
            try
            {
                var lst = new List<OrderReportBLL>();

                SqlParameter[] p = new SqlParameter[5];
                p[0] = new SqlParameter("@brandId", brandId);
                p[1] = new SqlParameter("@orderType", orderType);
                p[2] = new SqlParameter("@locationid", locationId);
                p[3] = new SqlParameter("@fromdate", FromDate);
                p[4] = new SqlParameter("@todate", ToDate);


                _dt = (new DBHelper().GetTableFromSP)("sp_rptSalesOrdersReportByType", p);
                if (_dt != null)
                {
                    if (_dt.Rows.Count > 0)
                    {
                        lst = JArray.Parse(Newtonsoft.Json.JsonConvert.SerializeObject(_dt)).ToObject<List<OrderReportBLL>>();
                    }
                }
                return lst;
            }
            catch (Exception ex)
            {
                return new List<OrderReportBLL>();
            }
        }

        public List<SalesUserwiseBLL> GetSalesUserwiseRpt(int brandID, string locationID, DateTime FromDate, DateTime ToDate)
        {
            try
            {
                var lst = new List<SalesUserwiseBLL>();

                SqlParameter[] p = new SqlParameter[4];
                p[0] = new SqlParameter("@brandid", brandID);
                p[1] = new SqlParameter("@locationid", locationID);
                p[2] = new SqlParameter("@fromdate", FromDate);
                p[3] = new SqlParameter("@todate", ToDate);


                _dt = (new DBHelper().GetTableFromSP)("sp_rptSalesUserwiseAdmin", p);
                if (_dt != null)
                {
                    if (_dt.Rows.Count > 0)
                    {
                        lst = _dt.DataTableToList<SalesUserwiseBLL>();
                    }
                }
                return lst;
            }
            catch (Exception ex)
            {
                return new List<SalesUserwiseBLL>();
            }
        }
        public List<SalesCustomerwiseBLL> GetSalesCustomerwiseRpt(int brandID, string locationID, int customerID, DateTime FromDate, DateTime ToDate)
        {
            try
            {
                var lst = new List<SalesCustomerwiseBLL>();

                SqlParameter[] p = new SqlParameter[5];
                p[0] = new SqlParameter("@brandid", brandID);
                p[1] = new SqlParameter("@locationid", locationID);
                p[2] = new SqlParameter("@fromdate", FromDate);
                p[3] = new SqlParameter("@todate", ToDate);
                p[4] = new SqlParameter("@customerid", DBNull.Value);


                _dt = (new DBHelper().GetTableFromSP)("sp_rptSalesDetailsReport", p);
                if (_dt != null)
                {
                    if (_dt.Rows.Count > 0)
                    {
                        lst = JArray.Parse(Newtonsoft.Json.JsonConvert.SerializeObject(_dt)).ToObject<List<SalesCustomerwiseBLL>>();
                    }
                }
                return lst;
            }
            catch (Exception ex)
            {
                return new List<SalesCustomerwiseBLL>();
            }
        }
    }
}
