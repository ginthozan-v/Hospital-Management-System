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
    public class HospitalController : Controller
    {
        private HMSEntities db = new HMSEntities();
      
        // GET: Hospital
        public ActionResult Index()
        {
            var hospitallist = (from h in db.Hospitals
                              select new HospitalViewModel
                              {
                                  Hospital_ID = h.Hospital_ID,
                                  Hospital_Name = h.Hospital_Name,
                                  Hospital_Address = h.Hospital_Address,
                                  Hospital_Phone = h.Hospital_Phone,
                                  Hospital_Email = h.Hospital_Email
                              }).ToList();

            return View(hospitallist);
        }

        // GET: Hospital/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                TempData["Warning"] = "HTTP Error 400.0 - Bad Request!";
                return RedirectToAction("Index");
            }
            Hospital hospital = db.Hospitals.Find(id);
            if (hospital == null)
            {
                TempData["Warning"] = "HTTP Error 404.0 - Not Found!";
                return RedirectToAction("Index");
            }
            return View(hospital);
        }

        // GET: Hospital/Create
        public ActionResult Create()
        {
            return PartialView("CreateEdit", new HospitalViewModel());
        }

        // GET: Hospital/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                TempData["Warning"] = "HTTP Error 400.0 - Bad Request!";
                return RedirectToAction("Index");
            }

            var hos = (from h in db.Hospitals
                       where h.Hospital_ID == id

                       select new HospitalViewModel
                       {
                           Hospital_ID = h.Hospital_ID,
                           Hospital_Name = h.Hospital_Name,
                           Hospital_Address = h.Hospital_Address,
                           Hospital_Phone = h.Hospital_Phone,
                           Hospital_Email = h.Hospital_Email

                       }).FirstOrDefault();

            if (hos == null)
            {
                TempData["Warning"] = "HTTP Error 404.0 - Not Found!";
                return RedirectToAction("Index");
            }
            return PartialView("CreateEdit", hos);
        }

        [HttpPost]
        public ActionResult CreateEdit(HospitalViewModel hos)
        {
            if (ModelState.IsValid)
            {
                if(hos.Hospital_ID == 0)
                {
                    var newHospital = new Hospital
                    {
                        Hospital_Name = hos.Hospital_Name,
                        Hospital_Address = hos.Hospital_Address,
                        Hospital_Phone = hos.Hospital_Phone,
                        Hospital_Email = hos.Hospital_Email
                    };

                    db.Hospitals.Add(newHospital);
                    db.SaveChanges();
                    return Json(new { success = true });
                }
                else
                {
                    Hospital model = (from h in db.Hospitals where h.Hospital_ID == hos.Hospital_ID select h).FirstOrDefault();
                    model.Hospital_Name = hos.Hospital_Name;
                    model.Hospital_Phone = hos.Hospital_Phone;
                    model.Hospital_Email = hos.Hospital_Email;
                    model.Hospital_Address = hos.Hospital_Address;
                    db.SaveChanges();

                    TempData["Success"] = "Hospital Updated Successfully!";
                    return Json(new { success = true });
                }
            }
            TempData["Unsuccess"] = "Oops! Something went wrong.";
            return Json(new { success = true });
        }

        // GET: Hospital/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                TempData["Warning"] = "HTTP Error 400.0 - Bad Request!";
                return RedirectToAction("Index");
            }
            Hospital hospital = db.Hospitals.Find(id);
            if (hospital == null)
            {
                TempData["Warning"] = "HTTP Error 404.0 - Not Found!";
                return RedirectToAction("Index");
            }
            return PartialView(hospital);
        }

        // POST: Hospital/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Hospital hospital = db.Hospitals.Find(id);
            db.Hospitals.Remove(hospital);
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
