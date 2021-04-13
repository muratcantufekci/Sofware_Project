using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SoftwareProject.Controllers
{
    public class DoctorController : Controller
    {
        // GET: Doctor
        public ActionResult DoctorIndex()
        {
            return View();
        }
    }
}