using HMS.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using HMS.ViewModels;
using HMS.BusinessLogic;

namespace HMS.Controllers
{
    [Authorize(Roles = "Pharmacists")]
    public class PharmacyDashBoardController : Controller
    {
        private HMSEntities db = new HMSEntities();
        PharmacyDashBoard Pharmacy = new PharmacyDashBoard();

        // GET: PharmacyDashBoard
        public ActionResult Index()
        {
            var Prescriptions = (from pr in db.Prescriptions
                                 where pr.Paid == null

                                 select new PrescriptionViewModel
                                 {
                                     PrescriptionId = pr.PrescriptionId,
                                     Patient = pr.Patient.First_Name

                                 }).ToList().OrderByDescending(m => m.PrescriptionId);

            ViewBag.PrescriptionList = Prescriptions;
            return View();
        }

        public ActionResult PrescriptionDetails(int id)
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

            return PartialView("_prescriptionDetails", prescription);
        }

        //GET: Checkout Form
        [HttpGet]
        public ActionResult Checkout(int id)
        {
            if (id == null)
            {
                TempData["Error"] = "HTTP Error 400.0 - Bad Request!";
                return PartialView("_Error");
            }

            var patient = (from pr in db.Prescriptions
                           join p in db.Patients on pr.PatientId equals p.Patient_ID
                           where pr.PrescriptionId == id

                           select new PatientViewModel
                           {
                               PrescriptionId = id,
                               First_Name = p.First_Name
                           }).FirstOrDefault();

            var summary = (from pr in db.Prescriptions
                           where pr.PrescriptionId == id

                           select new CreatePrescriptionViewModel
                           {
                               Drug_Name = pr.Drug_Name,
                               Quantity = pr.Quantity
                           }).FirstOrDefault();

            ViewBag.Patient = patient;
            ViewBag.Summary = summary;
            return View();
        }

        //POST: Checkout
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Checkout(CheckoutViewModel Checkout)
        {
            if (ModelState.IsValid)
            {
                var newCheckout = new Checkout
                {
                    prescription_id = Checkout.PrescriptionId,
                    patient_name = Checkout.PatientName,
                    name_on_card = Checkout.NameOnCard,
                    card_number = Checkout.CardNumber,
                    ccv = Checkout.CCV,
                    exp = Checkout.EXP,
                    amount = Checkout.Amount
                };

                Prescription paid = (from p in db.Prescriptions where p.PrescriptionId == Checkout.PrescriptionId select p).FirstOrDefault();
                paid.Paid = Checkout.PaymentOption;

                db.Checkouts.Add(newCheckout);
                db.SaveChanges();

                TempData["Success"] = " Checkout made Successfully!";
                return RedirectToAction("Index");
            }
            return View();
        }
    }
}