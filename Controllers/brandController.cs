
using System.Collections.Generic;
using StudioAdmin._Models;
using StudioAdmin.BLL._Services;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;

namespace StudioAdmin.Controllers
{
    [Route("api/[controller]")]
  
    public class brandController : ControllerBase
    {
        private readonly IWebHostEnvironment _env;
        brandService _service;
        public brandController(IWebHostEnvironment env)
        {
            _service = new brandService();
            _env = env;
        }
      

        [HttpGet("all/{brandid}")]
        public List<BrandBLL> GetAll(int brandid)
        {
            return _service.GetAll(brandid);
        }


        [HttpGet("{id}")]
        public BrandBLL Get(int id, int brandid)
        {
            return _service.Get(id, brandid);
        }

        [HttpPost]
        [Route("insert")]
        public int Post([FromBody]BrandBLL obj)
        {
            return _service.Insert(obj, _env);
        }

        [HttpPost]
        [Route("update")]
        public int Put([FromBody]BrandBLL obj)
        {
            return _service.Update(obj, _env);
        }


        [HttpPost]
        [Route("delete")]
        public int Delete(BrandBLL obj)
        {
            return _service.Delete(obj);
        }
    }
}
