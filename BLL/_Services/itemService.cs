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
    public class itemService : baseService
    {
        itemDB _service;
        public itemService()
        {
            _service = new itemDB();
        }
        public List<ItemBLL> GetItem(int CategoryID)
        {
            try
            {
                return _service.GetItem(CategoryID);
            }
            catch (Exception ex)
            {
                return new List<ItemBLL>();
            }
        }
        public List<ItemBLL> GetAll(int brandID)
        {
            try
            {
                return _service.GetAll(brandID);
            }
            catch (Exception ex)
            {
                return new List<ItemBLL>();
            }
        }
        public List<ModifierBLL> GetModifier(int ItemID)
        {
            try
            {
                return _service.GetModifier(ItemID);
            }
            catch (Exception ex)
            {
                return new List<ModifierBLL>();
            }
        }
        public ItemBLL Get(int id, int brandID)
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
        public ItemSettingsBLL GetItemSettings(int brandID)
        {
            return _service.GetItemSettings(brandID);
        }
        public int Insert(ItemBLL data, IWebHostEnvironment _env)
        {
            try
            {
                data.Image = UploadImage(data.Image, "Items", _env);
                data.LastUpdatedDate = _UTCDateTime_SA();
                var result = _service.Insert(data);

                return result;
            }
            catch (Exception ex)
            {
                return 0;
            }
        }

        public int UpdateItemSettings(ItemSettingsBLL data)
        {
            try
            {
                
                var result = _service.UpdateItemSettings(data);

                return result;
            }
            catch (Exception ex)
            {
                return 0;
            }
        }
        public int Update(ItemBLL data, IWebHostEnvironment _env)
        {
            try
            {
                data.Image = UploadImage(data.Image, "Items", _env);
                data.LastUpdatedDate = _UTCDateTime_SA();
                var result = _service.Update(data);

                return result;
            }
            catch (Exception ex)
            {
                return 0;
            }
        }

        public int Delete(ItemBLL data)
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
