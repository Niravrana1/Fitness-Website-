using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Fitness.Models;

namespace Fitness.Controllers
{
    [Authorize(Roles ="Trainer")]
    public class TrainerController : Controller
    {
        private FitnessEntitiesDbContext _context;

        public TrainerController()
        {
            _context = new FitnessEntitiesDbContext();
        }

        // GET: Trainer
        public ActionResult Index()
        {
            return View();
        }

        //GET: Trainer/ Availability (Edit / Delete Not available time , )
        [HttpGet]
        public ActionResult NotAvailable()
        {
            return View();
        }

        //public JsonResult CheckStartEndTime(int s,int e)
        //{
        //    var result; 
        //    return Json(result, JsonRequestBehavior.AllowGet);
        //}

        [HttpPost]
        public ActionResult NotAvailable(NotavailableDatetime notavailable)
        {
            if(ModelState.IsValid)
            {
                if(notavailable.StartTime.Value.Hours < notavailable.Endtime.Value.Hours)
                {
                    int trainerid = _context.Trainers.Single(m => m.userid == Session["UserId"].ToString()).TrainerId; // Getting trainer id of current loged in trainer
                    NotavailableDatetime na = new NotavailableDatetime();
                    na.TrainerId = trainerid;
                    na.NotavailableDate = notavailable.NotavailableDate;
                    na.StartTime = notavailable.StartTime;
                    na.Endtime = notavailable.Endtime;
                    _context.NotavailableDatetimes.Add(na);
                    _context.SaveChanges();
                    return RedirectToAction("Notavailable", "Trainer");
                }
                else
                {
                    ModelState.AddModelError("End time must be greater then start time");
                }
              
            }
            else
            {
                return View(notavailable);
            }

            
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