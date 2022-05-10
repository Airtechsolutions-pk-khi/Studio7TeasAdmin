
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Models
{
    class ViewModel
    {
    }


    public class Rsp
    {
        public string description { get; set; }
        public int status { get; set; }

    }
    public partial class RspBrandList : Rsp
    {
        public IEnumerable<BrandsBLL> brands { get; set; }
    }
    public class RspAutheticate : Rsp
    {
    }
    public class BrandsBLL
    {
        public int BrandID { get; set; }
        public string Username { get; set; }
        public string Name { get; set; }
        public string Image { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public string CompanyURl { get; set; }
        public string Address { get; set; }
        public Nullable<int> StatusID { get; set; }
        public string Currency { get; set; }
        public Nullable<long> BusinessKey { get; set; }
        public string LastUpdateBy { get; set; }
        public Nullable<System.DateTime> LastUpdatedDate { get; set; }

    }



    #region Category

    public class CategoryBLL
    {
        public int CategoryID { get; set; }
        public string Name { get; set; }
        public string ArabicName { get; set; }
        public string Description { get; set; }
        public string Image { get; set; }
        public int DisplayOrder { get; set; }
        public string LastUpdatedBy { get; set; }
        public Nullable<System.DateTime> LastUpdatedDate { get; set; }
        public Nullable<int> StatusID { get; set; }
        public Nullable<int> LocationID { get; set; }
        public Nullable<int> BrandID { get; set; }
    }
    public partial class RspCategoryList : Rsp
    {
        public IEnumerable<CategoryBLL> list { get; set; }
    }
    #endregion Category
}
