using Hospital.Models;
using HospitalBLL.Interfaces;
using HospitalBLL.Models;
using HospitalBLL.Services;
using Microsoft.AspNet.Identity.Owin;

using Microsoft.Owin.Security;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using SelectList = System.Web.Mvc.SelectList;

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

        private DoctorFilterService DoctorFilterService
        {
            get
            {
                return creator.CreateDoctorFilterService();
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
            var none = new List<SpecialityBLL> { new SpecialityBLL(0, "Специальность") };

            ViewBag.Specialities = new SelectList(none.Concat(specialities), "IdSpeciality", "NameSpeciality");
          //  var c = new SelectList(none.Concat(specialities), "IdSpeciality", "NameSpeciality");
            return View();
        }

        [HttpPost]
        public ActionResult SearchPartial(DoctorSortModel ds)
        {
            var specialities = SpecialityService.GetAllSpecialities();
            ViewBag.Specialities = new SelectList(specialities, "IdSpeciality", "NameSpeciality");
            ViewBag.Sp = specialities;
            //var doctors = DoctorService.GetAll();
            var doctors = DoctorFilterService.ApplyFilter(ds);
            return PartialView(doctors);
        }



        public ActionResult DoctorsPatients()
        {
            var model = DoctorService.GetPatients(UserService.GetId(User.Identity.Name));
            return View(model);
        }
    }   
}