

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

    public class offersDB : baseDB
    {
        public static OffersBLL repo;
        public static DataTable _dt;
        public static DataSet _ds;
        public offersDB()
           : base()
        {
            repo = new OffersBLL();
            _dt = new DataTable();
            _ds = new DataSet();
        }

        public List<OffersBLL> GetAll(int brandID)
        {
            try
            {
                var lst = new List<OffersBLL>();
                SqlParameter[] p = new SqlParameter[1];
                p[0] = new SqlParameter("@brandid", brandID);

                _dt = (new DBHelper().GetTableFromSP)("sp_getOffers", p);
                if (_dt != null)
                {
                    if (_dt.Rows.Count > 0)
                    {
                        lst = _dt.DataTableToList<OffersBLL>();
                    }
                }
                return lst;
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public OffersBLL Get(int id, int brandID)
        {
            try
            {
                var _obj = new OffersBLL();
                SqlParameter[] p = new SqlParameter[2];
                p[0] = new SqlParameter("@id", id);
                p[1] = new SqlParameter("@brandid", brandID);

                _dt = (new DBHelper().GetTableFromSP)("sp_GetOffersbyID_Admin", p);
                if (_dt != null)
                {
                    if (_dt.Rows.Count > 0)
                    {
                        _obj = _dt.DataTableToList<OffersBLL>().FirstOrDefault();
                    }
                }
                return _obj;
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public int Insert(OffersBLL data)
        {
            try
            {
                int rtn = 0;
                SqlParameter[] p = new SqlParameter[11];

                p[0] = new SqlParameter("@Name", data.Name);
                p[1] = new SqlParameter("@Description", data.Description);
                p[2] = new SqlParameter("@FromDate", Convert.ToDateTime(data.FromDate));
                p[3] = new SqlParameter("@ToDate", Convert.ToDateTime(data.ToDate));
                p[4] = new SqlParameter("@Image", data.Image);
                p[5] = new SqlParameter("@ItemID", data.ItemID);
                p[6] = new SqlParameter("@StatusID", data.StatusID);
                p[7] = new SqlParameter("@LastUpdatedBy", data.LastUpdatedBy);
                p[8] = new SqlParameter("@LastUpdatedDate", data.LastUpdatedDate);
                p[9] = new SqlParameter("@BrandID", data.BrandID);
                p[10] = new SqlParameter("@OfferID", data.OfferID);

                rtn = (new DBHelper().ExecuteNonQueryReturn)("dbo.sp_insertOffers_Admin", p);

                return rtn;
            }
            catch (Exception ex)
            {
                return 0;
            }
        }

        public int Update(OffersBLL data)
        {
            try
            {
                int rtn = 0;
                SqlParameter[] p = new SqlParameter[11];

                p[0] = new SqlParameter("@Name", data.Name);
                p[1] = new SqlParameter("@Description", data.Description);
                p[2] = new SqlParameter("@FromDate", Convert.ToDateTime(data.FromDate));
                p[3] = new SqlParameter("@ToDate", Convert.ToDateTime(data.ToDate));
                p[4] = new SqlParameter("@Image", data.Image);
                p[5] = new SqlParameter("@ItemID", data.ItemID);
                p[6] = new SqlParameter("@StatusID", data.StatusID);
                p[7] = new SqlParameter("@LastUpdatedBy", data.LastUpdatedBy);
                p[8] = new SqlParameter("@LastUpdatedDate", data.LastUpdatedDate);
                p[9] = new SqlParameter("@BrandID", data.BrandID);
                p[10] = new SqlParameter("@OfferID", data.OfferID);

                rtn = (new DBHelper().ExecuteNonQueryReturn)("dbo.sp_updateOffers_Admin", p);
                return rtn;
            }
            catch (Exception ex)
            {
                return 0;
            }
        }

        public int Delete(OffersBLL data)
        {
            try
            {
                int _obj = 0;
                SqlParameter[] p = new SqlParameter[2];
                p[0] = new SqlParameter("@id", data.OfferID);
                p[1] = new SqlParameter("@LastUpdatedDate", data.LastUpdatedDate);

                _obj = (new DBHelper().ExecuteNonQueryReturn)("sp_DeleteOffers", p);

                return _obj;
            }
            catch (Exception ex)
            {
                return 0;
            }
        }
    }
}
