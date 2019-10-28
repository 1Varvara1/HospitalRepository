using HospitalBLL.Infrastructure;
using HospitalBLL.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Office.Interop.Word;
using System.Reflection;
using Word = Microsoft.Office.Interop.Word;
using HospitalBLL.Models;
using HospitalDAL.Interfaces;
using HospitalDAL.Entities;
using HospitalDAL;

namespace HospitalBLL.Services
{
    public class DischargeDocumentCreator
    {
        private string TemplatePath;
      //  private ITreatmentService TreatmentService;
      //  private int IdDischarge;
        private Microsoft.Office.Interop.Word.Application wordApp;
        private string TemplateFileName;
        IUnitOfWork Database { get; set; }

      
        public DischargeDocumentCreator(IUnitOfWork uow)
           
        {
            TemplatePath = @"D:Discharges\";
            TemplateFileName = "template_new.docx";
           // TreatmentService = treatmentService;
            wordApp = new Word.Application();
            Database = uow;
        }

        public OperationDetails CreateDischargeDocument(DischargeBLL discharge, int idDischarge)
        {
            Doctor doctor;
            ClientProfile patient;
            Diagnosis diagnosis;

            try
            {
                Database.DischargeRepository.Get(idDischarge);

                doctor = Database.Complaint_DoctorRepository.GetAll().
                Where(dc => dc.ComplaintIdComplaint == discharge.Complaint.IdComplaint).
                FirstOrDefault().Doctor;

                patient = discharge.Complaint.ClientProfile;

                diagnosis = Database.Patient_DiagnosisRepository.GetAll().
                    Where(pd => pd.ComplaintIdComplaint == discharge.Complaint.IdComplaint).
                    FirstOrDefault().Diagnosis;


            }
            // if not exists in database
            catch (Exception )
            {
                return new OperationDetails(false,"such discharge wasn't found ","idDiacharge");
            }
            Document wordDoc;
            try
            {
                // Open template
                wordDoc = wordApp.Documents.Open( TemplatePath + TemplateFileName);
               
            }
            catch (Exception)
            {
                return new OperationDetails(false,
                    "Didn`t managed to find template by path " + TemplatePath+ TemplateFileName,
                    "TemplatePath");
            }

            // Form name for new document   TEMPLATE: TemplatePath\discharge[idDischarge]
            // var documentName =TemplatePath + "disharge" + idDischarge.ToString()+".docx";
            // var documentName = @"D:Discharges\discharge17.docx"; 
            var documentName = @"D:\Discharges\"+ "discharge" + discharge.Complaint.IdComplaint.ToString() + ".docx";
            // Create and save Copy
            wordDoc.SaveAs(documentName);
            wordDoc.Close();

            // Open File
            try
            {
                wordDoc = wordApp.Documents.Open(documentName);

            }
            catch (Exception)
            {
                return new OperationDetails(false,
                    "Didn`t managed to find file by path " + documentName,
                    "TemplatePath");
            }

            try
            {
                // Get all the bookmarks in the document 
              //  var wBookmarks = wordDoc.Bookmarks;

                // idDischarge, DateTime.Now, doctorName, doctorSpeciality, doctorEmail,
                // patientName, patientBirth, patientEmail, firstDiagnosis, secondDiagnosis,
                // recomendations

                //    string[] data = new string[11] { idDischarge.ToString() , DateTime.Now.ToString(),
                //    doctor.ClientProfile.Surname+doctor.ClientProfile.Name+doctor.ClientProfile.SecondName,
                //    doctor.Speciality.NameSpeciality,doctor.ClientProfile.ApplicationUser.Email,
                //    patient.Surname+patient.Name+patient.SecondName,patient.Birth.ToString(),
                //    patient.ApplicationUser.Email, discharge.Diagnosis.DiagnosisName,
                //    discharge.Diagnosis.DiagnosisName,  discharge.Recomendations
                //};
               

                //int i = 0;
                //Word.Range wRange;
                //// add data into bookmarks
                //foreach (Word.Bookmark mark in wBookmarks)
                //{

                //    wRange = mark.Range;
                //    wRange.Text = data[i];
                //    i++;
                //}

            }
            catch (Exception ex )
            {
                var v = ex.Message;
              
                wordDoc.Save();
                wordDoc.Close();
            };

            List<DrugsPrescription> dp = Database.DrugsPrescriptionRepository.
                GetAll().
                Where(d => d.ComplaintIdComplaint == discharge.Complaint.IdComplaint).
                ToList();
           
            List<ProcedurePrescription> pp = Database.ProcedurePrescriptionRepository.
                GetAll().
                Where(d => d.ComplaintIdComplaint == discharge.Complaint.IdComplaint).
                ToList();

            List<OperationPrescription> op= Database.OperationPrescriptionRepository.
                GetAll().
                Where(d => d.ComplaintIdComplaint == discharge.Complaint.IdComplaint).
                ToList();


            try
            {
                Word.Table IdDischargeTb = wordDoc.Tables[1];
                Word.Table DoctorPatientTb = wordDoc.Tables[2];
                Word.Table FirstDiagnosis= wordDoc.Tables[3];
                Word.Table tbDrugs = wordDoc.Tables[4];
                Word.Table tbProcedures = wordDoc.Tables[5];
                Word.Table tbOperations = wordDoc.Tables[6];
                Word.Table SecondDiagnosis = wordDoc.Tables[7];
                Word.Table Recomendations = wordDoc.Tables[8];
                Word.Table DateNow = wordDoc.Tables[9];
                // Fill first table
                IdDischargeTb.Cell(1, 1).Range.Text = idDischarge.ToString();

                // Fill Second table with information of doctor and patient 

                DoctorPatientTb.Cell(2, 2).Range.Text = doctor.ClientProfile.Surname + doctor.ClientProfile.Name + doctor.ClientProfile.SecondName;
                DoctorPatientTb.Cell(3, 2).Range.Text = doctor.Speciality.NameSpeciality;
                DoctorPatientTb.Cell(4, 2).Range.Text = doctor.ClientProfile.ApplicationUser.Email;
                DoctorPatientTb.Cell(2, 4).Range.Text = patient.Surname + patient.Name + patient.SecondName;
                DoctorPatientTb.Cell(3, 4).Range.Text = patient.Birth.ToString();
                DoctorPatientTb.Cell(4, 4).Range.Text = patient.ApplicationUser.Email;

                // Fill Third table with information of first diagnosis
                FirstDiagnosis.Cell(1, 1).Range.Text = diagnosis.DiagnosisName;

                // Fill  table with information of drugs Prescriptions 
                int i = 2;
                if (dp.Count > 0)
                {
                    foreach (var d in dp)
                    {
                        if(tbDrugs.Rows.Count<i)
                        {
                            tbDrugs.Rows.Add();
                        }
                        tbDrugs.Cell(i, 1).Range.Text = d.Drugs.DrugsName;
                        tbDrugs.Cell(i, 2).Range.Text = d.Recomendations;
                        tbDrugs.Cell(i, 3).Range.Text = d.Complited.ToString();
                        i++;
                    }
                }

                // Fill  table with information of procedure Prescriptions 
                i = 2;
                if (pp.Count > 0)
                {
                    foreach (var d in pp)
                    {
                        if (tbProcedures.Rows.Count < i)
                        {
                            tbProcedures.Rows.Add();
                        }
                        tbProcedures.Cell(i, 1).Range.Text = d.Procedure.ProcedureName;
                        tbProcedures.Cell(i, 2).Range.Text = d.Recomendations;
                        tbProcedures.Cell(i, 3).Range.Text = d.Complited.ToString();
                        i++;
                    }
                }

                // Fill  table with information of operation Prescriptions 
                i = 2;
                if (op.Count > 0)
                {
                    foreach (var d in op)
                    {
                        if (tbOperations.Rows.Count < i)
                        {
                            tbOperations.Rows.Add();
                        }
                        tbOperations.Cell(i, 1).Range.Text = d.Operation.OperationName;
                        tbOperations.Cell(i, 2).Range.Text = d.Recomendations;
                        tbOperations.Cell(i, 3).Range.Text = d.Complited.ToString();
                        tbOperations.Cell(i, 4).Range.Text = d.Doctor.ClientProfile.Surname +
                            d.Doctor.ClientProfile.Name;
                        i++;
                    }
                }

                SecondDiagnosis.Cell(1, 1).Range.Text = discharge.Diagnosis.DiagnosisName;
                Recomendations.Cell(1, 1).Range.Text = discharge.Recomendations;
                DateNow.Cell(1, 1).Range.Text = discharge.DateDisharged.ToString();

            }
            catch (Exception ex)
            {
                var v = ex.Message;
                return new OperationDetails(false, "", "");
            }
            finally
            {
                wordDoc.Save();
                wordDoc.Close();
            }
            return new OperationDetails(true,"","");
        }



    }
}
