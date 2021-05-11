using SoftwareProject.Models.EntityFramework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SoftwareProject.Controllers
{
    public class PredictionController : Controller
    {
        MeDiagEntities9 db = new MeDiagEntities9();
        // GET: Prediction
        [Authorize]
        public ActionResult Index(int id)
        {
            var findPatient = db.Patient.FirstOrDefault(x => x.Id == id);

            return View(findPatient);
        }
        [HttpPost]
        public ActionResult Index(string variable,int id)
        {
            var variableTrimmed = string.Concat(variable.Skip(2).Take(variable.Length - 4));
            Illness ıllness = new Illness();
            ıllness.Name = variableTrimmed;
            ıllness.PId = id;
            db.Illness.Add(ıllness);
            db.SaveChanges();
            var getPid = ıllness.Id;
            TempData["getPID"] = getPid;
            return Json(Url.Action("AppointmentIndex", "Appointment", new { @id = id }));
        }

       

    }
}