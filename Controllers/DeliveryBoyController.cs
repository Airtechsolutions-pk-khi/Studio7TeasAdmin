
using System.Collections.Generic;
using StudioAdmin._Models;
using StudioAdmin.BLL._Services;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;

namespace StudioAdmin.Controllers
{
    [Route("api/[controller]")]

    public class DeliveryBoyController : ControllerBase
    {
        private readonly IWebHostEnvironment _env;
        DeliveryBoyService _service;
        public DeliveryBoyController(IWebHostEnvironment env)
        {
            _service = new DeliveryBoyService();
            _env = env;
        }

        [HttpGet("all/{brandid}")]
        public List<DeliveryBoyBLL> GetAll(int brandid)
        {
            return _service.GetAll(brandid);
        }

        //[HttpGet("GetAllBrand")]
        //public List<DeliveryBoyBLL> GetAllBrand()
        //{
        //    return _service.GetAllBrand();
        //}

        //[HttpGet("settings/{brandid}")]
        //public BrandSettingsBLL GetItemSettings(int brandid)
        //{
        //    return _service.GetItemSettings(brandid);
        //}

        [HttpGet("{id}")]
        public DeliveryBoyBLL Get(int id)
        {
            return _service.Get(id);
        }

        [HttpPost]
        [Route("insert")]
        public int Post([FromBody] DeliveryBoyBLL obj)
        {
            return _service.Insert(obj);
        }

        [HttpPost]
        [Route("update")]
        public int PostUpdate([FromBody] DeliveryBoyBLL obj)
        {
            return _service.Update(obj, _env);
        }


        [HttpPost]
        [Route("delete")]
        public int PostDelete([FromBody] DeliveryBoyBLL obj)
        {
            return _service.Delete(obj);
        }
    }
}
