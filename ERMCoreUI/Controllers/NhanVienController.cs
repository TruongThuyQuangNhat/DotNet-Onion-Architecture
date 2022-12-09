using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using DomainLayer.Models;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using ServicesLayer;
using ServicesLayer.NhanVienService;

namespace ERMCoreUI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class NhanVienController : ControllerBase
    {
        private readonly INhanVienService _nhanVienService;
        private readonly IWebHostEnvironment _hostingEnvironment;

        public NhanVienController(INhanVienService nhanVienService, IWebHostEnvironment hostingEnvironment)
        {
            _nhanVienService = nhanVienService;
            _hostingEnvironment = hostingEnvironment;
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
        public IActionResult TotalCountOfGetAll(
            string search,
            string chucDanh_id,
            string chucVu_id,
            string phongBan_id
        )
        {
            int result = _nhanVienService.getTotalCount(search, chucDanh_id, chucVu_id, phongBan_id);
            return Ok(result);
        }

        [HttpGet(nameof(GetAllNhanVien))]
        public IActionResult GetAllNhanVien(
            string page, 
            string limit, 
            string search, 
            string key, 
            string options,
            string chucDanh_id,
            string chucVu_id,
            string phongBan_id
            )
        {
            var result = _nhanVienService.getAllAsync(page, limit, search, key, options, chucDanh_id, chucVu_id, phongBan_id);
            if (result is not null)
            {
                return Ok(result);
            }
            return BadRequest("No records found");

        }
        [HttpPost(nameof(InsertNhanVien))]
        public IActionResult InsertNhanVien(NhanVien NhanVien)
        {
            NhanVien temp = _nhanVienService.getOne(NhanVien.Id);
            if(temp == null)
            {
                _nhanVienService.create(NhanVien);
                string JSONResult = JsonConvert.SerializeObject(NhanVien);
                return Ok(JSONResult);
            } else
            {
                return Conflict();
            }
        }
        [HttpPut(nameof(UpdateNhanVien))]
        public IActionResult UpdateNhanVien(NhanVien NhanVien)
        {
            NhanVien temp = _nhanVienService.getOne(NhanVien.Id);
            if (temp == null)
            {
                return NotFound();

            }
            _nhanVienService.update(NhanVien);
            string JSONResult = JsonConvert.SerializeObject("updation one success");
            return Ok(JSONResult);
        }
        [HttpDelete(nameof(DeleteNhanVien))]
        public IActionResult DeleteNhanVien(string Id)
        {
            NhanVien temp = _nhanVienService.getOne(Id);
            if (temp == null)
            {
                return NotFound();

            }
            _nhanVienService.delete(Id);
            string JSONResult = JsonConvert.SerializeObject("Data Deleted");
            return Ok(JSONResult);

        }

        [HttpPost]
        [DisableRequestSizeLimit]
        public async Task<IActionResult> UploadFile()
        {
            if (!Request.Form.Files.Any())
                return BadRequest("No files found in the request");

            if (Request.Form.Files.Count > 1)
                return BadRequest("Cannot upload more than one file at a time");

            if (Request.Form.Files[0].Length <= 0)
                return BadRequest("Invalid file length, seems to be empty");

            try
            {
                string webRootPath = _hostingEnvironment.WebRootPath;
                string uploadsDir = Path.Combine(webRootPath, "images");

                // wwwroot/uploads/
                if (!Directory.Exists(uploadsDir))
                    Directory.CreateDirectory(uploadsDir);

                IFormFile file = Request.Form.Files[0];
                string fileName = ContentDispositionHeaderValue.Parse(file.ContentDisposition).FileName.Trim('"');
                Random rnd = new Random();
                string random = rnd.Next(11111111, 99999999).ToString();
                string fullPath = Path.Combine(uploadsDir, random + fileName);

                var buffer = 1024 * 1024;
                using var stream = new FileStream(fullPath, FileMode.Create, FileAccess.Write, FileShare.None, buffer, useAsync: false);
                await file.CopyToAsync(stream);
                await stream.FlushAsync();

                string location = $"images/{random + fileName}";

                var result = new
                {
                    message = "Upload successful",
                    url = location
                };

                return Ok(result);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Upload failed: " + ex.Message);
            }
        }
    }
}
