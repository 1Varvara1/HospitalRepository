using HospitalBLL.Models;
using HospitalDAL.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HospitalBLL.Interfaces
{
   public interface IComplaintService
    {
        List<ComplaintBLL> GetAll();
        Task<int> Create(string IdClientProfle,int idSpeciality);

        void MatchComplaintDoctor(int idComplaint, string idDoctor);


    }
}
