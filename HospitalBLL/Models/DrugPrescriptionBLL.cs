using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HospitalBLL.Models
{
    public class DrugPrescriptionBLL
    {
        public int IdDrugsPrescription { get; set; }
        public int ComplaintIdComplaint { get; set; }
        public DrugBLL drugs { get; set; }
        public string Recomendations { get; set; }
        public DateTime? Complited { get; set; }
        public string DoctorIdDoctor { get; set; }
      

    }
}
