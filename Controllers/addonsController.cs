
using System.Collections.Generic;
using StudioAdmin._Models;
using StudioAdmin.BLL._Services;
using Microsoft.AspNetCore.Mvc;

namespace StudioAdmin.Controllers
{
    [Route("api/[controller]")]

    public class addonsController : ControllerBase
    {
        addonsService _service;
        public addonsController()
        {
            _service = new addonsService();
        }

        [HttpGet("all/{brandid}")]
        public List<AddonsBLL> GetAll(int brandid)
        {
            return _service.GetAll(brandid);
        }


        [HttpGet("{id}/brand/{brandid}")]
        public AddonsBLL Get(int id, int brandid)
        {
            return _service.Get(id, brandid);
        }

        [HttpPost]
        [Route("insert")]
        public int Post([FromBody]AddonsBLL obj)
        {
            return _service.Insert(obj);
        }

        [HttpPost]
        [Route("update")]
        public int PostUpdate([FromBody]AddonsBLL obj)
        {
            return _service.Update(obj);
        }


        [HttpPost]
        [Route("delete")]
        public int PostDelete([FromBody]AddonsBLL obj)
        {
            return _service.Delete(obj);
        }
    }
}
