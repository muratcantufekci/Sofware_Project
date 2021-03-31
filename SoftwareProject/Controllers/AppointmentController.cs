using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SoftwareProject.Controllers
{
    public class AppointmentController : Controller
    {
        // GET: Appointment
        [Authorize]
        public ActionResult Index()
        {
            return View();
        }
    }
}