using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HospitalDAL.Entities
{
    public class Procedure
    {
        [Key]
        public int IdProcedure { get; set; }
        public string ProcedureName { get; set; }
        public ICollection<ProcedurePrescription> PocedurePrescriptions { get; set; }
    }
}
