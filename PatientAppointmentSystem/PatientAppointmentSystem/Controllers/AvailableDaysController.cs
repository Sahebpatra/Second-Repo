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
    public class AvailableDaysController : Controller
    {
        private DataModelEntities db = new DataModelEntities();

        // GET: AvailableDays
        public ActionResult Index()
        {
            var availableDays = db.AvailableDays.Include(a => a.Day).Include(a => a.Doctor);
            return View(availableDays.ToList());
        }

        // GET: AvailableDays/Create
        public ActionResult Create()
        {
            ViewBag.DayId = new SelectList(db.Days, "DayId", "Day1");
            ViewBag.DocId = new SelectList(db.Doctors, "DocId", "DoctorName");
            return View();
        }

        // POST: AvailableDays/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "AvdId,DayId,DocId")] AvailableDay availableDay)
        {
            if (ModelState.IsValid)
            {
                db.AvailableDays.Add(availableDay);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.DayId = new SelectList(db.Days, "DayId", "Day1", availableDay.DayId);
            ViewBag.DocId = new SelectList(db.Doctors, "DocId", "DoctorName", availableDay.DocId);
            return View(availableDay);
        }

        // GET: AvailableDays/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            AvailableDay availableDay = db.AvailableDays.Find(id);
            if (availableDay == null)
            {
                return HttpNotFound();
            }
            ViewBag.DayId = new SelectList(db.Days, "DayId", "Day1", availableDay.DayId);
            ViewBag.DocId = new SelectList(db.Doctors, "DocId", "DoctorName", availableDay.DocId);
            return View(availableDay);
        }

        // POST: AvailableDays/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "AvdId,DayId,DocId")] AvailableDay availableDay)
        {
            if (ModelState.IsValid)
            {
                db.Entry(availableDay).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.DayId = new SelectList(db.Days, "DayId", "Day1", availableDay.DayId);
            ViewBag.DocId = new SelectList(db.Doctors, "DocId", "DoctorName", availableDay.DocId);
            return View(availableDay);
        }

        // GET: AvailableDays/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            AvailableDay availableDay = db.AvailableDays.Find(id);
            if (availableDay == null)
            {
                return HttpNotFound();
            }
            return View(availableDay);
        }

        // POST: AvailableDays/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            AvailableDay availableDay = db.AvailableDays.Find(id);
            db.AvailableDays.Remove(availableDay);
            db.SaveChanges();
            return RedirectToAction("Index");
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
