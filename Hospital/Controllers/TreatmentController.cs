using HospitalBLL.Interfaces;
using HospitalBLL.Models;
using HospitalBLL.Services;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Hospital.Controllers
{
    public class TreatmentController : Controller
    {
        ServiceCreator creator;
        public TreatmentController()
        {
            creator = new ServiceCreator();

        }
        private IUserService UserService
        {
            get
            {
                return HttpContext.GetOwinContext().GetUserManager<IUserService>();
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

    }

}