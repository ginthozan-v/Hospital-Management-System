using HMS.Models;
using HMS.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Microsoft.AspNet.Identity;

namespace HMS.BusinessLogic
{
    public class DoctorDashBoard
    {
        private HMSEntities db = new HMSEntities();

        public IEnumerable<AppointmentViewModel> GetTodayAppointmentList()
        {

            var userID = HttpContext.Current.User.Identity.GetUserId();

            var TodayApp = (from ap in db.Appointments
                            join ts in db.TimeSlots on ap.Appointment_Date equals ts.TimeID
                            join u in db.AspNetUsers on ap.Doctor_ID equals u.DoctorId

                            from d in db.Doctors
                            join a in db.AspNetUsers on d.Doctor_ID equals a.DoctorId
                            where a.Id == userID

                            where ts.Date == DateTime.Today
                            where ap.Doctor_ID == d.Doctor_ID

                            select new AppointmentViewModel
                            {
                                Doctor_ID = ap.Doctor_ID,
                                Appointment_ID = ap.Appointment_ID,
                                PatientId = ap.Patient.Patient_ID,
                                Patient_Name = ap.Patient.First_Name + " " + ap.Patient.Last_Name,
                                Appointment_Time = ap.Appointment_Time,
                                Appointment_Status = ap.Appointment_Status

                            }).ToList();

            return (TodayApp);
        }


        public IEnumerable<AppointmentViewModel> GetUpcomingAppointmentList()
        {
            var userID = HttpContext.Current.User.Identity.GetUserId();

            var TodayApp = (from t in db.Appointments
                            join ts in db.TimeSlots on t.Appointment_Date equals ts.TimeID
                            join u in db.AspNetUsers on t.Doctor_ID equals u.DoctorId

                            from d in db.Doctors
                            join a in db.AspNetUsers on d.Doctor_ID equals a.DoctorId
                            where a.Id == userID

                            where ts.Date > DateTime.Today
                            where t.Doctor_ID == d.Doctor_ID


                            select new AppointmentViewModel
                            {
                                Patient_Name = t.Patient.First_Name + " " + t.Patient.Last_Name,
                                Appointment_Time = t.Appointment_Time,
                                Appointment_Status = t.Appointment_Status

                            }).ToList();

            return (TodayApp);
        }
    }
}