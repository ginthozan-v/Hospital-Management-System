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
    public class BloodgroupsController : Controller
    {
        private HMSEntities db = new HMSEntities();

        // GET: Bloodgroups
        public ActionResult Index()
        {
            var bloodgroup = (from bg in db.Bloodgroups
                                select new BloodgroupViewModel
                                {
                                    ID = bg.BG_ID,
                                    Bloodgroup = bg.BloodGroup1
                                }).ToList();

            return View(bloodgroup);
        }

        // GET: Bloodgroup/Create
        [HttpGet]
        public ActionResult Create()
        {
            return PartialView("CreateEdit", new BloodgroupViewModel());
        }

        //Get
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                TempData["Error"] = "HTTP Error 400.0 - Bad Request!";
                return PartialView("_Error");
            }

            var blood = (from b in db.Bloodgroups
                         where b.BG_ID == id

                         select new BloodgroupViewModel
                         {
                             ID = b.BG_ID,
                             Bloodgroup = b.BloodGroup1
                         }).FirstOrDefault();

            if (blood == null)
            {
                TempData["Error"] = "HTTP Error 404.0 - Not Found!";
                return PartialView("_Error");
            }
            return PartialView("CreateEdit", blood);
        }

        [HttpPost]
        public ActionResult CreateEdit(BloodgroupViewModel bg)
        {
            if (ModelState.IsValid)
            {
                if(bg.ID == 0)
                {
                    var newblood = new Bloodgroup
                    {
                        BloodGroup1 = bg.Bloodgroup
                    };

                    db.Bloodgroups.Add(newblood);
                    db.SaveChanges();
                    TempData["Success"] = "Blood Group Created Successfully!";
                    return Json(new { success = true });
                }
                else
                {
                    Bloodgroup model = (from b in db.Bloodgroups where b.BG_ID == bg.ID select b).FirstOrDefault();
                    model.BloodGroup1 = bg.Bloodgroup;
                    db.SaveChanges();

                    TempData["Success"] = "Bloodgroup Updated Successfully!";
                    return Json(new { success = true });
                }
            }
            TempData["Unsuccess"] = "Oops! Something went wrong.";
            return Json(new { success = true });
        }

        //GET: bloodgroup/delete/get
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                TempData["Error"] = "HTTP Error 400.0 - Bad Request!";
                return PartialView("_Error");
            }
            var blood = (from b in db.Bloodgroups
                       where b.BG_ID== id

                       select new BloodgroupViewModel
                       {
                           ID = b.BG_ID,
                       }).FirstOrDefault();

            if (blood == null)
            {
                TempData["Error"] = "HTTP Error 404.0 - Not Found!";
                return PartialView("_Error");
            }
            return PartialView(blood);
        }

        // POST: Doctors/Delete/5
        [HttpPost]
        public ActionResult Delete(BloodgroupViewModel blood)
        {
            try
            {
                Bloodgroup model = (from b in db.Bloodgroups where b.BG_ID == blood.ID select b).FirstOrDefault();

                db.Bloodgroups.Remove(model);
                db.SaveChanges();

                TempData["Success"] = "Bloodgroup Removed Successfully!";
                return Json(new { success = true });
            }
            catch
            {
                TempData["Unsuccess"] = "Couldn't Remove Bloodgroup!";
                return PartialView("Delete", blood);
            }
        }
    }
}