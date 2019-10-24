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
    class OperationPrescriptionRepository : IOperationPrescriptionRepository
    {
        ApplicationContext db;
        public OperationPrescriptionRepository(ApplicationContext context)
        {
            this.db = context;
        }
        public void Create(OperationPrescription op)
        {
            db.OperationPrescriptions.Add(op);
            db.SaveChanges();
        }

        public OperationPrescription Get(int idOP)
        {
            var dp= db.OperationPrescriptions.Where(obj => obj.IdOperationPrescription == idOP).FirstOrDefault();
            db.Entry(dp).Reference(r => r.Operation);
            dp.Operation = db.Operations.Where(o => o.IdOperation == dp.OperationIdOperation).
                FirstOrDefault();
            db.Entry(dp).Reference(r => r.Complaint);
            db.Entry(dp).Reference(r => r.Doctor);
            dp.Doctor = db.Doctors.Where(d => d.ClientProfile.IdClientProfile == dp.DoctorIdDoctor)
               .FirstOrDefault();
            return dp;
        }

        public IEnumerable<OperationPrescription> GetAll()
        {
            var dps= db.OperationPrescriptions.ToList();
            foreach (var dp in dps)
            {
                db.Entry(dp).Reference(r => r.Operation);
                dp.Operation = db.Operations.Where(o => o.IdOperation == dp.OperationIdOperation).
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
