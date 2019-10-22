using HospitalDAL.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HospitalDAL.Interfaces
{
    public interface IDischargeRepository
    {
        IEnumerable<Discharge> GetAll();
        Discharge Get(int idDischarge);
        void Create(Discharge discharge);
    }
}
