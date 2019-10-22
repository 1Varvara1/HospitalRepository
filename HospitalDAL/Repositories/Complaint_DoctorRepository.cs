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
        }

        public void Delete(int idComplaint)
        {
            db.Complaint_Doctors.
                Remove(db.Complaint_Doctors.Where(obj => obj.IdComplaint_Doctor == idComplaint).
                FirstOrDefault());
        }

        public Complaint_Doctor Get(int idComplaint)
        {
           var complaint_d= db.Complaint_Doctors.
                Where(obj => obj.IdComplaint_Doctor == idComplaint).
                FirstOrDefault();

            db.Entry(complaint_d).Reference(c => c.Complaint).Load();
            db.Entry(complaint_d).Reference(c => c.Doctor).Load();

            return complaint_d;
        }

        public IEnumerable<Complaint_Doctor> GetAll()
        {
            var comp_doctors= db.Complaint_Doctors.ToList();
            foreach (var cd in comp_doctors)
            {
                db.Entry(cd).Reference(c => c.Complaint).Load();
                db.Entry(cd).Reference(c => c.Doctor).Load();
            }

            return comp_doctors;
        }
    }
}
