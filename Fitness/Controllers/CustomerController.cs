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
            if (Session["User"] != null)
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

        //GET: Customer / View Trainers

        //GET: Customer/ Shcedule Training session
        [HttpGet]

        public ActionResult ScheduleTraining(int id)
        {
            var trainer = _Context.Trainers.FirstOrDefault(m => m.TrainerId == id);
            var trainerrates = _Context.Trainerrates.FirstOrDefault(m => m.trainerid == id);
            decimal? tprice = _Context.Trainerrates.Where(m => m.trainerid == id).FirstOrDefault(m => m.Duration == 1).Price;
            var Trainers = new Scheduletrainerviewmodel()
            {
                trainer = trainer,
                trainerrate = trainerrates,
                price = tprice
            };

            return View(Trainers);
        }

        // GET: Customer/ View Shceduled Training session
        [HttpGet]
        [Route("Customer/ScheduleTrainingsession/{Id}", Name = "trainingsession")]
        public ActionResult ScheduleTrainingsession(int id)
        {
            var trate = _Context.Trainerrates.ToList();
            ViewBag.Trainer = new SelectList(_Context.Trainers, "TrainerId", "FirstName");
            var selectedtrainer = new Shceduletrainingsessionviewmodel()
            {
                TrainerId = id
            };


            return View(selectedtrainer);
        }

        [HttpGet]
        public ActionResult ScheduleTrainingsession()
        {
            var trate = _Context.Trainerrates.ToList();
            ViewBag.Trainer = new SelectList(_Context.Trainers, "TrainerId", "FirstName");

            return View();
        }

        //[HttpGet]
        //public ActionResult ScheduleTrainingsession()
        //{
        //    var trate = _Context.Trainerrates.ToList();
        //    ViewBag.Trainer = new SelectList(_Context.Trainers, "TrainerId", "FirstName");

        //    return View();
        //}

        [HttpPost]
        [Route("trainingsession")]
        public ActionResult ScheduleTrainingsession(Shceduletrainingsessionviewmodel stv)
        {

            string user = Session["UserId"].ToString();
            int customer = _Context.Customers.Single(m => m.userid == user).CustomerId;
            if (ModelState.IsValid)
            {
                if (stv.id == 0)
                {
                   
                    ScheduleTrainer st = new ScheduleTrainer();
                    st.UserId = customer;
                    st.TrainerId = stv.TrainerId;
                    st.duration = stv.duration;
                    st.price = stv.price;
                    st.ScheduleDate = stv.ScheduledDate;
                    st.ScheduleTime = stv.ScheduledTime;
                    st.AppointmentStatus = true;
                    _Context.ScheduleTrainers.Add(st);
                    _Context.SaveChanges();

                    return RedirectToAction("Listofscheduledtrainingsession", "customer");
                }
                else
                {
                    var st = _Context.ScheduleTrainers.SingleOrDefault(m => m.Id == stv.id);

                    st.TrainerId = stv.TrainerId;
                    st.duration = stv.duration;
                    st.price = stv.price;
                    st.ScheduleDate = stv.ScheduledDate;
                    st.ScheduleTime = stv.ScheduledTime;
                    _Context.SaveChanges();
                    return RedirectToAction("Listofscheduledtrainingsession", "customer");
                }
            }
            else
            {
                return View(stv);
            }


        }


        public JsonResult gettrainerprice(string tid, string dtime)
        {
            if (tid != null && dtime != null)
            {
                int trainerid = Convert.ToInt32(tid);
                int duration = Convert.ToInt32(dtime);

                decimal? price = _Context.Trainerrates.Where(m => m.trainerid == trainerid).SingleOrDefault(m => m.Duration == duration).Price;
                if (price != null)
                {
                    return Json(new { result = price, status = "Success" }, JsonRequestBehavior.AllowGet);
                }
                else
                {
                    return Json(new { message = "Something went wrong", status = "Success" }, JsonRequestBehavior.AllowGet);
                }
            }
            else
            {
                return Json(new { message = "Please enter valid data", status = "Error" }, JsonRequestBehavior.AllowGet);
            }
        }


        public JsonResult checkavailabletime(string sdate, string dvalue, string tvalue)
        {
            DateTime scheduledate = Convert.ToDateTime(sdate);
            int duration = Convert.ToInt32(dvalue);
            int trainerid = Convert.ToInt32(tvalue);

            var date = _Context.NotavailableDatetimes.Where(m => m.TrainerId == trainerid).Where(m => m.NotavailableDate == scheduledate);

            List<SelectListItem> listoftime = new List<SelectListItem>();
            if (duration == 1)
            {
                for (int i = 9; i < 23; i++)
                {
                    var st = date.Where(m => m.StartTime.Value.Hours == i);
                    int countst = st.Count();

                    if (countst == 1)
                    {
                        var et = st.Select(m => m.Endtime.Value.Hours).SingleOrDefault();
                        i = Convert.ToInt32(et);
                    }

                    if (countst == 0)
                    {
                        int newi;
                        string newiformat;
                        if (i >= 13)
                        {
                            newi = i - 12;
                            newiformat = " PM";
                        }
                        else
                        {
                            newi = i;
                            newiformat = " AM";
                        }

                        listoftime.Add(new SelectListItem
                        {
                            Text = newi + newiformat + " to " + (newi + 1) + newiformat,
                            Value = i.ToString()
                        });
                    }

                }
            }

            if (duration == 2)
            {
                for (int i = 9; i < 23; i++)
                {
                    var st = date.Where(m => m.StartTime.Value.Hours == i);
                    int countst = st.Count();

                    if (countst == 1)
                    {
                        var et = st.Select(m => m.Endtime.Value.Hours).SingleOrDefault();
                        i = Convert.ToInt32(et);
                    }

                    if (countst == 0)
                    {
                        int newi;
                        string newiformat;
                        if (i >= 13)
                        {
                            newi = i - 12;
                            newiformat = " PM";
                        }
                        else
                        {
                            newi = i;
                            newiformat = " AM";
                        }

                        listoftime.Add(new SelectListItem
                        {
                            Text = newi + newiformat + " to " + (newi + 2) + newiformat,
                            Value = i.ToString()
                        });
                    }

                }
            }
            if (duration == 3)
            {
                for (int i = 9; i < 23; i++)
                {
                    var st = date.Where(m => m.StartTime.Value.Hours == i);
                    int countst = st.Count();

                    if (countst == 1)
                    {
                        var et = st.Select(m => m.Endtime.Value.Hours).SingleOrDefault();
                        i = Convert.ToInt32(et);
                    }

                    if (countst == 0)
                    {
                        int newi;
                        string newiformat;
                        if (i >= 13)
                        {
                            newi = i - 12;
                            newiformat = " PM";
                        }
                        else
                        {
                            newi = i;
                            newiformat = " AM";
                        }

                        listoftime.Add(new SelectListItem
                        {
                            Text = newi + newiformat + " to " + (newi + 3) + newiformat,
                            Value = i.ToString()
                        });
                    }

                }
            }

            return Json(new { result = listoftime, status = "Success" }, JsonRequestBehavior.AllowGet);
        }

        public ActionResult Listofscheduledtrainingsession()
        {
            string user = Session["UserId"].ToString();
            int customer = _Context.Customers.Single(m => m.userid == user).CustomerId;
            IEnumerable<ScheduleTrainer> scheduleTrainers = _Context.ScheduleTrainers.Where(m => m.UserId == customer).ToList();


            return View(scheduleTrainers);
        }

        [HttpGet]
        public ActionResult EditTrainingsession(int id)
        {
            var editsession = _Context.ScheduleTrainers.SingleOrDefault(m => m.Id == id);
            ViewBag.Trainer = new SelectList(_Context.Trainers, "TrainerId", "FirstName");
            var viewmodel = new Shceduletrainingsessionviewmodel()
            {
                id = editsession.Id,
                duration = editsession.duration,
                price = editsession.price,
                ScheduledDate = editsession.ScheduleDate,
                ScheduledTime = editsession.ScheduleTime
            };
            return View("ScheduleTrainingsession", viewmodel);
        }

        //[HttpPost]
        //public ActionResult EditTrainingsession(Shceduletrainingsessionviewmodel stv)
        //{
        //    if (ModelState.IsValid)
        //    {
        //       var st = _Context.ScheduleTrainers.SingleOrDefault(m => m.Id == stv.id);

        //        st.TrainerId = stv.TrainerId;
        //        st.duration = stv.duration;
        //        st.price = stv.price;
        //        st.ScheduleDate = stv.ScheduledDate;
        //        st.ScheduleTime = stv.ScheduledTime;
        //        _Context.SaveChanges();

        //        return RedirectToAction("Listofscheduledtrainingsession", "customer");
        //    }
        //    return View();
        //}





        public ActionResult DeleteTrainingsession(int id)
        {
            var deletetrainningsession = _Context.ScheduleTrainers.Find(id);
            _Context.ScheduleTrainers.Remove(deletetrainningsession);
            return RedirectToAction("Listofscheduledtrainingsession", "Customer");

        }

        public ActionResult Viewclasses()
        {
          var classlist =   _Context.Classes.ToList();
            return View(classlist);
        }

        [HttpGet]
        public ActionResult ScheduleClasssession()
        {
            ViewBag.classes = new SelectList(_Context.Classes, "ClassId", "ClassName");
            ViewBag.membershiptype = new SelectList(_Context.ClassMembershipTypes, "Weeks", "MembershipName");
            return View();
        }

        [HttpPost]
        public ActionResult ScheduleClasssession(Scheduleclassviewmodel scv)
        {
           if(ModelState.IsValid)
            {
                if(Session["UserId"]!=null)
                {
                    
                    string user = Session["UserId"].ToString();
                    int customer = _Context.Customers.Single(m => m.userid == user).CustomerId;
                    ScheduleClass sc = new ScheduleClass();
                    sc.UserId = customer;
                    sc.classid = scv.classid;
                    sc.Membershiptype = _Context.ClassMembershipTypes.FirstOrDefault(m => m.Weeks == scv.Membershiptype).id; // Cannot save directly because membershiptype in scheduleclasssession stores a week value.
                    sc.TotalAmount = Convert.ToDecimal(scv.price);
                    sc.ScheduleDate = Convert.ToDateTime(scv.ScheduleDate);
                    sc.ScheduleTime = scv.ScheduleTime;
                    _Context.ScheduleClasses.Add(sc);
                    _Context.SaveChanges();

                    return RedirectToAction("Viewscheduledclasses", "customer");
                }
                else
                {
                    return RedirectToAction("SignIn", "Account");
                }
                
            }
            else
            {
                return View();
            }
            
        }

        public JsonResult getclasstime(string classdate, string classid)
        {
            if (classdate != null && classdate != "" && classid != null && classid != "")
            {
                DateTime cdate = Convert.ToDateTime(classdate);
                int cid = Convert.ToInt32(classid);

                var classtime = _Context.ClassTimes.Where(m => m.Classid == cid).ToList();

                var classtimelist = new SelectList(classtime, "ClassTime1", "ClassTime1");



                return Json(new { result = classtimelist, status = "Success" }, JsonRequestBehavior.AllowGet);
            }
            else
            {
                return Json(new { result = "", status = "Error" }, JsonRequestBehavior.AllowGet);
            }
        }

        public JsonResult getclasspricewithmembership(string mtype, string cname)
        {
            int cid = Convert.ToInt32(cname);
            int mweeks = Convert.ToInt32(mtype);
            decimal? totalamount = 0;
            decimal? cprice = _Context.Classes.SingleOrDefault(m => m.ClassId == cid).ClassPrice;
            if (mweeks == 0)
            {
                totalamount = cprice;
            }
            else if (mweeks == 1)
            {
                totalamount = cprice * 7;
            }
            else if (mweeks == 2)
            {
                totalamount = cprice * 14;
            }
            else if (mweeks == 3)
            {
                totalamount = cprice * 21;
            }
            else if (mweeks == 4)
            {
                totalamount = cprice * 28;
            }


            return Json(new { result = totalamount, status = "Success" }, JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        public ActionResult Viewscheduledclasses()
        {

            if(Session["UserID"]!=null)
            {
                string user = Session["UserId"].ToString();
                int customer = _Context.Customers.Single(m => m.userid == user).CustomerId;

                //var classlist =  _Context.ScheduleClasses.Where(m => m.UserId == customer).ToList();
                var classeslist = (from s in _Context.ScheduleClasses.Where(m => m.UserId == customer)
                                   join c in _Context.Classes on s.classid equals c.ClassId
                                   select new Scheduleclassviewmodel
                                   {
                                       scheduledclassid = s.id,
                                       classname = c.ClassName,
                                       MembershipName = _Context.ClassMembershipTypes.FirstOrDefault(m =>m.id == s.Membershiptype).MembershipName,
                                       ScheduleDate = s.ScheduleDate,
                                       ScheduleTime = s.ScheduleTime
                                   }).ToList();


                return View(classeslist);
            }
            else
            {
                return RedirectToAction("SignIn", "Account");
            }

        }

        public ActionResult ScheduledclassDetails(int id)
        {

            if(id!=null)
            {
              var classes =  _Context.ScheduleClasses.FirstOrDefault(m => m.id == id);
                string fname = _Context.Customers.FirstOrDefault(m => m.CustomerId == classes.UserId).FirstName;
                string lname = _Context.Customers.FirstOrDefault(m => m.CustomerId == classes.UserId).LastName;
                ViewBag.Name = fname + " " + lname;
                ViewBag.ClassName= _Context.Classes.FirstOrDefault(m => m.ClassId == classes.classid).ClassName;
                ViewBag.Membershiptype = _Context.ClassMembershipTypes.FirstOrDefault(m => m.id == classes.Membershiptype).MembershipName;
                int? trainerid = _Context.Classes.FirstOrDefault(m => m.ClassId == classes.classid).Trainerid;
                string tfname = _Context.Trainers.FirstOrDefault(m => m.TrainerId == trainerid).FirstName;
                string tlname = _Context.Trainers.FirstOrDefault(m => m.TrainerId == trainerid).LastName;
                ViewBag.Trainername = tfname + " " + tlname;
                ViewBag.Totaprice = classes.TotalAmount;
                ViewBag.date = classes.ScheduleDate;
                ViewBag.time = classes.ScheduleTime;
               int? week =  _Context.ClassMembershipTypes.FirstOrDefault(m => m.id == classes.Membershiptype).Weeks;
                ViewBag.weeks = week;
                return View();

            }
            else
            {
                return HttpNotFound();
            }
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