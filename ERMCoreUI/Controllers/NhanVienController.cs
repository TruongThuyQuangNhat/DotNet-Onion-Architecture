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
                string uploadsDir = Path.Combine(webRootPath, "uploads");

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
