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
    public class DeseasesController : Controller
    {
        private HMSEntities db = new HMSEntities();

        // GET: Deseases
        public ActionResult Index()
        {
            var deseaselist = (from d in db.Deseases

                              select new DeseaseViewModel
                              {
                                  Desease_ID = d.Desease_ID,
                                  Desease = d.Desease1
                              }).ToList().OrderByDescending(d => d.Desease_ID);

            return View(deseaselist);

        }

        // GET: Deseases/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                TempData["Error"] = "HTTP Error 400.0 - Bad Request!";
                return PartialView("_Error");
            }
            Desease desease = db.Deseases.Find(id);
            if (desease == null)
            {
                TempData["Error"] = "HTTP Error 404.0 - Not Found!";
                return PartialView("_Error");
            }
            return View(desease);
        }

        // GET: Deseases/Create
        public ActionResult Create()
        {
            return PartialView("CreateEdit", new DeseaseViewModel());
        }

        // GET: Deseases/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                TempData["Error"] = "HTTP Error 400.0 - Bad Request!";
                return PartialView("_Error");
            }

            var desease = (from d in db.Deseases
                           where d.Desease_ID == id

                           select new DeseaseViewModel
                           {
                               Desease_ID = d.Desease_ID,
                               Desease = d.Desease1
                           }).FirstOrDefault();

            if (desease == null)
            {
                TempData["Error"] = "HTTP Error 404.0 - Not Found!";
                return PartialView("_Error");
            }
            return PartialView("CreateEdit", desease);
        }

        [HttpPost]
        public ActionResult CreateEdit(DeseaseViewModel dcs)
        {
            if (ModelState.IsValid)
            {
                if(dcs.Desease_ID == 0)
                {
                    var newDcs = new Desease
                    {
                        Desease1 = dcs.Desease,
                        Date = DateTime.Now
                    };

                    db.Deseases.Add(newDcs);
                    db.SaveChanges();
                    TempData["Success"] = "Desease Created Successfully!";
                    return Json(new { success = true });
                }
                else
                {
                    Desease model = (from d in db.Deseases where d.Desease_ID == dcs.Desease_ID select d).FirstOrDefault();
                    model.Desease1 = dcs.Desease;

                    db.SaveChanges();
                    TempData["Success"] = "Desease Updated Successfully!";
                    return Json(new { success = true });
                }
            }
            TempData["Unsuccess"] = "Oops! Something went wrong.";
            return Json(new { success = true });
        }

        // GET: Deseases/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                TempData["Error"] = "HTTP Error 400.0 - Bad Request!";
                return PartialView("_Error");
            }
            var des = (from d in db.Deseases
                       where d.Desease_ID == id

                       select new DeseaseViewModel
                       {
                           Desease_ID = d.Desease_ID,
                       }).FirstOrDefault();

            if (des == null)
            {
                TempData["Error"] = "HTTP Error 404.0 - Not Found!";
                return PartialView("_Error");
            }
            return PartialView(des);
        }

        // POST: Deseases/Delete/5
        [HttpPost]
        //[ValidateAntiForgeryToken]
        public ActionResult Delete(DeseaseViewModel des)
        {
            try
            {
                Desease model = (from d in db.Deseases where d.Desease_ID == des.Desease_ID select d).FirstOrDefault();

                db.Deseases.Remove(model);
                db.SaveChanges();

                TempData["Success"] = "Desease Removed Successfully!";
                return Json(new { success = true });
            }
            catch
            {
                TempData["Unsuccess"] = "Couldn't Remove Desease!";
                return PartialView("Delete", des);
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