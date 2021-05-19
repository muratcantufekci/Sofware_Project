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
        MeDiagEntities11 db = new MeDiagEntities11();
        // GET: Profile
        public ActionResult PatientProfile(int id)
        {
           
            var infoPatient = db.Patient.FirstOrDefault(x => x.Id == id);
            PatientHelper.id = infoPatient.Id;




            return View(infoPatient);
        }
        public ActionResult UpdatePatient(Patient patient)
        {
            
            Patient patientToUpdate = db.Patient.Find(patient.Id);
            
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
        public ActionResult PastOperations(ViewModel3 viewModel)
        {
            var findIllnessId = db.Illness.OrderByDescending(x => x.Id).FirstOrDefault(x => x.Name == viewModel.IllnessName);
            List<Appointment> appointmentList = db.Appointment.Where(x=>x.PatientId == PatientHelper.id).ToList();
            List<Patient> patientList = db.Patient.Where(x => x.Id == PatientHelper.id).ToList();
 
            ViewModel2 viewModel2 = new ViewModel2();
            viewModel2.appointments = appointmentList;
            viewModel2.patientss = patientList;
            viewModel2.PID = PatientHelper.id;


            return View(viewModel2);
        }
    }
}