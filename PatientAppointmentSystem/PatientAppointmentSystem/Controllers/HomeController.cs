using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using PatientAppointmentSystem.Models;
namespace PatientAppointmentSystem.Controllers
{

    public class HomeController : Controller
    {
        DataModelEntities db = new DataModelEntities();
        public ActionResult Index()
        {

            return View();
        }
    }
}