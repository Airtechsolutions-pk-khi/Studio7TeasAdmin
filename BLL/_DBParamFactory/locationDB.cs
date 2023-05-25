

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

    public class locationDB : baseDB
    {
        public static LocationBLL repo;
        public static DataTable _dt;
        public static DataSet _ds;
        public locationDB()
           : base()
        {
            repo = new LocationBLL();
            _dt = new DataTable();
            _ds = new DataSet();
        }

        public List<LocationBLL> GetAll(int brandID)
        {
            try
            {
                var lst = new List<LocationBLL>();
                SqlParameter[] p = new SqlParameter[1];
                p[0] = new SqlParameter("@brandid", brandID);

                _dt = (new DBHelper().GetTableFromSP)("sp_GetLocation", p);
                if (_dt != null)
                {
                    if (_dt.Rows.Count > 0)
                    {
                        //lst = _dt.DataTableToList<LocationBLL>();

                        lst = JArray.Parse(Newtonsoft.Json.JsonConvert.SerializeObject(_dt)).ToObject<List<LocationBLL>>();
                    }
                }
                return lst;
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public LocationBLL Get(int id, int brandID)
        {
            try
            {
                var _obj = new LocationBLL();
                SqlParameter[] p = new SqlParameter[2];
                p[0] = new SqlParameter("@id", id);
                p[1] = new SqlParameter("@brandid", brandID);

                _dt = (new DBHelper().GetTableFromSP)("sp_GetLocationID_Admin", p);
                if (_dt != null)
                {
                    if (_dt.Rows.Count > 0)
                    {
                        //_obj = _dt.DataTableToList<LocationBLL>().FirstOrDefault();
                        _obj = JArray.Parse(Newtonsoft.Json.JsonConvert.SerializeObject(_dt)).ToObject<List<LocationBLL>>().FirstOrDefault();
                    }
                }
                return _obj;
            }
            catch (Exception ex)
            {
                return null;
            }
        }
       
        public int Insert(LocationBLL data)
        {
            try
            {
                int rtn = 0;
                SqlParameter[] p = new SqlParameter[27];

                p[0] = new SqlParameter("@Name", data.Name);
                p[1] = new SqlParameter("@Description", data.Description);
                p[2] = new SqlParameter("@Address", data.Address);
                p[3] = new SqlParameter("@ContactNo", data.ContactNo);
                p[4] = new SqlParameter("@Email", data.Email);
                p[5] = new SqlParameter("@LicenseID", data.LicenseID);
                p[6] = new SqlParameter("@DeliveryServices", data.DeliveryServices);
                p[7] = new SqlParameter("@DeliveryCharges", data.DeliveryCharges);
                p[8] = new SqlParameter("@DeliveryTime", data.DeliveryTime);
                p[9] = new SqlParameter("@MinOrderAmount", data.MinOrderAmount);
                p[10] = new SqlParameter("@Longitude", data.Longitude);
                p[11] = new SqlParameter("@Latitude", data.Latitude);
                p[12] = new SqlParameter("@LastUpdatedBy", data.LastUpdatedBy);
                p[13] = new SqlParameter("@LastUpdatedDate", data.LastUpdatedDate);
                p[14] = new SqlParameter("@StatusID", data.StatusID);
                p[15] = new SqlParameter("@ImageURL", data.ImageURL);
                p[16] = new SqlParameter("@BrandID", data.BrandID);
                p[17] = new SqlParameter("@Opentime", data.Opentime);
                p[18] = new SqlParameter("@Closetime", data.Closetime);
                p[19] = new SqlParameter("@LocationID", data.LocationID);
                p[20] = new SqlParameter("@Passcode", data.Passcode);
                p[21] = new SqlParameter("@Currency", data.Currency);
                p[22] = new SqlParameter("@Discounts", data.Discounts);
                p[23] = new SqlParameter("@Tax", data.Tax);
                p[24] = new SqlParameter("@IsPickupAllowed", data.IsPickupAllowed);
                p[25] = new SqlParameter("@IsDeliveryAllowed", data.IsDeliveryAllowed);
                p[26] = new SqlParameter("@IsDeliveryAllowed", data.IsDineInAllowed);
                rtn = (new DBHelper().ExecuteNonQueryReturn)("dbo.sp_insertLocation_Admin", p);
              
                return rtn;
            }
            catch (Exception ex)
            {
                return 0;
            }
        }

        public int Update(LocationBLL data)
        {
            try
            {
                int rtn = 0;
                SqlParameter[] p = new SqlParameter[27];

                p[0] = new SqlParameter("@Name", data.Name);
                p[1] = new SqlParameter("@Description", data.Description);
                p[2] = new SqlParameter("@Address", data.Address);
                p[3] = new SqlParameter("@ContactNo", data.ContactNo);
                p[4] = new SqlParameter("@Email", data.Email);
                p[5] = new SqlParameter("@LicenseID", data.LicenseID);
                p[6] = new SqlParameter("@DeliveryServices", data.DeliveryServices);
                p[7] = new SqlParameter("@DeliveryCharges", data.DeliveryCharges);
                p[8] = new SqlParameter("@DeliveryTime", data.DeliveryTime);
                p[9] = new SqlParameter("@MinOrderAmount", data.MinOrderAmount);
                p[10] = new SqlParameter("@Longitude", data.Longitude);
                p[11] = new SqlParameter("@Latitude", data.Latitude);
                p[12] = new SqlParameter("@LastUpdatedBy", data.LastUpdatedBy);
                p[13] = new SqlParameter("@LastUpdatedDate", data.LastUpdatedDate);
                p[14] = new SqlParameter("@StatusID", data.StatusID);
                p[15] = new SqlParameter("@ImageURL", data.ImageURL);
                p[16] = new SqlParameter("@BrandID", data.BrandID);
                p[17] = new SqlParameter("@Opentime", data.Opentime);
                p[18] = new SqlParameter("@Closetime", data.Closetime);
                p[19] = new SqlParameter("@LocationID", data.LocationID);
                p[20] = new SqlParameter("@Passcode", data.Passcode);
                p[21] = new SqlParameter("@Currency", data.Currency);
                p[22] = new SqlParameter("@Discounts", data.Discounts);
                p[23] = new SqlParameter("@Tax", data.Tax);
                p[24] = new SqlParameter("@IsPickupAllowed", data.IsPickupAllowed);
                p[25] = new SqlParameter("@IsDeliveryAllowed", data.IsDeliveryAllowed);
                p[26] = new SqlParameter("@IsDeliveryAllowed", data.IsDineInAllowed);
                rtn = (new DBHelper().ExecuteNonQueryReturn)("dbo.sp_updateLocation_Admin", p);

                return rtn;
            }
            catch (Exception ex)
            {
                return 0;
            }
        }

        public int Delete(LocationBLL data)
        {
            try
            {
                int _obj = 0;
                SqlParameter[] p = new SqlParameter[2];
                p[0] = new SqlParameter("@id", data.LocationID);
                p[1] = new SqlParameter("@LastUpdatedDate", data.LastUpdatedDate);

                _obj = (new DBHelper().ExecuteNonQueryReturn)("sp_DeleteLocation", p);

                return _obj;
            }
            catch (Exception ex)
            {
                return 0;
            }
        }
    }
}
