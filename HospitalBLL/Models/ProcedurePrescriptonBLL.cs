using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HospitalBLL.Models
{
    public class ProcedurePrescriptonBLL
    {
        public int IdProcedurePrescription { get; set; }

        public int ComplaintIdComplaint { get; set; }
    
        public ProcedureBLL procedure { get; set; }
        public int procedureId { get; set; }
        public string Recomendations { get; set; }
        public DateTime? Complited { get; set; }

        public string DoctorIdDoctor { get; set; }

    }
}
