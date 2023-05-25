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
    public class DeliveryBoyService : baseService
    {
        DeliveryBoyDB _service;
        public DeliveryBoyService()
        {
            _service = new DeliveryBoyDB();
        }

        public List<DeliveryBoyBLL> GetAll(int brandID)
        {
            try
            {
                return _service.GetAll(brandID);
            }
            catch (Exception ex)
            {
                return new List<DeliveryBoyBLL>();
            }
        }
        public List<DeliveryBoyBLL> GetAllBrand()
        {
            try
            {
                return _service.GetAllBrand();
            }
            catch (Exception ex)
            {
                return new List<DeliveryBoyBLL>();
            }
        }
        public BrandSettingsBLL GetItemSettings(int brandID)
        {
            return _service.GetItemSettings(brandID);
        }

        public DeliveryBoyBLL Get(int id)
        {
            try
            {
                return _service.Get(id);
            }
            catch (Exception ex)
            {
                return null;
            }
        }
        public int Insert(DeliveryBoyBLL data)
        {
            try
            {
                //data.Image = UploadImage(data.Image, "Banner", _env);
                //data.LastUpdatedDate = _UTCDateTime_SA();
                var result = _service.Insert(data);

                return result;
            }
            catch (Exception ex)
            {
                return 0;
            }
        }

        public int Update(DeliveryBoyBLL data, IWebHostEnvironment _env)
        {
            try
            {
                //data.Image = UploadImage(data.Image, "Banner", _env);
                //data.LastUpdatedDate = _UTCDateTime_SA();
                var result = _service.Update(data);

                return result;
            }
            catch (Exception ex)
            {
                return 0;
            }
        }

        public int Delete(DeliveryBoyBLL data)
        {
            try
            {
                //data.LastUpdatedDate = _UTCDateTime_SA();
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
