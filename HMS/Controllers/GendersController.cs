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
    public class GendersController : Controller
    {
        private HMSEntities db = new HMSEntities();

        // GET: Genders
        public ActionResult Index()
        {
            var gender = (from g in db.Genders
                              select new GenderViewModel
                              {
                                  Gender_ID = g.Gender_ID,
                                  Gender = g.Gender1
                              }).ToList();

            return View(gender);
        }

        // GET: Genders/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                TempData["Error"] = "HTTP Error 400.0 - Bad Request!";
                return PartialView("_Error");
            }
            Gender gender = db.Genders.Find(id);
            if (gender == null)
            {
                TempData["Error"] = "HTTP Error 404.0 - Not Found!";
                return PartialView("_Error");
            }
            return View(gender);
        }

        // GET: Genders/Create
        public ActionResult Create()
        {
            return PartialView("CreateEdit", new GenderViewModel());
        }

        // GET: Genders/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                TempData["Error"] = "HTTP Error 400.0 - Bad Request!";
                return PartialView("_Error");
            }

            var gender = (from g in db.Genders
                          where g.Gender_ID == id

                          select new GenderViewModel
                          {
                              Gender_ID = g.Gender_ID,
                              Gender = g.Gender1
                          }).FirstOrDefault();

            if (gender == null)
            {
                TempData["Error"] = "HTTP Error 404.0 - Not Found!";
                return PartialView("_Error");
            }
            return PartialView("CreateEdit", gender);
        }

        [HttpPost]
        public ActionResult CreateEdit(GenderViewModel gen)
        {
            if (ModelState.IsValid)
            {
                if(gen.Gender_ID == 0)
                {
                    var newGender = new Gender
                    {
                        Gender1 = gen.Gender
                    };

                    db.Genders.Add(newGender);
                    db.SaveChanges();
                    TempData["Success"] = "Gender Created Successfully!";
                    return Json(new { success = true });
                }
                else
                {
                    Gender model = (from g in db.Genders where g.Gender_ID == gen.Gender_ID select g).FirstOrDefault();
                    model.Gender1 = gen.Gender;
                    db.SaveChanges();

                    TempData["Success"] = "Gender Updated Successfully!";
                    return Json(new { success = true });
                }
            }
            TempData["Unsuccess"] = "Oops! Something went wrong.";
            return Json(new { success = true });
        }

        // GET: Genders/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                TempData["Error"] = "HTTP Error 400.0 - Bad Request!";
                return PartialView("_Error");
            }
            var gen = (from g in db.Genders
                         where g.Gender_ID == id

                         select new GenderViewModel
                         {
                             Gender_ID = g.Gender_ID,
                         }).FirstOrDefault();

            if (gen == null)
            {
                TempData["Error"] = "HTTP Error 404.0 - Not Found!";
                return PartialView("_Error");
            }
            return PartialView("Delete", gen);
        }

        // POST: Genders/Delete/5
        [HttpPost]
        public ActionResult Delete(GenderViewModel gen)
        {
            try
            {
                Gender model = (from g in db.Genders where g.Gender_ID == gen.Gender_ID select g).FirstOrDefault();

                db.Genders.Remove(model);
                db.SaveChanges();

                TempData["Success"] = "Gender Removed Successfully!";
                return Json(new { success = true });
            }
            catch
            {
                TempData["Unsuccess"] = "Couldn't Remove Gender!";
                return PartialView("Delete", gen);
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