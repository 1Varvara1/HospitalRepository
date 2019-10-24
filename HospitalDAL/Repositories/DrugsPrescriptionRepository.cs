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
            var dp= db.DrugsPrescriptions.Where(d => d.IdDrugsPrescription == idDP).FirstOrDefault();
            db.Entry(dp).Reference(r => r.Drugs);
            dp.Drugs = db.Drugs.Where(d => d.IdDrugs == dp.DrugsIdDrugs).FirstOrDefault();
            db.Entry(dp).Reference(r => r.Complaint);
            db.Entry(dp).Reference(r => r.Doctor);
            dp.Doctor = db.Doctors.Where(d => d.ClientProfile.IdClientProfile == dp.DoctorIdDoctor)
                .FirstOrDefault();
            return dp;
        }

        public IEnumerable<DrugsPrescription> GetAll()
        {
           var dps= db.DrugsPrescriptions.ToList();
            foreach (var dp in dps)
            {
                db.Entry(dp).Reference(r => r.Drugs);
                dp.Drugs = db.Drugs.Where(d => d.IdDrugs == dp.DrugsIdDrugs).FirstOrDefault();
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
