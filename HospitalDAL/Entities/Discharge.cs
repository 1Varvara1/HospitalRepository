using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HospitalDAL.Entities
{
    public class Discharge
    {
        [Key]
        public int IdDischarge { get; set; }

        public int ComplaintIdComplaint { get; set; }
        public Complaint Complaint { get; set; }
        public DateTime DateDisharged { get; set; }
        [ForeignKey("Diagnosis")]
        public int DiagnosisIdDiagnosis { get; set; }
        public Diagnosis Diagnosis { get; set; }
        public string Recomendations { get; set; }
    }
}
