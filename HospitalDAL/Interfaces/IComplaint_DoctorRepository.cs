using HospitalDAL.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HospitalDAL.Interfaces
{
    public interface IComplaint_DoctorRepository
    {
        IEnumerable<Complaint_Doctor> GetAll();
        Complaint_Doctor Get(int id);
        void Create(Complaint_Doctor complaint_doctor);
        void Delete(int id);
    }
}
