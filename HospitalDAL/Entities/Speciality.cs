using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HospitalDAL.Entities
{
    public class Speciality
    {
        [Key]
        public int IdSpeciality { get; set; }
        public string NameSpeciality { get; set; }
        public ICollection<Complaint> Complaints { get; set; }
        public ICollection<Doctor> Doctors { get; set; }
        public string PathPh { get; set; }
    }
}
