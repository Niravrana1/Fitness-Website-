using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Fitness.Controllers
{
    public class CustomerController : Controller
    {
        // GET: Customer/Home
        [HttpGet]
        public ActionResult Home()
        {
            return View();
        }
        
        //GET: Customer/ MyAccount
        public ActionResult MyAccount()
        {
            return View();
        }

        //GET: Customer/ Class
        public ActionResult Classes()
        {
            return View();
        }


        //GET: Customer/ Profile
        public ActionResult profile()
        {
            return View();
        }

        //GET: Customer/ Billing
        public ActionResult Billing()
        {
            return View();
        }

        //GET: Customer/ BookTraining
        public ActionResult ScheduleTrainingSession()
        {
            return View();
        }

        //GET: Customer/ BookedTraining  : View scheduled trainer (Manage/delete)
        public ActionResult ScheduledTrainingSession()
        {
            return View();
        }



        //GET: Customer/ Trainers
        public ActionResult Trainers()
        {
            return View();
        }


        //GET: Customer/ Help
        public ActionResult Help()
        {
            return View();
        }






    }
}