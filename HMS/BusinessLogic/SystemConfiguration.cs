using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using HMS.Models;
using HMS.ViewModels;

namespace HMS.BusinessLogic
{

    public class SystemConfiguration
    {
        private HMSEntities db = new HMSEntities();


        //Config-ShiftPatterns-List
        public List<ShiftViewModel> GetShiftPatternsList()
        {
            var Shift = (from sp in db.ShiftPatterns
                         select new ShiftViewModel
                         {
                             ID = sp.Id,
                             Name = sp.Name,
                             StartTime = sp.StartTime,
                         }).ToList();
            return (Shift);
        }


        //Config-DateFormats-List
        public List<DateFormatViewModel> GetDateFormatList()
        {
            var date = (from d in db.DateFormats
                        select new DateFormatViewModel
                        {
                            DF_ID = d.DF_ID,
                            Format = d.Format
                        }).ToList();
            return (date);
        }


        //Config-Genders-List
        public List<GenderViewModel> GetGenderList()
        {
            var genders = (from d in db.Genders
                           select new GenderViewModel
                           {
                               Gender_ID = d.Gender_ID,
                               Gender = d.Gender1
                           }).ToList();

            return (genders);
        }

        //Config-Bloodgroup-List
        public List<BloodgroupViewModel> GetBloodGroup()
        {
            var blood = (from bg in db.Bloodgroups
                         select new BloodgroupViewModel
                         {
                             ID = bg.BG_ID,
                             Bloodgroup = bg.BloodGroup1
                         }).ToList();

            return (blood);
        }

        //Config-Specialization-List
        public List<SpecializationViewModel> GetSpecializationList()
        {
            var spcl = (from sp in db.Specializations
                        select new SpecializationViewModel
                        {
                            Specialization_ID = sp.Spcl_ID,
                            Specialization = sp.Spcl_Name
                        }).ToList();

            return (spcl);
        }

    }


}

