using HospitalBLL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HospitalBLL.Comparers
{
    public class DoctorAmountPatientsBeingTreatedComparer : IComparer<DoctorBLL>
    {
        public int Compare(DoctorBLL x, DoctorBLL y)
        {
           return Comparer<int>.Default.Compare(x.AmountBeingTreated, y.AmountBeingTreated);
        }
    }
}
