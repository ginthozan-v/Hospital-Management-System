using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using HMS.Models;
using HMS.ViewModels;
using HMS.BusinessLogic;
using System.Net;
using System.Data.Entity;

namespace HMS.Controllers
{
    [Authorize(Roles = "Admin")]
    public class ConfigurationController : Controller
    {
        private HMSEntities db = new HMSEntities();
        SystemConfiguration config = new SystemConfiguration();

        // GET: Configuration Index
        public ActionResult Index()
        {
            ViewBag.ShiftPatternList = config.GetShiftPatternsList();
            ViewBag.DateFormatList = config.GetDateFormatList();
            ViewBag.GenderList = config.GetGenderList();
            ViewBag.BloodGroupList = config.GetBloodGroup();
            ViewBag.SpecializationList = config.GetSpecializationList();
            return View();
        }


        // GET: ShiftPatterns Create
        public ActionResult Create()
        {
            return PartialView("CreateEdit", new ShiftViewModel());
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

        //POST: CreateEdit
        public ActionResult CreateEdit(ShiftViewModel SP)
        {
            if (ModelState.IsValid)
            {
                //Create
                if (SP.ID == 0)
                {
                    var shift = new ShiftPattern
                    {
                       Id = SP.ID,
                       Name = SP.Name,
                       StartTime = SP.StartTime,
                    };

                    db.ShiftPatterns.Add(shift);
                    db.SaveChanges();
                    TempData["Success"] = "Shift Created Successfully!";
                    return Json(new { success = true });
                }

                //Edit
                else
                {
                    ShiftPattern model = (from sp in db.ShiftPatterns where sp.Id == SP.ID select sp).FirstOrDefault();

                    model.Id = SP.ID;
                    model.Name = SP.Name;
                    model.StartTime = SP.StartTime;

                    db.SaveChanges();
                    TempData["Success"] = "Shift Updated Successfully!";
                    return Json(new { success = true });
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
            var shift = (from sp in db.ShiftPatterns
                       where sp.Id == id

                       select new ShiftViewModel
                       {
                           ID = sp.Id
                       }).FirstOrDefault();

            if (shift == null)
            {
                TempData["Error"] = "HTTP Error 404.0 - Not Found!";
                return PartialView("_Error");
            }
            return PartialView(shift);
        }

        // POST: ShiftPatterns/Delete/5
        [HttpPost]
        //[ValidateAntiForgeryToken]
        public ActionResult Delete(ShiftViewModel sp)
        {
            try
            {
                ShiftPattern model = (from shift in db.ShiftPatterns where shift.Id == sp.ID select shift).FirstOrDefault();

                db.ShiftPatterns.Remove(model);
                db.SaveChanges();

                TempData["Success"] = "Shift Removed Successfully!";
                return Json(new { success = true });
            }
            catch
            {
                TempData["Unsuccess"] = "Couldn't Remove Appointment!";
                return PartialView("Delete", sp);
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
