using DomainLayer.Models;
using Microsoft.AspNetCore.Mvc;
using ServicesLayer.PhongBanService;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ERMCoreUI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PhongBanController : ControllerBase
    {
        private readonly IPhongBanService _phongBanService;
        public PhongBanController (IPhongBanService phongBanService)
        {
            _phongBanService = phongBanService;
        }

        [HttpGet(nameof(GetPhongBan))]
        public IActionResult GetPhongBan(string id)
        {
            var result = _phongBanService.getOne(id);
            if (result is not null)
            {
                return Ok(result);
            }
            return new NotFoundResult();
        }

        [HttpGet(nameof(GetAllPhongBan))]
        public IActionResult GetAllPhongBan()
        {
             var result1 = _phongBanService.getAllAsync().ToList();
            var test1 = new List<tempModel>();
            var list = new List<PhongBan>();
            //    list.ForEach(x => { x.listPhongBan.AddRange(result1) });

            list = result1.Take(2).ToList();

            foreach (var item in result1)
            {
                var model = new tempModel();
                model.PhongBan = item;
                model.listPhongBan = list;
                test1.Add(model);

            }
            if (test1 is not null)
            {
                return Ok(test1 ?? null);
            }
            // return test1;
            return BadRequest("No records found");
        }
        [HttpPost(nameof(InsertPhongBan))]
        public IActionResult InsertPhongBan(PhongBan PhongBan)
        {
            _phongBanService.create(PhongBan);
            return Ok("Data inserted");

        }
        [HttpPut(nameof(UpdatePhongBan))]
        public IActionResult UpdatePhongBan(PhongBan PhongBan)
        {
            _phongBanService.update(PhongBan);
            return Ok("Updation done");

        }
        [HttpDelete(nameof(DeletePhongBan))]
        public IActionResult DeletePhongBan(string Id)
        {
            _phongBanService.delete(Id);
            return Ok("Data Deleted");

        }
    }
}
