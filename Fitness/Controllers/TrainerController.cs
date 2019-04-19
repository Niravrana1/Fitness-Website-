using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Fitness.Models;
using Fitness.Models.Viewmodel;

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
            if(Session["UserId"]!=null)
            {
                return View();
            }
            else
            {
                return RedirectToAction("SignIn", "Account");
            }
        }

        //public JsonResult CheckStartEndTime(int s,int e)
        //{
        //    var result; 
        //    return Json(result, JsonRequestBehavior.AllowGet);
        //}

        [HttpPost]
        public JsonResult Checkdatetime(string NaDate, string Stime)
        {
            DateTime d = Convert.ToDateTime(NaDate);
            TimeSpan st = TimeSpan.Parse(Stime);
            var countdate = _context.NotavailableDatetimes.Where(m => m.NotavailableDate == d).Select(m => m.StartTime.Value.Hours == st.Hours);
            if(countdate.Count() > 0)
            {
                return Json(new { message = "Start time is already selected for this date", status = "error" }, JsonRequestBehavior.AllowGet);
            }
            else
            {
                return Json(new { message = "Sucess", status = "Success" }, JsonRequestBehavior.AllowGet);
            }
            

        }

        [HttpPost]
        public ActionResult NotAvailable(NotavailableDatetime notavailable)
        {
            if(ModelState.IsValid)
            {
                if(notavailable.StartTime.Value.Hours < notavailable.Endtime.Value.Hours && notavailable.StartTime!=notavailable.Endtime)
                {
                    string trainer = Session["UserId"].ToString();
                    int trainerid = _context.Trainers.Single(m => m.userid == trainer).TrainerId; // Getting trainer id of current loged in trainer
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
                    ModelState.AddModelError(string.Empty,"End time must be greater then start time");
                    return View(notavailable);
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

        [HttpGet]
        public ActionResult Addclass()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Addclass(Addclassviewmodel acv)
        {
            if(ModelState.IsValid)
            {
                string trainer = Session["UserId"].ToString();         
                Class c = new Class();
                c.Trainerid = _context.Trainers.Single(m => m.userid == trainer).TrainerId;
                c.ClassName = acv.ClassName;
                c.ClassDiscription = acv.ClassDiscription;
                c.ClassPrice = acv.ClassPrice;
                _context.Classes.Add(c);
                _context.SaveChanges();

                return RedirectToAction("Index", "Trainer");
            }
            else
            {
                ModelState.AddModelError(string.Empty, "Please enter valid data");
                return View(acv);
            }
            
        }

        //GET: Trainer/ Class (Edit / Delete Clas,Edit class Price )
        [HttpGet]
        public ActionResult Viewclasses()
        {
            string trainer = Session["UserId"].ToString();
            int trainerid = _context.Trainers.Single(m => m.userid == trainer).TrainerId;
            var Trainerclasses = _context.Classes.Where(m => m.Trainerid == trainerid);  
            return View(Trainerclasses);
        }

        [HttpGet]
        public ActionResult EditClass(int Id)
        {
            TempData["ClassId"] = Id;
           var classvalues = _context.Classes.SingleOrDefault(m => m.ClassId == Id);
            var viewclass = new Addclassviewmodel
            {
                ClassName = classvalues.ClassName,
                ClassDiscription = classvalues.ClassDiscription,
                ClassPrice = classvalues.ClassPrice
            };
            return View(viewclass);
        }

        [HttpPost]
        public ActionResult EditClass(Addclassviewmodel acv)
        {
            if (ModelState.IsValid)
            {
                int classid = Convert.ToInt32(TempData["ClassId"]);
                var updateclass = _context.Classes.Single(m => m.ClassId == classid);
                updateclass.ClassName = acv.ClassName;
                updateclass.ClassDiscription = acv.ClassDiscription;
                updateclass.ClassPrice = acv.ClassPrice;
                _context.SaveChanges();
                return RedirectToAction("Index", "Trainer");
            }
            else
            {
                ModelState.AddModelError(string.Empty, "Please enter valid data");
                return View(acv);
            }
        }

        [HttpDelete]
        public ActionResult DeleteClass(int id)
        {
            var classid = _context.Classes.Find(id);
            _context.Classes.Remove(classid);
            _context.SaveChanges();
            return RedirectToAction("ViewClasses", "Trainer");
        }

        public ActionResult AddClassTime()
        {
            return View();
        }

        public ActionResult ViewClassTime()
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