using HospitalBLL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HospitalBLL.Interfaces
{
    public interface IDrugsService
    {
        List<DrugBLL> GetAllDrags();
       
    }
}
