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
        }

        public Patient_Diagnosis Get(int idPatient_Diagnosis)
        {
            return db.Patient_Diagnoses.Where(pd => pd.IdPatient_Diagnosis == idPatient_Diagnosis).FirstOrDefault();
        }

        public IEnumerable<Patient_Diagnosis> GetAll()
        {
            return db.Patient_Diagnoses.ToList();
            
        }
    }
}
