
using System.Collections.Generic;
using StudioAdmin._Models;
using StudioAdmin.BLL._Services;
using Microsoft.AspNetCore.Mvc;

namespace StudioAdmin.Controllers
{
    [Route("api/[controller]")]

    public class modifierController : ControllerBase
    {
        modifierService _service;
        public modifierController()
        {
            _service = new modifierService();
        }

        [HttpGet("all/{brandid}")]
        public List<ModifierBLL> GetAll(int brandid)
        {
            return _service.GetAll(brandid);
        }


        [HttpGet("{id}/brand/{brandid}")]
        public ModifierBLL Get(int id, int brandid)
        {
            return _service.Get(id, brandid);
        }

        [HttpPost]
        [Route("insert")]
        public int Post([FromBody]ModifierBLL obj)
        {
            return _service.Insert(obj);
        }

        [HttpPost]
        [Route("update")]
        public int PostUpdate([FromBody]ModifierBLL obj)
        {
            return _service.Update(obj);
        }


        [HttpPost]
        [Route("delete")]
        public int PostDelete([FromBody]ModifierBLL obj)
        {
            return _service.Delete(obj);
        }
    }
}
