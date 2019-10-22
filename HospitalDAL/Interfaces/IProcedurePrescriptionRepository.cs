using HospitalDAL.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HospitalDAL.Interfaces
{
    public interface IProcedurePrescriptionRepository
    {
        IEnumerable<ProcedurePrescription> GetAll();
        ProcedurePrescription Get(int idProcedurePrescription);
        void Create(ProcedurePrescription ProcedurePrescription);
    }
}
