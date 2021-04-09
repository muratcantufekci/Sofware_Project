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
        public ActionResult UpdatePatient(Patient patient)
        {
            var patientToUpdate = db.Patient.Find(patient.Id);
            if(patientToUpdate == null)
            {
               return HttpNotFound();
            }
            else
            {
                patientToUpdate.Email = patient.Email;
                patientToUpdate.Password = patient.Password;
                db.SaveChanges();
                ViewBag.UpdatedMessage= "Your informations updated succesfully!";
                return View("PatientProfile", patientToUpdate);
            }
            
            
        }
    }
}