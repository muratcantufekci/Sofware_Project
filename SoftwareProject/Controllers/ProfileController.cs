using SoftwareProject.Models.EntityFramework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Helpers;
using System.Web.Mvc;

namespace SoftwareProject.Controllers
{
    public class ProfileController : Controller
    {
        MeDiagEntities8 db = new MeDiagEntities8();
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
                
                if (patient.Password == null)
                {
                    patientToUpdate.Email = patient.Email;
                    db.SaveChanges();
                    ViewBag.UpdatedMessage = "Your informations updated succesfully!";
                    return View("PatientProfile", patientToUpdate);
                }
                else if(patient.Email == null)
                {

                    string hashedPassword = Crypto.SHA256(patient.Password);
                    patientToUpdate.Password = hashedPassword;
                    db.SaveChanges();
                    ViewBag.UpdatedMessage = "Your informations updated succesfully!";
                    return View("PatientProfile", patientToUpdate);
                }
                else
                {
                    string hashedPassword = Crypto.SHA256(patient.Password);
                    patientToUpdate.Email = patient.Email;
                    patientToUpdate.Password = hashedPassword;
                    db.SaveChanges();
                    ViewBag.UpdatedMessage = "Your informations updated succesfully!";
                    return View("PatientProfile", patientToUpdate);
                }
                
            }
            
            
        }
    }
}