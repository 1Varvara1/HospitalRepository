using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HospitalBLL.Interfaces
{
    public interface IServiceCreator
    {
        IUserService CreateUserService();
        IDrugsService CreateDrugService();
        IDoctorService CreateDoctorService();
        ISpecialityService CreateSpecialityService();
        IComplaintService CreteComplaintService();
        ITreatmentService CreateTreatmentService();
    }
}
