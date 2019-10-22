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
    public class OperationRepository: IOperationRepository
    {
        ApplicationContext db;
        public OperationRepository(ApplicationContext context)
        {
            this.db = context;
        }

        public Operation Get(int idOperation)
        {
            return db.Operations.Where(d => d.IdOperation == idOperation).FirstOrDefault();
        }

        public IEnumerable<Operation> GetAll()
        {
            return db.Operations.ToList();
        }
    }
}
