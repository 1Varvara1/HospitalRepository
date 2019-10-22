using HospitalDAL.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HospitalDAL.Interfaces
{
    public interface IPatient_DiagnosisRepository
    {
        IEnumerable<Patient_Diagnosis> GetAll();
        Patient_Diagnosis Get(int idPatient_Diagnosis);
        void Create(Patient_Diagnosis Patient_Diagnosis);
    }
}
