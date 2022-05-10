
using System.Collections.Generic;
using StudioAdmin._Models;
using StudioAdmin.BLL._Services;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;

namespace StudioAdmin.Controllers
{
    [Route("api/[controller]")]
  
    public class categoryController : ControllerBase
    {
        private readonly IWebHostEnvironment _env;
        categoryService _service;
      
        public categoryController(IWebHostEnvironment env)
        {
            _service = new categoryService();
            _env = env;
        }


        [HttpGet("all/{brandid}")]
        public List<CategoryBLL> GetAll(int brandid)
        {
            return _service.GetAll(brandid);
        }


        [HttpGet("{id}/brand/{brandid}")]
        public CategoryBLL Get(int id, int brandid)
        {
            return _service.Get(id, brandid);
        }

        [HttpPost]
        [Route("insert")]
        public int Post([FromBody]CategoryBLL obj)
        {
            return _service.Insert(obj, _env);
        }

        [HttpPost]
        [Route("update")]
        public int PostUpdate([FromBody]CategoryBLL obj)
        {
            return _service.Update(obj, _env);
        }


        [HttpPost]
        [Route("delete")]
        public int PostDelete([FromBody]CategoryBLL obj)
        {
            return _service.Delete(obj);
        }
    }
}
