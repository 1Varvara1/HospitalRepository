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
    public class Patient_DiagnosisRepository: IPatient_DiagnosisRepository
    {
        ApplicationContext db;
        public Patient_DiagnosisRepository(ApplicationContext context)
        {
            this.db = context;
        }

        public void Create(Patient_Diagnosis Patient_Diagnosis)
        {
            db.Patient_Diagnoses.Add(Patient_Diagnosis);

            db.SaveChanges();
        }

        public Patient_Diagnosis Get(int idPatient_Diagnosis)
        {
            var pd=db.Patient_Diagnoses.Where(p => p.IdPatient_Diagnosis == idPatient_Diagnosis).
                FirstOrDefault();
            db.Entry(pd).Reference(c => c.Complaint);
            db.Entry(pd).Reference(c => c.Diagnosis);
            return pd;
        }

        public IEnumerable<Patient_Diagnosis> GetAll()
        {
             var dps= db.Patient_Diagnoses.ToList();
            foreach (var item in dps)
            {
                db.Entry(item).Reference(c => c.Complaint);
                db.Entry(item).Reference(c => c.Diagnosis);
            }
            return dps;
        }
    }
}
