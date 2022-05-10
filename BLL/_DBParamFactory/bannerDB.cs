

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

    public class bannerDB : baseDB
    {
        public static BannerBLL repo;
        public static DataTable _dt;
        public static DataSet _ds;
        public bannerDB()
           : base()
        {
            repo = new BannerBLL();
            _dt = new DataTable();
            _ds = new DataSet();
        }

        public List<BannerBLL> GetAll(int brandID)
        {
            try
            {
                var lst = new List<BannerBLL>();
                SqlParameter[] p = new SqlParameter[1];
                p[0] = new SqlParameter("@brandid", brandID);

                _dt = (new DBHelper().GetTableFromSP)("sp_GetBanner", p);
                if (_dt != null)
                {
                    if (_dt.Rows.Count > 0)
                    {
                        lst = _dt.DataTableToList<BannerBLL>();
                    }
                }
                return lst;
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public BannerBLL Get(int id, int brandID)
        {
            try
            {
                var _obj = new BannerBLL();
                SqlParameter[] p = new SqlParameter[2];
                p[0] = new SqlParameter("@id", id);
                p[1] = new SqlParameter("@brandid", brandID);

                _dt = (new DBHelper().GetTableFromSP)("sp_GetBannerbyID_Admin", p);
                if (_dt != null)
                {
                    if (_dt.Rows.Count > 0)
                    {
                        _obj = _dt.DataTableToList<BannerBLL>().FirstOrDefault();
                    }
                }
                return _obj;
            }
            catch (Exception ex)
            {
                return null;
            }
        }
       
        public int Insert(BannerBLL data)
        {
            try
            {
                int rtn = 0;
                SqlParameter[] p = new SqlParameter[8];

                p[0] = new SqlParameter("@Name", data.Name);
                p[1] = new SqlParameter("@Description", data.Description);
                p[2] = new SqlParameter("@Image", data.Image);
                p[3] = new SqlParameter("@StatusID", data.StatusID);
                p[4] = new SqlParameter("@LastUpdatedBy", data.LastUpdatedBy);
                p[5] = new SqlParameter("@LastUpdatedDate", data.LastUpdatedDate);
                p[6] = new SqlParameter("@BrandID", data.BrandID);
                p[7] = new SqlParameter("@BannerID", data.BannerID);
             
                rtn = (new DBHelper().ExecuteNonQueryReturn)("dbo.sp_insertBanner_Admin", p);
              
                return rtn;
            }
            catch (Exception ex)
            {
                return 0;
            }
        }

        public int Update(BannerBLL data)
        {
            try
            {
                int rtn = 0;
                SqlParameter[] p = new SqlParameter[8];

                p[0] = new SqlParameter("@Name", data.Name);
                p[1] = new SqlParameter("@Description", data.Description);
                p[2] = new SqlParameter("@Image", data.Image);
                p[3] = new SqlParameter("@StatusID", data.StatusID);
                p[4] = new SqlParameter("@LastUpdatedBy", data.LastUpdatedBy);
                p[5] = new SqlParameter("@LastUpdatedDate", data.LastUpdatedDate);
                p[6] = new SqlParameter("@BrandID", data.BrandID);
                p[7] = new SqlParameter("@BannerID", data.BannerID);

                rtn = (new DBHelper().ExecuteNonQueryReturn)("dbo.sp_updateBanner_Admin", p);
                return rtn;
            }
            catch (Exception ex)
            {
                return 0;
            }
        }

        public int Delete(BannerBLL data)
        {
            try
            {
                int _obj = 0;
                SqlParameter[] p = new SqlParameter[2];
                p[0] = new SqlParameter("@id", data.BannerID);
                p[1] = new SqlParameter("@LastUpdatedDate", data.LastUpdatedDate);

                _obj = (new DBHelper().ExecuteNonQueryReturn)("sp_DeleteBanner", p);

                return _obj;
            }
            catch (Exception ex)
            {
                return 0;
            }
        }
    }
}
