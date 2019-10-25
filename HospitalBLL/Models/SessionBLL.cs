using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HospitalBLL.Models
{
    public class SessionBLL
    {
        public ComplaintBLL Complaint{set;get;}
        public DiagnosisBLL Diagnosiis { set; get; }
        public DoctorBLL Doctor { set; get; }
        public List<DrugPrescriptionBLL> DrugPrescriptions { set; get; }
        public List<ProcedurePrescriptonBLL> ProcedurePrescriptions { set; get; }
        public List<OperationPrescriptionsBLL> OperatonPrescriptions { set; get; }
        public DischargeBLL Diascharge { set; get; }

    }
}
