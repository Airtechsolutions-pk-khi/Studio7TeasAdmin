

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

    public class ordersDB : baseDB
    {
        public static OrdersBLL repo;
        public static DataTable _dt;
        public static DataSet _ds;
        public ordersDB()
           : base()
        {
            repo = new OrdersBLL();
            _dt = new DataTable();
            _ds = new DataSet();
        }

        public List<OrdersBLL> GetAll(int brandID,string locationID, DateTime FromDate, DateTime ToDate)
        {
            try
            {
                var lst = new List<OrdersBLL>();
                SqlParameter[] p = new SqlParameter[4];
                p[0] = new SqlParameter("@brandid", brandID);
                p[1] = new SqlParameter("@locationid", locationID);
                p[2] = new SqlParameter("@fromdate", FromDate.Date);
                p[3] = new SqlParameter("@todate", ToDate.Date);

                _dt = (new DBHelper().GetTableFromSP)("sp_rptSalesOrders", p);
                if (_dt != null)
                {
                    if (_dt.Rows.Count > 0)
                    {
                        lst = JArray.Parse(Newtonsoft.Json.JsonConvert.SerializeObject(_dt)).ToObject<List<OrdersBLL>>();
                    }
                }
           
                return lst;
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public DataSet Get(int id, int brandID)
        {
            try
            {
                var _obj = new OrdersBLL();
                SqlParameter[] p = new SqlParameter[1];
                p[0] = new SqlParameter("@OrderID", id);
                //p[1] = new SqlParameter("@brandid", brandID);

                _ds = (new DBHelper().GetDatasetFromSP)("sp_GetOrdersbyID_Admin", p);
              
                return _ds;
            }
            catch (Exception ex)
            {
                return null;
            }
        }
       
       
        public int Update(OrdersBLL data)
        {
            try
            {
                int rtn = 0;
                SqlParameter[] p = new SqlParameter[3];

                p[0] = new SqlParameter("@date", data.LastUpdatedDate);
                p[1] = new SqlParameter("@statusID", data.StatusID);
                p[2] = new SqlParameter("@orderid", data.OrderID);
                rtn = (new DBHelper().ExecuteNonQueryReturn)("sp_updateOrderstatus_Admin", p);

                return rtn;
            }
            catch (Exception ex)
            {
                return 0;
            }
        }

        public int Delete(OrdersBLL data)
        {
            try
            {
                int _obj = 0;
                SqlParameter[] p = new SqlParameter[2];
                p[0] = new SqlParameter("@id", data.OrderID);
                p[1] = new SqlParameter("@LastUpdatedDate", data.LastUpdatedDate);

                _obj = (new DBHelper().ExecuteNonQueryReturn)("sp_DeleteOrders", p);

                return _obj;
            }
            catch (Exception ex)
            {
                return 0;
            }
        }
    }
}
