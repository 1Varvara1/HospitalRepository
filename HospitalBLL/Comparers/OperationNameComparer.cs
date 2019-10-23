using HospitalBLL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HospitalBLL.Comparers
{
    public class OperationNameComparer : IComparer<OperationBLL>
    {
        public int Compare(OperationBLL x, OperationBLL y)
        {
            return Comparer<int>.Default.Compare(x.OperationName[0], y.OperationName[0]);
        }
    }
}
