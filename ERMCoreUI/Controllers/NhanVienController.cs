using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DomainLayer.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ServicesLayer;
using ServicesLayer.NhanVienService;

namespace ERMCoreUI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class NhanVienController : ControllerBase
    {
        private readonly INhanVienService _nhanVienService;

        public NhanVienController(INhanVienService nhanVienService)
        {
            _nhanVienService = nhanVienService;
        }

        [HttpGet(nameof(GetNhanVien))]
        public IActionResult GetNhanVien(string id)
        {
            var result = _nhanVienService.getOne(id);
            if (result is not null)
            {
                return Ok(result);
            }
            return new NotFoundResult();
        }

        [HttpGet(nameof(TotalCountOfGetAll))]
        public IActionResult TotalCountOfGetAll(string search)
        {
            int result = _nhanVienService.getTotalCount(search);
            return Ok(result);
        }

        [HttpGet(nameof(GetAllNhanVien))]
        public IActionResult GetAllNhanVien(string page, string limit, string search, string key, string options)
        {
            var result = _nhanVienService.getAllAsync(page, limit, search, key, options);
            if (result is not null)
            {
                return Ok(result);
            }
            return BadRequest("No records found");

        }
        [HttpPost(nameof(InsertNhanVien))]
        public IActionResult InsertNhanVien(NhanVien NhanVien)
        {
            _nhanVienService.create(NhanVien);
            return Ok("Data inserted");

        }
        [HttpPut(nameof(UpdateNhanVien))]
        public IActionResult UpdateNhanVien(NhanVien NhanVien)
        {
            _nhanVienService.update(NhanVien);
            return Ok("Updation done");

        }
        [HttpDelete(nameof(DeleteNhanVien))]
        public IActionResult DeleteNhanVien(string Id)
        {
            _nhanVienService.delete(Id);
            return Ok("Data Deleted");

        }
    }
}
