using Hospital.Models;
using HospitalBLL.Comparers;
using HospitalBLL.Interfaces;
using HospitalBLL.Models;
using HospitalBLL.Services;
using Microsoft.AspNet.Identity.Owin;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace Hospital.Controllers
{
    public class PatientController : Controller
    {
        ServiceCreator creator;
        public PatientController()
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

        private ISpecialityService SpecialityService
        {
            get
            {
                return creator.CreateSpecialityService();
            }
        }
        private IComplaintService ComplaintService
        {
            get
            {
                return creator.CreteComplaintService();
            }

        }

        private IDoctorService DoctorService
        {
            get
            {
                return creator.CreateDoctorService();
            }
        }

        [HttpGet]
        public ActionResult PatientSearch()
        {
            // var complaints = ComplaintService.GetAll();
            var models = UserService.GetPatients();

            return View(models);
        }



        [HttpPost]
        public ActionResult SearchPartial(string typeSearchS1, string typeSearchS2)
        {
            var models = UserService.GetPatients();
            if (typeSearchS1 != "none")
            {
                switch (typeSearchS1)
                {
                    case "SurnameA_Z":
                        models.Sort(new SurnameUserComparer());
                        break;
                    case "SurnameZ_A":
                        models.Sort(new SurnameUserComparer());
                        models.Reverse();
                        break;
                    case "BirthMax_Min":
                        models.Sort(new BirthUserComparer());
                        break;
                    case "BirthMin_Max":
                        models.Sort(new BirthUserComparer());
                        models.Reverse();
                        break;
                };
            }
            if (typeSearchS2 != "none")
            {
                switch (typeSearchS2)
                {
                    case "NeedDoctor":
                        // Pick patients who needs a doctor
                        models = models.Where(m => m.NeedDoctor == true).ToList();
                        break;
                    case "AreBeingTreated":
                        models = UserService.GetPatientsAreBeingTreated();
                        break;
                }

            }
            return PartialView(models);
        }

        //[HttpGet]
        //public ActionResult ComplaintRegstration()
        //{
        //    var specialities = SpecialityService.GetAllSpecialities();
        //    ViewBag.Specialities = new SelectList(specialities, "IdSpeciality", "NameSpeciality");


        //    return View();
        //}

        public ComplaintRegistrationService FormComplaintRegistrationService(string Idpatient=null)
        {
            var model = new UserBLL();

            // Fill List of Patients
            var patients = UserService.GetPatients();

            // Fill List of Patients of doctors
            var doctors = DoctorService.GetAll();


            if (Idpatient != null)
            {
                model = UserService.GetPatients().Where(p => p.IdClientProfile == Idpatient).FirstOrDefault();
                //  return View(model);
            }

            //Form model for the view
            var service = new ComplaintRegistrationService(model, patients, doctors);
            return service;
        }

        [HttpGet]
        public ActionResult ComplaintRegstration(string Idpatient)
        {
            // Fill SelectList  
            var specialities = SpecialityService.GetAllSpecialities();
            specialities.RemoveAll(s=>s.NameSpeciality== "Медсестренство");
            ViewBag.Specialities = new SelectList(specialities, "IdSpeciality", "NameSpeciality");
            ViewBag.Sp = specialities;
        
            var service = FormComplaintRegistrationService(Idpatient);
            return View(service);
        }



        [HttpPost]
        public async Task<ActionResult> PatientComplaintRegistration(ComplaintRegistration complaintRegistration)
        {
            // Create complaint
            int idComplaint = await ComplaintService.Create(complaintRegistration.IdPatient, complaintRegistration.IdSpeciality);

            if (complaintRegistration.IdDoctor != null)
            {
                ComplaintService.MatchComplaintDoctor(idComplaint, complaintRegistration.IdDoctor);
            }

            // Fill SelectList  
            var specialities = SpecialityService.GetAllSpecialities();
            specialities.RemoveAll(s => s.NameSpeciality == "Медсестренство");
            ViewBag.Specialities = new SelectList(specialities, "IdSpeciality", "NameSpeciality");
            ViewBag.Sp = specialities;
            ViewBag.SuccessRegistration = true;

            var service = FormComplaintRegistrationService();
            return View("ComplaintRegstration", service);
        }
    }
}