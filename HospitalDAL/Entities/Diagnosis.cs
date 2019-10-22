using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HospitalDAL.Entities
{
    public class Diagnosis
    {
        [Key]
        public int IdDiagnosis { get; set; }
        public string DiagnosisName { get; set; }
        public ICollection<Patient_Diagnosis> Patient_Diagnoses { get; set; }
    }
}
