using HospitalBLL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HospitalBLL.Comparers
{
    public class BirthUserComparer : IComparer<UserBLL>
    {
        public int Compare(UserBLL x, UserBLL y)
        {
            var v = x.Birth.ToString().Substring(6, 4);
            return Comparer<int>.Default.Compare(Int32.Parse(x.Birth.ToString().Substring(6,4)),
                Int32.Parse(y.Birth.ToString().Substring(6, 4)));
        }
        //public int Compare(UserBLL x, UserBLL y)
        //{
        //    throw new NotImplementedException();  }
  
    }
}
