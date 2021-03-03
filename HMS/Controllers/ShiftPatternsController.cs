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
    public class ShiftPatternsController : Controller
    {
        private HMSEntities db = new HMSEntities();

        // GET: ShiftPatterns
        public ActionResult Index()
        {
            return View(db.ShiftPatterns.ToList());
        }

        // GET: ShiftPatterns/Create
        public ActionResult Create()
        { 
            return PartialView("CreateEdit", new ShiftPattern());
        }

        // GET: ShiftPatterns/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                TempData["Error"] = "HTTP Error 400.0 - Bad Request!";
                return PartialView("_Error");
            }

            var shift = (from sf in db.ShiftPatterns
                       where sf.Id == id

                       select new ShiftViewModel
                       {
                           ID = sf.Id,
                           Name = sf.Name,
                           StartTime = sf.StartTime
                       }).FirstOrDefault();

            if (shift == null)
            {
                TempData["Error"] = "HTTP Error 404.0 - Not Found!";
                return PartialView("_Error");
            }

            return PartialView("CreateEdit", shift);
        }


        public ActionResult CreateEdit(ShiftPattern SP)
        {
            if (ModelState.IsValid)
            {
                //Create
                if (SP.Id == 0)
                {
                    db.ShiftPatterns.Add(SP);
                    db.SaveChanges();
                    TempData["Success"] = "Shift Created Successfully!";
                    return Json(new { success = true });
                }
                //Edit
                else
                {
                    //ShiftPattern shiftPattern = (from sp in db.ShiftPatterns where sp.ID == shift.ID select sp).FirstOrDefault();

                    //shiftPattern.ID = shift.ID;
                    //shiftPattern.Name = shift.Name;
                    //shiftPattern.StartTime = shift.StartTime;
                    //shiftPattern.Duration = shift.Duration;

                    //db.SaveChanges();
                    //TempData["Success"] = "Shift Updated Successfully!";
                    db.Entry(SP).State = EntityState.Modified;
                    db.SaveChanges();
                    return RedirectToAction("Index");
                }

            }
            TempData["Unsuccess"] = "Oops! Something went wrong.";
            return Json(new { success = true });
        }



    
        // GET: ShiftPatterns/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                TempData["Error"] = "HTTP Error 400.0 - Bad Request!";
                return PartialView("_Error");
            }
            ShiftPattern shiftPattern = db.ShiftPatterns.Find(id);
            if (shiftPattern == null)
            {
                TempData["Error"] = "HTTP Error 404.0 - Not Found!";
                return PartialView("_Error");
            }
            return View(shiftPattern);
        }

        // POST: ShiftPatterns/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            ShiftPattern shiftPattern = db.ShiftPatterns.Find(id);
            db.ShiftPatterns.Remove(shiftPattern);
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
