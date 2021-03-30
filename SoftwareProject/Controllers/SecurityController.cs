using SoftwareProject.Models.EntityFramework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;

namespace SoftwareProject.Controllers
{
    public class SecurityController : Controller
    {
        MeDiagEntities1 db = new MeDiagEntities1();
        // GET: Security
        public ActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Login(Patient patient)
        {
            var patientInDb = db.Patient.FirstOrDefault(x => x.Email == patient.Email && x.Password == patient.Password);
            if(patientInDb != null)
            {
                FormsAuthentication.SetAuthCookie(patientInDb.Name, false);
                return RedirectToAction("Index", "Home");
            }
            else
            {
                ViewBag.Message = "Wrong email or password";
                return View();
            }
            
        }
    }
}