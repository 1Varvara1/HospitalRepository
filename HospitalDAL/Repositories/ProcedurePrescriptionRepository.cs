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
    public class ProcedurePrescriptionRepository : IProcedurePrescriptionRepository
    {
        ApplicationContext db;
        public ProcedurePrescriptionRepository(ApplicationContext context)
        {
            this.db = context;
        }

        public void Create(ProcedurePrescription ProcedurePrescription)
        {
            db.ProcedurePrescriptions.Add(ProcedurePrescription);
            db.SaveChanges();
        }

        public ProcedurePrescription Get(int idProcedurePrescription)
        {
            var dp = db.ProcedurePrescriptions.Where(pp => pp.IdProcedurePrescription == idProcedurePrescription).FirstOrDefault();

            db.Entry(dp).Reference(r => r.Procedure);
            dp.Procedure = db.Procedures.Where(p => p.IdProcedure == dp.ProcedureIdProcedure).
                FirstOrDefault();
            db.Entry(dp).Reference(r => r.Complaint);
            db.Entry(dp).Reference(r => r.Doctor);
            dp.Doctor = db.Doctors.Where(d => d.ClientProfile.IdClientProfile == dp.DoctorIdDoctor)
              .FirstOrDefault();


            return dp;
        }

        public IEnumerable<ProcedurePrescription> GetAll()
        {
            var dps = db.ProcedurePrescriptions.ToList();
            foreach (var dp in dps)
            {
                db.Entry(dp).Reference(r => r.Procedure);
                dp.Procedure = db.Procedures.Where(p => p.IdProcedure == dp.ProcedureIdProcedure).
               FirstOrDefault();
                db.Entry(dp).Reference(r => r.Complaint);
                db.Entry(dp).Reference(r => r.Doctor);
                dp.Doctor = db.Doctors.Where(d => d.ClientProfile.IdClientProfile == dp.DoctorIdDoctor)
              .FirstOrDefault();
            }

            return dps;
        }

        public void Save()
        {
            db.SaveChanges();
        }
    }
}
