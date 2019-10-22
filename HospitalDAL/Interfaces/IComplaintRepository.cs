using HospitalDAL.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HospitalDAL.Interfaces
{
    public interface IComplaintRepository: ISave
    {
        IEnumerable<Complaint> GetAll();
        Complaint Get(int idComplaint);
        void Create(Complaint complaint);
        void Delete(int idComplaint);
       // void Update()
    }
}
