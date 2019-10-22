using HospitalBLL.Infrastructure;
using HospitalBLL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HospitalBLL.Interfaces
{
    public interface IDoctorService
    {
        Task<OperationDetails> Create(UserBLL doctorBLL, SpecialityBLL speciality);
        List<DoctorBLL> GetDoctorsByIdSpeciality(int IdSpeciality);
        List<DoctorBLL> GetAll();
        List<PatientBLL> GetPatients(string IdDoctor);
    }
}
