

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

    public class deliveryDB : baseDB
    {
        public static DeliveryBLL repo;
        public static DataTable _dt;
        public static DataSet _ds;
        public deliveryDB()
           : base()
        {
            repo = new DeliveryBLL();
            _dt = new DataTable();
            _ds = new DataSet();
        }

        public List<DeliveryBLL> GetAll(int brandID)
        {
            try
            {
                var lst = new List<DeliveryBLL>();
                SqlParameter[] p = new SqlParameter[1];
                p[0] = new SqlParameter("@brandid", brandID);

                _dt = (new DBHelper().GetTableFromSP)("sp_GetDelivery", p);
                if (_dt != null)
                {
                    if (_dt.Rows.Count > 0)
                    {
                        lst = _dt.DataTableToList<DeliveryBLL>();
                    }
                }
                return lst;
            }
            catch (Exception ex)
            {
                return null;
            }
        }
        public List<DeliveryBLL> GetAllBrand()
        {
            try
            {
                var lst = new List<DeliveryBLL>();
                SqlParameter[] p = new SqlParameter[1];
                //p[0] = new SqlParameter("@brandid", brandID);

                _dt = (new DBHelper().GetTableFromSP)("sp_getAllBrand", p);
                if (_dt != null)
                {
                    if (_dt.Rows.Count > 0)
                    {
                        lst = _dt.DataTableToList<DeliveryBLL>();
                    }
                }
                return lst;
            }
            catch (Exception ex)
            {
                return null;
            }
        }
        public BrandSettingsBLL GetItemSettings(int brandID)
        {
            try
            {
                var _obj = new BrandSettingsBLL();
                SqlParameter[] p = new SqlParameter[1];
                p[0] = new SqlParameter("@brandid", brandID);

                _dt = (new DBHelper().GetTableFromSP)("sp_GetBrandSettings_Admin", p);
                if (_dt != null)
                {
                    if (_dt.Rows.Count > 0)
                    {
                        _obj = JArray.Parse(Newtonsoft.Json.JsonConvert.SerializeObject(_dt)).ToObject<List<BrandSettingsBLL>>().FirstOrDefault();
                        _obj.BrandID = brandID;
                    }
                }
                return _obj;
            }
            catch (Exception ex)
            {
                return new BrandSettingsBLL();
            }
        }
        public DeliveryBLL Get(int id)
        {
            try
            {
                var _obj = new DeliveryBLL();
                SqlParameter[] p = new SqlParameter[1];
                p[0] = new SqlParameter("@id", id);
                //p[1] = new SqlParameter("@brandid", brandID);

                _dt = (new DBHelper().GetTableFromSP)("sp_GetDeliverybyID_Admin", p);
                if (_dt != null)
                {
                    if (_dt.Rows.Count > 0)
                    {
                        _obj = _dt.DataTableToList<DeliveryBLL>().FirstOrDefault();
                    }
                }
                return _obj;
            }
            catch (Exception ex)
            {
                return null;
            }
        }
       
        public int Insert(DeliveryBLL data)
        {
            try
            {
                int rtn = 0;
                SqlParameter[] p = new SqlParameter[4];

                p[0] = new SqlParameter("@Name", data.Name);                              
                p[1] = new SqlParameter("@Amount", data.Amount);
                p[2] = new SqlParameter("@StatusID", data.StatusID);
                p[3] = new SqlParameter("@DeliveryAreaID", data.DeliveryAreaID);               

                //rtn = (new DBHelper().ExecuteNonQueryReturn)("dbo.sp_insertDelivery_Admin", p);
                rtn = int.Parse(new DBHelper().GetTableFromSP("dbo.sp_insertDelivery_Admin", p).Rows[0]["DeliveryAreaID"].ToString());

                if (data.brands != "" && data.brands != null)
                {
                    SqlParameter[] p1 = new SqlParameter[2];
                    p1[0] = new SqlParameter("@DeliveryAreaID", rtn);
                    p1[1] = new SqlParameter("@BrandIDs", data.brands);
                    (new DBHelper().ExecuteNonQueryReturn)("sp_insertBrandDelivery_Admin", p1);
                }
                return rtn;
            }
            catch (Exception ex)
            {
                return 0;
            }
        }
       
        public int Update(DeliveryBLL data)
        {
            try
            {
                int rtn = 0;
                SqlParameter[] p = new SqlParameter[4];

                p[0] = new SqlParameter("@DeliveryAreaID", data.DeliveryAreaID);
                p[1] = new SqlParameter("@Name", data.Name);                
                p[2] = new SqlParameter("@Amount", data.Amount);
                p[3] = new SqlParameter("@StatusID", data.StatusID);
                                
                rtn = (new DBHelper().ExecuteNonQueryReturn)("dbo.sp_updateDelivery_Admin", p);
                
                if (data.brands != "" && data.brands != null)
                {
                    SqlParameter[] p1 = new SqlParameter[2];
                    p1[0] = new SqlParameter("@DeliveryAreaID", data.DeliveryAreaID);
                    p1[1] = new SqlParameter("@BrandIDs", data.brands);
                    (new DBHelper().ExecuteNonQueryReturn)("sp_insertBrandDelivery_Admin", p1);
                }
                return rtn;
            }
            catch (Exception ex)
            {
                return 0;
            }
        }

        public int Delete(DeliveryBLL data)
        {
            try
            {
                int _obj = 0;
                SqlParameter[] p = new SqlParameter[1];
                p[0] = new SqlParameter("@id", data.DeliveryAreaID);
                
                _obj = (new DBHelper().ExecuteNonQueryReturn)("sp_DeleteDelivery", p);

                return _obj;
            }
            catch (Exception ex)
            {
                return 0;
            }
        }
    }
}
