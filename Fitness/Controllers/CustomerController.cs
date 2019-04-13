using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Fitness.Models;
using Fitness.Models.Viewmodel;

namespace Fitness.Controllers
{
    public class CustomerController : Controller
    {
        private FitnessEntitiesDbContext _Context;

        public CustomerController()
        {
            _Context = new FitnessEntitiesDbContext();
        }


        // GET: Customer/Home
        [HttpGet]
        public ActionResult Home()
        {
            if(Session["User"]!= null)
            {
                ViewBag.username = Session["User"].ToString();
                return View();
            }
            else
            {
                return RedirectToAction("Signin", "Account");
            }
        }
        
        //GET: Customer/ MyAccount (Profle, Billing, Help)
        public ActionResult MyAccount()
        {
            return View();
        }

        //GET: Customer/ Class (Classes(Price and details),Membership for the class (Unlimited classes 12 month contract , Unlimited class month to month,Single class) )
        public ActionResult Classes()
        {
            return View();
        }

        //GET: Customer/ Training (Schedule Training session, Shceduled Training session,Trainers)
        public ActionResult Training()
        {
            return View();
        }


        //GET: Customer/ Profile
        public ActionResult profile()
        {
            if (Session["UserId"] != null)
            {
                string user = Session["UserId"].ToString();
                var userprofile = _Context.Customers.Where(m => m.userid == user).SingleOrDefault();
                return View(userprofile);
            }
            else
            {
                return RedirectToAction("Signin", "Account");
            }
           
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