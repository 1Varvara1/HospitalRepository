using HospitalDAL.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HospitalDAL.Interfaces
{
    public interface IDrugsPrescriptionRepository
    {
        IEnumerable<DrugsPrescription> GetAll();
        DrugsPrescription Get(int idDP);
        void Create(DrugsPrescription dp);
        void Save();
    }
}
