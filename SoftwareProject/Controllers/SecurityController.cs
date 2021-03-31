﻿using SoftwareProject.Models.EntityFramework;
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
            Patient patientToAdd = new Patient();
            patientToAdd.Name = patient.Name;
            patientToAdd.Surname = patient.Surname;
            patientToAdd.Password = patient.Password;
            patientToAdd.Email = patient.Email;
            patientToAdd.IdNumber = patient.IdNumber;
            db.Patient.Add(patientToAdd);
            db.SaveChanges();
            return RedirectToAction("Index", "Home");
        }
    }
}