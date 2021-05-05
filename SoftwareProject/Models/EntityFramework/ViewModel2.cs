using SoftwareProject.Models.EntityFramework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SoftwareProject.Models
{
    public class ViewModel2
    {
        public Patient patients;
        public List<Doctor> doctors { get; set; }
        public List<Illness> ıllnesses { get; set; }
        public List<Hospital> hospitals { get; set; }
        public List<DAppDate> dAppDates { get; set; }
        public List<Department> departments { get; set; }
        public List<Appointment> appointments { get; set; }
        public Hospital hospital { get; set; }

    }
}