using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Task_11.Entities;

namespace Task_11.DTO
{
    public class DoctorServiceResponse
    {
        public List<Doctor> doctors { get; set; }
        public string Error { get; set; }
    }
}
