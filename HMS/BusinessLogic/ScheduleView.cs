using HMS.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using HMS.ViewModels;

namespace HMS.BusinessLogic
{
    public class ScheduleView
    {
       private HMSEntities db = new HMSEntities();

        public IEnumerable<TimeSlotViewModel> GetSingleDayList()
        {
            var SingleDayList = (from t in db.TimeSlots
                                 where t.Date == DateTime.Today
                                 select new TimeSlotViewModel
                                 {
                                     TimeID = t.TimeID,
                                     Doctor = t.Doctor.First_Name,
                                     Specialization = t.Doctor.Specialization.Spcl_Name,
                                     Date = t.Date,
                                     StartTime = t.ShiftPattern.StartTime,
                                     Hours = t.Duration

                                 }).ToList();

            return (SingleDayList);
        }



    }
}