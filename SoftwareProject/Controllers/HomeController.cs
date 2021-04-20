using SoftwareProject.Models.EntityFramework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SoftwareProject.Controllers
{    
    public class HomeController : Controller
    {
        MeDiagEntities7 db = new MeDiagEntities7();
        // GET: Home


        public ActionResult Index()
        {
            Patient patient = TempData["getid"] as Patient;
            return View(patient);
        }
    }
        
}
