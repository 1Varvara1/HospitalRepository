using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HospitalBLL.Models
{
    public class DischargeBLL
    {
 
        public ComplaintBLL Complaint { get; set; }
        public DateTime DateDisharged { get; set; }
        public DiagnosisBLL Diagnosis { get; set; }
        public string Recomendations { get; set; }
    }
}
