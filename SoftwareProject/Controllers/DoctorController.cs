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
        MeDiagEntities3 db = new MeDiagEntities3();
        // GET: Doctor
        public ActionResult DoctorIndex()
        {
            Doctor doctor = TempData["getdoctorid"] as Doctor;
            return View(doctor);
        }
        public ActionResult DoctorProfile(int id)
        {
            var infoDoctor = db.Doctor.FirstOrDefault(x => x.Id == id);
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
       
    }
}