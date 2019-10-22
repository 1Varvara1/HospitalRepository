using HospitalDAL.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HospitalDAL.HelpModels
{
    public class ComplaintsEqualityComparer : IEqualityComparer<Complaint>
    {
        public bool Equals(Complaint x, Complaint y)
        {
            if (x.ClientProfileIdClientProfile == y.ClientProfileIdClientProfile)
            {
                return true;

            }
            else return false;
        }

        public int GetHashCode(Complaint obj)
        {
            int code = obj.IdComplaint * obj.Speciality.IdSpeciality;
            return code.GetHashCode();
        }
    }
}
