using HospitalDAL.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HospitalDAL.Interfaces
{
    public interface IProcedureRepository
    {
        IEnumerable<Procedure> GetAll();
        Procedure Get(int idProcedureRepository);


    }
}
