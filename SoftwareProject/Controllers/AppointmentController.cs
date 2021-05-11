﻿using SoftwareProject.Models;
using SoftwareProject.Models.EntityFramework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SoftwareProject.Controllers
{
    public class AppointmentController : Controller
    {
        MeDiagEntities9 db = new MeDiagEntities9();
        // GET: Appointment
        [Authorize]
        public ActionResult AppointmentIndex(int id)
        {
            int findLastIllness = (int)TempData["getPID"];
            var model = new ViewModel2();
            model.doctors = db.Doctor.ToList();           
            model.hospitals = db.Hospital.ToList();
            model.departments = db.Department.ToList();
            model.dAppDates = db.DAppDate.ToList();
            model.patients = db.Patient.FirstOrDefault(x=>x.Id ==id);
            model.illness = db.Illness.FirstOrDefault(x => x.Id == findLastIllness);

            

            List<Hospital> hospital = db.Hospital.ToList();

            ViewBag.HospitalNames = new SelectList(hospital, "Id", "Name");

            return View(model);
        }

        [HttpPost]
        public ActionResult AppointmentIndex(ViewModel3 viewModel)
        {
            var findPatientId = db.Patient.FirstOrDefault(x => x.Email == viewModel.PatientEmail);
            if (ModelState.IsValid)
            {
                
                Illness ıllness = new Illness();
                ıllness.Name = viewModel.IllnessName;
                ıllness.PId = findPatientId.Id;
                db.Illness.Add(ıllness);
                db.SaveChanges();

                Hospital hospital = new Hospital();
                hospital.Name = viewModel.HospitalName;
                var findDoctorId = db.Doctor.FirstOrDefault(x => x.Name + " " + x.Surname == viewModel.DoctorName);
                var findIllnessId = db.Illness.FirstOrDefault(x => x.Name == viewModel.IllnessName && x.PId == findPatientId.Id);
                var findDappId = db.DAppDate.FirstOrDefault(x => x.Time == viewModel.DappTime && x.Doctor_id == findDoctorId.Id);
                Appointment appointment = new Appointment();
                appointment.IllId = findIllnessId.Id;
                appointment.DId = findDappId.Id;
                appointment.Name = viewModel.IllnessName;
                appointment.Date = viewModel.AppDate;
                db.Appointment.Add(appointment);
                db.SaveChanges();
                TempData["success"] = "Your appointment is succesfully created!";
                return RedirectToAction("PatientProfile", "Profile", findPatientId);
            }
            else
            {
                
                TempData["failed"] = "Please fill all blanks!";
                return RedirectToAction("AppointmentIndex", findPatientId.Id);
            }
            
        }
    }

}