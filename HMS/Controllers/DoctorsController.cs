using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.Entity;
using System.Data.SqlClient;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using HMS.Models;
using HMS.ViewModels;
using Microsoft.Owin.Security;
using HMS.BusinessLogic;
using System.IO;

namespace HMS.Controllers
{
    [Authorize(Roles = "Admin")]
    public class DoctorsController : Controller
    {
        HMSEntities db = new HMSEntities();
        AddressClass add = new AddressClass();

        private void DropdownList()
        {
            ViewBag.BloodgroupList = new SelectList(db.Bloodgroups, "BG_ID", "BloodGroup1");
            ViewBag.SpclList = new SelectList(db.Specializations, "Spcl_ID", "Spcl_Name");
            ViewBag.GenderList = new SelectList(db.Genders, "Gender_ID", "Gender1");
        }

        // GET: Doctors
        public ActionResult Index()
        {
            var doctorlist = (from d in db.Doctors
                              select new DoctorViewModel
                              {
                                  Doctor_ID = d.Doctor_ID,
                                  First_Name = d.First_Name,
                                  Last_Name = d.Last_Name,
                                  Specialization = d.Specialization.Spcl_Name,
                                  Doctor_Age = d.Doctor_Age,
                                  Doctor_Gender = d.Gender.Gender1,
                                  Doctor_Phone = d.Doctor_Phone,
                                  Doctor_Status = d.Doctor_Status,
                                  Doctor_Email = d.Doctor_Email,
                                  BloodGroup = d.Bloodgroup.BloodGroup1,
                                  ImagePath = d.ImagePath,

                              }).ToList().OrderByDescending(d => d.Doctor_ID);

            return View(doctorlist);
        }

        // GET: Doctors/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                TempData["Warning"] = "HTTP Error 400.0 - Bad Request!";
                return RedirectToAction("Index");
            }
            var doc = (from d in db.Doctors
                       join a in db.Addresses on d.AddressID equals a.Id
                       where d.Doctor_ID == id

                       select new DetailDoctorViewModel
                       {
                           Doctor_ID = d.Doctor_ID,
                           First_Name = d.First_Name,
                           Last_Name = d.Last_Name,
                           Specialization = d.Specialization.Spcl_Name,
                           Doctor_Age = d.Doctor_Age,
                           Gender = d.Gender.Gender1,
                           Doctor_Phone = d.Doctor_Phone,
                           Doctor_Status = d.Doctor_Status,
                           Doctor_Email = d.Doctor_Email,
                           Bloodgroup = d.Bloodgroup.BloodGroup1,
                           Registered_Date = d.Registered_Date,
                           ImagePath = d.ImagePath,

                           Address = new AddressViewModel
                           {
                               Street = a.Street,
                               Province = a.Province,
                               City = a.City,
                               Post_Code = a.Post_Code,
                               Country = a.Country
                           }

                       }).FirstOrDefault();
            if (doc == null)
            {
                TempData["Warning"] = "HTTP Error 404.0 - Not Found!";
                return RedirectToAction("Index");
            }
            return View(doc);
        }

        // GET: Doctors/Create
        public ActionResult Create()
        {
            DropdownList();
            return PartialView("CreateEdit", new CreateDoctorViewModel());
        }

        //GET: Doctors/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                TempData["Warning"] = "HTTP Error 400.0 - Bad Request!";
                return RedirectToAction("Index");
            }

            var doc = (from d in db.Doctors
                       join a in db.Addresses on d.AddressID equals a.Id
                       where d.Doctor_ID == id

                       select new CreateDoctorViewModel
                       {
                           Doctor_ID = d.Doctor_ID,
                           First_Name = d.First_Name,
                           Last_Name = d.Last_Name,
                           Specialization_ID = d.Spcl_ID,
                           Doctor_Age = d.Doctor_Age,
                           Gender_ID = d.Gender_ID,
                           Doctor_Phone = d.Doctor_Phone,
                           Doctor_Status = d.Doctor_Status,
                           Doctor_Email = d.Doctor_Email,
                           Bloodgroup_ID = d.BG_ID,
                           ImagePath = d.ImagePath,

                           Address = new AddressViewModel
                           {
                               Id = a.Id,
                               Street = a.Street,
                               Province = a.Province,
                               City = a.City,
                               Post_Code = a.Post_Code,
                               Country = a.Country
                           }

                       }).FirstOrDefault();

            if (doc == null)
            {
                TempData["Warning"] = "HTTP Error 404.0 - Not Found!";
                return View();
            }

            DropdownList();
            return PartialView("CreateEdit", doc);
        }

        public JsonResult CreateEdit(CreateDoctorViewModel d)
        {
            var result = false;
            string filename;

            IEnumerable<ModelError> allErrors = ModelState.Values.SelectMany(v => v.Errors);
            //string id = "0";
            if (ModelState.IsValid)
            {
                d.Address.Id = add.SaveAddress(d.Address);

                if (d.Doctor_ID == 0)
                {
                    if (d.ProfilePicture != null)
                    {
                        filename = Path.GetFileNameWithoutExtension(d.ProfilePicture.FileName);
                        string extension = Path.GetExtension(d.ProfilePicture.FileName);

                        filename = filename + DateTime.Now.ToString("yymmssff") + extension;
                        string path = Path.Combine(Server.MapPath("~/Content/Images/ProfilePictures"), filename);
                        d.ProfilePicture.SaveAs(path);
                    }
                    else
                    {
                        filename = "default.jpg";
                    }

                    var newDoc = new Doctor
                    {
                        First_Name = d.First_Name,
                        Last_Name = d.Last_Name,
                        Spcl_ID = d.Specialization_ID,
                        BG_ID = d.Bloodgroup_ID,
                        Gender_ID = d.Gender_ID,
                        AddressID = d.Address.Id,
                        Doctor_Phone = d.Doctor_Phone,
                        Doctor_Age = d.Doctor_Age,
                        Doctor_Email = d.Doctor_Email,
                        Doctor_Status = 1,
                        Registered_Date = DateTime.Now,
                        ImagePath = filename,
                    };

                    db.Doctors.Add(newDoc);
                    db.SaveChanges();

                    TempData["Success"] = "Doctor Created Successfully!";
                    return Json(new { success = true });
                }
                else
                {

                    Doctor model = (from doc in db.Doctors where doc.Doctor_ID == d.Doctor_ID select doc).FirstOrDefault();

                    model.First_Name = d.First_Name;
                    model.Last_Name = d.Last_Name;
                    model.Doctor_Phone = d.Doctor_Phone;
                    model.Spcl_ID = d.Specialization_ID;
                    model.Gender_ID = d.Gender_ID;
                    model.BG_ID = d.Bloodgroup_ID;
                    model.Doctor_Email = d.Doctor_Email;
                    model.Doctor_Age = d.Doctor_Age;
                    model.AddressID = d.Address.Id;

                    if (d.ProfilePicture != null)
                    {
                        filename = model.ImagePath;
                        string path = Path.Combine(Server.MapPath("~/Content/Images/ProfilePictures"), filename);
                        d.ProfilePicture.SaveAs(path);

                        model.ImagePath = filename;
                    }

                    db.SaveChanges();

                    TempData["Success"] = "Doctor Updated Successfully!";
                    return Json(new { success = true, JsonRequestBehavior.AllowGet });


                }
            }
            TempData["Unsuccess"] = "Error!";
            return Json(new { success = false, JsonRequestBehavior.AllowGet });

        }

        //GET: Doctors/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                TempData["Warning"] = "HTTP Error 400.0 - Bad Request!";
                return PartialView();
            }

            var doc = (from d in db.Doctors
                       where d.Doctor_ID == id

                       select new CreateDoctorViewModel
                       {
                           Doctor_ID = d.Doctor_ID
                       }).FirstOrDefault();

            if (doc == null)
            {
                TempData["Warning"] = "HTTP Error 404.0 - Not Found!";
                return PartialView(doc);
            }
            return PartialView(doc);
        }

        // POST: Doctors/Delete/5
        [HttpPost]
        public ActionResult Delete(DoctorViewModel doc)
        {
            try
            {
                Doctor doctor = (from d in db.Doctors
                                 where d.Doctor_ID == doc.Doctor_ID
                                 select d).FirstOrDefault();

                Address add = (from a in db.Addresses
                               where a.Id == doctor.AddressID
                               select a).FirstOrDefault();

                var img = doctor.ImagePath;
                string fileName = ("~/Content/Images/ProfilePictures/" + img);
                db.Doctors.Remove(doctor);
                db.Addresses.Remove(add);


                db.SaveChanges();

                if (img != "default.jpg")
                {
                    if (System.IO.File.Exists(HttpContext.Server.MapPath("~/Content/Images/ProfilePictures/" + img)))
                    {
                        System.IO.File.Delete(HttpContext.Server.MapPath("~/Content/Images/ProfilePictures/" + img));
                    }
                }

                TempData["Success"] = "Doctor Removed Successfully!";
                return Json(new { success = true });
            }
            catch
            {
                TempData["Unsuccess"] = "Couldn't Remove Doctor!";
                return PartialView(doc);
            }
        }
    }
}
