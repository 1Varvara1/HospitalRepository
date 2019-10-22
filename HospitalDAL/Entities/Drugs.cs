using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HospitalDAL.Entities
{
    public class Drugs
    {
        [Key]
        public int IdDrugs { get; set; }
        public string DrugsName { get; set; }
        public ICollection<DrugsPrescription> DrugsPrescriptions { get; set; }
        public string PathPh { get; set; }

    }
}
