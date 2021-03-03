using HMS.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using HMS.ViewModels;
using HMS.BusinessLogic;

namespace HMS.BusinessLogic
{
    internal class PharmacyDashBoard
    {
        private HMSEntities db = new HMSEntities();

        public IEnumerable<PrescriptionViewModel> GetTodayPrescriptionList()
        {
            var Prescriptions = (from pr in db.Prescriptions
                                 where pr.Paid == null

                                 select new PrescriptionViewModel
                                 {
                                     PrescriptionId = pr.PrescriptionId,
                                     Patient = pr.Patient.First_Name

                                 }).ToList().OrderByDescending(m => m.PrescriptionId);

            return (Prescriptions);
        }
    }
}