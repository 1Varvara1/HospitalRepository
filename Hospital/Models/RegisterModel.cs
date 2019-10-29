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
        [Display(Name = "Дата рождения")]
        [Required]
        public DateTime Birth { get; set; }
        [Display(Name = "Адрес")]
        [Required]
        public string Address { get; set; }
        [Display(Name = "Имя")]
        [Required]
        public string Name { get; set; }
        [Display(Name = "Фамиля")]
        [Required]
        public string Surname { get; set; }
        [Display(Name = "Отчество")]
        [Required]
        public string SecondName { get; set; }
       
    }
}