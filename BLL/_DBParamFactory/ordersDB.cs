

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
        public int Edit(OrdersEditBLL data)
        {
            try
            {
                int rtn = 0;
                int rtn1 = 0;
                double? value = 0;
                SqlParameter[] p = new SqlParameter[4];

                p[0] = new SqlParameter("@CustomerName", data.CustomerName);
                p[1] = new SqlParameter("@CustomerAddress", data.CustomerAddress);
                p[2] = new SqlParameter("@CustomerMobile", data.CustomerMobile);
                p[3] = new SqlParameter("@OrderID", data.OrderID);
                rtn = (new DBHelper().ExecuteNonQueryReturn)("sp_editorder_Admin", p);

                int rtn2 = 0;
                SqlParameter[] a = new SqlParameter[4];
                a[0] = new SqlParameter("@StatusID", data.StatusID);
                a[1] = new SqlParameter("@DeliveryBoyID", data.DeliveryBoyID == 0 ? null : data.DeliveryBoyID);
                a[2] = new SqlParameter("@OrderType", data.OrderType);
                a[3] = new SqlParameter("@OrderID", data.OrderID);
                rtn2 = (new DBHelper().ExecuteNonQueryReturn)("sp_editorder_Admin_2", a);

                foreach (var item in data.OrderDetails)
                {
                    SqlParameter[] para = new SqlParameter[6];
                    para[0] = new SqlParameter("@OrderID", data.OrderID);//Hard Coded Value Pass
                    para[1] = new SqlParameter("@ItemID", item.ItemID);
                    para[2] = new SqlParameter("@Quantity", item.Quantity);
                    para[3] = new SqlParameter("@Price", item.Price);
                    para[4] = new SqlParameter("@LastUpdatedDate", DateTime.UtcNow.AddMinutes(300));
                    para[5] = new SqlParameter("@LastUpdatedBy", 0);
                    rtn1 = int.Parse(new DBHelper().GetTableFromSP("sp_OrderDetails", para).Rows[0]["ID"].ToString());
                    if (item.OrderDetailAddons != null)
                    {
                        foreach (var i in item.OrderDetailAddons)
                        {
                            SqlParameter[] para1 = new SqlParameter[7];
                            para1[0] = new SqlParameter("@OrderDetailID", rtn1);
                            para1[1] = new SqlParameter("@AddonID", i.AddonID);
                            para1[2] = new SqlParameter("@Quantity", i.Quantity);
                            para1[3] = new SqlParameter("@Price", i.Price);
                            para1[4] = new SqlParameter("@LastUpdatedDate", DateTime.UtcNow.AddMinutes(300));
                            para1[5] = new SqlParameter("@LastUpdatedBy", 0);
                            para1[6] = new SqlParameter("@StatusID", 201);
                            (new DBHelper().ExecuteNonQueryReturn)("sp_OrderDetailAddons", para1);

                        }
                    }
                    if (item.OrderDetailModifiers != null)
                    {

                        foreach (var i in item.OrderDetailModifiers)
                        {
                            if (i.ModifierID != null)
                            {
                                SqlParameter[] pa1 = new SqlParameter[7];
                                pa1[0] = new SqlParameter("@OrderDetailID", rtn1);
                                pa1[1] = new SqlParameter("@ModifierID", i.ModifierID);
                                pa1[2] = new SqlParameter("@Quantity", item.Quantity);
                                pa1[3] = new SqlParameter("@Price", i.Price);
                                pa1[4] = new SqlParameter("@LastUpdatedDate", DateTime.UtcNow.AddMinutes(300));
                                pa1[5] = new SqlParameter("@LastUpdatedBy", 0);
                                pa1[6] = new SqlParameter("@StatusID", 201);
                                (new DBHelper().ExecuteNonQueryReturn)("sp_OrderModifierDetails", pa1);
                            }

                        }
                    }
                }

                foreach (var item in data.OrderDetails)
                {
                    value += item.Price * item.Quantity;
                }
                //decimal? DC = data.order.DeliveryAmount;
                //var GT = Convert.ToDecimal(value) + DC;
                SqlParameter[] par = new SqlParameter[3];
                par[0] = new SqlParameter("@AmountTotal", value);
                par[1] = new SqlParameter("@GrandTotal", value);
                par[2] = new SqlParameter("@OrderID", data.OrderID);

                (new DBHelper().ExecuteNonQueryReturn)("sp_UpdateOrderValue_Admin", par);
                return 1;
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
