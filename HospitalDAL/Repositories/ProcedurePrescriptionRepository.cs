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
    public class ProcedurePrescriptionRepository: IProcedurePrescriptionRepository
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
            return db.ProcedurePrescriptions.Where(pp => pp.IdProcedurePrescription == idProcedurePrescription).FirstOrDefault();
        }

        public IEnumerable<ProcedurePrescription> GetAll()
        {
            return db.ProcedurePrescriptions.ToList();
        }
    }
}
