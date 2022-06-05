using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using PatientAppointmentSystem.Models;

namespace PatientAppointmentSystem.Controllers
{
    public class AppointmentsController : Controller
    {
        private DataModelEntities db = new DataModelEntities();

        // GET: Appointments
        public ActionResult Index()
        {
            var appointments = db.Appointments.Include(a => a.AvailableDay).Include(a => a.Department).Include(a => a.Doctor);
            return View(appointments.ToList());
        }  
        public ActionResult MyAppointment()
        {
            var n =Session["UserName"].ToString();
            var appointments = db.Appointments.Include(a => a.AvailableDay).Include(a => a.Department).Include(a => a.Doctor).Include(a=>a.User).Where(x=>x.UserName== n);
            return View(appointments.ToList());
        }

        // GET: Appointments/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Appointment appointment = db.Appointments.Find(id);
            if (appointment == null)
            {
                return HttpNotFound();
            }
            return View(appointment);
        }

        // GET: Appointments/Create
        public ActionResult Create()
        {
            ViewBag.DeptList = new SelectList(GetDeptList(), "DeptId", "Name");

            return View();
        }
     
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "UserName,ApId,PatientName,DeptId,DocId,AvdId,Address,MobileNo")] Appointment appointment)
        {
           
            if (ModelState.IsValid)
            {
                db.Appointments.Add(appointment);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.AvdId = new SelectList(db.AvailableDays, "AvdId", "AvdId", appointment.AvdId);
            ViewBag.DeptId = new SelectList(db.Departments, "DeptId", "Name", appointment.DeptId);
            ViewBag.DocId = new SelectList(db.Doctors, "DocId", "DoctorName", appointment.DocId);
            return View(appointment);
        }

        //// GET: Appointments/Edit/5
        //public ActionResult Edit(int? id)
        //{
        //    if (id == null)
        //    {
        //        return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
        //    }
        //    Appointment appointment = db.Appointments.Find(id);
        //    if (appointment == null)
        //    {
        //        return HttpNotFound();
        //    }
        //    ViewBag.DeptList = new SelectList(GetDeptList(), "DeptId", "Name");
        
        //    return View(appointment);
        //}

        //// POST: Appointments/Edit/5
        
        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public ActionResult Edit([Bind(Include = "ApId,PatientName,DeptId,DocId,AvdId,Address,MobileNo")] Appointment appointment)
        //{
        //    if (ModelState.IsValid)
        //    {
        //        db.Entry(appointment).State = EntityState.Modified;
        //        db.SaveChanges();
        //        return RedirectToAction("Index");
        //    }
        //    ViewBag.AvdId = new SelectList(db.AvailableDays, "AvdId", "AvdId", appointment.AvdId);
        //    ViewBag.DeptId = new SelectList(db.Departments, "DeptId", "Name", appointment.DeptId);
        //    ViewBag.DocId = new SelectList(db.Doctors, "DocId", "DoctorName", appointment.DocId);
        //    return View(appointment);
        //}

        // GET: Appointments/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Appointment appointment = db.Appointments.Find(id);
            if (appointment == null)
            {
                return HttpNotFound();
            }
            return View(appointment);
        }

        // POST: Appointments/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Appointment appointment = db.Appointments.Find(id);
            db.Appointments.Remove(appointment);
            db.SaveChanges();
            return RedirectToAction("Index");
        }
        public List<Department> GetDeptList()
        {
            List<Department> dptlist = db.Departments.ToList();
            return dptlist;
        }

        //for getting doc list
        public ActionResult GetDocList(int deptid)
        {
            List<Doctor> dlist = db.Doctors.Where(x => x.DeptId == deptid).ToList();
            ViewBag.DocOpt = new SelectList(dlist, "DocId", "DoctorName");
            return PartialView("_Doctor");
        }


        //For getting Available Day list
        public ActionResult GetDayList(int docid)
        {
           
            List<AvailableDay> adaylist = db.AvailableDays.Where(x => x.DocId == docid).Include(x => x.Day).ToList();
            ViewBag.DayOpt = new SelectList(adaylist, "AvdId", "DayId");
            return PartialView("_Day");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
