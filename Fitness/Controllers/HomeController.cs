using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Fitness.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
        public ActionResult Practice()
        {
            return View();
        }
        public ActionResult signin()
        {
            return View();
        }
        public ActionResult SignUp()
        {
            return View();
        }
        public ActionResult Trainers()
        {
            return View();
        }
    }
}