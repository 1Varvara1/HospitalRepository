using HospitalBLL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HospitalBLL.Comparers
{
    public class SurnameUserComparer : IComparer<UserBLL>
    {
        public int Compare(UserBLL x, UserBLL y)
        {
            return Comparer<int>.Default.Compare(x.Surname[0], y.Surname[0]);
        }
    }
}
