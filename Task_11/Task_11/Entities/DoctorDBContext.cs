using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Task_11.Entities
{
    public class DoctorDBContext : DbContext
    {
        public DbSet<Doctor> Doctors { get; set; }
        public DbSet<Prescription> Prescriptions { get; set; }
        public DbSet<Patient> Patients { get; set; }
        public DbSet<Medicament> Medicaments { get; set; }
        public DbSet<Prescription_Medicament> Prescriptions_Medicaments { get; set; }
        public DoctorDBContext()
        {

        }

        public DoctorDBContext(DbContextOptions options) : base(options)
        {

        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Doctor>(entity =>
            {
                entity.HasKey(e => e.IdDoctor);
                entity.Property(e => e.IdDoctor).ValueGeneratedOnAdd();
                entity.Property(e => e.FirstName).IsRequired().HasMaxLength(100);
                entity.Property(e => e.LastName).IsRequired().HasMaxLength(100);
                entity.Property(e => e.Email).IsRequired().HasMaxLength(100);

                entity.ToTable("Doctor");

                entity.HasMany(d => d.Prescriptions)
                      .WithOne(p => p.Doctor)
                      .HasForeignKey(p => p.IdDoctor)
                      .IsRequired();

                var data = new List<Doctor>();
                data.Add(new Doctor { IdDoctor = 1, FirstName = "Jan", LastName = "Kowalski", Email = "kow@jan.com" });
                data.Add(new Doctor { IdDoctor = 2, FirstName = "Steven", LastName = "Smith", Email = "healer99@gmail.com" });
                entity.HasData(data);
            });

            modelBuilder.Entity<Patient>(entity =>
            {
                entity.HasKey(e => e.IdPatient);
                entity.Property(e => e.IdPatient).ValueGeneratedOnAdd();
                entity.Property(e => e.FirstName).IsRequired().HasMaxLength(100);
                entity.Property(e => e.LastName).IsRequired().HasMaxLength(100);

                entity.ToTable("Patient");

                entity.HasMany(d => d.Prescriptions)
                      .WithOne(p => p.Patient)
                      .HasForeignKey(p => p.IdPatient)
                      .IsRequired();

                var data = new List<Patient>();
                data.Add(new Patient { IdPatient = 1, FirstName = "John", LastName = "Doe", Birthdate = DateTime.Now });
                data.Add(new Patient { IdPatient = 2, FirstName = "Gustavus", LastName = "Adolfus", Birthdate = DateTime.Now });
                entity.HasData(data);
            });

            modelBuilder.Entity<Prescription>(entity =>
            {
                entity.HasKey(e => e.IdPrescription);
                entity.Property(e => e.IdPrescription).ValueGeneratedOnAdd();

                entity.ToTable("Prescription");

                entity.HasMany(d => d.Prescriptions_Medicaments)
                      .WithOne(p => p.Prescription)
                      .HasForeignKey(p => p.IdPrescription)
                      .IsRequired();

                var data = new List<Prescription>();
                data.Add(new Prescription { IdPrescription = 1, IdPatient = 1, IdDoctor = 1, Date = DateTime.Now, DueDate = DateTime.Now });
                data.Add(new Prescription { IdPrescription = 2, IdPatient = 2, IdDoctor = 1, Date = DateTime.Now, DueDate = DateTime.Now });
                entity.HasData(data);
            });

            modelBuilder.Entity<Medicament>(entity =>
            {
                entity.HasKey(e => e.IdMedicament);
                entity.Property(e => e.IdMedicament).ValueGeneratedOnAdd();
                entity.Property(e => e.Name).IsRequired().HasMaxLength(100);
                entity.Property(e => e.Description).IsRequired().HasMaxLength(100);
                entity.Property(e => e.Type).IsRequired().HasMaxLength(100);

                entity.ToTable("Medicament");

                entity.HasMany(d => d.Prescriptions_Medicaments)
                      .WithOne(p => p.Medicament)
                      .HasForeignKey(p => p.IdMedicament)
                      .IsRequired();

                var data = new List<Medicament>();
                data.Add(new Medicament { IdMedicament = 1, Name = "Headin", Description = "Cheap", Type = "For head" });
                data.Add(new Medicament { IdMedicament = 2, Name = "Healin", Description = "Cool", Type = "For soul" });
                entity.HasData(data);
            });

            modelBuilder.Entity<Prescription_Medicament>(entity =>
            {
                entity.HasKey(e => new { e.IdMedicament, e.IdPrescription});
                entity.Property(e => e.Details).IsRequired().HasMaxLength(100);
                entity.ToTable("Prescription_Medicament");

                var data = new List<Prescription_Medicament>();
                data.Add(new Prescription_Medicament { IdMedicament = 1, IdPrescription = 1, Dose = 1, Details = "daily" });
                data.Add(new Prescription_Medicament { IdMedicament = 2, IdPrescription = 1, Dose = null, Details = "hourly" });
                data.Add(new Prescription_Medicament { IdMedicament = 2, IdPrescription = 2, Dose = 2, Details = "hourly" });
                entity.HasData(data);
            });
        }
    }
}
