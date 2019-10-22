using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HospitalBLL.Models
{
    public class SpecialityBLL
    {
        public int IdSpeciality { get; set; }
        public string NameSpeciality { get; set; }
        public string PathPh { get; set; }

        public SpecialityBLL(int id, string name, string path="")
        {
            IdSpeciality = id;
            NameSpeciality = name;
            PathPh = path;
        }
    }
}
