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
    public class SpecialityRepository: ISpecialityRepository
    {
        ApplicationContext db;
        public SpecialityRepository(ApplicationContext context)
        {
            this.db = context;
        }

        public Speciality Get(int idSpeciality)
        {
           return db.Specialities.Where(s => s.IdSpeciality == idSpeciality).FirstOrDefault();
        }

        public IEnumerable<Speciality> GetAll()
        {
            return db.Specialities.ToList();
        }
    }
}
