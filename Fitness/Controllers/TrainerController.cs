using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Fitness.Controllers
{
    public class TrainerController : Controller
    {
        // GET: Trainer
        public ActionResult Index()
        {
            return View();
        }

        //GET: Trainer/ Availability (Edit / Delete Not available time , )
        public ActionResult Availability()
        {
            return View();
        }

        //GET: Trainer/ Class (Edit / Delete Class, Edit / Delete Class Time ,Edit class Price )
        public ActionResult Class()
        {
            return View();
        }

        //GET: Trainer/ Training (Edit / Cancel Scheduled training session, Edit session price)
        public ActionResult Training()
        {
            return View();
        }

        //GET: Trainer/ Billing
        public ActionResult Billing()
        {
            return View();
        }

    }
}