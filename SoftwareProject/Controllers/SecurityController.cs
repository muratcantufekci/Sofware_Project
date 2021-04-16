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
        MeDiagEntities3 db = new MeDiagEntities3();
        // GET: Security
        public ActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Login(Patient patient)
        {
            string hashpassword = Crypto.SHA256(patient.Password);
            var patientInDb = db.Patient.FirstOrDefault(x => x.Email == patient.Email && x.Password == hashpassword);
            if(patientInDb != null)
            {
                FormsAuthentication.SetAuthCookie(patientInDb.Name, false);
                TempData["getid"] = patientInDb;
                return RedirectToAction("Index", "Home");
                
            }
            else
            {
                ViewBag.Message = "Wrong email or password";
                return View();
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
            if (doctorInDb != null)
            {
                FormsAuthentication.SetAuthCookie(doctorInDb.Name, false);
                TempData["getid"] = doctorInDb;
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