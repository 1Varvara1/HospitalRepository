using Hospital.Models;
using HospitalBLL.Infrastructure;
using HospitalBLL.Interfaces;
using HospitalBLL.Models;
using HospitalBLL.Services;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin.Security;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace Hospital.Controllers
{
    public class AccountController : Controller
    {
        ServiceCreator creator;

        public AccountController()
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

        [HttpPost]
        public async Task<ActionResult> Login(LoginModel model)
        {

            if (ModelState.IsValid)
            {
                UserBLL userBLL = new UserBLL { Email = model.Email, Password = model.Password };
                ClaimsIdentity claim = await UserService.Authenticate(userBLL);
                if (claim == null)
                {
                    ModelState.AddModelError("", "Неверный логин или пароль.");
                }
                else
                {
                    AuthenticationManager.SignOut();
                    AuthenticationManager.SignIn(new AuthenticationProperties
                    {
                        IsPersistent = true
                    }, claim);
                    return RedirectToAction("Index", "Home");
                }
            }
            return View(model);
        }

        public ActionResult Logout()
        {
            AuthenticationManager.SignOut();
            return RedirectToAction("Index", "Home");
        }

        public ActionResult Register()
        {
            return View();
        }

        [HttpPost]
        public async Task<ActionResult> Register(RegisterModel model, string redirect)
        {

            if (ModelState.IsValid)
            {
                if (model.Birth > DateTime.Now || DateTime.Now.Subtract(model.Birth).TotalDays / 365 > 120)
                {
                    ModelState.AddModelError("Birth", "Проверте правильность ввода даты рожденния");
                }
                UserBLL user = new UserBLL(model.Name, model.Surname, model.SecondName, model.Birth, model.Address, model.Email, "user", "999999");
                OperationDetails operationDetails = await UserService.Create(user);


                if (operationDetails.Succedeed)
                {
                    var userBll = UserService.GetPatients().
                    Where(u => u.Name == model.Name && u.Surname == model.Surname
                    && u.SecondName == model.SecondName && u.Birth == model.Birth).
                    FirstOrDefault();

                    ViewBag.Succes = true;
                    if (redirect == "true")
                    {
                        return RedirectToActionPermanent("ComplaintRegstration", "Patient",
                            new { Idpatient = userBll.IdClientProfile });
                    }
                }

                else
                {
                    ModelState.AddModelError(operationDetails.Property, operationDetails.Message);
                    ViewBag.Succes = false;
                }

            }
            else { ViewBag.Succes = false; }

            // return RedirectToAction("PatientSearch", "Patient");
            return View();
        }

        [HttpGet]
        public ActionResult DoctorRegister()
        {

            var specialities = SpecialityService.GetAllSpecialities();
            ViewBag.Specialities = new SelectList(specialities, "IdSpeciality", "NameSpeciality");
            ViewBag.Registred = false;
            return View();
        }


        [HttpPost]
        public async Task<ActionResult> DoctorRegister(DoctorRegisterModel model)
        {
            var specialities = SpecialityService.GetAllSpecialities();
            ViewBag.Specialities = new SelectList(specialities, "IdSpeciality", "NameSpeciality");

            // returns Speciality object by guven id
            var speciality = SpecialityService.GetAllSpecialities().Where(s => s.IdSpeciality == model.IdSpeciality).FirstOrDefault();

            if (DateTime.Now.Subtract(model.Birth).Days < 18 * 365)
            {
                ModelState.AddModelError("Birth", "Доктор должен быть старше 18 лет");
                ViewBag.Success = false;
            }
            ViewBag.Success = false;
            if (ModelState.IsValid)
            {
                UserBLL user;
                if (speciality.NameSpeciality == "nurse")
                {
                    user = new UserBLL(model.Name, model.Surname, model.SecondName, model.Birth, model.Address, model.Email, "nurse", model.Password);
                }
                else
                {
                    user = new UserBLL(model.Name, model.Surname, model.SecondName, model.Birth, model.Address, model.Email, "doctor", model.Password);
                }

                OperationDetails operationDetails = await DoctorService.Create(user, speciality);
                if (operationDetails.Succedeed)
                {
                    ViewBag.Success = true;
                    //return View(model);
                    return View(new DoctorRegisterModel());
                }

                else
                {
                    ModelState.AddModelError("Email", operationDetails.Message);
                    ViewBag.Success = false;
                }

            }
            return View(model);
        }


        public ActionResult PersonalPage()
        {
            var name = User.Identity.Name;
            // UserService.
            var user = UserService.GetProfile(name);
            return View(user);
        }
        public ActionResult PersonalPage1()
        {
            var name = User.Identity.Name;
            // UserService.
            var user = UserService.GetProfile(name);
            return View(user);
        }

    }
}