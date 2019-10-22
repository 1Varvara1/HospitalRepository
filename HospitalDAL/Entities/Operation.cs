using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HospitalDAL.Entities
{
    public class Operation
    {
        [Key]
        public int IdOperation { get; set; }
        public string OperationName { get; set; }
        public ICollection<OperationPrescription> OperationPrescriptions { get; set; }
    }
}
