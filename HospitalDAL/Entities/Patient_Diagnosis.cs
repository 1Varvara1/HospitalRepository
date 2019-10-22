using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HospitalDAL.Entities
{
    public class Patient_Diagnosis
    {
        [Key]
        public int IdPatient_Diagnosis { get; set; }

        
        public int ComplaintIdComplaint { get; set; }
        public Complaint Complaint { get; set; }

      
        public int DiagnosisIdDiagnosis { get; set; }
        public Diagnosis Diagnosis { get; set; }
    }
}
