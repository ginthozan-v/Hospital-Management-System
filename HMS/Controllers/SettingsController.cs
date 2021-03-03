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
    public class SettingsController : Controller
    {
        private HMSEntities db = new HMSEntities();

        // GET: Settings
        public ActionResult Index()
        {
            var setting = (from s in db.Settings
                           where s.Name == "DateFormat"

                           select new SettingViewModel
                           {
                               DF_ID = s.DF_ID
                           }).FirstOrDefault();

            if (setting == null)
            {
                TempData["Warning"] = "HTTP Error 404.0 - Not Found!";
                return RedirectToAction("Index");
            }
            ViewBag.DateFormat = new SelectList(db.DateFormats, "DF_ID", "Format");
            return View(setting);
        }

        // POST: Settings/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Update(SettingViewModel setting)
        {
            if (ModelState.IsValid)
            {
                Setting model = (from s in db.Settings where s.Name == "DateFormat" select s).FirstOrDefault();
                model.DF_ID = setting.DF_ID;

                db.SaveChanges();
                MvcApplication.DATEFORMAT = model.DateFormat.Format;
                TempData["Success"] = "Setting changed Successfully!";
                return RedirectToAction("Index");
            }
            ViewBag.DateFormat = new SelectList(db.DateFormats, "DF_ID", "Format", setting.DateFormat); ;
            return View(setting);
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
