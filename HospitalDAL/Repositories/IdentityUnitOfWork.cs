using HospitalDAL.Entities;
using HospitalDAL.EntityFramework;
using HospitalDAL.Identity;
using HospitalDAL.Interfaces;
using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HospitalDAL.Repositories
{
    public class IdentityUnitOfWork : IUnitOfWork
    {
        private ApplicationContext db;

        private AppUserManager userManager;
        private AppRoleManager roleManager;
        private IClientManager clientManager;
        private IComplaintRepository complaintRepository;
        
        IDiagnosisRepository dignosisRepository;
        IDoctorRepository doctorRepository;
        IDrugsRepository drugsRepository;
        IDrugsPrescriptionRepository drugsPrescriptionRepository;
        IOperationPrescriptionRepository operationPrescriptionRepository;
        IOperationRepository operationRepository;
        IPatient_DiagnosisRepository patient_DiagnosisRepository;
        IProcedurePrescriptionRepository procedurePrescriptionRepository;
        IProcedureRepository procedureRepository;
        ISpecialityRepository specialityRepository;
        IComplaint_DoctorRepository complaint_DoctorRepository;

        public IdentityUnitOfWork(string connectionString)
        {
            db = new ApplicationContext(connectionString);
          
            userManager = new AppUserManager(new UserStore<ApplicationUser>(db));
            roleManager = new AppRoleManager(new RoleStore<ApplicationRole>(db));
            clientManager = new ClientManager(db);
            complaintRepository = new ComplaintRepository(db);
            dignosisRepository=new DiagnosisRepository(db);
            doctorRepository=new DoctorRepository(db);
            drugsRepository= new DrugsRepository(db);
            drugsPrescriptionRepository=new DrugsPrescriptionRepository(db);
            operationPrescriptionRepository=new OperationPrescriptionRepository(db);
            operationRepository=new OperationRepository(db);
            patient_DiagnosisRepository=new Patient_DiagnosisRepository(db);
            procedurePrescriptionRepository=new ProcedurePrescriptionRepository(db);
            procedureRepository=new ProcedureRepository(db);
            specialityRepository = new SpecialityRepository(db);
            complaint_DoctorRepository = new Complaint_DoctorRepository(db);

        }

        public IComplaint_DoctorRepository Complaint_DoctorRepository {

            get { return complaint_DoctorRepository; }
        }


        public AppUserManager UserManager
        {
            get { return userManager; }
        }

        public IClientManager ClientManager
        {
            get { return clientManager; }
        }

        public IComplaintRepository ComplaintRepository
        {
            get { return complaintRepository; }
        }
        public AppRoleManager RoleManager
        {
            get { return roleManager; }
        }

        public IDiagnosisRepository DiagnosisRepository
        {
            get { return this.dignosisRepository; }
        }

        public IDoctorRepository DoctorRepository
        {
            get { return this.doctorRepository; }
        }

        public IDrugsRepository DrugsRepository
        {
            get { return this.drugsRepository; }
        }

        public IDrugsPrescriptionRepository DrugsPrescriptionRepository
        {
            get { return this.drugsPrescriptionRepository; }
        }

        public IOperationPrescriptionRepository OperationPrescriptionRepository
        {
            get { return this.operationPrescriptionRepository; }
        }

        public IOperationRepository OperationRepository
        {
            get { return this.operationRepository; }
        }

        public IPatient_DiagnosisRepository Patient_DiagnosisRepository
        {
            get { return this.patient_DiagnosisRepository; }
        }

        public IProcedurePrescriptionRepository ProcedurePrescriptionRepository
        {
            get { return this.procedurePrescriptionRepository; }
        }

        public IProcedureRepository ProcedureRepository
        {
            get { return this.procedureRepository; }
        }
        public ISpecialityRepository SpecialityRepository
        {
            get { return this.specialityRepository; }
        }

        public async Task SaveAsync()
        {
            await db.SaveChangesAsync();
        }
        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
        private bool disposed = false;

        public virtual void Dispose(bool disposing)
        {
            if (!this.disposed)
            {
                if (disposing)
                {
                    userManager.Dispose();
                    roleManager.Dispose();
                    clientManager.Dispose();
                }
                this.disposed = true;
            }
        }


    }
}
