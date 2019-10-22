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
    public class DiagnosisRepository : IDiagnosisRepository
    {
        ApplicationContext db;
        public DiagnosisRepository(ApplicationContext context)
        {
            this.db = context;
        }

        public Diagnosis Get(int idDiagnosis)
        {
            return db.Diagnoses.Where(d => d.IdDiagnosis == idDiagnosis).FirstOrDefault();
        }

        public IEnumerable<Diagnosis> GetAll()
        {
            return db.Diagnoses.ToList();
        }
    }
}
