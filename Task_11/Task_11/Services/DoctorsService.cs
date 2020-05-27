using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Task_11.DTO;
using Task_11.Entities;

namespace Task_11.Services
{
    public class DoctorsService : IDoctorsDBService
    {
        DoctorDBContext context;
        public void InitContext(DoctorDBContext _context)
        {
            context = _context;
        }

        public DoctorServiceResponse getDoctors()
        {
            return new DoctorServiceResponse
            {
                doctors = context.Doctors.ToList(),
                Error = ""
            };
        }

        public DoctorServiceResponse addDoctor(Doctor doctor)
        {
            if (context.Doctors.Any(s => s.IdDoctor == doctor.IdDoctor))
            {
                return new DoctorServiceResponse
                {
                    doctors = null,
                    Error = "There already is doctor with this index"
                };
            }
            context.Add<Doctor>(doctor);
            context.SaveChanges();
            return getDoctors();
        }

        public DoctorServiceResponse modifyDoctor(Doctor doctor)
        {
            if (!context.Doctors.Any(s => s.IdDoctor == doctor.IdDoctor))
            {
                return new DoctorServiceResponse
                {
                    doctors = null,
                    Error = "There are no doctors with this id"
                };
            }
            context.Entry(doctor).State = EntityState.Modified;
            context.SaveChanges();
            return getDoctors();
        }


        public DoctorServiceResponse deleteDoctor(int id)
        {
            if (!context.Doctors.Any(s => s.IdDoctor == id))
            {
                return new DoctorServiceResponse
                {
                    doctors = null,
                    Error = "There are no students with this id"
                };
            }
            var prescripts = context.Prescriptions.Where(p => p.IdDoctor == id);
            var prescript_medics = (from p_m in context.Prescriptions_Medicaments
                                    join p in context.Prescriptions on p_m.IdPrescription equals p.IdPrescription
                                    where p.IdDoctor == id 
                                    select p_m);
            context.RemoveRange(prescript_medics);
            context.RemoveRange(prescripts);

            var stud = context.Doctors.First(s => s.IdDoctor == id);
            context.Entry(stud).State = EntityState.Deleted;
            context.SaveChanges();
            return getDoctors();
        }
    }
}
