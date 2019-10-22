using HospitalDAL.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HospitalDAL.Interfaces
{
    public interface IUnitOfWork:IDisposable
    {
        AppUserManager UserManager { get; }
        IClientManager ClientManager { get; }
        AppRoleManager RoleManager { get; }
        IComplaintRepository ComplaintRepository { get; }
        IDiagnosisRepository DiagnosisRepository { get; }
        IDoctorRepository DoctorRepository { get; }
        IDrugsRepository DrugsRepository { get; }
        IDrugsPrescriptionRepository DrugsPrescriptionRepository{ get; }
        IOperationPrescriptionRepository OperationPrescriptionRepository { get; }
        IOperationRepository OperationRepository { get; }
        IPatient_DiagnosisRepository Patient_DiagnosisRepository { get; }
        IProcedurePrescriptionRepository ProcedurePrescriptionRepository { get; }
        IProcedureRepository ProcedureRepository { get; }
        ISpecialityRepository SpecialityRepository { get; }
        IComplaint_DoctorRepository Complaint_DoctorRepository { get; }
        Task SaveAsync();
    }
}
