using HMS.BusinessLogic;
using HMS.Models;
using HMS.ViewModels;
using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace HMS.Controllers
{
    [Authorize(Roles = "Doctor")]
    public class DoctorDashBoardController : Controller
    {
        DoctorDashBoard docDash = new DoctorDashBoard();
        private HMSEntities db = new HMSEntities();

        // GET: DoctorDashBoard
        public ActionResult Index()
        {
            ViewBag.DocAppList = docDash.GetTodayAppointmentList();
            ViewBag.DocUpcomingApp = docDash.GetUpcomingAppointmentList();
            return View();
        }

        //Patient Details
        public ActionResult PatientDetails(int? id)
        {
            if (id == null)
            {
                TempData["Warning"] = "HTTP Error 400.0 - Bad Request!";
                return RedirectToAction("Index");
            }
            
            var patient = (from ap in db.Appointments
                           join p in db.Patients on ap.Patient_ID equals p.Patient_ID
                           join a in db.Addresses on p.AddressID equals a.Id
                           where ap.Appointment_ID == id


                           select new PatientViewModel
                           {
                               Patient_ID = p.Patient_ID,
                               First_Name = p.First_Name,
                               Last_Name = p.Last_Name,
                               Patient_Phone = p.Patient_Phone,
                               Patient_Age = p.Patient_Age,
                               Gender = p.Gender.Gender1,
                               Bloodgroup = p.Bloodgroup.BloodGroup1,
                               Patient_Email = p.Patient_Email,

                               Address = new AddressViewModel
                               {
                                   Id = p.Address.Id,
                                   Street = a.Street,
                                   Province = a.Province,
                                   City = a.City,
                                   Post_Code = a.Post_Code,
                                   Country = a.Country
                               }

                           }).FirstOrDefault();



            var PatientHealthDetails = (from ph in db.PatientHealthDetails
                                        join p in db.Patients on ph.PatientId equals p.Patient_ID
                                        where p.Patient_ID == patient.Patient_ID
                                        select new PatientHealthDetailViewModel
                                        {
                                            Height = ph.Height,
                                            Weight = ph.Weight,
                                            HeartRate = ph.HeartRate,
                                            BodyTemperature = ph.BodyTemperature,
                                            Glucose = ph.Glucose,
                                            Allergies = ph.Allergies,
                                            Diseases = ph.Diseases,
                                            LastVisit = ph.LastVisit,

                                        }).FirstOrDefault();



            var prescriptionList = (from pr in db.Prescriptions
                                    join p in db.Patients on pr.PatientId equals p.Patient_ID
                                    where p.Patient_ID == patient.Patient_ID
                                    orderby pr.DateTime descending

                                    select new PrescriptionViewModel
                                    {
                                        PrescriptionId = pr.PrescriptionId,
                                        Desease = pr.Appointment.Desease.Desease1,
                                        Date = pr.DateTime,
                                        Years = System.Data.Entity.DbFunctions.DiffYears(pr.DateTime, DateTime.Now),
                                        Months = System.Data.Entity.DbFunctions.DiffMonths(pr.DateTime, DateTime.Now),
                                        Days = System.Data.Entity.DbFunctions.DiffDays(pr.DateTime, DateTime.Now),
                                        Hours = System.Data.Entity.DbFunctions.DiffHours(pr.DateTime, DateTime.Now),
                                        Minutes = System.Data.Entity.DbFunctions.DiffMinutes(pr.DateTime, DateTime.Now),
                                        Seconds = System.Data.Entity.DbFunctions.DiffSeconds(pr.DateTime, DateTime.Now)
                                    }).ToList();


            if (patient == null)
            {
                TempData["Warning"] = "HTTP Error 404.0 - Not Found!";
                return RedirectToAction("Index");
                //return HttpNotFound();
            }
            ViewBag.PrescriptionList = prescriptionList;
            ViewBag.PatientHealthDetail = PatientHealthDetails;

            return View("_PatientDetails", patient);
        }

        // GET: Prescription/Create
        [HttpGet]
        public ActionResult Create(int id)
        {
            var userID = User.Identity.GetUserId();

            var patientID = (from p in db.Patients
                             join ap in db.Appointments on p.Patient_ID equals ap.Patient_ID
                             where p.Patient_ID == id
                             where ap.TimeSlot.Date == DateTime.Today

                             from d in db.Doctors
                             join a in db.AspNetUsers on d.Doctor_ID equals a.DoctorId
                             where a.Id == userID

                             //from ap in db.Appointments
                             select new CreatePrescriptionViewModel
                             {
                                 PrescriptionId = 0,
                                 AppointmentId = ap.Appointment_ID,
                                 PatientId = p.Patient_ID,
                                 DoctorId = d.Doctor_ID
                             }).FirstOrDefault();
            return PartialView("_CreateEditPrescription", patientID);
        }

        // POST: Prescription/Create
        [HttpPost]
        public ActionResult CreateEdit(CreatePrescriptionViewModel prescription)
        {
            //checked Error
            //IEnumerable<ModelError> allErrors = ModelState.Values.SelectMany(v => v.Errors);

            if (ModelState.IsValid)
            {
                if (prescription.PrescriptionId == 0)
                {
                    var newPrescription = new Prescription
                    {
                        AppointmentId = prescription.AppointmentId,
                        PatientId = prescription.PatientId,
                        DoctorId = prescription.DoctorId,
                        Drug_Name = prescription.Drug_Name,

                        Quantity = prescription.Quantity + " " + prescription.Qty,
                        Instruction = prescription.Instruction,
                        DateTime = DateTime.Now
                    };

                    db.Prescriptions.Add(newPrescription);
                    db.SaveChanges();
                    TempData["Success"] = "Prescription Created Successfully!";
                    return Json(new { success = true });
                }
                else
                {
                    Prescription model = (from p in db.Prescriptions where p.PrescriptionId == prescription.PrescriptionId select p).FirstOrDefault();
                    model.Drug_Name = prescription.Drug_Name;
                    model.Quantity = prescription.Quantity;
                    model.Instruction = prescription.Instruction;
                    model.DateTime = DateTime.Now;
                }
            }
            TempData["Unsuccess"] = "Oops! Something went wrong.";
            return Json(new { success = true });
        }

        //GET: Prescription Details
        [HttpGet]
        public ActionResult PrescriptionDetails(int? id)
        {
            if (id == null)
            {
                TempData["Error"] = "HTTP Error 400.0 - Bad Request!";
                return PartialView("_Error");
            }

            var prescription = (from pr in db.Prescriptions
                       where pr.PrescriptionId == id

                       select new CreatePrescriptionViewModel
                       {
                           PrescriptionId = pr.PrescriptionId,
                           AppointmentId = pr.AppointmentId,
                           Desease = pr.Appointment.Desease.Desease1,
                           Drug_Name = pr.Drug_Name,
                           Quantity = pr.Quantity,
                           Instruction = pr.Instruction,
                           DateTime = pr.DateTime
                       }).FirstOrDefault();

            if (prescription == null)
            {
                TempData["Error"] = "HTTP Error 404.0 - Not Found!";
                return PartialView("_Error");
            }

            return PartialView("_presDetails", prescription);
        }

        //GET : Patient Health Create
        [HttpGet]
        public ActionResult CreateHealth(int id)
        {
            var patientId = (from p in db.Patients
                             where p.Patient_ID == id

                             select new PatientHealthDetailViewModel
                             {
                                 PatientId = p.Patient_ID

                             }).FirstOrDefault();

            return PartialView("_updatePatient", patientId);
        }


        //GET: Patient health Update
        [HttpGet]
        public ActionResult Update(int id)
        {
            if (id == null)
            {
                TempData["Error"] = "HTTP Error 400.0 - Bad Request!";
                return PartialView("_Error");
            }

            var patHealth = (from ph in db.PatientHealthDetails
                             where ph.PatientId == id

                             select new PatientHealthDetailViewModel
                             {
                                 Ph_Id = ph.Ph_Id,
                                 PatientId = ph.PatientId,
                                 Height = ph.Height,
                                 Weight = ph.Weight,
                                 HeartRate = ph.HeartRate,
                                 BodyTemperature = ph.BodyTemperature,
                                 Glucose = ph.Glucose,
                                 Allergies = ph.Allergies,
                                 Diseases = ph.Diseases
                             }).FirstOrDefault();

            if (patHealth == null)
            {
                TempData["Error"] = "HTTP Error 404.0 - Not Found!";
                return PartialView("_Error");
            }

            return PartialView("_updatePatient", patHealth);
        }

        //POST : Patient Create/Update
        [HttpPost]
        public ActionResult Update(PatientHealthDetailViewModel ph)
        {
            if (ModelState.IsValid)
            {
                if (ph.Ph_Id == 0)
                {
                    var patHealth = new PatientHealthDetail
                    {
                        PatientId = ph.PatientId,
                        Height = ph.Height,
                        Weight = ph.Weight,
                        HeartRate = ph.HeartRate,
                        BodyTemperature = ph.BodyTemperature,
                        Glucose = ph.Glucose,
                        Allergies = ph.Allergies,
                        Diseases = ph.Diseases,
                        LastVisit = DateTime.Today
                    };

                    db.PatientHealthDetails.Add(patHealth);
                    db.SaveChanges();
                    TempData["Success"] = "Patient Health Update Successfully!";
                    return Json(new { success = true });
                }
                else
                {
                    PatientHealthDetail model = (from phEdit in db.PatientHealthDetails where phEdit.Ph_Id == ph.Ph_Id select phEdit).FirstOrDefault();
                    model.Height = ph.Height;
                    model.BodyTemperature = ph.BodyTemperature;
                    model.Allergies = ph.Allergies;
                    model.HeartRate = ph.HeartRate;
                    model.Glucose = ph.Glucose;
                    model.Diseases = ph.Diseases;
                    model.Weight = ph.Weight;
                    model.LastVisit = DateTime.Today;

                    db.SaveChanges();

                    TempData["Success"] = "Patient Health Updated Successfully!";
                    return Json(new { success = true });
                }
            }
            TempData["Unsuccess"] = "Oops! Something went wrong.";
            return Json(new { success = true });
        }
    }




}