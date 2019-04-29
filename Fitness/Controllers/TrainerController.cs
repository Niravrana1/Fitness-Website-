using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Fitness.Models;
using Fitness.Models.Viewmodel;

namespace Fitness.Controllers
{
    [Authorize(Roles = "Trainer")]
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
            if (Session["UserId"] != null)
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
            if (NaDate != "" && Stime != "")
            {
                DateTime d = Convert.ToDateTime(NaDate);
                TimeSpan st = TimeSpan.Parse(Stime);
                var countdate = _context.NotavailableDatetimes.Where(m => m.NotavailableDate == d).Select(m => m.StartTime.Value.Hours == st.Hours);
                if (countdate.Count() > 0)
                {
                    return Json(new { message = "Start time is already selected for this date", status = "error" }, JsonRequestBehavior.AllowGet);
                }
                else
                {
                    return Json(new { message = "Sucess", status = "Success" }, JsonRequestBehavior.AllowGet);
                }
            }
            else
            {
                return Json(new { message = "Insert valid values", status = "Error" }, JsonRequestBehavior.AllowGet);
            }

        }

        [HttpPost]
        public ActionResult NotAvailable(NotavailableDatetime notavailable)
        {
            if (ModelState.IsValid)
            {
                if (notavailable.StartTime.Value.Hours < notavailable.Endtime.Value.Hours && notavailable.StartTime != notavailable.Endtime)
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
                    ModelState.AddModelError(string.Empty, "End time must be greater then start time");
                    return View(notavailable);
                }

            }
            else
            {
                return View(notavailable);
            }


        }

        [HttpGet]
        public ActionResult Trainerrate()
        {
            if (Session["UserId"] != null)
            {


                return View();
            }
            else
            {
                return RedirectToAction("Signin", "Account");
            }

        }

        [HttpPost]
        public ActionResult Trainerrate(Trainerrate tr)
        {
            if (ModelState.IsValid)
            {
                string trainerid = Session["UserId"].ToString();
                TempData["trainerid"] = trainerid;
                int tid = _context.Trainers.Single(m => m.userid == trainerid).TrainerId;
                TempData["tid"] = tid;
                Trainerrate trainerrate = new Trainerrate();
                trainerrate.trainerid = tid;
                trainerrate.Duration = Convert.ToInt32(tr.Duration);
                trainerrate.Price = Convert.ToDecimal(tr.Price);
                _context.Trainerrates.Add(trainerrate);
                _context.SaveChanges();
                return RedirectToAction("Viewprices", "Trainer");
            }
            return View();
        }

        public JsonResult Checktrainerrate(string time)
        {
            if (time != null && time != "")
            {
                int duration = Convert.ToInt32(time);
                int tid = Convert.ToInt32(TempData["tid"]);
                int countduration = _context.Trainerrates.Where(m => m.trainerid == tid).Select(m => m.Duration == duration).Count();

                if (countduration == 0)
                {
                    return Json(new { message = "Success", status = "Success" }, JsonRequestBehavior.AllowGet);
                }
                else
                {
                    return Json(new { message = "Select different time", status = "Error" }, JsonRequestBehavior.AllowGet);
                }

            }
            else
            {
                return Json(new { message = "Please enter valid data", status = "Error" }, JsonRequestBehavior.AllowGet);
            }
        }

        public ActionResult Viewprices()
        {
            if (Session["UserId"] != null)
            {
                string trainer = Session["UserId"].ToString();
                int trainerid = _context.Trainers.Single(m => m.userid == trainer).TrainerId;

                IEnumerable<Trainerrate> trainerrate = _context.Trainerrates.Where(m => m.trainerid == trainerid).ToList();
                return View(trainerrate);
            }
            else
            {
                return RedirectToAction("SignIn", "Account");
            }

        }

        [HttpGet]
        public ActionResult Editrate(int id)
        {

            var trainerrate = _context.Trainerrates.SingleOrDefault(m => m.Id == id);
            var trainer = new Trainerrate()
            {
                Id = trainerrate.Id,
                trainerid = trainerrate.trainerid,
                Duration = trainerrate.Duration,
                Price = trainerrate.Price
            };
            return View(trainer);
        }

        [HttpPost]
        public ActionResult Editrate(Trainerrate tr)
        {
            if (ModelState.IsValid)
            {
                var trainerrates = _context.Trainerrates.Single(m => m.Id == tr.Id);
                trainerrates.Duration = Convert.ToInt32(tr.Duration);
                trainerrates.Price = Convert.ToDecimal(tr.Price);
                _context.SaveChanges();
                return RedirectToAction("Viewprices", "Trainer");
            }


            return View(tr);
        }

        public ActionResult Deleterate(int id)
        {
            return View();
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
            if (ModelState.IsValid)
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
            TempData["Trainerid"] = trainerid;
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

        [HttpGet]
        public ActionResult AddClassTime()
        {
            string trainer = Session["UserId"].ToString();
            int trainerid = _context.Trainers.Single(m => m.userid == trainer).TrainerId;
            var classlist = _context.Classes.Where(m => m.Trainerid == trainerid).ToList();
            //ViewBag.ClassList = classlist.Select(m => new SelectListItem {Text = m.ClassName, Value=m.ClassId.ToString() });
            ViewBag.ClassList = new SelectList(_context.Classes, "ClassId", "ClassName");
            return View();
        }

        public JsonResult Checkdateforclass(string cvalue, string ctime)
        {
            if (cvalue != "" && ctime != "")
            {
                int classvalue = Convert.ToInt32(cvalue);
                TimeSpan classtime = TimeSpan.Parse(ctime);
                int checkdate = _context.ClassTimes.Where(m => m.Classid == classvalue).Select(m => m.ClassTime1 == classtime).Count();

                if (checkdate == 0)
                {

                    return Json(new { message = "Date and time is available for class session", status = "Success" }, JsonRequestBehavior.AllowGet);
                }
                else
                {
                    return Json(new { message = "Choose another time", status = "Error" }, JsonRequestBehavior.AllowGet);
                }
            }
            else
            {
                return Json(new { message = "Insert valid data", status = "Error" }, JsonRequestBehavior.AllowGet);
            }


        }

        [HttpPost]
        public ActionResult AddClassTime(Addclasstimeviewmodel ct)
        {

            if (ModelState.IsValid)
            {
                ClassTime c = new ClassTime();
                c.Classid = Convert.ToInt32(ct.Classid);
                c.ClassTime1 = ct.ClassTime;
                _context.ClassTimes.Add(c);
                _context.SaveChanges();
                return RedirectToAction("Viewclasses", "Trainer");
            }
            else
            {
                return View(ct);
            }
        }



        [HttpGet]
        public ActionResult ViewClassTime()
        {
            if (Session["UserId"] != null)
            {
                string tid = Session["UserId"].ToString();
                int trainerid = _context.Trainers.Single(m => m.userid == tid).TrainerId;

                //select Classes.ClassName , ClassTime.ClassTime
                //from Classes
                //join ClassTime On
                // Classes.ClassId = ClassTime.Classid
                // and Classes.Trainerid = 2
                // ;

                IEnumerable<viewclasstimeviewmodel> vclass;

                vclass = (from c1 in _context.Classes
                          join c2 in _context.ClassTimes on c1.ClassId equals c2.Classid
                          where c1.Trainerid == trainerid
                          select new viewclasstimeviewmodel { ClassName = c1.ClassName, ClassTime = c2.ClassTime1 }
                                );

                return View(vclass);
            }
            else
            {
                return RedirectToAction("SignIn", "Account");
            }


        }


        [Route("Trainer/DeleteClassTime/{Name?}/{time?}", Name = "DeleteClassTime")]
        public ActionResult DeleteClassTime(string Name, TimeSpan time)
        {

            var classtime = _context.ClassTimes.Where(m => m.Class.ClassName == Name).SingleOrDefault(m => m.ClassTime1 == time);
            _context.ClassTimes.Remove(classtime);
            _context.SaveChanges();
            return RedirectToAction("ViewClassTime", "Trainer");
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