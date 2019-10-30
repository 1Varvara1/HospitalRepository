using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Hospital.Models
{
    public class NurseController : Controller
    {
        // GET: Nurse
        public ActionResult Index()
        {
            return View();
        }
    }
}