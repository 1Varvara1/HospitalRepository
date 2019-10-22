using HospitalDAL.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace HospitalDAL
{
    public class ClientProfile
    {
        [Key]
        [ForeignKey("ApplicationUser")]
        public string IdClientProfile { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        public string SecondName { get; set; }
        public DateTime? Birth { get; set; }
        public string Address { get; set; }
        public  ICollection<Complaint> Complaints{ get; set; }
        public  ApplicationUser ApplicationUser { get; set; }
    }
}
