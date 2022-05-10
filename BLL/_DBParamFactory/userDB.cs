
using DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BAL.Repositories
{

    public class userDB : baseDB
    {
        //public userRepository()
        //    : base()
        //{
        //    DBContext = new DB_A35721_lunchboxDBEntities();
        //}

        //public userRepository(DB_A35721_lunchboxDBEntities contextDB)
        //    : base(contextDB)
        //{
        //    DBContext = contextDB;
        //}
        //public int DeleteSubUser(Nullable<int> subUserID, string lastUpdatedBy, Nullable<System.DateTime> lastUpdatedDate, string companyCode, Nullable<int> locationID)
        //{

        //    try
        //    {
        //        SubUser subuser = DBContext.SubUsers.Where(x => x.SubUserID == subUserID && x.StatusID == 1 ).FirstOrDefault();
        //        subuser.LastUpdatedBy = lastUpdatedBy;
        //        subuser.LastUpdatedDate = Convert.ToDateTime(lastUpdatedDate);
        //        subuser.StatusID = 3;
        //        DBContext.SubUsers.Attach(subuser);
        //        //DBContext.UpdateOnly<SubUser>(
        //        //    subuser, x => x.LastUpdatedBy, x => x.LastUpdatedDate, x => x.StatusID);

        //        DBContext.SaveChanges();
        //        return 1;
        //    }
        //    catch (Exception ex)
        //    {
        //        ErrorLog(ex, "UserRepository/DeleteSubUser", "Exception");
        //        return 0;
        //    }
        //}

        //public IEnumerable<SubUserList> GetSubUsers(int LocationID)
        //{
        //    try
        //    {
        //        var modal = DBContext.SubUsers.Where(x => x.LocationID == LocationID && x.StatusID == 1
        //        && x.StatusID == 1)
        //           .AsEnumerable().Select(x => new SubUserList
        //           {
        //               RowID = x.RowID,
        //               UserName = x.UserName,
        //               FirstName = x.FirstName,
        //               LastName = x.LastName,
        //               Email = x.Email,
        //               Passcode = x.Passcode,
        //               UserType = x.UserType,
        //               SubUserID = x.SubUserID,
        //               LocationID = x.LocationID,
        //               GroupName = ""
        //           }).ToList();
        //        return modal;
        //    }
        //    catch (Exception ex)
        //    {
        //        ErrorLog(ex, "UserRepository/GetSubUsers", "Exception");
        //        return null;
        //    }
        //}
        //public IEnumerable<Role_Group> GetRoleGroup(int UserID)
        //{
        //    try
        //    {
        //        var modal = DBContext.Role_Group.Where(x => x.UserID == UserID && x.StatusID == 1 
        //        && x.StatusID == 1)
        //           .AsEnumerable().Select(x => new Role_Group
        //           {
        //               RowID = x.RowID,
        //               GroupID = x.GroupID,
        //               GroupName = x.GroupName,
        //               StatusID = x.StatusID,
        //               UserID = x.UserID

        //           }).ToList();
        //        return modal;
        //    }
        //    catch (Exception ex)
        //    {
        //        ErrorLog(ex, "UserRepository/GetRoleGroup", "Exception");
        //        return null;
        //    }
        //}
    

        //public int InsertSubUser(SubUserList subuser)
        //{
        //    try
        //    {
        //        SubUser obj = new SubUser();
        //        obj.UserName = subuser.UserName;
        //        obj.FirstName = subuser.FirstName;
        //        obj.LastName = subuser.LastName;
        //        obj.UserType = subuser.UserType;
        //        obj.Designation = subuser.Designation;
        //        obj.RowID = subuser.RowID;
        //        obj.SubUserID = subuser.SubUserID==null?0:int.Parse(subuser.SubUserID.ToString());
        //        obj.UserID = subuser.UserID;
        //        obj.ImagePath = subuser.ImagePath;
        //        obj.Password = subuser.Password;
        //        obj.Email = subuser.Email;
        //        obj.Address = subuser.Address;
        //        obj.CityID = subuser.CityID;
        //        obj.CountryID = subuser.CountryID;
        //        obj.GroupID = subuser.GroupID;
        //        obj.Passcode = subuser.Passcode;
        //        obj.TimeZoneID = subuser.TimeZoneID;
        //        obj.LastUpdatedBy = subuser.LastUpdatedBy;
        //        obj.LastUpdatedDate = subuser.LastUpdatedDate;
        //        obj.StatusID = subuser.StatusID;
        //        obj.CompanyCode = subuser.CompanyCode;
        //        obj.DateTo = subuser.DateTo;
        //        obj.DateFrom = subuser.DateFrom;
        //        obj.Zipcode = subuser.Zipcode;
        //        obj.States = subuser.States;
        //        obj.LocationID = subuser.LocationID;



        //        SubUser data = DBContext.SubUsers.Add(obj);

        //        return data.SubUserID;
        //    }
        //    catch (Exception ex)
        //    {
        //        ErrorLog(ex, "UserRepository/InsertSubUser", "Exception");
        //        return 0;
        //    }
        //}
        //public int UpdateSubUser(SubUserList subuser)
        //{
        //    try
        //    {

        //        //SubUser data =
        //        //    DBContext.Locations.Add(subuser);

        //        return 0;
        //        //data.RowID;
        //    }
        //    catch (Exception ex)
        //    {
        //        ErrorLog(ex, "UserRepository/UpdateSubUser", "Exception");
        //        return 0;
        //    }
        //}
    }
}
