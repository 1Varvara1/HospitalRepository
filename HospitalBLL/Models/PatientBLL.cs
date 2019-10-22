using HospitalDAL;
using HospitalDAL.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HospitalBLL.Models
{
    public class PatientBLL
    {
        public ClientProfileBLL ClientProfile { set; get; }
        public int IdComplaint { set; get; }
        public DiagnosisBLL Diagnosis { set; get; }
        public List<DrugPrescriptionBLL> DrugsPrescriptions { set; get; }
        public List<ProcedurePrescriptonBLL> ProcedurePrescriptions { set; get; }
        public List<OperationPrescriptionsBLL> OperatonPrescriptions { set; get; }

    }
}
