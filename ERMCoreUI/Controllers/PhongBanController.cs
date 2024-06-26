﻿using DomainLayer.Models;
using Microsoft.AspNetCore.Mvc;
using ServicesLayer.PhongBanService;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json;
using System.Text.Json.Serialization;
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
            List<tempModel> test1 = _phongBanService.getAllAsync();
            
            if (test1 != null)
            {
                return Ok(test1);
            }
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
