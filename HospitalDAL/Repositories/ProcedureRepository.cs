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
    public class ProcedureRepository: IProcedureRepository
    {
        ApplicationContext db;
        public ProcedureRepository(ApplicationContext context)
        {
            this.db = context;
        }

        public Procedure Get(int idProcedureRepository)
        {
            return db.Procedures.Where(pr => pr.IdProcedure == idProcedureRepository).FirstOrDefault();
        }

        public IEnumerable<Procedure> GetAll()
        {
            return db.Procedures.ToList();
        }
    }
}
