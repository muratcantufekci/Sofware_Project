using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SoftwareProject.Models.EntityFramework
{
    public class ViewModel
    {
        public Doctor doctor { get; set; }
        public DAppDate dApp { get; set; }
        public Appointment appointment { get; set; }
        public Illness ilness { get; set; }
        public Patient patient { get; set; }
    }
}