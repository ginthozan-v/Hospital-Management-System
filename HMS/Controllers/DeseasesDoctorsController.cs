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
    [Authorize(Roles = "Admin")]
    public class DeseasesDoctorsController : Controller
    {
        private HMSEntities db = new HMSEntities();

        private void DropdownList()
        {
            ViewBag.DeseaseList = new SelectList(db.Deseases, "Desease_ID", "Desease1");
            ViewBag.DoctorList = new SelectList(db.Doctors, "Doctor_ID", "First_Name");
        }

        // GET: DeseasesDoctors
        public ActionResult Index()
        {
            var desDoc = (from dd in db.Deseases_Doctors
                                   select new DeseasesDoctorsViewModel
                                   {
                                       dd_ID = dd.dd_ID,
                                       Desease = dd.Desease.Desease1,
                                       Doctor = dd.Doctor.First_Name
                                   }).ToList().OrderByDescending(dd => dd.dd_ID);

            return View(desDoc);
        }

        // GET: DeseasesDoctors/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                TempData["Error"] = "HTTP Error 400.0 - Bad Request!";
                return PartialView("_Error");
            }
            Deseases_Doctors deseases_Doctors = db.Deseases_Doctors.Find(id);
            if (deseases_Doctors == null)
            {
                TempData["Error"] = "HTTP Error 404.0 - Not Found!";
                return PartialView("_Error");
            }
            return View(deseases_Doctors);
        }

        // GET: DeseasesDoctors/Create
        public ActionResult Create()
        {
            DropdownList();
            return PartialView("CreateEdit",new DeseasesDoctorsViewModel());
        }

        // GET: DeseasesDoctors/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                TempData["Error"] = "HTTP Error 400.0 - Bad Request!";
                return PartialView("_Error");
            }

            var desDoc = (from dd in db.Deseases_Doctors
                          where dd.dd_ID == id

                          select new DeseasesDoctorsViewModel
                          {
                              dd_ID = dd.dd_ID,
                              Desease_ID = dd.Desease_ID,
                              Doctor_ID = dd.Doctor_ID

                          }).FirstOrDefault();

            if (desDoc == null)
            {
                TempData["Error"] = "HTTP Error 404.0 - Not Found!";
                return PartialView("_Error");
            }
            DropdownList();
            return PartialView("CreateEdit", desDoc);
        }

        [HttpPost]
        public ActionResult CreateEdit(DeseasesDoctorsViewModel desdoc)
        {
            if (ModelState.IsValid)
            {
                if (desdoc.dd_ID == 0)
                {
                    var newDesDoc = new Deseases_Doctors
                    {
                        dd_ID = desdoc.dd_ID,
                        Desease_ID = desdoc.Desease_ID,
                        Doctor_ID = desdoc.Doctor_ID
                    };

                    db.Deseases_Doctors.Add(newDesDoc);
                    db.SaveChanges();

                    TempData["Success"] = "Assigned Successfully!";
                    return Json(new { success = true });
          
                }
                else
                {
                    Deseases_Doctors model = (from d in db.Deseases_Doctors where d.dd_ID == desdoc.dd_ID select d).FirstOrDefault();

                    model.Desease_ID = desdoc.Desease_ID;
                    model.Doctor_ID = desdoc.Doctor_ID;
                    

                    db.SaveChanges();
                    TempData["Success"] = "Updated Successfully!";
                    return Json(new { success = true });
                }
            }
            DropdownList();
            TempData["Unsuccess"] = "Oops! Something went wrong.";
            return Json(new { success = true });
        }

        // GET: DeseasesDoctors/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                TempData["Error"] = "HTTP Error 400.0 - Bad Request!";
                return PartialView("_Error");
            }
            var desDoc = (from dd in db.Deseases_Doctors
                          where dd.dd_ID == id

                          select new DeseasesDoctorsViewModel
                          {
                              dd_ID = dd.dd_ID

                          }).FirstOrDefault();

            if (desDoc == null)
            {
                TempData["Error"] = "HTTP Error 404.0 - Not Found!";
                return PartialView("_Error");
            }
            return PartialView("Delete", desDoc);
        }

        // POST: DeseasesDoctors/Delete/5
        [HttpPost]
        //[ValidateAntiForgeryToken]
        public ActionResult Delete(DeseasesDoctorsViewModel desDoc)
        {
            try
            {
                Deseases_Doctors model = (from dd in db.Deseases_Doctors where dd.dd_ID == desDoc.dd_ID select dd).FirstOrDefault();

                db.Deseases_Doctors.Remove(model);
                db.SaveChanges();

                TempData["Success"] = "Removed Successfully!";
                return Json(new { success = true });
            }
            catch
            {
                TempData["Unsuccess"] = "Remove Appointment!";
                return PartialView("Delete", desDoc);
            }
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