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

        //GET: Home/ Find Class
        [HttpGet]
        public ActionResult FindClass()
        {
            return View();
        }

        //GET: Home/ Personal Training
        [HttpGet]
        public ActionResult PersonalTraining()
        {
            // DropdownList
            //  Book Personal Training Sessions
            //  booked training sessions
            //  view Trainers
            return View();
        }

        //GET: Home/ Member Area
        [HttpGet]
        public ActionResult MemberArea()
        {
            return View();
        }
        
        //GET: Home/SocialMedia
        [HttpGet]
        public ActionResult SocialMedia()
        {
            return View();
        }

        //GET: Home/About
        public ActionResult About()
        {
            return View();
        }

        //GET: Home/ ContactUs
        public ActionResult ContactUs()
        {
            return View();
        }




        public ActionResult Trainer()
        {
            return View();
        }
       
    }
}