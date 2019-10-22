using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HospitalDAL.Entities
{
    public class Complaint_Doctor
    {
        [Key]
        [ForeignKey("Complaint")]
        public int ComplaintIdComplaint { get; set; }
    
        public virtual Complaint Complaint { get; set; }
        public int DoctorIdDoctor { get; set; }

        public Doctor Doctor { get; set; }
       



    }
}
