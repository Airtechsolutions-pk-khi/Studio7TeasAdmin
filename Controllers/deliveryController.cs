
using System.Collections.Generic;
using StudioAdmin._Models;
using StudioAdmin.BLL._Services;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;

namespace StudioAdmin.Controllers
{
    [Route("api/[controller]")]
  
    public class deliveryController : ControllerBase
    {
        private readonly IWebHostEnvironment _env;
        deliveryService _service;
        public deliveryController(IWebHostEnvironment env)
        {
            _service = new deliveryService();
            _env = env;
        }

        [HttpGet("all/{brandid}")]
        public List<DeliveryBLL> GetAll(int brandid)
        {
            return _service.GetAll(brandid);
        }
        //[HttpGet("GetAllBrand")]
        //public List<DeliveryBLL> GetAllBrand()
        //{
        //    return _service.GetAllBrand();
        //}

        //[HttpGet("settings/{brandid}")]
        //public BrandSettingsBLL GetItemSettings(int brandid)
        //{
        //    return _service.GetItemSettings(brandid);
        //}

        [HttpGet("{id}")]
        public DeliveryBLL Get(int id)
        {
            return _service.Get(id);
        }

        [HttpPost]
        [Route("insert")]
        public int Post([FromBody] DeliveryBLL obj)
        {
            return _service.Insert(obj);
        }

        [HttpPost]
        [Route("update")]
        public int PostUpdate([FromBody] DeliveryBLL obj)
        {
            return _service.Update(obj, _env);
        }


        [HttpPost]
        [Route("delete")]
        public int PostDelete([FromBody] DeliveryBLL obj)
        {
            return _service.Delete(obj);
        }
    }
}
