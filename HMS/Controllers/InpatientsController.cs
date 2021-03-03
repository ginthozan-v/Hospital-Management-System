using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using HMS.Models;
using HMS.ViewModels;

namespace HMS.Controllers
{
    public class InpatientsController : Controller
    {
        private HMSEntities db = new HMSEntities();


        // GET: Inpatients
        public ActionResult Index()
        {
            //var inpatients = db.Inpatients.Include(i => i.Patient).Include(i => i.Ward);

            var Inpatientlist = (from i in db.
                               join p in db.Patients on i.patientsId equals p.id
                               join w in db.Wards on i.wardNo equals w.w_No
                               select new InpatientViewModel
                               {
                                   Id = p.id,
                                   PatientName = p.Name,
                                   WardNo = w.w_No,
                                   AdmissionDate = i.AdmissionDate,
                                   DischargeDate = i.DischargeDate
                               }).ToList();

            return View(Inpatientlist);
        }

        // GET: Inpatients/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Inpatient inpatient = db.Inpatients.Find(id);
            if (inpatient == null)
            {
                return HttpNotFound();
            }
            return View(inpatient);
        }

        // GET: Inpatients/Create
        public ActionResult Create()
        {
            ViewBag.patientsId = new SelectList(db.Patients, "id", "Name");
            ViewBag.Ward_No = new SelectList(db.Wards, "Ward_no", "Ward_no");
            return PartialView();
        }

        // POST: Inpatients/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "id,patientsId,Ward_No,AdmissionDate,DischargeDate")] Inpatient inpatient)
        {
            if (ModelState.IsValid)
            {
                db.Inpatients.Add(inpatient);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.patientsId = new SelectList(db.Patients, "id", "Name", inpatient.patientsId);
            ViewBag.Ward_No = new SelectList(db.Wards, "Ward_no", "Ward_no", inpatient.wardNo);
            return View(inpatient);
        }

        // GET: Inpatients/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Inpatient inpatient = db.Inpatients.Find(id);
            if (inpatient == null)
            {
                return HttpNotFound();
            }
            ViewBag.patientsId = new SelectList(db.Patients, "id", "Name", inpatient.patientsId);
            ViewBag.Ward_No = new SelectList(db.Wards, "Ward_no", "Ward_no", inpatient.wardNo);
            return PartialView(inpatient);
        }

        // POST: Inpatients/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "id,patientsId,Ward_No,AdmissionDate,DischargeDate")] Inpatient inpatient)
        {
            if (ModelState.IsValid)
            {
                db.Entry(inpatient).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.patientsId = new SelectList(db.Patients, "id", "Name", inpatient.patientsId);
            ViewBag.Ward_No = new SelectList(db.Wards, "Ward_no", "Ward_no", inpatient.wardNo);
            return View(inpatient);
        }

        // GET: Inpatients/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Inpatient inpatient = db.Inpatients.Find(id);
            if (inpatient == null)
            {
                return HttpNotFound();
            }
            return PartialView(inpatient);
        }

        // POST: Inpatients/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Inpatient inpatient = db.Inpatients.Find(id);
            db.Inpatients.Remove(inpatient);
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
