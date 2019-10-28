using HospitalDAL.Entities;
using HospitalDAL.EntityFramework;
using HospitalDAL.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HospitalDAL.Repositories
{
    public class Complaint_DoctorRepository : IComplaint_DoctorRepository
    {
        ApplicationContext db;
        public Complaint_DoctorRepository(ApplicationContext context)
        {
            this.db = context;
        }
        public void Create(Complaint_Doctor complaint_doctor)
        {
            db.Complaint_Doctors.Add(complaint_doctor);
            db.SaveChanges();
        }

        public void Delete(int idComplaint)
        {
            db.Complaint_Doctors.
                Remove(db.Complaint_Doctors.Where(obj => obj.ComplaintIdComplaint == idComplaint).
                FirstOrDefault());
            db.SaveChanges();
        }

        public Complaint_Doctor Get(int idComplaint)
        {
           var complaint_d= db.Complaint_Doctors.
                Where(obj => obj.ComplaintIdComplaint == idComplaint).
                FirstOrDefault();

            db.Entry(complaint_d).Reference(c => c.Complaint).Load();
            db.Entry(complaint_d).Reference(c => c.Doctor).Load();
            db.Entry(complaint_d.Complaint).Reference(c => c.ClientProfile).Load();

            return complaint_d;
        }

        public IEnumerable<Complaint_Doctor> GetAll()
        {
            var comp_doctors= db.Complaint_Doctors.ToList();
            foreach (var cd in comp_doctors)
            {
                db.Entry(cd).Reference(c => c.Complaint).Load();
                db.Entry(cd).Reference(c => c.Doctor).Load();
                db.Entry(cd.Doctor).Reference(c => c.ClientProfile).Load();
                db.Entry(cd.Doctor.ClientProfile).Reference(c => c.ApplicationUser).Load();
                db.Entry(cd.Doctor).Reference(c => c.Speciality).Load();
                db.Entry(cd.Complaint).Reference(c => c.ClientProfile).Load();
             //   cd.Doctor.ClientProfile=db.ClientProfiles.Where()
            }

            return comp_doctors;
        }
    }
}
