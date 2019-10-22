using HospitalBLL.Infrastructure;
using HospitalBLL.Interfaces;
using HospitalBLL.Models;
using HospitalDAL;
using HospitalDAL.Entities;
using HospitalDAL.HelpModels;
using HospitalDAL.Interfaces;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HospitalBLL.Services
{
    public class DoctorService : IDoctorService
    {
        IUnitOfWork Database { get; set; }

        public DoctorService(IUnitOfWork uow)
        {
            Database = uow;
        }

        public async Task<OperationDetails> Create(UserBLL userBll, SpecialityBLL speciality)
        {
            ApplicationUser user = await Database.UserManager.FindByEmailAsync(userBll.Email);
            if (user == null)
            {
                user = new ApplicationUser { Email = userBll.Email, UserName = userBll.Email };
                var result = await Database.UserManager.CreateAsync(user, userBll.Password);
                if (result.Errors.Count() > 0)
                    return new OperationDetails(false, result.Errors.FirstOrDefault(), "");
                // добавляем роль
                await Database.UserManager.AddToRoleAsync(user.Id, userBll.Role);
                // создаем профиль клиента
                ClientProfile clientProfile = new ClientProfile
                {
                    IdClientProfile = user.Id,
                    Address = userBll.Address,
                    Name = userBll.Name,
                    Surname = userBll.Surname,
                    SecondName = userBll.SecondName,
                    Birth = userBll.Birth

                };
                Database.ClientManager.Create(clientProfile);

                var doc = new Doctor
                {
                    ClientProfileIdClientProfile = user.Id,
                    PathPhoto = "",
                    SpecialityIdSpeciality = speciality.IdSpeciality

                };
                Database.DoctorRepository.Create(doc);

                await Database.SaveAsync();
                return new OperationDetails(true, "Регистрация успешно пройдена", "");
            }
            else
            {
                return new OperationDetails(false, "Пользователь с таким логином уже существует", "Email");
            }
        }

        public List<DoctorBLL> GetDoctorsByIdSpeciality(int Idspeciality)
        {
            // Search for doctors with given speciality
            var doctors = Database.DoctorRepository.GetAll().Where(d => d.SpecialityIdSpeciality == Idspeciality);
            var DoctorsBLL = new List<DoctorBLL>();
            foreach (var d in doctors)
            {
                var item = new DoctorBLL(d.ClientProfile, d.Speciality, d.Complaint_Doctors.ToList(), d.PathPhoto);
                DoctorsBLL.Add(item);
            }

            return DoctorsBLL;
        }

        public List<DoctorBLL> GetAll()
        {
            var doctors = Database.DoctorRepository.GetAll();
            var DoctorsBLL = new List<DoctorBLL>();

            // Figuring out how many patients doctors have
            foreach (var d in doctors)
            {
                // Form new DoctorBLL item
                var item = new DoctorBLL(d.ClientProfile, d.Speciality, d.Complaint_Doctors.ToList(), d.PathPhoto);

                var complaints_doctor = d.Complaint_Doctors.ToList();
                List<Complaint> complaints = new List<Complaint>();

                foreach (var cd in complaints_doctor)
                {
                    complaints.Add(cd.Complaint);
                }

                // Find all the patients of Doctor
                var patients = complaints.Distinct(new ComplaintsEqualityComparer()).ToList();

                // Find all the patients of Doctor who being treated now
                var patientsTreated = patients.Where(c => IsBeingTreated(c.ClientProfile) == true).ToList();

                item.AmountPatients = patients.Count();
                item.AmountBeingTreated = patientsTreated.Count();

                DoctorsBLL.Add(item);
            }

            return DoctorsBLL;
        }

        public bool IsBeingTreated(ClientProfile user)
        {
            if (user.Complaints.Count == 0)
            {
                return false;
            }


            var complaints = user.Complaints.ToList();
            var disch = Database.DischargeRepository.GetAll();
            var discharges = Database.DischargeRepository.GetAll().
                Where(d => d.Complaint.ClientProfile.IdClientProfile == user.IdClientProfile).ToList();

            // Check if any patient wasn`t discharged
            foreach (var item in complaints)
            {
                if (!discharges.Any(d => d.Complaint.IdComplaint == item.IdComplaint))
                {
                    return true;
                }
            }

            return false;
        }

        public List<PatientBLL> GetPatients(string IdDoctor)
        {
            // Get Id of the doctor by Id of user profile
            var doctorId = Database.DoctorRepository.Get(IdDoctor).IdDoctor;

            // Get records for the doctor 
            var doctor_complaints = Database.Complaint_DoctorRepository.GetAll().
                Where(dc => dc.DoctorIdDoctor == doctorId).ToList();

            var discharges = Database.DischargeRepository.GetAll().ToList();

            // Check if someody of patients has been already discharged
            foreach (var item in doctor_complaints)
            {
                if (discharges.Any(d => d.ComplaintIdComplaint == item.ComplaintIdComplaint))
                {
                    doctor_complaints.Remove(item);
                }
            }
            var patients = new List<PatientBLL>();

            foreach (var item in doctor_complaints)
            {
                var patient = item.Complaint.ClientProfile;
                var complaintId = item.ComplaintIdComplaint;

                // Get all drug perscriptions for patient 
                var drugPrescriplions = Database.DrugsPrescriptionRepository.GetAll().
                    Where(p => p.ComplaintIdComplaint == item.ComplaintIdComplaint).ToList();

                var drugPrescriplionsBll = new List<DrugPrescriptionBLL>();
                foreach (var dp in drugPrescriplions)
                {
                    drugPrescriplionsBll.Add(new DrugPrescriptionBLL
                    {
                        IdDrugsPrescription = dp.IdDrugsPrescription,
                        ComplaintIdComplaint = dp.ComplaintIdComplaint,
                        drugs = new DrugBLL(dp.Drugs.IdDrugs, dp.Drugs.DrugsName, dp.Drugs.PathPh),
                        Recomendations = dp.Recomendations,
                        Complited = dp.Complited,
                        DoctorIdDoctor = dp.DoctorIdDoctor
                    });
                }

                // Get all procedure perscriptions for patient 
                var procedurePrescriplions = Database.ProcedurePrescriptionRepository.GetAll().
                    Where(p => p.ComplaintIdComplaint == item.ComplaintIdComplaint).ToList();

                var procedurePrescriplionsBll = new List<ProcedurePrescriptonBLL>();
                foreach (var dp in procedurePrescriplions)
                {
                    procedurePrescriplionsBll.Add(new ProcedurePrescriptonBLL
                    {
                        IdProcedurePrescription = dp.IdProcedurePrescription,
                        ComplaintIdComplaint = dp.ComplaintIdComplaint,
                        procedure = new ProcedureBLL(dp.Procedure.IdProcedure,
                        dp.Procedure.ProcedureName),
                        Recomendations = dp.Recomendations,
                        Complited = dp.Complited,
                        DoctorIdDoctor = dp.DoctorIdDoctor
                    });
                }

                // Get all operation perscriptions for patient 
                var operationPrescriptions = Database.OperationPrescriptionRepository.GetAll().
                   Where(p => p.ComplaintIdComplaint == item.ComplaintIdComplaint).ToList();

                var operationPrescriptionsBll = new List<OperationPrescriptionsBLL>();

                foreach (var dp in operationPrescriptions)
                {
                    operationPrescriptionsBll.Add(new OperationPrescriptionsBLL
                    {
                        IdOperationPrescription = dp.IdOperationPrescription,
                        ComplaintIdComplaint = dp.ComplaintIdComplaint,
                        operation = new OperationBLL(dp.Operation.IdOperation,
                        dp.Operation.OperationName),
                        Recomendations = dp.Recomendations,
                        Complited = dp.Complited,
                        DoctorIdDoctor = dp.DoctorIdDoctor
                    });
                }

                var diagnos = Database.Patient_DiagnosisRepository.GetAll().
                    Where(d => d.Complaint.IdComplaint == item.ComplaintIdComplaint).
                    FirstOrDefault().Diagnosis;

                patients.Add(new PatientBLL
                {
                    ClientProfile = new ClientProfileBLL(patient.IdClientProfile, patient.Name,
                    patient.Surname, patient.SecondName, patient.Birth, "", "user"),
                    IdComplaint = item.ComplaintIdComplaint,
                    DrugsPrescriptions= drugPrescriplionsBll,
                    ProcedurePrescriptions= procedurePrescriplionsBll,
                    OperatonPrescriptions= operationPrescriptionsBll,
                    Diagnosis= new DiagnosisBLL { IdDiagnosis= diagnos.IdDiagnosis,
                        DiagnosisName= diagnos.DiagnosisName}
                });; 


            }
            return patients;
        }
    }
}
