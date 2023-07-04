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
    public class categoryService : baseService
    {
        categoryDB _service;
        public categoryService()
        {
            _service = new categoryDB();
        }
        public List<CategoryBLL> GetCategory()
        {
            try
            {
                return _service.GetCategory();
            }
            catch (Exception ex)
            {
                return new List<CategoryBLL>();
            }
        }
        public List<CategoryBLL> GetAll(int brandID)
        {
            try
            {
                return _service.GetAll(brandID);
            }
            catch (Exception ex)
            {
                return new List<CategoryBLL>();
            }
        }
        
        public CategoryBLL Get(int id, int brandID)
        {
            try
            {
                return _service.Get(id, brandID);
            }
            catch (Exception ex)
            {
                return null;
            }
        }
        public int Insert(CategoryBLL data, IWebHostEnvironment _env)
        {
            try
            {
                data.Image = UploadImage(data.Image, "Category", _env);
                data.LastUpdatedDate = _UTCDateTime_SA();
                var result = _service.Insert(data);

                return result;
            }
            catch (Exception ex)
            {
                return 0;
            }
        }

        public int Update(CategoryBLL data, IWebHostEnvironment _env)
        {
            try
            {
                data.Image = UploadImage(data.Image, "Category", _env);
                data.LastUpdatedDate = _UTCDateTime_SA();
                var result = _service.Update(data);

                return result;
            }
            catch (Exception ex)
            {
                return 0;
            }
        }

        public int Delete(CategoryBLL data)
        {
            try
            {
                data.LastUpdatedDate = _UTCDateTime_SA();
                var result = _service.Delete(data);

                return result;
            }
            catch (Exception ex)
            {
                return 0;
            }
        }

    }
}
