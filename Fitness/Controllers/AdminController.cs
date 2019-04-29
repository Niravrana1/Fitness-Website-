using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Fitness.Models;

namespace Fitness.Controllers
{
    public class AdminController : Controller
    {
        private FitnessEntitiesDbContext _Context;

        public AdminController()
        {
            _Context = new FitnessEntitiesDbContext();
        }
        // GET: Admin
        public ActionResult Index()
        {
            return View();
        }

        //GET: Admin/ Addrole
        [HttpGet]
        public ActionResult AddRole()
        {
            return View();
        }

        [HttpPost]
        public ActionResult AddRole(AspNetRole Ar)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    var context = new Models.ApplicationDbContext();

                    context.Roles.Add(new Microsoft.AspNet.Identity.EntityFramework.IdentityRole()
                    {
                        Name = Ar.Name
                    });
                    context.SaveChanges();
                    ViewBag.Message = "Role created successfully !";
                    return RedirectToAction("Index", "Home");
                }
                catch
                {
                    return View();
                }
            }

            return View(Ar);

        }


        public ActionResult Createaccount()
        {
            return View();
        }
    }
}