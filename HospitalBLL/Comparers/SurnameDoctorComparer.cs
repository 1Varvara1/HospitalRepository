using HospitalBLL.Models;
using HospitalDAL.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HospitalBLL.Comparers
{
    public class SurnameDoctorComparer : IComparer<DoctorBLL>
    {
        public int Compare(DoctorBLL x, DoctorBLL y)
        {
            return Comparer<int>.Default.Compare(x.ClientProfile.Surname[0],
                y.ClientProfile.Surname[0]);
        }
    }
}
