using HospitalBLL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Hospital.Models
{
    public class ComplaintRegistrationService
    {
        public UserBLL UserBLL { get; }
        public List<UserBLL> Users { get; }
        public List<DoctorBLL> Doctors { get; }

        public ComplaintRegistrationService(UserBLL user, List<UserBLL> users, List<DoctorBLL> doctors )
        {
            UserBLL = user;
            Users = users;
            Doctors = doctors;
        }
    }
}