using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Hospital.Models
{
    public class RegisterModel
    {
        [Required]
        public string Email { get; set; }
        [Required]
        public DateTime Birth { get; set; }
        [Required]
        public string Address { get; set; }
        [Required]
        public string Name { get; set; }
        [Required]
        public string Surname { get; set; }
        [Required]
        public string SecondName { get; set; }
    }
}