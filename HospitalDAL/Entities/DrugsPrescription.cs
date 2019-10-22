using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HospitalDAL.Entities
{
    public class DrugsPrescription
    {
        [Key]
        public int IdDrugsPrescription{ get; set;}
   
        public int ComplaintIdComplaint { get; set; }
        public Complaint Complaint { get; set; }
      
        public int DrugsIdDrugs { get; set; }
        public Drugs Drugs { get; set; }
        public  string Recomendations { get; set; }
        public DateTime? Complited { get; set; }
      
        public string DoctorIdDoctor { get; set; }
        public Doctor Doctor { get; set; }
    }
}
