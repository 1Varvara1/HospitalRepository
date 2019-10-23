using HospitalBLL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HospitalBLL.Comparers
{
    public class DiagnosisNameComparer : IComparer<DiagnosisBLL>
    {
        public int Compare(DiagnosisBLL x, DiagnosisBLL y)
        {
            return Comparer<int>.Default.Compare(x.DiagnosisName[0], y.DiagnosisName[0]);
         
        }
    }
}
