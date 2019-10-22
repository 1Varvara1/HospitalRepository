using HospitalDAL.Entities;
using HospitalDAL.EntityFramework;
using HospitalDAL.Interfaces;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HospitalDAL.Repositories
{
    public class DoctorRepository : IDoctorRepository
    {
        ApplicationContext db;
        public DoctorRepository(ApplicationContext context)
        {
            this.db = context;
        }

        public void Create(Doctor doctor)
        {
            db.Doctors.Add(doctor);
        }

        public Doctor Get(string idDoctor)
        { 
            var doc= db.Doctors.Where(d => d.ClientProfileIdClientProfile==idDoctor).FirstOrDefault();
            db.Entry(doc).Reference("ClientProfile").Load();
            db.Entry(doc).Collection("Complaint_Doctors").Load();
            db.Entry(doc).Reference("Speciality").Load();
            return doc;
        }

        public IEnumerable<Doctor> GetAll()
        {
            var doctors = db.Doctors.ToList();
            foreach (var d in doctors)
            {
                db.Entry(d).Reference("ClientProfile").Load();
                db.Entry(d).Collection("Complaint_Doctors").Load();
                db.Entry(d).Reference("Speciality").Load();
            }

            //var doctors = db.Doctors;
            //doctors.Include(d => d.ClientProfile);
            //doctors.Include(d => d.Complaint_Doctors);
            //doctors.Include(d=>d.Speciality);



            return doctors;
        }
    }
}
