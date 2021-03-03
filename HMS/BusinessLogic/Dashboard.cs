using HMS.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using HMS.ViewModels;

namespace HMS.BusinessLogic
{
    public class Dashboard
    {
        private HMSEntities db = new HMSEntities();

        public IEnumerable<AppointmentViewModel> GetTodayAppointmentList()
        {
            var SingleDayList = (from t in db.Appointments
                                 join ts in db.TimeSlots on t.Appointment_Date equals ts.TimeID
                                 where ts.Date == DateTime.Today
                                 
                                 select new AppointmentViewModel
                                 {
                                     Appointment_ID = t.Appointment_ID,
                                     Patient_Name = t.Patient.First_Name,
                                     Doctor_Name = t.Doctor.First_Name,
                                     Contact = t.Patient.Patient_Phone,
                                     Appointment_Time = t.Appointment_Time,
                                     Appointment_Status = t.Appointment_Status,
                                     ProfilePic = t.Doctor.ImagePath,
                                     
                                 }).ToList();

            return (SingleDayList);
        }

        public IEnumerable<TimeSlotViewModel> GetTodayAttendanceDoctors()
        {
            var TodayDoctorList = (from ts in db.TimeSlots
                                   where ts.Date == DateTime.Today

                                   select new TimeSlotViewModel
                                   {
                                       Doctor = ts.Doctor.Last_Name + " " + ts.Doctor.First_Name,
                                       ProfilePic = ts.Doctor.ImagePath,
                                       StartTime = ts.ShiftPattern.StartTime,
                                       Status = ts.Doctor.Doctor_Status,
                                       Specialization = ts.Doctor.Specialization.Spcl_Name
                                   }).ToList();

                return (TodayDoctorList);
        }
    }
}