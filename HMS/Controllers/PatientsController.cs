using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using HMS.Models;
using HMS.BusinessLogic;
using HMS.ViewModels;

namespace HMS.Controllers
{
    [Authorize]
    public class PatientsController : Controller
    {
        private HMSEntities db = new HMSEntities();
        AddressClass add = new AddressClass();

        private void DropdownList()
        {
            ViewBag.BloodgroupList = new SelectList(db.Bloodgroups, "BG_ID", "BloodGroup1");
            ViewBag.GenderList = new SelectList(db.Genders, "Gender_ID", "Gender1");
        }

        // GET: Patients
        public ActionResult Index()
        {
            var patientlist = (from p in db.Patients
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
                                   Registered_Date = p.Registered_Date
                               }).ToList().OrderByDescending(p => p.Patient_ID);
            return View(patientlist);
        }

        // GET: Patients/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                TempData["Warning"] = "HTTP Error 400.0 - Bad Request!";
                return RedirectToAction("Index");
                //return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            var patient = (from p in db.Patients
                           join a in db.Addresses on p.AddressID equals a.Id
                           where p.Patient_ID == id

                           select new DetailPatientViewModel
                           {
                               Patient_ID = p.Patient_ID,
                               First_Name = p.First_Name,
                               Last_Name = p.Last_Name,
                               Patient_Phone = p.Patient_Phone,
                               Patient_Age = p.Patient_Age,
                               Gender = p.Gender.Gender1,
                               Bloodgroup = p.Bloodgroup.BloodGroup1,
                               Patient_Email = p.Patient_Email,
                               Registered_Date = p.Registered_Date,

                               Address = new AddressViewModel
                               {
                                   Id = a.Id,
                                   Street = a.Street,
                                   Province = a.Province,
                                   City = a.City,
                                   Post_Code = a.Post_Code,
                                   Country = a.Country
                               }

                           }).FirstOrDefault();

            if (patient == null)
            {
                TempData["Warning"] = "HTTP Error 404.0 - Not Found!";
                return RedirectToAction("Index");
                //return HttpNotFound();
            }
            return View(patient);
        }

        // GET: Patients/Create
        public ActionResult Create()
        {
            DropdownList();
            return PartialView("CreateEdit", new CreatePatientViewModel());
        }

        // GET: Patients/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                TempData["Warning"] = "HTTP Error 400.0 - Bad Request!";
                return RedirectToAction("Index");
            }

            var patient = (from p in db.Patients
                           join a in db.Addresses on p.AddressID equals a.Id
                           where p.Patient_ID == id

                           select new CreatePatientViewModel
                           {
                               Patient_ID = p.Patient_ID,
                               First_Name = p.First_Name,
                               Last_Name = p.Last_Name,
                               Patient_Phone = p.Patient_Phone,
                               Patient_Age = p.Patient_Age,
                               Gender_ID = p.Gender_ID,
                               BG_ID = p.BG_ID,
                               Patient_Email = p.Patient_Email,

                               Address = new AddressViewModel
                               {
                                   Id = a.Id,
                                   Street = a.Street,
                                   Province = a.Province,
                                   City = a.City,
                                   Post_Code = a.Post_Code,
                                   Country = a.Country
                               }

                           }).FirstOrDefault();

            if (patient == null)
            {
                TempData["Warning"] = "HTTP Error 404.0 - Not Found!";
                return RedirectToAction("Index");
                //return HttpNotFound();
            }

            DropdownList();
            return PartialView("CreateEdit", patient);
        }

        [HttpPost]
        public ActionResult CreateEdit(CreatePatientViewModel patientview)
        {
            //IEnumerable<ModelError> allErrors = ModelState.Values.SelectMany(v => v.Errors);
            //ModelState.AddModelError("","Error");
            if (ModelState.IsValid)
            {
                patientview.Address.Id = add.SaveAddress(patientview.Address);

                if (patientview.Patient_ID == 0)
                {
                    var newPatient = new Patient
                    {
                        First_Name = patientview.First_Name,
                        Last_Name = patientview.Last_Name,
                        Patient_Phone = patientview.Patient_Phone,
                        Patient_Age = patientview.Patient_Age,
                        Gender_ID = patientview.Gender_ID,
                        BG_ID = patientview.BG_ID,
                        Patient_Email = patientview.Patient_Email,
                        AddressID = patientview.Address.Id,
                        Registered_Date = DateTime.Now
                    };

                    db.Patients.Add(newPatient);
                    db.SaveChanges();
                    TempData["Success"] = "Patient Registered!";
                    return Json(new { success = true });
                }
                else
                {
                    Patient model = (from p in db.Patients where p.Patient_ID == patientview.Patient_ID select p).FirstOrDefault();
                    model.Patient_ID = patientview.Patient_ID;
                    model.First_Name = patientview.First_Name;
                    model.Last_Name = patientview.Last_Name;
                    model.Patient_Phone = patientview.Patient_Phone;
                    model.Patient_Age = patientview.Patient_Age;
                    model.Gender_ID = patientview.Gender_ID;
                    model.BG_ID = patientview.BG_ID;
                    model.Patient_Email = patientview.Patient_Email;
                    model.AddressID = patientview.Address.Id;

                    db.SaveChanges();
                    TempData["Success"] = "Patient Updated Successfully!";
                    return Json(new { success = true });
                }
            }
            TempData["Unsuccess"] = "Oops! Something went wrong.";
            return Json(new { success = true });
        }

        // GET: Patients/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                TempData["Warning"] = "HTTP Error 400.0 - Bad Request!";
                return RedirectToAction("Index");
                //return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var patient = (from p in db.Patients
                           where p.Patient_ID == id

                           select new PatientViewModel
                           {
                               Patient_ID = p.Patient_ID
                           }).FirstOrDefault();

            if (patient == null)
            {
                TempData["Warning"] = "HTTP Error 404.0 - Not Found!";
                return RedirectToAction("Index");
                //return HttpNotFound();
            }
            return PartialView(patient);
        }

        // POST: Patients/Delete/5
        [HttpPost]
        //[ValidateAntiForgeryToken]
        public ActionResult Delete(PatientViewModel patient)
        {
            try
            {
                Patient pat = (from p in db.Patients where p.Patient_ID == patient.Patient_ID select p).FirstOrDefault();

                Address add = (from a in db.Addresses where a.Id == pat.AddressID select a).FirstOrDefault();


                db.Patients.Remove(pat);
                db.Addresses.Remove(add);
                db.SaveChanges();

                TempData["Success"] = "Patient Removed Successfully!";
                return Json(new { success = true });
            }
            catch
            {
                TempData["Unsuccess"] = "This Patient can't be removed!";
                return PartialView("Delete", patient);
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