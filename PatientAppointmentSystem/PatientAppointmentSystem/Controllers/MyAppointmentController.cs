using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Data.Entity;
using PatientAppointmentSystem.Models;

namespace PatientAppointmentSystem.Controllers
{
    public class MyAppointmentController : Controller
    {
        DataModelEntities db = new DataModelEntities();
        public ActionResult Index()
        {
            var appointments = db.Appointments.Include(a => a.AvailableDay).Include(a => a.Department).Include(a => a.Doctor);
            return View(appointments.ToList());
        }
    }
}