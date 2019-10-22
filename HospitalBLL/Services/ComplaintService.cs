using HospitalBLL.Interfaces;
using HospitalBLL.Models;
using HospitalDAL.Entities;
using HospitalDAL.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HospitalBLL.Services
{
    public class ComplaintService: IComplaintService
    {
        IUnitOfWork Database { get; set; }

        public ComplaintService(IUnitOfWork uow)
        {
            Database = uow;
        }

        public List<ComplaintBLL> GetAll()
        {
            var complaints=Database.ComplaintRepository.GetAll().ToList();
            var complaintsBLL = new List<ComplaintBLL>();
            foreach (var item in complaints)
            {
                complaintsBLL.Add(new ComplaintBLL(item.ClientProfile, item.Speciality,
                    item.ComplaintInformation, item.Date, item.IsProccesed));
            }
            return complaintsBLL;
        }
    }
}
