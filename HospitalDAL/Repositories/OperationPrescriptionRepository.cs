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
        }

        public OperationPrescription Get(int idOP)
        {
            return db.OperationPrescriptions.Where(obj => obj.IdOperationPrescription == idOP).FirstOrDefault();
        }

        public IEnumerable<OperationPrescription> GetAll()
        {
            return db.OperationPrescriptions.ToList();
        }
    }
}
