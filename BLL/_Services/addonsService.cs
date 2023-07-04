using BAL.Repositories;
using StudioAdmin._Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;

namespace StudioAdmin.BLL._Services
{
    public class addonsService : baseService
    {
        addonsDB _service;
        public addonsService()
        {
            _service = new addonsDB();
        }
        public List<AddonsBLL> GetAddon()
        {
            try
            {
                return _service.GetAddon();
            }
            catch (Exception ex)
            {
                return new List<AddonsBLL>();
            }
        }
        public List<AddonsBLL> GetAll(int brandID)
        {
            try
            {
                return _service.GetAll(brandID);
            }
            catch (Exception ex)
            {
                return new List<AddonsBLL>();
            }
        }
        
        public AddonsBLL Get(int id, int brandID)
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
        public int Insert(AddonsBLL data)
        {
            try
            {
                data.LastUpdatedDate = _UTCDateTime_SA();
                var result = _service.Insert(data);

                return result;
            }
            catch (Exception ex)
            {
                return 0;
            }
        }

        public int Update(AddonsBLL data)
        {
            try
            {
                data.LastUpdatedDate = _UTCDateTime_SA();
                var result = _service.Update(data);

                return result;
            }
            catch (Exception ex)
            {
                return 0;
            }
        }

        public int Delete(AddonsBLL data)
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
