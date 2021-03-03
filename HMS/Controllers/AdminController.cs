using HMS.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace HMS.Controllers
{
    [Authorize]
    public class AdminController : Controller
    {
        HMSEntities db = new HMSEntities();
        // GET: Admin
        public ActionResult Index()
        {
            var userlist = (from u in db.AspNetUsers

                            select new RegisterViewModel
                            {
                                Username = u.UserName,
                                Email = u.Email,
                                Phone = u.PhoneNumber,
                            }).ToList();

            return View(userlist);

        }

        [HttpGet]
        public ActionResult Register()
        {
            List<SelectListItem> list = new List<SelectListItem>();
            foreach (var role in db.AspNetRoles)
                list.Add(new SelectListItem()
                {
                    Value = role.Name,
                    Text = role.Name
                });
            ViewBag.Roles = list;
            return PartialView();
        }
    }
}