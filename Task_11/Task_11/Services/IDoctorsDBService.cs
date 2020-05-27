using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Task_11.DTO;
using Task_11.Entities;

namespace Task_11.Services
{
    public interface IDoctorsDBService
    {
        void InitContext(DoctorDBContext context);
        DoctorServiceResponse getDoctors();
        DoctorServiceResponse addDoctor(Doctor doctor);
        DoctorServiceResponse modifyDoctor(Doctor doctor);
        DoctorServiceResponse deleteDoctor(int id);
    }
}
