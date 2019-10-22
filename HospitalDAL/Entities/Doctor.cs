using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HospitalDAL.Entities
{
    public class Doctor
    {
        [Key]
         public int IdDoctor { get; set; }
        public string ClientProfileIdClientProfile { get; set; }
        public ClientProfile ClientProfile { get; set; }
        public int SpecialityIdSpeciality { get; set; }
        public  Speciality Speciality { get; set; }
        public  ICollection<Complaint_Doctor> Complaint_Doctors { get; set; }
        public string PathPhoto { get; set; }
        public  ICollection<DrugsPrescription> DrugsPrescriptions { get; set; }
        public ICollection<ProcedurePrescription> ProcedurePrescription { get; set; }
        public ICollection<OperationPrescription> OperationPrescription { get; set; }

    }
}
