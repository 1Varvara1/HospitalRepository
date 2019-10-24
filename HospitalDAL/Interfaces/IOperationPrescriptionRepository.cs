using HospitalDAL.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HospitalDAL.Interfaces
{
    public interface IOperationPrescriptionRepository
    {
        IEnumerable<OperationPrescription> GetAll();
        OperationPrescription Get(int idOP);
        void Create(OperationPrescription op);

        void Save();

    }
}
