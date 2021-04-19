using SoftwareProject.Models.EntityFramework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Helpers;
using System.Web.Mvc;
using System.Web.Security;

namespace SoftwareProject.Controllers
{
    public class SecurityController : Controller
    {
        MeDiagEntities5 db = new MeDiagEntities5();
        // GET: Security
        public ActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Login(Patient patient)
        {
            
            if (patient.Email !=null && patient.Password != null)
            {

                string hashpassword = Crypto.SHA256(patient.Password);
                var patientInDb = db.Patient.FirstOrDefault(x => x.Email == patient.Email && x.Password == hashpassword);
                var getPatientId = db.Patient.SingleOrDefault(x => x.Email == patient.Email).Id;

                if (patientInDb != null)
                {
                    FormsAuthentication.SetAuthCookie(patientInDb.Name, false);
                    TempData["getpatientid"] = getPatientId;
                    return RedirectToAction("Index", "Home");

                }
                else
                {
                    ViewBag.Message = "Wrong email or password";
                    return View();
                }
            }
            else
            {
                return View("Login");
            }
            

        }

        public ActionResult Logout()
        {
            FormsAuthentication.SignOut();
            return RedirectToAction("Index", "Home");
        }

        [HttpGet]
        public ActionResult SignUp()
        {
            return View(new Patient());
        }
       
        [HttpPost]
        public ActionResult SignUp(Patient patient)
        {
            if (!ModelState.IsValid)
            {

                return View("SignUp");

            }
            if (ModelState.IsValid)
            {
                Patient patientToAdd = new Patient();
                var patientControl = db.Patient.FirstOrDefault(x => x.Email == patient.Email || x.IdNumber == patient.IdNumber);
                if (patientControl != null)
                {
                    ViewBag.Message2 = "There is an account with this email or Id number please log in your account";
                    return View("Login");
                }
                else
                {
        
                    patientToAdd.Name = patient.Name;
                    patientToAdd.Surname = patient.Surname;
                    patientToAdd.Password = Crypto.SHA256(patient.Password);
                    patientToAdd.Email = patient.Email;
                    patientToAdd.IdNumber = patient.IdNumber;
                    db.Patient.Add(patientToAdd);
                    db.SaveChanges();
                    return RedirectToAction("Index", "Home");
                }
            }
            return View("SignUp");
        }
        public ActionResult DoctorLogin()
        {

            return View();
        }

        [HttpPost]
        public ActionResult DoctorLogin(Doctor doctor)
        {
            string hashpassword = Crypto.SHA256(doctor.Password);
            var doctorInDb = db.Doctor.FirstOrDefault(x => x.Email == doctor.Email && x.Password == hashpassword);
            var getDoctorId = db.Doctor.SingleOrDefault(x => x.Email == doctor.Email).Id;
            if (doctorInDb != null)
            {
                FormsAuthentication.SetAuthCookie(doctorInDb.Name, false);
                TempData["getdoctorid"] = getDoctorId;
                return RedirectToAction("DoctorIndex", "Doctor");

            }
            else
            {
                ViewBag.Message = "Wrong email or password";
                return View();
            }

        }
        public ActionResult DoctorLogout()
        {
            FormsAuthentication.SignOut();
            return RedirectToAction("Index", "Home");
        }
    }
}