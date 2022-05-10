

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

    public class aboutDB : baseDB
    {
        public static AboutBLL repo;
        public static DataTable _dt;
        public static DataSet _ds;
        public aboutDB()
           : base()
        {
            repo = new AboutBLL();
            _dt = new DataTable();
            _ds = new DataSet();
        }

        //public List<CategoryBLL> GetAll(int brandID)
        //{
        //    try
        //    {
        //        var lst = new List<CategoryBLL>();
        //        SqlParameter[] p = new SqlParameter[1];
        //        p[0] = new SqlParameter("@brandid", brandID);

        //        _dt = (new DBHelper().GetTableFromSP)("sp_GetCategory", p);
        //        if (_dt != null)
        //        {
        //            if (_dt.Rows.Count > 0)
        //            {
        //                lst = _dt.DataTableToList<CategoryBLL>();

        //                //lst= JArray.Parse(Newtonsoft.Json.JsonConvert.SerializeObject(_dt)).ToArray<CategoryBLL>()
        //                //lst = _dt.ToList<CategoryBLL>().ToList();
        //            }
        //        }

        //        return lst;
        //    }
        //    catch (Exception ex)
        //    {
        //        return null;
        //    }
        //}

        public AboutBLL Get( int BrandID)
        {
            try
            {
                var _obj = new AboutBLL();
                SqlParameter[] p = new SqlParameter[1];
                p[0] = new SqlParameter("@BrandID", BrandID);

                _ds = (new DBHelper().GetDatasetFromSP)("sp_GetAppsettingbyID_Admin", p);
                //_dt = (new DBHelper().GetTableFromSP)("sp_GetAppsettingbyID_Admin", p);
                if (_ds != null)
                {
                    if (_ds.Tables.Count > 0)
                    {
                        
                        //_obj = JArray.Parse(Newtonsoft.Json.JsonConvert.SerializeObject(_dt)).ToObject<List<AboutBLL>>().FirstOrDefault();
                        _obj = JArray.Parse(Newtonsoft.Json.JsonConvert.SerializeObject(_ds.Tables[0])).ToObject<List<AboutBLL>>().FirstOrDefault();
                        var _obj1 = JArray.Parse(Newtonsoft.Json.JsonConvert.SerializeObject(_ds.Tables[1])).ToObject<List<AboutBLL>>().FirstOrDefault();

                        
                        _obj.AppDescription = _obj1.AppDescription;
                        _obj.Facebook = _obj1.Facebook;
                        _obj.Twitter = _obj1.Twitter;
                        _obj.Instagram = _obj1.Instagram;
                        _obj.AppInfoID = _obj1.AppInfoID;

                    }
                }

                return _obj;
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public int Insert(AboutBLL data)
        {
            try
            {
                int rtn = 0;
                SqlParameter[] p = new SqlParameter[9];

                p[0] = new SqlParameter("@BranchName", data.BranchName);
                p[1] = new SqlParameter("@BranchAddress", data.BranchAddress);
                p[2] = new SqlParameter("@BranchTiming", data.BranchTiming);
                p[3] = new SqlParameter("@DeliveryNo", data.DeliveryNo);
                p[4] = new SqlParameter("@Discount", data.Discount);
                p[5] = new SqlParameter("@Discountdescription", data.Discountdescription);
                p[6] = new SqlParameter("@StatusID", data.StatusID);                
                p[7] = new SqlParameter("@BrandID", data.BrandID);
                p[8] = new SqlParameter("@WhatsappNo", data.WhatsappNo);

                rtn = (new DBHelper().ExecuteNonQueryReturn)("dbo.sp_insertAppsetting_Admin", p);

                try
                {
                    SqlParameter[] pn = new SqlParameter[4];

                    pn[0] = new SqlParameter("@AppDescription", data.AppDescription);
                    pn[1] = new SqlParameter("@Facebook", data.Facebook);
                    pn[2] = new SqlParameter("@Twitter", data.Twitter);
                    pn[3] = new SqlParameter("@Instagram", data.Instagram);

                    rtn = (new DBHelper().ExecuteNonQueryReturn)("dbo.sp_insertAppInfo_Admin", pn);
                }
                catch (Exception ex)
                {

                    throw;
                }
                return rtn;
            }
            catch (Exception ex)
            {
                return 0;
            }
        }

        public int Update(AboutBLL data)
        {
            try
            {
                int rtn = 1;
                SqlParameter[] p = new SqlParameter[10];

                p[0] = new SqlParameter("@BranchName", data.BranchName);
                p[1] = new SqlParameter("@BranchAddress", data.BranchAddress);
                p[2] = new SqlParameter("@BranchTiming", data.BranchTiming);
                p[3] = new SqlParameter("@DeliveryNo", data.DeliveryNo);
                p[4] = new SqlParameter("@Discount", data.Discount);
                p[5] = new SqlParameter("@Discountdescription", data.Discountdescription);
                p[6] = new SqlParameter("@StatusID", data.StatusID);
                p[7] = new SqlParameter("@BrandID", data.BrandID);
                p[8] = new SqlParameter("@ID", data.ID);
                p[9] = new SqlParameter("@WhatsappNo", data.WhatsappNo);
                rtn = (new DBHelper().ExecuteNonQueryReturn)("dbo.sp_updateAppsetting_Admin", p);
                
                try
                {
                    SqlParameter[] pn = new SqlParameter[5];

                    pn[0] = new SqlParameter("@AppDescription", data.AppDescription);
                    pn[1] = new SqlParameter("@Facebook", data.Facebook);
                    pn[2] = new SqlParameter("@Twitter", data.Twitter);
                    pn[3] = new SqlParameter("@Instagram", data.Instagram);
                    pn[4] = new SqlParameter("@AppInfoID", 2);

                    rtn = (new DBHelper().ExecuteNonQueryReturn)("dbo.sp_updateAppInfo_Admin", pn);
                    
                }
                catch (Exception ex)
                {

                    throw;
                }
                return rtn;
            }
            catch (Exception ex)
            {
                return 0;
            }
        }

        //public int Delete(CategoryBLL data)
        //{
        //    try
        //    {
        //        int _obj = 0;
        //        SqlParameter[] p = new SqlParameter[2];
        //        p[0] = new SqlParameter("@id", data.CategoryID);
        //        p[1] = new SqlParameter("@LastUpdatedDate", data.LastUpdatedDate);

        //        _obj = (new DBHelper().ExecuteNonQueryReturn)("sp_DeleteCategory", p);

        //        return _obj;
        //    }
        //    catch (Exception ex)
        //    {
        //        return 0;
        //    }
        //}
    }
}
