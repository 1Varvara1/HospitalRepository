using HospitalDAL.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HospitalDAL.Interfaces
{
    public interface IOperationRepository
    {
        IEnumerable<Operation> GetAll();
        Operation Get(int idOperation);
    }
}
