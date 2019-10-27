using HospitalDAL.Entities;
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
        public Doctor Doctor { set; get; }
        public List<DrugPrescriptionBLL> DrugPrescriptions { set; get; }
        public List<ProcedurePrescriptonBLL> ProcedurePrescriptions { set; get; }
        public List<OperationPrescriptionsBLL> OperatonPrescriptions { set; get; }
        public DischargeBLL Diascharge { set; get; }

        public SessionBLL()
        {
            DrugPrescriptions = new List<DrugPrescriptionBLL>();
            ProcedurePrescriptions = new List<ProcedurePrescriptonBLL>();
            OperatonPrescriptions = new List<OperationPrescriptionsBLL>();
        }
    }
}
