using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Hospital.Models
{
    // Class for transporting data from ComplaintRegistration.cshtml to Controller
    public class ComplaintRegistration
    {
       public int IdSpeciality { get; set; }
       public string IdPatient { get; set; }
       public string IdDoctor { get; set; }

        public ComplaintRegistration(int idSpeciality, string idPatient, string idDoctor=null)
        {
            IdSpeciality = idSpeciality;
            IdPatient = idPatient;
            IdDoctor = idDoctor;
        }

        public ComplaintRegistration()
        {

        }
    }
}