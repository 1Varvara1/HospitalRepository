using HospitalDAL;
using HospitalDAL.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HospitalBLL.Models
{
    public class DoctorBLL
    {
        public ClientProfile ClientProfile { get; set; }
        public Speciality Speciality { get; set; }
        public string PathPhoto { get; set; }
        public List<Complaint_Doctor> Complaint_Doctors { get; set; }
        public int AmountPatients { get; set; }


        public DoctorBLL(ClientProfile profile, Speciality speciality, List<Complaint_Doctor> complaint_Doctors, string pathPhoto="")
        {
            ClientProfile = profile;
            Speciality = speciality;
            Complaint_Doctors = new List<Complaint_Doctor>();
            Complaint_Doctors = complaint_Doctors;
            PathPhoto = pathPhoto;
            AmountPatients = 0;
        }
    }
}
