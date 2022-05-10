
using System.Collections.Generic;
using StudioAdmin._Models;
using StudioAdmin.BLL._Services;
using Microsoft.AspNetCore.Mvc;

namespace StudioAdmin.Controllers
{
    [Route("api/[controller]")]
  
    public class locationController : ControllerBase
    {
        locationService _service;
        public locationController()
        {
            _service = new locationService();
        }

        [HttpGet("all/{brandid}")]
        public List<LocationBLL> GetAll(int brandid)
        {
            return _service.GetAll(brandid);
        }


        [HttpGet("{id}/brand/{brandid}")]
        public LocationBLL Get(int id, int brandid)
        {
            return _service.Get(id, brandid);
        }

        [HttpPost]
        [Route("insert")]
        public int Post([FromBody]LocationBLL obj)
        {
            return _service.Insert(obj);
        }

        [HttpPost]
        [Route("update")]
        public int PostUpdate([FromBody]LocationBLL obj)
        {
            return _service.Update(obj);
        }


        [HttpPost]
        [Route("delete")]
        public int PostDelete([FromBody]LocationBLL obj)
        {
            return _service.Delete(obj);
        }
    }
}
