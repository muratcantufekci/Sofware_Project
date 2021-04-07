using SoftwareProject.Models.EntityFramework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SoftwareProject.Controllers
{
    public class ProfileController : Controller
    {
        MeDiagEntities1 db = new MeDiagEntities1();
        // GET: Profile
        public ActionResult PatientProfile(int id)
        {
            var infoPatient = db.Patient.FirstOrDefault(x => x.Id == id);
            return View(infoPatient);
        }
    }
}