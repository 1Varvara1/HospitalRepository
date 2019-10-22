using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HospitalDAL.Entities
{
    public class OperationPrescription
    {
        [Key]
        public int IdOperationPrescription { get; set; }

      
        public int ComplaintIdComplaint { get; set; }
        public Complaint Complaint { get; set; }

        public int OperationIdOperation { get; set; }
       
        public string Recomendations { get; set; }

        public  Operation Operation { get; set; }
      
        public DateTime? Complited { get; set; }
      
        public string DoctorIdDoctor { get; set; }
        public  Doctor Doctor { get; set; }

    }
}
