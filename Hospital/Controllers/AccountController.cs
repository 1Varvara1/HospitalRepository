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
        public async Task<ActionResult> Register(RegisterModel model)
        {
         
            if (ModelState.IsValid)
            {
                UserBLL user= new UserBLL(model.Name,model.Surname,model.SecondName,model.Birth,model.Address,model.Email,"user","999999");
                OperationDetails operationDetails = await UserService.Create(user);
                if (operationDetails.Succedeed)
                {
                    ViewBag.Succes = true;
                    return View(model) ;
                }

                else
                {
                    ModelState.AddModelError(operationDetails.Property, operationDetails.Message);
                    ViewBag.Succes = true;
                }
                  
            }
            return View(model);
        }

        public ActionResult DoctorRegister()
        {
            
            var specialities= SpecialityService.GetAllSpecialities();
            ViewBag.Specialities = new SelectList(specialities,"IdSpeciality", "NameSpeciality");
            return View();
        }


        [HttpPost]
        public async Task<ActionResult> DoctorRegister(DoctorRegisterModel model)
        {
            var specialities = SpecialityService.GetAllSpecialities();
            ViewBag.Specialities = new SelectList(specialities, "IdSpeciality", "NameSpeciality");

            // returns Speciality object by guven id
            var speciality = SpecialityService.GetAllSpecialities().Where(s=>s.IdSpeciality== model.IdSpeciality).FirstOrDefault();

            if (ModelState.IsValid)
            {
                UserBLL user = new UserBLL(model.Name, model.Surname, model.SecondName, model.Birth, model.Address, model.Email, "doctor", model.Password);
                OperationDetails operationDetails = await DoctorService.Create(user, speciality);
                if (operationDetails.Succedeed)
                {
                    ViewBag.Succes = true;
                    return View(model);
                }

                else
                {
                    ModelState.AddModelError(operationDetails.Property, operationDetails.Message);
                    ViewBag.Succes = true;
                }

            }
            return View(model);
        }

    }
}