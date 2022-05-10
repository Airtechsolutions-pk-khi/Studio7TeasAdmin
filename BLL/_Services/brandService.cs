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
    public class brandService : baseService
    {
        brandDB _service;
        public brandService()
        {
            _service = new brandDB();
        }

        public List<BrandBLL> GetAll(int brandID)
        {
            try
            {
                return _service.GetAll(brandID);
            }
            catch (Exception ex)
            {
                return new List<BrandBLL>();
            }
        }
        
        public BrandBLL Get(int id, int brandID)
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
        public int Insert(BrandBLL data,IWebHostEnvironment _env)
        {
            try
            {
                data.CompanyURl = UploadImage(data.CompanyURl, "Brand", _env);
                data.Image = UploadImage(data.Image, "Brand", _env);
                data.LastUpdatedDate = _UTCDateTime_SA();
                var result = _service.Insert(data);

                return result;
            }
            catch (Exception ex)
            {
                return 0;
            }
        }

        public int Update(BrandBLL data, IWebHostEnvironment _env)
        {
            try
            {
                data.CompanyURl = UploadImage(data.CompanyURl, "Brand", _env);
                data.Image = UploadImage(data.Image, "Brand", _env);
                data.LastUpdatedDate = _UTCDateTime_SA();
                var result = _service.Update(data);
                return result;
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
