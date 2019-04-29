using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Fitness.Models;

namespace Fitness.Controllers
{
    public class HomeController : Controller
    {

        private FitnessEntitiesDbContext _Context;

        public HomeController()
        {
            _Context = new FitnessEntitiesDbContext();
        }

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




        public ActionResult Practice()
        {
            return View();
        }

        public ActionResult ViewTrainers()
        {
            var Listoftrainers = _Context.Trainers.ToList();
            Listoftrainers.Count();
            return View(Listoftrainers);
        }

    }
}