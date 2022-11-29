using DomainLayer.Models;
using Microsoft.AspNetCore.Mvc;
using ServicesLayer.ChucVuService;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ERMCoreUI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ChucVuController : ControllerBase
    {
        private readonly IChucVuService _ChucVuService;
        public ChucVuController (IChucVuService ChucVuService)
        {
            _ChucVuService = ChucVuService;
        }

        [HttpGet(nameof(GetChucVu))]
        public IActionResult GetChucVu(string id)
        {
            var result = _ChucVuService.getOne(id);
            if (result is not null)
            {
                return Ok(result);
            }
            return new NotFoundResult();
        }

        [HttpGet(nameof(GetAllChucVu))]
        public IActionResult GetAllChucVu(string page, string limit, string search)
        {
            var result = _ChucVuService.getAllAsync(page, limit, search);
            if (result is not null)
            {
                return Ok(result);
            }
            return BadRequest("No records found");
        }
        [HttpPost(nameof(InsertChucVu))]
        public IActionResult InsertChucVu(ChucVu ChucVu)
        {
            _ChucVuService.create(ChucVu);
            return Ok("Data inserted");

        }
        [HttpPut(nameof(UpdateChucVu))]
        public IActionResult UpdateChucVu(ChucVu ChucVu)
        {
            _ChucVuService.update(ChucVu);
            return Ok("Updation done");

        }
        [HttpDelete(nameof(DeleteChucVu))]
        public IActionResult DeleteChucVu(string Id)
        {
            _ChucVuService.delete(Id);
            return Ok("Data Deleted");

        }
    }
}
