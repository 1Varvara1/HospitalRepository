using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HospitalDAL.Entities
{
    public class Complaint
    {
        [Key]
        public int IdComplaint { get; set; }
      
        public string ClientProfileIdClientProfile { get; set; }
        public  ICollection<ClientProfile> ClientProfile { get; set; }
        public string ComplaintInformation { get; set; }
      
        public int SpecialityIdSpeciality { get; set; }
        public Speciality Speciality{ get; set; }
        public DateTime Date { get; set; }
        public bool? IsProccesed { get; set; }
    }
}
