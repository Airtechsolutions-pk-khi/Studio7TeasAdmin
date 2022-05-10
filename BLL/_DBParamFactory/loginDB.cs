
using DAL.Models;
using StudioAdmin._Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using WebAPICode.Helpers;

namespace BAL.Repositories
{

    public class loginDB : baseDB
    {
        public static LoginBLL repo;
        public static DataTable _dt;
        public static DataSet _ds;

        public loginDB()
           : base()
        {
            repo = new LoginBLL();
            _dt = new DataTable();
            _ds = new DataSet();
        }

        public LoginBLL GetAuthenticateUser(string username,string password)
        {
            try
            {
                SqlParameter[] p = new SqlParameter[2];
                p[0] = new SqlParameter("@email", username);
                p[1] = new SqlParameter("@password", password);

                _ds = (new DBHelper().GetDatasetFromSP)("sp_authenticateUser_admin", p);
                if (_ds != null)
                {
                    if (_ds.Tables[0].Rows.Count > 0)
                    {
                        repo = _ds.Tables[0].DataTableToList<LoginBLL>().FirstOrDefault();
                    }
                    else repo = null;

                }
                return repo;
            }
            catch (Exception ex)
            {
                return null;
            }
        }

    }
}
