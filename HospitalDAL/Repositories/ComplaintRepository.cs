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
    public class ComplaintRepository : IComplaintRepository
    {
        ApplicationContext db;
        public ComplaintRepository(ApplicationContext context)
        {
            this.db = context;
        }
        public void Create(Complaint complaint)
        {
            db.Complaints.Add(complaint);
        }

        public void Delete(int id)
        {
            db.Complaints.Remove(db.Complaints.Where(obj => obj.IdComplaint == id).FirstOrDefault());
        }

        public Complaint Get(int id)
        {
            // Load adding information from other entities
            var comp=db.Complaints.Where(obj=>obj.IdComplaint==id).FirstOrDefault();
            db.Entry(comp).Collection(c => c.ClientProfile).Load();
            db.Entry(comp).Reference(c => c.Speciality).Load();

            return comp;
        }

        public IEnumerable<Complaint> GetAll()
        {
            var complaints = db.Complaints.ToList();

            // Load adding information from other entities
            foreach (var comp in complaints)
            {
                db.Entry(comp).Collection(c => c.ClientProfile).Load();
                db.Entry(comp).Reference(c => c.Speciality).Load();
            }

            return complaints;
        }

    }
}
