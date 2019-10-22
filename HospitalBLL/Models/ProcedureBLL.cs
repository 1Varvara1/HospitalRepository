using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HospitalBLL.Models
{
    public class ProcedureBLL
    {
        public int IdProcedure { get; set; }
        public string ProcedureName { get; set; }

        public ProcedureBLL(int id, string name)
        {
            IdProcedure = id;
            ProcedureName = name;
        }
    }
}
