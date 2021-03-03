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
    public class WardsController : Controller
    {
        private HMSEntities db = new HMSEntities();


        // GET: Wards
        public ActionResult Index()
        {
            var Wardlist = (from w in db.Wards
                            select new WardViewModel
                            {
                                Ward_No = w.Ward_No,
                                Ward_Status = w.Ward_Status
                            }).ToList().OrderByDescending(w => w.Ward_No);


            return View(Wardlist);
        }


        // GET: Wards/Create
        public ActionResult Create()
        {
            return PartialView("CreateEdit", new CreateWardModel());
        }

        // GET: Wards/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                TempData["Error"] = "HTTP Error 400.0 - Bad Request!";
                return PartialView("_Error");
            }

            var ward = (from w in db.Wards
                        where w.Ward_No == id

                        select new WardViewModel
                        {
                            Ward_No = w.Ward_No,
                            Ward_Status = w.Ward_Status
                        }).FirstOrDefault();

            if (ward == null)
            {
                TempData["Error"] = "HTTP Error 404.0 - Not Found!";
                return PartialView("_Error");
            }
            ViewBag.h_id = new SelectList(db.Hospitals, "Hospital_id", "h_Name", ward.Ward_No);
            return PartialView("CreateEdit", ward);
        }

        public ActionResult CreateEdit(CreateWardModel ward)
        {
            if (ModelState.IsValid)
            {
                if(ward.Ward_No == 0)
                {
                    var newWards = new Ward
                    {
                        Ward_Status = ward.Ward_Status,
                        Hospital_ID = 1
                    };
                    db.Wards.Add(newWards);
                    db.SaveChanges();
                    return Json(new { success = true });
                }
                else
                {
                    Ward model = (from w in db.Wards where w.Ward_No == ward.Ward_No select w).FirstOrDefault();
                    model.Ward_Status = ward.Ward_Status;
                    db.SaveChanges();


                    TempData["Success"] = "Ward Updated Successfully!";
                    return Json(new { success = true });
                }
            }
            ViewBag.h_id = new SelectList(db.Hospitals, "h_id", "h_Name", ward.Ward_No);
            TempData["Unsuccess"] = "Oops! Something went wrong.";
            return Json(new { success = true });
        }

        // GET: Wards/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                TempData["Error"] = "HTTP Error 400.0 - Bad Request!";
                return PartialView("_Error");
            }
            Ward ward = db.Wards.Find(id);
            if (ward == null)
            {
                TempData["Error"] = "HTTP Error 404.0 - Not Found!";
                return PartialView("_Error");
            }
            return PartialView(ward);
        }

        // POST: Wards/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Ward ward = db.Wards.Find(id);
            db.Wards.Remove(ward);
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
