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
    public class SpecializationsController : Controller
    {
        private HMSEntities db = new HMSEntities();

      
        // GET: Specializations
        public ActionResult Index()
        {
            var gender = (from s in db.Specializations
                          select new SpecializationViewModel
                          {
                              Specialization_ID = s.Spcl_ID,
                              Specialization = s.Spcl_Name
                          }).ToList().OrderByDescending(s => s.Specialization_ID);

            return View(gender);
        }

        // GET: Specializations/Create
        public ActionResult Create()
        {
            return PartialView("CreateEdit", new SpecializationViewModel());
        }

        // GET: Specializations/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                TempData["Error"] = "HTTP Error 400.0 - Bad Request!";
                return PartialView("_Error");
            }

            var spcl = (from s in db.Specializations
                        where s.Spcl_ID == id

                        select new SpecializationViewModel
                        {
                            Specialization_ID = s.Spcl_ID,
                            Specialization = s.Spcl_Name
                        }).FirstOrDefault();

            if (spcl == null)
            {
                TempData["Error"] = "HTTP Error 404.0 - Not Found!";
                return PartialView("_Error");
            }
            return PartialView("CreateEdit", spcl);
        }

        [HttpPost]
        public ActionResult CreateEdit(SpecializationViewModel spcl)
        {
            if (ModelState.IsValid)
            {
                if(spcl.Specialization_ID == 0)
                {
                    var newSpcl = new Specialization
                    {
                        Spcl_Name = spcl.Specialization
                    };

                    db.Specializations.Add(newSpcl);
                    db.SaveChanges();
                    TempData["Success"] = "Specialization Created Successfully!";
                    return Json(new { success = true });
                }
                else
                {
                    Specialization model = (from s in db.Specializations where s.Spcl_ID == spcl.Specialization_ID select s).FirstOrDefault();
                    model.Spcl_Name = spcl.Specialization;
                    db.SaveChanges();

                    TempData["Success"] = "Specialization Updated Successfully!";
                    return Json(new { success = true });
                }
            }
            TempData["Unsuccess"] = "Oops! Something went wrong.";
            return Json(new { success = true });
        }

        // GET: Specializations/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                TempData["Error"] = "HTTP Error 400.0 - Bad Request!";
                return PartialView("_Error");
            }
            var spcl = (from s in db.Specializations
                       where s.Spcl_ID == id

                       select new SpecializationViewModel
                       {
                           Specialization_ID = s.Spcl_ID,
                       }).FirstOrDefault();

            if (spcl == null)
            {
                TempData["Error"] = "HTTP Error 404.0 - Not Found!";
                return PartialView("_Error");
            }
            return PartialView(spcl);
        }

        // POST: Specializations/Delete/5
        [HttpPost]
        //[ValidateAntiForgeryToken]
        public ActionResult Delete(SpecializationViewModel spcl)
        {
            try
            {
                Specialization model = (from s in db.Specializations where s.Spcl_ID == spcl.Specialization_ID select s).FirstOrDefault();

                db.Specializations.Remove(model);
                db.SaveChanges();

                TempData["Success"] = "Specialization Removed Successfully!";
                return Json(new { success = true });
            }
            catch
            {
                TempData["Unsuccess"] = "Couldn't Remove Specialization!";
                return PartialView("Delete", spcl);
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