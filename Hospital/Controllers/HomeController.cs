using HospitalBLL.Interfaces;
using HospitalBLL.Services;
using log4net;
using System;
using System.Collections.Generic;
using System.Drawing.Printing;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Hospital.Controllers
{
   
    public class HomeController : Controller
    {
        ServiceCreator creator;
        private static readonly ILog Log = LogManager.GetLogger(typeof(HomeController));
        public HomeController()
        {
            creator= new ServiceCreator();
            
        }
  
        public ActionResult Index()
        {
           
            Log.Debug("Hi I am log4net Debug Level");
            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}