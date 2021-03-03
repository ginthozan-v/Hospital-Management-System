using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data.Entity.Core.Objects;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using HMS.BusinessLogic;
using HMS.Models;
using HMS.ViewModels;
using Microsoft.AspNet.Identity;

namespace HMS.Controllers
{
    [Authorize(Roles = "Admin")]
    public class HomeController : Controller
    {
        private HMSEntities db = new HMSEntities();
        Dashboard dash = new Dashboard();

        public JsonResult GetTotalPat()
        {
            var PatientCount = (from p in db.Patients
                                where p.Registered_Date.Year == System.DateTime.Now.Year
                                select p).Count();
            return Json(PatientCount, JsonRequestBehavior.AllowGet);
        }

        public JsonResult GetTotaDoc()
        {
            var DoctorCount = db.Doctors.Count();
            return Json(DoctorCount, JsonRequestBehavior.AllowGet);
        }

        public ActionResult Index()
        {
            var DoctorCount = db.Doctors.Count();
            var PatientCount = db.Patients.Count();

            //var patient = new ObservableCollection<int>();
            //var PatientCount = (from p in patient
            //                        //where p.Registered_Date.Year == System.DateTime.Now.Year
            //                    select p).Count();

            ViewBag.TotPatients = PatientCount;
            ViewBag.TotDoctors = DoctorCount;

            ViewBag.AppointmentList = dash.GetTodayAppointmentList();
            ViewBag.DoctorList = dash.GetTodayAttendanceDoctors();
            return View();
        }
        
        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }


        //GET : Appointment Details
        [HttpGet]
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                TempData["Error"] = "HTTP Error 400.0 - Bad Request!";
                return PartialView("_Error");
            }

            var app = (from a in db.Appointments
                       where a.Appointment_ID == id

                       select new AppointmentViewModel
                       {
                           Appointment_ID = a.Appointment_ID,
                           Patient_Name = a.Patient.First_Name + " " + a.Patient.Last_Name,
                           Contact = a.Patient.Patient_Phone,
                           Doctor_Name = a.Doctor.Last_Name + " " + a.Doctor.First_Name,
                           Desease = a.Desease.Desease1,
                           Appointment_Status = a.Appointment_Status,
                           Appointment_Date = a.TimeSlot.Date,
                           Appointment_Time = a.Appointment_Time,
                       }).FirstOrDefault();

            if (app == null)
            {
                TempData["Error"] = "HTTP Error 404.0 - Not Found!";
                return PartialView("_Error");
            }

            return PartialView("_appDetails", app);
        }
    }
}