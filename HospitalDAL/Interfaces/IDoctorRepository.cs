using HospitalDAL.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HospitalDAL.Interfaces
{
    public interface IDoctorRepository
    {
        IEnumerable<Doctor> GetAll();
        Doctor Get(string idDoctor);
        void Create(Doctor doctor);
    
    }
}
