

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

    public class brandDB : baseDB
    {
        public static BrandBLL repo;
        public static DataTable _dt;
        public static DataSet _ds;
        public brandDB()
           : base()
        {
            repo = new BrandBLL();
            _dt = new DataTable();
            _ds = new DataSet();
        }

        public List<BrandBLL> GetAll(int brandID)
        {
            try
            {
                var lst = new List<BrandBLL>();

                _dt = (new DBHelper().GetTableFromSP)("sp_GetBrand");
                if (_dt != null)
                {
                    if (_dt.Rows.Count > 0)
                    {
                        lst = _dt.DataTableToList<BrandBLL>();
                    }
                }
                return lst;
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public BrandBLL Get(int id, int brandID)
        {
            try
            {
                var _obj = new BrandBLL();
                SqlParameter[] p = new SqlParameter[1];
                p[0] = new SqlParameter("@id", id);

                _dt = (new DBHelper().GetTableFromSP)("sp_GetBrandID_Admin", p);
                if (_dt != null)
                {
                    if (_dt.Rows.Count > 0)
                    {
                        _obj = _dt.DataTableToList<BrandBLL>().FirstOrDefault();
                        //_obj = JObject.Parse(Newtonsoft.Json.JsonConvert.SerializeObject(_dt)).ToObject<BrandBLL>();
                    }
                }
                return _obj;
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public int Insert(BrandBLL data)
        {
            try
            {
                int rtn = 0;
                SqlParameter[] p = new SqlParameter[13];

                p[0] = new SqlParameter("@Username", data.Name);
                p[1] = new SqlParameter("@Name", data.Name);
                p[2] = new SqlParameter("@Image", data.Image);
                p[3] = new SqlParameter("@Email", data.Email);
                p[4] = new SqlParameter("@Password", data.Password);
                p[5] = new SqlParameter("@CompanyURl", data.CompanyURl);
                p[6] = new SqlParameter("@Address", data.Address);
                p[7] = new SqlParameter("@StatusID", data.StatusID);
                p[8] = new SqlParameter("@Currency", data.Currency);
                p[9] = new SqlParameter("@BusinessKey", data.BusinessKey);
                p[10] = new SqlParameter("@LastUpdateBy", data.LastUpdateBy);
                p[11] = new SqlParameter("@LastUpdatedDate", data.LastUpdatedDate);
                p[12] = new SqlParameter("@BrandID", data.BrandID);

                rtn = (new DBHelper().ExecuteNonQueryReturn)("dbo.sp_insertBrand_Admin", p);

                return rtn;
            }
            catch (Exception ex)
            {
                return 0;
            }
        }

        public int Update(BrandBLL data)
        {
            try
            {
                int rtn = 0;
                SqlParameter[] p = new SqlParameter[13];

                p[0] = new SqlParameter("@Username", data.Name);
                p[1] = new SqlParameter("@Name", data.Name);
                p[2] = new SqlParameter("@Image", data.Image);
                p[3] = new SqlParameter("@Email", data.Email);
                p[4] = new SqlParameter("@Password", data.Password);
                p[5] = new SqlParameter("@CompanyURl", data.CompanyURl);
                p[6] = new SqlParameter("@Address", data.Address);
                p[7] = new SqlParameter("@StatusID", data.StatusID);
                p[8] = new SqlParameter("@Currency", data.Currency);
                p[9] = new SqlParameter("@BusinessKey", data.BusinessKey == null ? 0 : data.BusinessKey);
                p[10] = new SqlParameter("@LastUpdateBy", data.LastUpdateBy == null ? "" : data.LastUpdateBy);
                p[11] = new SqlParameter("@LastUpdatedDate", data.LastUpdatedDate == null ? null: data.LastUpdatedDate);
                p[12] = new SqlParameter("@BrandID", data.BrandID);

                rtn = (new DBHelper().ExecuteNonQueryReturn)("dbo.sp_updateBrand_Admin", p);

                return rtn;
            }
            catch (Exception ex)
            {
                return 0;
            }
        }

        public int Delete(BrandBLL data)
        {
            try
            {
                int _obj = 0;
                SqlParameter[] p = new SqlParameter[2];
                p[0] = new SqlParameter("@id", data.BrandID);
                p[1] = new SqlParameter("@LastUpdatedDate", data.LastUpdatedDate);

                _obj = (new DBHelper().ExecuteNonQueryReturn)("sp_DeleteBrand", p);

                return _obj;
            }
            catch (Exception ex)
            {
                return 0;
            }
        }
    }
}
