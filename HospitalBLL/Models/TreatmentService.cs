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

            var drPrescription = Database.DrugsPrescriptionRepository.GetAll().
                 Where(dp => dp.ComplaintIdComplaint == idComplaint && dp.DrugsIdDrugs == idDrugs
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
                 && dp.ProcedureIdProcedure == procedureId && dp.Complited == null)
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

            opPrescription.Complited = DateTime.Now;
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

        public List<SessionBLL> GetPatientTreatmentHistory(string idPatient)
        {
            var complaints = Database.ComplaintRepository.GetAll().
                Where(c => c.ClientProfileIdClientProfile == idPatient).
                ToList();

            List<SessionBLL> sesionList = new List<SessionBLL>();
            foreach (var c in complaints)
            {
                SessionBLL session = new SessionBLL();
                ComplaintBLL complaint = new ComplaintBLL(c.ClientProfile, c.Speciality,
                    c.ComplaintInformation, c.Date);

                //Initialize Complaint field 
                session.Complaint = complaint;


                if (Database.Complaint_DoctorRepository.GetAll().
                    Where(cd => cd.ComplaintIdComplaint == c.IdComplaint) != null)
                {
                    //Initialize Doctor field 
                    session.Doctor = Database.Complaint_DoctorRepository.GetAll().
                    Where(cd => cd.ComplaintIdComplaint == c.IdComplaint).
                    FirstOrDefault().Doctor;

                    if (Database.Patient_DiagnosisRepository.GetAll().
                    Where(cd => cd.ComplaintIdComplaint == c.IdComplaint).Count()>0)
                    {
                        var diagnosis = Database.Patient_DiagnosisRepository.GetAll().
                            Where(cd => cd.ComplaintIdComplaint == c.IdComplaint).
                            FirstOrDefault().Diagnosis;

                        //Initialize Diagnosis field 
                        session.Diagnosiis = new DiagnosisBLL
                        {
                            IdDiagnosis = diagnosis.IdDiagnosis,
                            DiagnosisName = diagnosis.DiagnosisName
                        };

                    }
                    else
                    {
                        session.Diagnosiis = null;
                    }
                    /////
                    if (Database.DrugsPrescriptionRepository.GetAll().
                        Where(dp => dp.ComplaintIdComplaint == c.IdComplaint) != null)
                    {
                        var drugsPrescriptions = Database.DrugsPrescriptionRepository.GetAll().
                        Where(dp => dp.ComplaintIdComplaint == c.IdComplaint);

                        // Initialize List<DrugPrescriptions> field 
                        foreach (var dp in drugsPrescriptions)
                        {
                            session.DrugPrescriptions.Add(new DrugPrescriptionBLL
                            {
                                IdDrugsPrescription = dp.IdDrugsPrescription,
                                ComplaintIdComplaint = dp.ComplaintIdComplaint,
                                drugsId = dp.DrugsIdDrugs,
                                drugs = new DrugBLL(dp.Drugs.IdDrugs, dp.Drugs.DrugsName, dp.Drugs.PathPh),
                                Recomendations = dp.Recomendations,
                                Complited = dp.Complited,
                                DoctorIdDoctor = dp.DoctorIdDoctor
                            });


                        }
                    }
                 
                    ///////////////
                    if (Database.ProcedurePrescriptionRepository.GetAll().
                       Where(dp => dp.ComplaintIdComplaint == c.IdComplaint) != null)
                    {
                        var procedurePrescriptions = Database.ProcedurePrescriptionRepository.GetAll().
                        Where(dp => dp.ComplaintIdComplaint == c.IdComplaint);

                        // Initialize List<ProcedurePrescriptions> field 
                        foreach (var dp in procedurePrescriptions)
                        {
                            session.ProcedurePrescriptions.Add(new ProcedurePrescriptonBLL
                            {
                                IdProcedurePrescription = dp.IdProcedurePrescription,
                                ComplaintIdComplaint = dp.ComplaintIdComplaint,
                                procedureId = dp.ProcedureIdProcedure,
                                procedure = new ProcedureBLL(dp.Procedure.IdProcedure, dp.Procedure.ProcedureName),
                                Recomendations = dp.Recomendations,
                                Complited = dp.Complited,
                                DoctorIdDoctor = dp.DoctorIdDoctor
                            });


                        }
                    }
                    ///////////
                    if (Database.OperationPrescriptionRepository.GetAll().
                      Where(dp => dp.ComplaintIdComplaint == c.IdComplaint) != null)
                    {
                        var operationPrescriptions = Database.OperationPrescriptionRepository.GetAll().
                        Where(dp => dp.ComplaintIdComplaint == c.IdComplaint);

                        // Initialize List<OperationPrescriptions> field 
                        foreach (var dp in operationPrescriptions)
                        {
                            session.OperatonPrescriptions.Add(new OperationPrescriptionsBLL
                            {
                                IdOperationPrescription = dp.IdOperationPrescription,
                                ComplaintIdComplaint = dp.ComplaintIdComplaint,
                                operationId = dp.OperationIdOperation,
                                operation = new OperationBLL(dp.Operation.IdOperation, dp.Operation.OperationName),
                                Recomendations = dp.Recomendations,
                                Complited = dp.Complited,
                                DoctorIdDoctor = dp.DoctorIdDoctor
                            });


                        }
                    }
                    ////////////////
                    // Initialize Discharge field 
                    if (Database.DischargeRepository.GetAll().
                      Where(dp => dp.ComplaintIdComplaint == c.IdComplaint).FirstOrDefault()
                            != null)
                    {
                        var discharge = Database.DischargeRepository.GetAll().
                      Where(dp => dp.ComplaintIdComplaint == c.IdComplaint).
                      FirstOrDefault();

                        session.Diascharge = new DischargeBLL
                        {
                            Complaint = new ComplaintBLL(discharge.Complaint.ClientProfile,
                            discharge.Complaint.Speciality, discharge.Complaint.ComplaintInformation,
                            discharge.Complaint.Date),
                            DateDisharged = discharge.DateDisharged,
                            Diagnosis = new DiagnosisBLL
                            {
                                IdDiagnosis = discharge.Diagnosis.IdDiagnosis,
                                DiagnosisName = discharge.Diagnosis.DiagnosisName
                            },
                            Recomendations = discharge.Recomendations
                        };

                    }
                    else
                    {
                        session.Diascharge= null;
                    }
                }
                else
                {
                    session.Doctor = null;
                }
                sesionList.Add(session);
            }
            return sesionList;
        }

        public List<Doctor> GetAllDoctors()
        {
           return Database.DoctorRepository.GetAll().ToList();
        }

        public DischargeBLL GetDischarge(int idComplaint)
        {
            var disharge= Database.DischargeRepository.GetAll().
                Where(d => d.ComplaintIdComplaint == idComplaint).
                FirstOrDefault();
            var c = Database.ComplaintRepository.Get(idComplaint);
            var complaint = new ComplaintBLL(c.ClientProfile, c.Speciality, c.ComplaintInformation, c.Date);
           var r=new DischargeBLL {
                Complaint= complaint,
               
                DateDisharged=disharge.DateDisharged,
                Diagnosis= new DiagnosisBLL { IdDiagnosis= disharge.Diagnosis.IdDiagnosis,
                    DiagnosisName =disharge.Diagnosis.DiagnosisName},
                Recomendations= disharge.Recomendations
            };
            r.Complaint.IdComplaint = idComplaint;
            return r;
    }

        public int GetIdDischarge(int idComplaint)
        {
            return Database.DischargeRepository.GetAll().
                Where(d => d.ComplaintIdComplaint == idComplaint).
                FirstOrDefault().IdDischarge;
        }

        public List<ComplaintBLL> GetUnprocessedComplaints()
        {
            
            var complaints = Database.ComplaintRepository.GetAll().ToList();
            var comp_doc = Database.Complaint_DoctorRepository.GetAll().ToList();
            var complBll = new List<ComplaintBLL>();

            // Get patients whoo needs doctor 
            foreach (var item in complaints)
            {
                if (comp_doc.Where(cd => cd.ComplaintIdComplaint == item.IdComplaint).ToList().Count == 0)
                {
                    complBll.Add(new ComplaintBLL(item.ClientProfile, item.Speciality, 
                        item.ComplaintInformation, item.Date));
                }
            }

            return complBll;
        }

        public ComplaintMatchBLL GetUnprocessedComplaint(string idPatient)
        {
            var complaints = Database.ComplaintRepository.GetAll().
                Where(c=>c.ClientProfile.IdClientProfile==idPatient).
                ToList();

            var comp_doc = Database.Complaint_DoctorRepository.GetAll().ToList();
            var complBll = new ComplaintMatchBLL();

          
            // Get patients whoo needs doctor 
            foreach (var item in complaints)
            {
                if (comp_doc.Where(cd => cd.ComplaintIdComplaint == item.IdComplaint).ToList().Count == 0)
                {
                    complBll = new ComplaintMatchBLL
                    {
                        ClientProfile = new ClientProfileBLL(item.ClientProfile.IdClientProfile,
                        item.ClientProfile.Name, item.ClientProfile.Surname, item.ClientProfile.SecondName,
                        item.ClientProfile.Birth, item.ClientProfile.ApplicationUser.Email, "user"),
                        Speciality = new SpecialityBLL(item.Speciality.IdSpeciality, item.Speciality.NameSpeciality,
                        item.Speciality.PathPh),
                        IdComplaint = item.IdComplaint,
                        ComplaintInformation = item.ComplaintInformation,
                        Date = item.Date,
                        IsProccesed = false
                    };
                }
            }

            return complBll;
        }
    }
}
