using HospitalBLL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HospitalBLL.Comparers
{
    public class DrugsNameComparer : IComparer<DrugBLL>
    {
        public int Compare(DrugBLL x, DrugBLL y)
        {
           
                return Comparer<int>.Default.Compare(x.DrugsName[0], y.DrugsName[0]);
            
        }
    }
}
