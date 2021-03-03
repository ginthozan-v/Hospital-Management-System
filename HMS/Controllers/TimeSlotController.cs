using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.SqlServer;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using HMS.Models;
using HMS.BusinessLogic;
using HMS.ViewModels;

namespace HMS.Controllers
{
    [Authorize(Roles = "Admin")]
    public class TimeSlotController : Controller
    {
        public HMSEntities db = new HMSEntities();
        ScheduleView sh = new ScheduleView();

        private void DropdownList()
        {
            ViewBag.DoctorList = new SelectList(db.Doctors, "Doctor_ID", "First_Name");
            ViewBag.ShiftPatternList = new SelectList(db.ShiftPatterns, "ID", "Name");
        }
        // GET: TimeSlot
        public ActionResult Index()
        {
            var scheduleList = (from s in db.TimeSlots
                                select new TimeSlotViewModel
                                {
                                    TimeID = s.TimeID,
                                    Doctor = s.Doctor.First_Name,
                                    Specialization = s.Doctor.Specialization.Spcl_Name,
                                    Date = s.Date,
                                    StartTime = s.ShiftPattern.StartTime,
                                    Hours = s.Duration

                                }).ToList().OrderBy(s => s.Date);

            //.OrderBy(s => s.Date).Where(s => s.Date >= DateTime.Today)
                     
            ViewBag.SingleDayList = sh.GetSingleDayList();
            return View(scheduleList);
        }

        // GET: TimeSlot/Create
        public ActionResult Create()
        {
            DropdownList();
            return PartialView("CreateEdit", new CreateTimeSlotModel());
        }

        // GET: TimeSlot/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                TempData["Error"] = "HTTP Error 400.0 - Bad Request!";
                return PartialView("_Error");
            }
            //TimeSlot oPD_TimeSlot = db.TimeSlots.Find(id);

            var slot = (from t in db.TimeSlots
                        where t.TimeID == id

                        select new CreateTimeSlotModel
                        {
                            TimeID = t.TimeID,
                            Doctor_ID = t.Doctor_ID,
                            Date = t.Date,
                            SID = t.SID,

                        }).FirstOrDefault();

            if (slot == null)
            {
                TempData["Error"] = "HTTP Error 404.0 - Not Found!";
                return PartialView("_Error");
            }

            DropdownList();
            return PartialView("CreateEdit", slot);
        }

        // POST: Create/Edit
        [HttpPost]
        public ActionResult CreateEdit(CreateTimeSlotModel ts)
        {
            //ModelState.AddModelError("", "Model Error Added");
            if (ModelState.IsValid)
            {
                if (ts.TimeID == 0)
                {
                    List<DateTime> selectedDateList = new List<DateTime>();

                    for (var date = ts.StartDate; date.Date <= ts.EndDate; date = date.AddDays(1))
                    {

                        switch (date.DayOfWeek)
                        {
                            case DayOfWeek.Monday:
                                if (ts.Mon == true)
                                    selectedDateList.Add(date);
                                // code block
                                break;
                            case DayOfWeek.Tuesday:
                                if (ts.Tue == true)
                                    selectedDateList.Add(date);
                                // code block
                                break;
                            case DayOfWeek.Wednesday:
                                if (ts.Wed == true)
                                    selectedDateList.Add(date);
                                // code block
                                break;
                            case DayOfWeek.Thursday:
                                if (ts.Thu == true)
                                    selectedDateList.Add(date);
                                // code block
                                break;
                            case DayOfWeek.Friday:
                                if (ts.Fri == true)
                                    selectedDateList.Add(date);
                                // code block
                                break;
                            case DayOfWeek.Saturday:
                                if (ts.Sat == true)
                                    selectedDateList.Add(date);
                                // code block
                                break;
                            case DayOfWeek.Sunday:
                                if (ts.Sun == true)
                                    selectedDateList.Add(date);
                                // code block
                                break;
                            default:
                                // code block
                                break;
                        }
                    }

                    foreach (var x in selectedDateList)
                    {
                        if (db.TimeSlots.Any(t => t.Date.Equals(x) && t.Doctor_ID.Equals(ts.Doctor_ID) && t.SID.Equals(ts.SID)))
                        {
                            DropdownList();
                            TempData["Warning"] = "Schedule already allocated!";
                            return PartialView("CreateEdit", ts);
                        }
                        else
                        {
                            var schedule = new TimeSlot
                            {
                                SID = ts.SID,
                                Date = x,
                                Doctor_ID = ts.Doctor_ID,
                                Duration = ts.Hours,
                                Interval = ts.Interval
                            };
                            db.TimeSlots.Add(schedule);
                            db.SaveChanges();
                        }
                    }

                    TempData["Success"] = "Schedule Created Successfully!";
                    return Json(new { success = true });
                }
                else
                {

                    TimeSlot model = (from t in db.TimeSlots where t.TimeID == ts.TimeID select t).FirstOrDefault();

                    model.TimeID = ts.TimeID;
                    model.SID = ts.SID;
                    model.Date = ts.Date;
                    model.Doctor_ID = ts.Doctor_ID;
                    model.Duration = ts.Hours;
                    model.Interval = ts.Interval;

                    if (db.TimeSlots.Any(t => t.ShiftPattern.StartTime.Equals(ts.Time) && t.Date.Equals(ts.Date) && t.Doctor_ID.Equals(ts.Doctor_ID)))
                    {
                        DropdownList();
                        TempData["Warning"] = "Schedule already allocated!";
                        return Json(new { success = false });
                    }
                    else
                    {
                        db.SaveChanges();
                        TempData["Success"] = "Schedule Updated Successfully!";
                        return Json(new { success = true });
                    }
                }
            }
            ViewBag.Title = "Create Timeslot";
            DropdownList();
            TempData["Unsuccess"] = "Oops! Something went wrong.";
            return Json(new { success = true });
        }

        // GET: TimeSlot/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                TempData["Error"] = "HTTP Error 400.0 - Bad Request!";
                return PartialView("_Error");
            }
            TimeSlot oPD_TimeSlot = db.TimeSlots.Find(id);
            if (oPD_TimeSlot == null)
            {
                TempData["Error"] = "HTTP Error 404.0 - Not Found!";
                return PartialView("_Error");
            }
            return View(oPD_TimeSlot);
        }

        // POST: TimeSlot/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            TimeSlot oPD_TimeSlot = db.TimeSlots.Find(id);
            db.TimeSlots.Remove(oPD_TimeSlot);
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
