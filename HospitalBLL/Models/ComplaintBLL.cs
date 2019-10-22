using HospitalDAL;
using HospitalDAL.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HospitalBLL.Models
{
    public class ComplaintBLL
    {
        public ClientProfile ClientProfile { get; set; }
        public string ComplaintInformation { get; set; }
        public Speciality Speciality { get; set; }
        public DateTime Date { get; set; }
        public bool? IsProccesed { get; set; }

        public ComplaintBLL()
        {

        }
        public ComplaintBLL(ClientProfile profile, Speciality speciality, string inform, DateTime date, bool? processed=false)
        {
            ClientProfile = profile;
            Speciality = speciality;
            ComplaintInformation = inform;
            Date = date;
            IsProccesed = processed;
        }
    }
}
