using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HospitalBLL.Models
{
    public class ComplaintMatchBLL
    {
        public int IdComplaint { get; set; }
        public ClientProfileBLL ClientProfile { get; set; }
        public string ComplaintInformation { get; set; }
        public SpecialityBLL Speciality { get; set; }
        public DateTime Date { get; set; }
        public bool? IsProccesed { get; set; }

        public ComplaintMatchBLL()
        {

        }
        public ComplaintMatchBLL(ClientProfileBLL profile, SpecialityBLL speciality, string inform, DateTime date, bool? processed = false)
        {
            ClientProfile = profile;
            Speciality = speciality;
            ComplaintInformation = inform;
            Date = date;
            IsProccesed = processed;
        }
    }
}
