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
    public class DischargeRepository: IDischargeRepository
    {
        ApplicationContext db;
        public DischargeRepository(ApplicationContext context)
        {
            this.db = context;
        }

        public void Create(Discharge discharge)
        {
            db.Discharges.Add(discharge);
            db.SaveChanges();
        }

        public  Discharge Get(int idDischarge)
        {
            var discharge = db.Discharges.Where(d=>d.IdDischarge==idDischarge).FirstOrDefault();
             db.Entry(discharge).Reference(d => d.Complaint).Load();
             db.Entry(discharge).Reference(d => d.Diagnosis).Load();
            return discharge;
        }

        public IEnumerable<Discharge> GetAll()
        {
            var discharges = db.Discharges.ToList();
            foreach(var item in discharges)
            {
                 db.Entry(item).Reference(d => d.Complaint).Load();
                 db.Entry(item).Reference(d => d.Diagnosis).Load();
            }
            return discharges;
        }

        public void Save()
        {
            db.SaveChanges();
        }

        public void UpdateDocPath(int idComplaint, string path)
        {
            var discharge = db.Discharges.Where(d => d.ComplaintIdComplaint == idComplaint).
                FirstOrDefault().DocumentPath=path;
            Save();
        }
    }
}
