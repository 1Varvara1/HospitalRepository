using HospitalBLL.Interfaces;
using HospitalBLL.Services;
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

            ViewBag.Diagnosis = TreatmentService.GetDiagnosis();

            return RedirectToAction("DoctorsPatients", "Doctor");
        }

        [HttpPost]
        public ActionResult AddProcedurePatient(int idComplaint, int idDiagnosis)
        {

        }
    }
}