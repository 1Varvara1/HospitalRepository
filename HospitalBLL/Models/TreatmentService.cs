using HospitalBLL.Comparers;
using HospitalBLL.Interfaces;
using HospitalDAL.Entities;
using HospitalDAL.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNet.Identity;

namespace HospitalBLL.Models
{
    public class TreatmentService : ITreatmentService
    {
        IUnitOfWork Database { get; set; }

        public TreatmentService(IUnitOfWork uow)
        {
            Database = uow;
        }

        public List<DrugBLL> GetAllDrags()
        {
            var drugs = Database.DrugsRepository.GetAll().ToList();
            var model = new List<DrugBLL>();
            foreach (var d in drugs)
            {
                model.Add(new DrugBLL(d.IdDrugs, d.DrugsName, d.PathPh));
            }
            model.Sort(new DrugsNameComparer());

            return model;
        }

        public List<OperationBLL> GetOperations()
        {
            var operations = Database.OperationRepository.GetAll().ToList();
            var model = new List<OperationBLL>();
            foreach (var d in operations)
            {
                model.Add(new OperationBLL(d.IdOperation, d.OperationName));
            }
            model.Sort(new OperationNameComparer());

            return model;
        }

        public List<ProcedureBLL> GetProcedures()
        {
            var procedures = Database.ProcedureRepository.GetAll().ToList();
            var model = new List<ProcedureBLL>();
            foreach (var d in procedures)
            {
                model.Add(new ProcedureBLL(d.IdProcedure, d.ProcedureName));
            }
            model.Sort(new ProcedureNameComparer());

            return model;
        }

        public List<DiagnosisBLL> GetDiagnosis()
        {
            var diagnosises = Database.DiagnosisRepository.GetAll().ToList();
            var model = new List<DiagnosisBLL>();
            foreach (var d in diagnosises)
            {
                model.Add(new DiagnosisBLL
                {
                    IdDiagnosis = d.IdDiagnosis,
                    DiagnosisName = d.DiagnosisName
                });

            }
            model.Sort(new DiagnosisNameComparer());

            return model;
        }

        public void MatchPatientDiagnosis(int idComplaint, int idDiagnosis)
        {
            var diagnosis = Database.DiagnosisRepository.Get(idDiagnosis);
            var complaint = Database.ComplaintRepository.Get(idComplaint);
            //var pd = Database.Patient_DiagnosisRepository.GetAll().
            //    Where(d => d.ComplaintIdComplaint == idComplaint).FirstOrDefault();
            var PD = new Patient_Diagnosis
            {
                ComplaintIdComplaint = idComplaint,
                DiagnosisIdDiagnosis = idDiagnosis,
                Diagnosis = diagnosis,
                Complaint = complaint
            };

            // Create new row int the table
            Database.Patient_DiagnosisRepository.Create(PD);

        }

        public void AddProcedurePrescriptionPatient(ProcedurePrescriptonBLL procPrescr)
           
        {
            var complaint = Database.ComplaintRepository.Get(procPrescr.ComplaintIdComplaint);
            var doctor = Database.DoctorRepository.GetAll().
                Where(doc => doc.ClientProfileIdClientProfile == procPrescr.DoctorIdDoctor).
                FirstOrDefault();
            var prosedure = Database.ProcedureRepository.Get(procPrescr.procedureId);
            var d = Database.DiagnosisRepository.GetAll();
            // Create a row
            var prescription = new ProcedurePrescription
            {
                ComplaintIdComplaint = procPrescr.ComplaintIdComplaint,
                Complaint = complaint,
                ProcedureIdProcedure = procPrescr.procedureId,
                Procedure = prosedure,
                Recomendations = procPrescr.Recomendations,
                Complited = null,
                DoctorIdDoctor = procPrescr.DoctorIdDoctor,
                Doctor = doctor
            };
           

            Database.ProcedurePrescriptionRepository.Create(prescription);

    }

        public void AddDrugPrescriptionPatient(DrugPrescriptionBLL dPrescr)
        {
            var complaint = Database.ComplaintRepository.Get(dPrescr.ComplaintIdComplaint);
            var doctor = Database.DoctorRepository.GetAll().
                Where(d => d.ClientProfileIdClientProfile == dPrescr.DoctorIdDoctor).
                FirstOrDefault();
            var drug = Database.DrugsRepository.Get(dPrescr.drugsId);

            // Create a row
            var drugs = new DrugsPrescription
            {
                ComplaintIdComplaint = dPrescr.ComplaintIdComplaint,
                Complaint = complaint,
                DrugsIdDrugs = dPrescr.drugsId,
                Drugs = drug,
                Recomendations = dPrescr.Recomendations,
                Complited = null,
                DoctorIdDoctor = dPrescr.DoctorIdDoctor,
                Doctor = doctor
            };

            Database.DrugsPrescriptionRepository.Create(drugs);
        }

        public void AddOperationPrescriptionPatient(OperationPrescriptionsBLL opPrescr)
        {
            var complaint = Database.ComplaintRepository.Get(opPrescr.ComplaintIdComplaint);
            var doctor = Database.DoctorRepository.GetAll().
                Where(d => d.ClientProfileIdClientProfile == opPrescr.DoctorIdDoctor).
                FirstOrDefault();
            var op = Database.OperationRepository.Get(opPrescr.operationId);

            // Create a row
            var operation = new OperationPrescription
            {
                ComplaintIdComplaint = opPrescr.ComplaintIdComplaint,
                Complaint = complaint,
                OperationIdOperation = opPrescr.operationId,
                Operation = op,
                Recomendations = opPrescr.Recomendations,
                Complited = null,
                DoctorIdDoctor = opPrescr.DoctorIdDoctor,
                Doctor = doctor
            };

            Database.OperationPrescriptionRepository.Create(operation);
        }

        public void CompleteDrugPrescription(int idDrugs, int idComplaint, string idDoctor)
        {
          
            var drPrescription= Database.DrugsPrescriptionRepository.GetAll().
                 Where(dp => dp.ComplaintIdComplaint == idComplaint && dp.DrugsIdDrugs==idDrugs
                 && dp.Complited == null)
                 .FirstOrDefault();

            // Add information about person who has given drugs and when 
            drPrescription.Complited = DateTime.Now;
            drPrescription.DoctorIdDoctor = idDoctor;


            Database.DrugsPrescriptionRepository.Save();
            
        }

        public void CompleteProcedurePrescription(int procedureId, int idComplaint, string idDoctor)
        {

            var prPrescription = Database.ProcedurePrescriptionRepository.GetAll().
                 Where(dp => dp.ComplaintIdComplaint == idComplaint
                 && dp.ProcedureIdProcedure == procedureId && dp.Complited==null)
                 .FirstOrDefault();

            // Add information about person who has appointed procedure and when 
            prPrescription.Complited = DateTime.Now;
            prPrescription.DoctorIdDoctor = idDoctor;


            Database.ProcedurePrescriptionRepository.Save();
        }

        public void CompleteOperationPrescription(int operationId, int idComplaint, string idDoctor)
        {
            var opPrescription = Database.OperationPrescriptionRepository.GetAll()
                .Where(op => op.ComplaintIdComplaint == idComplaint &&
                op.OperationIdOperation == operationId && op.Complited == null)
                .FirstOrDefault();

            opPrescription.Complited= DateTime.Now;
            opPrescription.DoctorIdDoctor = idDoctor;

            Database.OperationPrescriptionRepository.Save();
        }

        public void DischagePatient(int idComplaint, string recomendations, int IdDiagnosis)
        {
            var discharge = new Discharge
            {
                ComplaintIdComplaint = idComplaint,
                DateDisharged = DateTime.Now,
                DiagnosisIdDiagnosis = IdDiagnosis,
                Recomendations = recomendations
            };

            Database.DischargeRepository.Create(discharge);

    }
    }
}
