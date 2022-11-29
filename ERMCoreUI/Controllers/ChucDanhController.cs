using DomainLayer.Models;
using Microsoft.AspNetCore.Mvc;
using ServicesLayer.ChucDanhService;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ERMCoreUI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ChucDanhController : ControllerBase
    {
        private readonly IChucDanhService _chucDanhService;
        public ChucDanhController(IChucDanhService chucDanhService)
        {
            _chucDanhService = chucDanhService;
        }
        [HttpGet(nameof(GetChucDanh))]
        public IActionResult GetChucDanh(string id)
        {
            var result = _chucDanhService.getOne(id);
            if (result is not null)
            {
                return Ok(result);
            }
            return new NotFoundResult();
        }

        [HttpGet(nameof(GetAllChucDanh))]
        public IActionResult GetAllChucDanh(string page, string limit, string search)
        {
            var result = _chucDanhService.getAllAsync(page, limit, search);
            if (result is not null)
            {
                return Ok(result);
            }
            return BadRequest("No records found");
        }
        [HttpPost(nameof(InsertChucDanh))]
        public IActionResult InsertChucDanh(ChucDanh ChucDanh)
        {
            _chucDanhService.create(ChucDanh);
            return Ok("Data inserted");

        }
        [HttpPut(nameof(UpdateChucDanh))]
        public IActionResult UpdateChucDanh(ChucDanh ChucDanh)
        {
            _chucDanhService.update(ChucDanh);
            return Ok("Updation done");

        }
        [HttpDelete(nameof(DeleteChucDanh))]
        public IActionResult DeleteChucDanh(string Id)
        {
            _chucDanhService.delete(Id);
            return Ok("Data Deleted");

        }
    }
}
