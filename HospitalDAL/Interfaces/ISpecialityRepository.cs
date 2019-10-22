using HospitalDAL.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HospitalDAL.Interfaces
{
    public interface ISpecialityRepository
    {
        IEnumerable<Speciality> GetAll();
        Speciality Get(int idSpeciality);
    }
}
