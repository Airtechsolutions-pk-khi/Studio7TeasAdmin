

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

    public class addonsDB : baseDB
    {
        public static AddonsBLL repo;
        public static DataTable _dt;
        public static DataSet _ds;
        public addonsDB()
           : base()
        {
            repo = new AddonsBLL();
            _dt = new DataTable();
            _ds = new DataSet();
        }
        public List<AddonsBLL> GetAddon()
        {
            try
            {
                var lst = new List<AddonsBLL>();
                SqlParameter[] p = new SqlParameter[0];
                _dt = (new DBHelper().GetTableFromSP)("sp_GetAddons_V2");
                if (_dt != null)
                {
                    if (_dt.Rows.Count > 0)
                    {
                        lst = _dt.DataTableToList<AddonsBLL>();
                    }
                }
                return lst;
            }
            catch (Exception ex)
            {
                return null;
            }
        }
        public List<AddonsBLL> GetAll(int brandID)
        {
            try
            {
                var lst = new List<AddonsBLL>();
                SqlParameter[] p = new SqlParameter[1];
                p[0] = new SqlParameter("@brandid", brandID);

                _dt = (new DBHelper().GetTableFromSP)("sp_GetAddons", p);
                if (_dt != null)
                {
                    if (_dt.Rows.Count > 0)
                    {
                        lst = _dt.DataTableToList<AddonsBLL>();
                    }
                }
                return lst;
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public AddonsBLL Get(int id, int brandID)
        {
            try
            {
                var _obj = new AddonsBLL();
                SqlParameter[] p = new SqlParameter[2];
                p[0] = new SqlParameter("@id", id);
                p[1] = new SqlParameter("@brandid", brandID);

                _dt = (new DBHelper().GetTableFromSP)("sp_GetAddonsbyID_Admin", p);
                if (_dt != null)
                {
                    if (_dt.Rows.Count > 0)
                    {
                        _obj = _dt.DataTableToList<AddonsBLL>().FirstOrDefault();
                    }
                }
                return _obj;
            }
            catch (Exception ex)
            {
                return null;
            }
        }
       
        public int Insert(AddonsBLL data)
        {
            try
            {
                int rtn = 0;
                SqlParameter[] p = new SqlParameter[10];

                p[0] = new SqlParameter("@Name", data.Name);
                p[1] = new SqlParameter("@ArabicName", data.ArabicName);
                p[2] = new SqlParameter("@Description", data.Description);
                p[3] = new SqlParameter("@Image", data.Image);
                p[4] = new SqlParameter("@Price", data.Price);
                p[5] = new SqlParameter("@LastUpdatedBy", data.LastUpdatedBy);
                p[6] = new SqlParameter("@LastUpdatedDate", data.LastUpdatedDate);
                p[7] = new SqlParameter("@StatusID", data.StatusID);
                p[8] = new SqlParameter("@BrandID", data.BrandID);
                p[9] = new SqlParameter("@AddonID", data.AddonID);
                rtn = (new DBHelper().ExecuteNonQueryReturn)("dbo.sp_insertAddon_Admin", p);
              
                return rtn;
            }
            catch (Exception ex)
            {
                return 0;
            }
        }

        public int Update(AddonsBLL data)
        {
            try
            {
                int rtn = 0;
                SqlParameter[] p = new SqlParameter[10];

                p[0] = new SqlParameter("@Name", data.Name);
                p[1] = new SqlParameter("@ArabicName", data.ArabicName);
                p[2] = new SqlParameter("@Description", data.Description);
                p[3] = new SqlParameter("@Image", data.Image);
                p[4] = new SqlParameter("@Price", data.Price);
                p[5] = new SqlParameter("@LastUpdatedBy", data.LastUpdatedBy);
                p[6] = new SqlParameter("@LastUpdatedDate", data.LastUpdatedDate);
                p[7] = new SqlParameter("@StatusID", data.StatusID);
                p[8] = new SqlParameter("@BrandID", data.BrandID);
                p[9] = new SqlParameter("@AddonID", data.AddonID);
                rtn = (new DBHelper().ExecuteNonQueryReturn)("dbo.sp_updateAddon_Admin", p);


                return rtn;
            }
            catch (Exception ex)
            {
                return 0;
            }
        }

        public int Delete(AddonsBLL data)
        {
            try
            {
                int _obj = 0;
                SqlParameter[] p = new SqlParameter[2];
                p[0] = new SqlParameter("@id", data.AddonID);
                p[1] = new SqlParameter("@LastUpdatedDate", data.LastUpdatedDate);

                _obj = (new DBHelper().ExecuteNonQueryReturn)("sp_DeleteAddon", p);

                return _obj;
            }
            catch (Exception ex)
            {
                return 0;
            }
        }
    }
}
