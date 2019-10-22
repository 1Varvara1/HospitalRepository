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
    public class DrugsRepository: IDrugsRepository
    {
        ApplicationContext db;
        public DrugsRepository(ApplicationContext context)
        {
            this.db = context;
        }
        public Drugs Get(int idDrugs)
        {
            return db.Drugs.Where(d => d.IdDrugs == idDrugs).FirstOrDefault();
        }
        public IEnumerable<Drugs> GetAll()
        {
            return db.Drugs.ToList();
        }
    }
}
