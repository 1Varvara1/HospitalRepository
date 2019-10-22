using HospitalBLL.Interfaces;
using HospitalBLL.Services;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin.Security;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Hospital.Controllers
{
    public class DoctorController : Controller
    {
        ServiceCreator creator;
        public DoctorController()
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

        private IAuthenticationManager AuthenticationManager
        {
            get
            {
                return HttpContext.GetOwinContext().Authentication;
            }
        }
        private IDoctorService DoctorService
        {
            get
            {
                return creator.CreateDoctorService();
            }
        }

        private ISpecialityService SpecialityService
        {
            get
            {
                return creator.CreateSpecialityService();
            }
        }
        public ActionResult Login()
        {
            return View();
        }
        // GET: Doctor
        public ActionResult Index()
        {//
            return View();
        }
        [HttpGet]
        public ActionResult DoctorSearch()
        {
            // Initial dropdown with specialities
            var specialities = SpecialityService.GetAllSpecialities();
            ViewBag.Specialities = new SelectList(specialities, "IdSpeciality", "NameSpeciality");

            return View();
        }

        [HttpPost]
        public ActionResult SearchPartial(string sort, int idSpeciality)
        {
            var specialities = SpecialityService.GetAllSpecialities();
            ViewBag.Specialities = new SelectList(specialities, "IdSpeciality", "NameSpeciality");

            var doctors = DoctorService.GetAll();

            return PartialView(doctors);
        }
    }   
}