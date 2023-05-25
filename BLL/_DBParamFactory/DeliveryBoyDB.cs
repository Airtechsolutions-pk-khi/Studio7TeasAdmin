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

    public class DeliveryBoyDB : baseDB
    {
        public static DeliveryBoyBLL repo;
        public static DataTable _dt;
        public static DataSet _ds;
        public DeliveryBoyDB()
           : base()
        {
            repo = new DeliveryBoyBLL();
            _dt = new DataTable();
            _ds = new DataSet();
        }

        public List<DeliveryBoyBLL> GetAll(int brandID)
        {
            try
            {
                var lst = new List<DeliveryBoyBLL>();
                SqlParameter[] p = new SqlParameter[1];
                p[0] = new SqlParameter("@brandid", brandID);

                _dt = (new DBHelper().GetTableFromSP)("sp_GetDeliveryBoy", p);
                if (_dt != null)
                {
                    if (_dt.Rows.Count > 0)
                    {
                        lst = _dt.DataTableToList<DeliveryBoyBLL>();
                    }
                }
                return lst;
            }
            catch (Exception ex)
            {
                return null;
            }
        }
        public List<DeliveryBoyBLL> GetAllBrand()
        {
            try
            {
                var lst = new List<DeliveryBoyBLL>();
                SqlParameter[] p = new SqlParameter[1];
                //p[0] = new SqlParameter("@brandid", brandID);

                _dt = (new DBHelper().GetTableFromSP)("sp_getAllBrand", p);
                if (_dt != null)
                {
                    if (_dt.Rows.Count > 0)
                    {
                        lst = _dt.DataTableToList<DeliveryBoyBLL>();
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
        public DeliveryBoyBLL Get(int id)
        {
            try
            {
                var _obj = new DeliveryBoyBLL();
                SqlParameter[] p = new SqlParameter[1];
                p[0] = new SqlParameter("@id", id);
                //p[1] = new SqlParameter("@brandid", brandID);

                _dt = (new DBHelper().GetTableFromSP)("sp_GetDeliveryBoybyID_Admin", p);
                if (_dt != null)
                {
                    if (_dt.Rows.Count > 0)
                    {
                        _obj = _dt.DataTableToList<DeliveryBoyBLL>().FirstOrDefault();
                    }
                }
                return _obj;
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public int Insert(DeliveryBoyBLL data)
        {
            try
            {
                int rtn = 0;
                SqlParameter[] p = new SqlParameter[10];

                p[0] = new SqlParameter("@DBName", data.DBName);
                p[1] = new SqlParameter("@DBAddress", data.DBAddress);
                p[2] = new SqlParameter("@DBContactNo", data.DBContactNo);
                p[3] = new SqlParameter("@DBVehicleNo", data.DBVehicleNo);
                p[4] = new SqlParameter("@DBCNICNo", data.DBCNICNo);
                p[5] = new SqlParameter("@CreatedBy", data.CreatedBy);
                p[6] = new SqlParameter("@UpdatedBy", data.UpdatedBy);
                p[7] = new SqlParameter("@BrandID", data.BrandID);
                p[8] = new SqlParameter("@Image", "");
                p[9] = new SqlParameter("@Amount", data.Amount);
                rtn = int.Parse(new DBHelper().GetTableFromSP("dbo.sp_insertDeliveryBoy_Admin", p).Rows[0]["DeliveryBoyID"].ToString());

                //if (data.brands != "" && data.brands != null)
                //{
                //    SqlParameter[] p1 = new SqlParameter[2];
                //    p1[0] = new SqlParameter("@DeliveryBoyAreaID", rtn);
                //    p1[1] = new SqlParameter("@BrandIDs", data.brands);
                //    (new DBHelper().ExecuteNonQueryReturn)("sp_insertBrandDeliveryBoy_Admin", p1);
                //}
                return rtn;
            }
            catch (Exception ex)
            {
                return 0;
            }
        }

        public int Update(DeliveryBoyBLL data)
        {
            try
            {
                int rtn = 0;
                SqlParameter[] p = new SqlParameter[11];
                
                p[0] = new SqlParameter("@DeliveryBoyID", data.DeliveryBoyID);
                p[1] = new SqlParameter("@DBName", data.DBName);
                p[2] = new SqlParameter("@DBAddress", data.DBAddress);
                p[3] = new SqlParameter("@DBContactNo", data.DBContactNo);
                p[4] = new SqlParameter("@DBVehicleNo", data.DBVehicleNo);
                p[5] = new SqlParameter("@DBCNICNo", data.DBCNICNo);
                p[6] = new SqlParameter("@UpdatedBy", data.UpdatedBy);
                p[7] = new SqlParameter("@StatusID", data.StatusID);
                p[8] = new SqlParameter("@BrandID", data.BrandID);
                p[9] = new SqlParameter("@Image", "");
                p[10] = new SqlParameter("@Amount", data.Amount);

                rtn = (new DBHelper().ExecuteNonQueryReturn)("dbo.sp_updateDeliveryBoy_Admin", p);

                //if (data.brands != "" && data.brands != null)
                //{
                //    SqlParameter[] p1 = new SqlParameter[2];
                //    p1[0] = new SqlParameter("@DeliveryBoyAreaID", data.DeliveryBoyAreaID);
                //    p1[1] = new SqlParameter("@BrandIDs", data.brands);
                //    (new DBHelper().ExecuteNonQueryReturn)("sp_insertBrandDeliveryBoy_Admin", p1);
                //}
                return rtn;
            }
            catch (Exception ex)
            {
                return 0;
            }
        }

        public int Delete(DeliveryBoyBLL data)
        {
            try
            {
                int _obj = 0;
                SqlParameter[] p = new SqlParameter[1];
                p[0] = new SqlParameter("@id", data.DeliveryBoyID);

                _obj = (new DBHelper().ExecuteNonQueryReturn)("sp_DeleteDeliveryBoy", p);

                return _obj;
            }
            catch (Exception ex)
            {
                return 0;
            }
        }
    }
}

