using HospitalBLL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Hospital.Models
{
    public class MatchDoctor
    {
        public ComplaintMatchBLL Complaint { get; }
        public List<DoctorBLL> Doctor { get; }

        public MatchDoctor(ComplaintMatchBLL complaint, List<DoctorBLL> doctor)
        {
            Complaint = complaint;
            Doctor = doctor;
        }
    }
}