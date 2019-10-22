using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HospitalBLL.Models
{
    public class OperationBLL
    {
        public int IdOperation { get; set; }
        public string OperationName { get; set; }

        public OperationBLL(int id, string name)
        {
            IdOperation = id;
            OperationName = name;
        }
    }
}
