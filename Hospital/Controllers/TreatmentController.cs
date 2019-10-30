using Hospital.Models;
using HospitalBLL.Infrastructure;
using HospitalBLL.Interfaces;
using HospitalBLL.Models;
using HospitalBLL.Services;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using PagedList;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace Hospital.Controllers
{
    public class TreatmentController : Controller
    {
        ServiceCreator creator;
        List<SessionBLL> sessions;
        public TreatmentController()
        {
            List<SessionBLL> sessions = new List<SessionBLL>();
            creator = new ServiceCreator();

        }
        private IUserService UserService
        {
            get
            {
                return HttpContext.GetOwinContext().GetUserManager<IUserService>();
            }
        }
        private IComplaintService ComplaintService
        {
            get
            {
                return creator.CreteComplaintService();
            }
        }


        private ITreatmentService TreatmentService
        {
            get
            {
                return creator.CreateTreatmentService();
            }
        }
        private IDoctorService DoctorService
        {
            get
            {
                return creator.CreateDoctorService();
            }
        }
        private DischargeDocumentCreator DischargeDocumentCreator
        {
            get
            {
                return creator.CreateDischargeDocumentCreator();
            }
        }


        [HttpPost]
        public ActionResult MatchDiagnosis(int idComplaint, int idDiagnosis)
        {
            TreatmentService.MatchPatientDiagnosis(idComplaint, idDiagnosis);

            return RedirectToAction("DoctorsPatients", "Doctor");
        }

        [HttpPost]
        public ActionResult AddProcedurePatient(ProcedurePrescriptonBLL procPrescr, string recomendations)
        {
            procPrescr.Recomendations = recomendations;

            // Create 
            TreatmentService.AddProcedurePrescriptionPatient(procPrescr);
            return RedirectToAction("DoctorsPatients", "Doctor");

        }

        [HttpPost]
        public ActionResult AddDrugPatient(DrugPrescriptionBLL dPrescr, string recomendations)
        {
            dPrescr.Recomendations = recomendations;

            // Create 
            TreatmentService.AddDrugPrescriptionPatient(dPrescr);

            return RedirectToAction("DoctorsPatients", "Doctor");

        }

        [HttpPost]
        public ActionResult AddOperationPatient(OperationPrescriptionsBLL oPrescr, string recomendations)
        {
            oPrescr.Recomendations = recomendations;

            // Create 
            TreatmentService.AddOperationPrescriptionPatient(oPrescr);

            return RedirectToAction("DoctorsPatients", "Doctor");

        }

        [HttpPost]
        public ActionResult CompleteDrugPrescription(int idDrugs, int idComplaint)
        {
            var idDoctor= User.Identity.GetUserId();
            TreatmentService.CompleteDrugPrescription(idDrugs, idComplaint, idDoctor);
            return RedirectToAction("DoctorsPatients", "Doctor");

        }

        [HttpPost]
        public ActionResult CompleteProcedurePrescription(int procedureId, int idComplaint)
        {
            var idDoctor = User.Identity.GetUserId();
            TreatmentService.CompleteProcedurePrescription(procedureId, idComplaint, idDoctor);
            return RedirectToAction("DoctorsPatients", "Doctor");

        }

        [HttpPost]
        public ActionResult CompleteOperationPrescription(int operationId, int idComplaint)
        {
            var idDoctor = User.Identity.GetUserId();
            TreatmentService.CompleteOperationPrescription(operationId, idComplaint, idDoctor);
            return RedirectToAction("DoctorsPatients", "Doctor");

        }


        [HttpPost]
        public ActionResult PatientDischage(int idComplaint, string recomendations, int IdDiagnosis)
        {
            TreatmentService.DischagePatient(idComplaint,recomendations, IdDiagnosis);
            var disacharge = TreatmentService.GetDischarge(idComplaint);
            var dischargeId = TreatmentService.GetIdDischarge(idComplaint);
            string path= DischargeDocumentCreator.CreateDischargeDocument(disacharge, dischargeId).Property;
            ViewBag.SuccessDischarge=true;
            return RedirectToAction("DoctorsPatientsDischarged", "Doctor", new RouteValueDictionary
            { { "path",path} }); ;
        }

        [HttpGet]
        public ActionResult PatientCard(string idPatient, int? page)
        {
            //var id = ComplaintService.GetAll().Where(c => c.IdComplaint == idComplaint).FirstOrDefault();
            sessions = TreatmentService.GetPatientTreatmentHistory(idPatient);

            int pageSize = 1;
            int pageNumber = (page ?? 1);
            return View(sessions.ToPagedList(pageNumber, pageSize));
        //    return RedirectToAction("Pages");
        }

        public ActionResult Pages(int? page)
        {
            //var id = ComplaintService.GetAll().Where(c => c.IdComplaint == idComplaint).FirstOrDefault();
           // sessions = TreatmentService.GetPatientTreatmentHistory(idPatient);

            int pageSize = 1;
            int pageNumber = (page ?? 1);
            return View(sessions.ToPagedList(pageNumber, pageSize));
        }



        [HttpGet]
        public ActionResult OpenFile(string path)
        {
            FileOpender opeder = new FileOpender();
            opeder.OpenFile(path);
            return View("DoctorsPatients","Doctor");
        }


        [HttpPost]
        public ActionResult MatchDoctor(string idPatient)
        {
            var complaint = TreatmentService.GetUnprocessedComplaint(idPatient);
            var doctors= DoctorService.GetDoctorsByIdSpeciality(complaint.Speciality.IdSpeciality);
            var model = new MatchDoctor(complaint,doctors);
            return View(model);
        }
    }

}