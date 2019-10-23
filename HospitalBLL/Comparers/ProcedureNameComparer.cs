using HospitalBLL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HospitalBLL.Comparers
{
    public class ProcedureNameComparer : IComparer<ProcedureBLL>
    {
        public int Compare(ProcedureBLL x, ProcedureBLL y)
        {
            return Comparer<int>.Default.Compare(x.ProcedureName[0], y.ProcedureName[0]);
        }
    }
}
