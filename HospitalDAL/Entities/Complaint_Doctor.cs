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
        public int IdComplaint_Doctor { get; set;}
       
        public int ComplaintIdComplaint { get; set; }
        public  Complaint Complaint { get; set; }

    
        public string DoctorIdDoctor { get; set; }

        public ClientProfile Doctor { get; set; }
       



    }
}
