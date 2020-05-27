using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Task_11.Entities;
using Task_11.Services;

namespace Task_11.Controllers
{
    [Route("api/doctors")]
    [ApiController]
    public class DoctorsController : ControllerBase
    {
        private IDoctorsDBService _service;

        public DoctorsController(IDoctorsDBService service, DoctorDBContext context)
        {
            _service = service;
            _service.InitContext(context);
        }

        [HttpGet]
        public IActionResult getDoctors()
        {
            return Ok(_service.getDoctors());
        }


        [HttpPost]
        public IActionResult addDoctor(Doctor doctor)
        {
            var res = _service.addDoctor(doctor);
            if (res.Error == "")
                return Ok(res.doctors);
            else
                return BadRequest(res.Error);
        }

        [HttpPut]
        public IActionResult modifyDoctor(Doctor doctor)
        {
            var res = _service.modifyDoctor(doctor);
            if (res.Error == "")
                return Ok(res.doctors);
            else
                return BadRequest(res.Error);
        }

        [HttpDelete]
        public IActionResult deleteDoctor(int id)
        {
            var res = _service.deleteDoctor(id);
            if (res.Error == "")
                return Ok(res.doctors);
            else
                return BadRequest(res.Error);
        }
    }
}