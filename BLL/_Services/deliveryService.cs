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
    public class deliveryService : baseService
    {
        deliveryDB _service;
        public deliveryService()
        {
            _service = new deliveryDB();
        }

        public List<DeliveryBLL> GetAll(int brandID)
        {
            try
            {
                return _service.GetAll(brandID);
            }
            catch (Exception ex)
            {
                return new List<DeliveryBLL>();
            }
        }
        public List<DeliveryBLL> GetAllBrand()
        {
            try
            {
                return _service.GetAllBrand();
            }
            catch (Exception ex)
            {
                return new List<DeliveryBLL>();
            }
        }
        public BrandSettingsBLL GetItemSettings(int brandID)
        {
            return _service.GetItemSettings(brandID);
        }

        public DeliveryBLL Get(int id )
        {
            try
            {
                return _service.Get(id );
            }
            catch (Exception ex)
            {
                return null;
            }
        }
        public int Insert(DeliveryBLL data)
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

        public int Update(DeliveryBLL data, IWebHostEnvironment _env)
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

        public int Delete(DeliveryBLL data)
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
