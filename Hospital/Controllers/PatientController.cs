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

        [HttpGet]
        public ActionResult PatientSearch()
        {
            var complaints = ComplaintService.GetAll();
            var model = UserService.GetPatients();
            //foreach (var item in model)
            //{
            //    //if (complaints.Any(i => i.ClientProfile.))
            //   // var needDoc=UserService.GetPatients().Where(c=>c.)
            //}
            return View(model);
        }

    }
}