using SoftwareProject.Models.EntityFramework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Helpers;
using System.Web.Mvc;

namespace SoftwareProject.Controllers
{
    public class DoctorController : Controller
    {
        MeDiagEntities7 db = new MeDiagEntities7();
        // GET: Doctor
        public ActionResult DoctorIndex()
        {
            var jointest = from d in db.Doctor
                           join dapp in db.DAppDate on d.Id equals dapp.Doctor_id
                           join app in db.Appointment on dapp.Id equals app.DId
                           join ill in db.Illness on app.IllId equals ill.Id
                           join p in db.Patient on ill.PId equals p.Id
                           select new ViewModel
                           {
                               doctor = d,
                               dApp = dapp,
                               appointment = app,
                               ilness = ill,
                               patient = p
                           };
            ViewData["Jointable"] = jointest;
            
            Doctor doctor = new Doctor();
            if(TempData["getdoctorid"] == null)
            {
                doctor.Id = (int)TempData["senddoctorid"];
            }
            else
            {
                doctor.Id = (int)TempData["getdoctorid"];
            }
            
            
            return View(doctor);
        }
        public ActionResult DoctorProfile(int id)
        {
            var infoDoctor = db.Doctor.FirstOrDefault(x => x.Id == id);
            TempData["senddoctorid"] = id;
            return View(infoDoctor);
        }
        public ActionResult UpdateDoctor(Doctor doctor)
        {
            var doctorToUpdate = db.Doctor.Find(doctor.Id);
            if (doctorToUpdate == null)
            {
                return HttpNotFound();
            }
            else
            {

                if (doctor.Password == null)
                {
                    doctorToUpdate.Email = doctor.Email;
                    db.SaveChanges();
                    ViewBag.UpdatedMessage = "Your informations updated succesfully!";
                    return View("DoctorProfile", doctorToUpdate);
                }
                else if (doctor.Email == null)
                {

                    string hashedPassword = Crypto.SHA256(doctor.Password);
                    doctorToUpdate.Password = hashedPassword;
                    db.SaveChanges();
                    ViewBag.UpdatedMessage = "Your informations updated succesfully!";
                    return View("DoctorProfile", doctorToUpdate);
                }
                else
                {
                    string hashedPassword = Crypto.SHA256(doctor.Password);
                    doctorToUpdate.Email = doctor.Email;
                    doctorToUpdate.Password = hashedPassword;
                    db.SaveChanges();
                    ViewBag.UpdatedMessage = "Your informations updated succesfully!";
                    return View("DoctorProfile", doctorToUpdate);
                }

            }
        }

        public ActionResult DoctorChat(int id)
        {
            var infoDoctor = db.Doctor.FirstOrDefault(x => x.Id == id);


            return View(infoDoctor);
        }
       
    }
}