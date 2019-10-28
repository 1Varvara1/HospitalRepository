using HospitalBLL.Interfaces;
using HospitalBLL.Models;
using HospitalDAL.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HospitalBLL.Services
{
    public class ServiceCreator : IServiceCreator
    {
        string connection;
        public ServiceCreator()
        {
            connection = "DefaultConnection";
        }
        
        public DoctorFilterService CreateDoctorFilterService() {
            return new DoctorFilterService(new IdentityUnitOfWork(connection));
        }
        public ITreatmentService CreateTreatmentService()
        {
            return new TreatmentService(new IdentityUnitOfWork(connection));
        }

        public IUserService CreateUserService()
        {
            return new UserService(new IdentityUnitOfWork(connection));
        }
        public IDrugsService CreateDrugService()
        {
            return new DrugService(new IdentityUnitOfWork(connection));
        }
        public IDoctorService CreateDoctorService()
        {
            return new DoctorService(new IdentityUnitOfWork(connection));
        }
        public ISpecialityService CreateSpecialityService()
        {
            return new SpecialityService(new IdentityUnitOfWork(connection));
        }
        public IComplaintService CreteComplaintService()
        {
            return new ComplaintService(new IdentityUnitOfWork(connection));
        }

        public DischargeDocumentCreator CreateDischargeDocumentCreator()
        {
            return new DischargeDocumentCreator(new IdentityUnitOfWork(connection));
        }
    }
}
