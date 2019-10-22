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
    public class DrugsPrescriptionRepository: IDrugsPrescriptionRepository
    {
        ApplicationContext db;
        public DrugsPrescriptionRepository(ApplicationContext context)
        {
            this.db = context;
        }

        public void Create(DrugsPrescription dp)
        {
            db.DrugsPrescriptions.Add(dp);
            db.SaveChanges();
        }

        public DrugsPrescription Get(int idDP)
        {
            return db.DrugsPrescriptions.Where(d => d.IdDrugsPrescription == idDP).FirstOrDefault();
        }

        public IEnumerable<DrugsPrescription> GetAll()
        {
            return db.DrugsPrescriptions.ToList();
        }
    }
}
