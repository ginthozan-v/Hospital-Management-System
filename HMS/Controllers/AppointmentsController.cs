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
    public class AppointmentsController : Controller

    {
        private HMSEntities db = new HMSEntities();

        private void DropdownList()
        {
            ViewBag.PatientList = new SelectList(db.Patients, "Patient_ID", "First_Name");
            ViewBag.DeseaseList = new SelectList(db.Deseases, "Desease_ID", "Desease1");
            ViewBag.DoctorList = new SelectList(db.Doctors, "Doctor_ID", "First_Name");
            ViewBag.DateList = new SelectList(db.TimeSlots, "TimeID", "Date");
            ViewBag.TimeList = new SelectList(db.ShiftPatterns, "ID", "StartTime");
        }


        // GET: Appointments List
        public ActionResult Index()
        {
            //var appointments = db.Appointments.Include(a => a.Patient).Include(a => a.Doctor);
            var appointmentlist = (from a in db.Appointments
                                   select new AppointmentViewModel
                                   {
                                       Appointment_ID = a.Appointment_ID,
                                       Patient_Name = a.Patient.First_Name,
                                       Doctor_Name = a.Doctor.First_Name,
                                       Desease = a.Desease.Desease1,
                                       Appointment_Status = a.Appointment_Status,
                                       Appointment_Date = a.TimeSlot.Date,
                                       Appointment_Time = a.Appointment_Time
                                   }).ToList().OrderByDescending(a => a.Appointment_ID);

            return View(appointmentlist);
        }


        // GET: Appointments/Create
        public ActionResult Create()
        {
            DropdownList();
            return PartialView("CreateEdit", new CreateAppointmentModel());
        }


        //GetDoctor Ajax
        public JsonResult GetDoctor(int Id)
        {
            var DoctorList = (from d in db.Doctors
                              join dd in db.Deseases_Doctors on d.Doctor_ID equals dd.Doctor_ID
                              where dd.Desease_ID == Id
                              select new CreateAppointmentModel
                              {
                                  Doctor_ID = d.Doctor_ID,
                                  Doctor_Name = d.First_Name

                              }).ToList();

            return Json(DoctorList, JsonRequestBehavior.AllowGet);
        }

        //GetDate Ajax
        public JsonResult GetDate(int? Id)
        {
            var DateList = (from d in db.TimeSlots
                            join doc in db.Doctors on d.Doctor_ID equals doc.Doctor_ID
                            where doc.Doctor_ID == Id
                            orderby d.Date
                            where d.Date >= DateTime.Today
                            select new CreateAppointmentModel
                            {
                                TimeID = d.TimeID,
                                App_Date = d.Date.ToString()

                            }).ToList();

            return Json(DateList, JsonRequestBehavior.AllowGet);
        }

        //GetTime Ajax 
        public JsonResult GetTime(int Id)
        {
            var TimeList = (from d in db.ShiftPatterns
                            join ts in db.TimeSlots on d.Id equals ts.SID
                            where ts.TimeID == Id
                            select new CreateAppointmentModel
                            {
                                Appointment_Time = d.StartTime,
                                Hour = d.StartTime.Hours,
                                Duration = ts.Duration,
                                Interval = ts.Interval

                            }).FirstOrDefault();
            return Json(TimeList, JsonRequestBehavior.AllowGet);
        }


        //GET: Appointments/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                TempData["Error"] = "HTTP Error 400.0 - Bad Request!";
                return PartialView("_Error");
            }

            var app = (from a in db.Appointments
                       where a.Appointment_ID == id

                       select new CreateAppointmentModel
                       {
                           Appointment_ID = a.Appointment_ID,
                           Patient_ID = a.Patient_ID,
                           Doctor_ID = a.Doctor_ID,
                           Desease_ID = a.Desease_ID,
                           Appointment_Status = a.Appointment_Status,
                           Appointment_Date_Id = a.Appointment_Date,
                           Appointment_Time = a.Appointment_Time,

                       }).FirstOrDefault();

            if (app == null)
            {
                TempData["Error"] = "HTTP Error 404.0 - Not Found!";
                return PartialView("_Error");
            }

            DropdownList();
            return PartialView("CreateEdit", app);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult CreateEdit(CreateAppointmentModel app)
        {
            if (ModelState.IsValid)
            {
                if (app.Appointment_ID == 0)
                {
                    var newApmt = new Appointment
                    {
                        Patient_ID = app.Patient_ID,
                        Doctor_ID = app.Doctor_ID,
                        Desease_ID = app.Desease_ID,
                        Appointment_Status = 2,
                        Appointment_Date = app.Appointment_Date_Id,
                        Appointment_Time = app.Appointment_Time
                    };

                    db.Appointments.Add(newApmt);
                    db.SaveChanges();
                    TempData["Success"] = "Appointment Created Successfully!";
                    return Json(new { success = true });
                }
                else
                {
                    Appointment model = (from a in db.Appointments where a.Appointment_ID == app.Appointment_ID select a).FirstOrDefault();

                    model.Patient_ID = app.Patient_ID;
                    model.Doctor_ID = app.Doctor_ID;
                    model.Desease_ID = app.Desease_ID;
                    model.Appointment_Status = app.Appointment_Status;
                    model.Appointment_Date = app.Appointment_Date_Id;
                    model.Appointment_Time = app.Appointment_Time;

                    db.SaveChanges();
                    TempData["Success"] = "Appointment Updated Successfully!";
                    return Json(new { success = true });
                }

            }
            DropdownList();
            TempData["Unsuccess"] = "Oops! Something went wrong.";
            return Json(new { success = true });
        }

        //GET: Appointments/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                TempData["Error"] = "HTTP Error 400.0 - Bad Request!";
                return PartialView("_Error");
            }
            var app = (from a in db.Appointments
                       where a.Appointment_ID == id

                       select new CreateAppointmentModel
                       {
                           Appointment_ID = a.Appointment_ID
                       }).FirstOrDefault();

            if (app == null)
            {
                TempData["Error"] = "HTTP Error 404.0 - Not Found!";
                return PartialView("_Error");
            }
            return PartialView(app);
        }

        //Delete POST
        [HttpPost]
        public ActionResult Delete(CreateAppointmentModel app)
        {
            try
            {
                Appointment model = (from a in db.Appointments where a.Appointment_ID == app.Appointment_ID select a).FirstOrDefault();

                db.Appointments.Remove(model);
                db.SaveChanges();

                TempData["Success"] = "Appointment Removed Successfully!";
                return Json(new { success = true });
            }
            catch
            {
                TempData["Unsuccess"] = "Couldn't Remove Appointment!";
                return PartialView("Delete", app);
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

    public class Err : HandleErrorAttribute
    {
        public override void OnException(ExceptionContext filterContext)
        {
            Exception ex = filterContext.Exception;
            filterContext.ExceptionHandled = true;
            var model = new HandleErrorInfo(filterContext.Exception, "Controller", "Action");

            filterContext.Result = new ViewResult()
            {
                ViewName = "_Error",
                ViewData = new ViewDataDictionary(model)
            };
        }
    }
}