using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using HMS.Models;
using HMS.ViewModels;

namespace HMS.Controllers
{
    [Authorize(Roles = "Admin")]
    public class DateFormatController : Controller
    {
        private HMSEntities db = new HMSEntities();

        // GET: DateFormat
        public ActionResult Index()
        {
            return View();
        }


        // GET: DateFormat/Create
        [HttpGet]
        public ActionResult Create()
        {
            return PartialView("_CreateEdit", new DateFormatViewModel());
        }


        // GET: DateFormat/Edit
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                TempData["Error"] = "HTTP Error 400.0 - Bad Request!";
                return PartialView("_Error");
            }

            var dformat = (from df in db.DateFormats
                         where df.DF_ID == id

                         select new DateFormatViewModel
                         {
                             DF_ID = df.DF_ID,
                             Format = df.Format
                         }).FirstOrDefault();

            if (dformat == null)
            {
                TempData["Error"] = "HTTP Error 404.0 - Not Found!";
                return PartialView("_Error");
            }
            return PartialView("_CreateEdit", dformat);
        }

        //GET: DateFormat/Edit/Post
        [HttpPost]
        public ActionResult CreateEdit(DateFormatViewModel df)
        {
            if (ModelState.IsValid)
            {
                if (df.DF_ID == 0)
                {
                    var newDF = new DateFormat
                    {
                        Format = df.Format
                    };

                    db.DateFormats.Add(newDF);
                    db.SaveChanges();
                    TempData["Success"] = "DateFormat Created Successfully!";
                    return Json(new { success = true });
                }
                else
                {
                    DateFormat model = (from d in db.DateFormats where d.DF_ID == df.DF_ID select d).FirstOrDefault();
                    //if (model == null)
                    //{
                    //    TempData["Warning"] = "HTTP Error 404.0 - Not Found!";
                    //    return RedirectToAction("Index", "Configuration");
                    //}
                    model.Format = df.Format;
                    db.SaveChanges();

                    TempData["Success"] = "DateFormat Updated Successfully!";
                    return Json(new { success = true });
                }
            }
            TempData["Unsuccess"] = "Oops! Something went wrong.";
            return Json(new { success = true });
        }


        //GET: DateFormat/Delete/get
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                TempData["Error"] = "HTTP Error 400.0 - Bad Request!";
                return PartialView("_Error");
            }
            var dformat = (from df in db.DateFormats
                           where df.DF_ID == id

                           select new DateFormatViewModel
                           {
                               DF_ID = df.DF_ID,
                               Format = df.Format
                           }).FirstOrDefault();

            if (dformat == null)
            {
                TempData["Error"] = "HTTP Error 404.0 - Not Found!";
                return PartialView("_Error");
            }
            return PartialView("_Delete", dformat);
        }


        // POST: DateFormat/Delete/5
        [HttpPost]
        public ActionResult Delete(DateFormatViewModel dformat)
        {
            try
            {
                DateFormat model = (from d in db.DateFormats where d.DF_ID == dformat.DF_ID select d).FirstOrDefault();

                db.DateFormats.Remove(model);
                db.SaveChanges();

                TempData["Success"] = "DateFormat Removed Successfully!";
                return Json(new { success = true });
            }
            catch
            {
                TempData["Unsuccess"] = "Couldn't Remove DateFormat!";
                return PartialView("_Delete", dformat);
            }
        }
    }
}