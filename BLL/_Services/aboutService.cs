using BAL.Repositories;
using StudioAdmin._Models;
using Microsoft.AspNetCore.Hosting;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;

namespace StudioAdmin.BLL._Services
{
    public class aboutService : baseService
    {
        aboutDB _service;
        public aboutService()
        {
            _service = new aboutDB();
        }

        //public List<CategoryBLL> GetAll(int brandID)
        //{
        //    try
        //    {
        //        return _service.GetAll(brandID);
        //    }
        //    catch (Exception ex)
        //    {
        //        return new List<CategoryBLL>();
        //    }
        //}

        public AboutBLL Get(int brandID)
        {
            try
            {
                return _service.Get(brandID);
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
                //data.Image = UploadImage(data.Image, "Category", _env);
                //data.LastUpdatedDate = _UTCDateTime_SA();
                var result = _service.Insert(data);

                return result;
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
                //data.Image = UploadImage(data.Image, "Category", _env);
                //data.LastUpdatedDate = _UTCDateTime_SA();
                var result = _service.Update(data);

                return result;
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
        //        data.LastUpdatedDate = _UTCDateTime_SA();
        //        var result = _service.Delete(data);

        //        return result;
        //    }
        //    catch (Exception ex)
        //    {
        //        return 0;
        //    }
        //}

    }
}
