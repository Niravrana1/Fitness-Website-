using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;
using Fitness.Models.Validation;

namespace Fitness.Models.Viewmodel
{
    public class Fitnessviewmodel
    {
    }

    public class SigninViewmodel
    {
        [Required(ErrorMessage = "PLease enter User name")]
        [StringLength(50)]
        public string UserName { get; set; }

        [Required(ErrorMessage = "PLease enter Password")]
        [DataType(DataType.Password)]
        [StringLength(50)]
        public string Password { get; set; }

        [Display(Name = "Remember me?")]
        public bool RememberMe { get; set; }
    }

    public class Signupviewmodel
    {
        [Required(ErrorMessage = "Please enter first name")]
        [StringLength(100)]
        [Display(Name = "First Name")]
        public string FirstName { get; set; }

        [Required(ErrorMessage = "Please enter Last name")]
        [StringLength(100)]
        [Display(Name = "Last Name")]
        public string LastName { get; set; }

        [_18yearandold]
        [DisplayFormat(DataFormatString = "{0:mm/dd/yy}")]
        [Column(TypeName = "DateTime2")]
        [Display(Name = "Date of birth")]
        public DateTime DateOfBirth { get; set; }

        [Required(ErrorMessage = "Please enter Email address")]
        [StringLength(100)]
        [EmailAddress(ErrorMessage = "Invalid Email Address")]
        [Display(Name = "Email")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Please enter phone number")]
        [Display(Name = "Phone Number")]
        [StringLength(15)]
        [RegularExpression("^[0-9]*$", ErrorMessage = "No special characters allowed")]
        [DataType(DataType.PhoneNumber)]
        public string PhoneNumber { get; set; }

        [Required(ErrorMessage = "Please enter street address")]
        [StringLength(100)]
        [Display(Name = "Street Address")]
        public string StreetAddress { get; set; }

        [Required(ErrorMessage = "Please enter City name")]
        [StringLength(100)]
        [Display(Name = "City")]
        public string City { get; set; }

        [Required(ErrorMessage = "Please select State")]
        [StringLength(100)]
        [Display(Name = "State")]
        public string State { get; set; }

        [Required(ErrorMessage = "Please enter ZipCode")]
        [Display(Name = "ZipCode")]
        public int ZipCode { get; set; }

        [Required(ErrorMessage = "PLease enter User name")]
        [StringLength(50)]
        [Display(Name = "UserName")]
        public string UserName { get; set; }

        [Required(ErrorMessage = "PLease enter Password")]
        [DataType(DataType.Password)]
        [StringLength(50)]
        [Display(Name = "Password")]
        public string Password { get; set; }

        [Required(ErrorMessage = "PLease enter Confirm Password")]
        [DataType(DataType.Password,ErrorMessage = "Passwords must be at least 6 characters. Passwords must have at least one non letter or digit character. Passwords must have at least one digit ('0'-'9')")]
        [StringLength(50)]
        [Compare("Password")]
        [Display(Name = "Confirm Password")]
        public string ConfirmPassword { get; set; }

    }

    public class Signuptrainerviewmodel
    {
        [Required(ErrorMessage = "Please enter first name")]
        [StringLength(100)]
        [Display(Name = "First Name")]
        public string FirstName { get; set; }

        [Required(ErrorMessage = "Please enter Last name")]
        [StringLength(100)]
        [Display(Name = "Last Name")]
        public string LastName { get; set; }

        [_18yearandold]
        [DisplayFormat(DataFormatString = "{0:mm/dd/yy}")]
        [Column(TypeName = "DateTime2")]
        [Display(Name = "Date of birth")]
        public DateTime DateOfBirth { get; set; }

        [Required(ErrorMessage = "Please enter Email address")]
        [StringLength(100)]
        [EmailAddress(ErrorMessage = "Invalid Email Address")]
        [Display(Name = "Email")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Please enter phone number")]
        [Display(Name = "Phone Number")]
        [StringLength(15)]
        [RegularExpression("^[0-9]*$", ErrorMessage = "No special characters allowed")]
        [DataType(DataType.PhoneNumber)]
        public string PhoneNumber { get; set; }

        [Required(ErrorMessage = "Please enter street address")]
        [StringLength(100)]
        [Display(Name = "Street Address")]
        public string StreetAddress { get; set; }

        [Required(ErrorMessage = "Please enter City name")]
        [StringLength(100)]
        [Display(Name = "City")]
        public string City { get; set; }

        [Required(ErrorMessage = "Please select State")]
        [StringLength(100)]
        [Display(Name = "State")]
        public string State { get; set; }

        [Required(ErrorMessage = "Please enter ZipCode")]
        [Display(Name = "ZipCode")]
        public int ZipCode { get; set; }

        [Required(ErrorMessage = "PLease enter User name")]
        [StringLength(50)]
        [Display(Name = "UserName")]
        public string UserName { get; set; }

        [Required(ErrorMessage = "PLease enter Password")]
        [DataType(DataType.Password)]
        [StringLength(50)]
        [Display(Name = "Password")]
        public string Password { get; set; }

        [Required(ErrorMessage = "PLease enter Confirm Password")]
        [DataType(DataType.Password, ErrorMessage = "Passwords must be at least 6 characters. Passwords must have at least one non letter or digit character. Passwords must have at least one digit ('0'-'9')")]
        [StringLength(50)]
        [Compare("Password")]
        [Display(Name = "Confirm Password")]
        public string ConfirmPassword { get; set; }

        [Display(Name ="About You")]
        public string Aboutyou { get; set; }

        [Display(Name ="Profile Picture")]
        [ValidateImageFile(ErrorMessage = "Please select image smaller than 1MB")]
        public HttpPostedFile  Image { get; set; }

    }

    public class Addclassviewmodel
    {
        [Required(ErrorMessage = "Please enter Class name")]
        [StringLength(50)]
        [Display(Name ="Class Name")]
        public string ClassName { get; set; }

        [Required(ErrorMessage = "Please enter Class discription")]
        [StringLength(50)]
        [Display(Name = "Class Discription")]
        public string ClassDiscription { get; set; }

        [Required(ErrorMessage = "Please enter class price")]
        [Display(Name ="Class Price")]
        public Nullable<decimal> ClassPrice { get; set; }
    }

    public class Addclasstimeviewmodel
    {
        [Required(ErrorMessage = "Please select class")]
        [Display(Name ="Class")]
        public int Classid { get; set; }

        [Required(ErrorMessage = "Please select class date")]
        [DisplayFormat(DataFormatString = "{0:mm/dd/yy}")]
        [Column(TypeName = "DateTime2")]
        [Display(Name = "Class date")]
        public DateTime ClassDate { get; set; }

        [Required(ErrorMessage = "Please select class time")]
        [Display(Name ="class time")]
        public TimeSpan ClassTime { get; set; }
    }

    public class viewclasstimeviewmodel
    {
        [Display(Name = "Class name")]
        public string ClassName { get; set; }

        [Display(Name = "Class time")]
        public Nullable<System.TimeSpan> ClassTime { get; set; }
    }

    public class Scheduletrainerviewmodel
    {
        public Trainer trainer { get; set; }
        public Trainerrate trainerrate { get; set; }
        public decimal? price { get; set; }
        
    }

    public class Shceduletrainingsessionviewmodel
    {
        public int id { get; set; }

        public List<Trainerrate> Trainerrate { get; set; }

        public int? TrainerId { get; set; }

        public int? duration { get; set; }

        public decimal? price { get; set; }

        public DateTime? ScheduledDate { get; set; }

        public TimeSpan? ScheduledTime { get; set; }



    }

    public class Listoftrainingsessionviewmodel
    {
        public IEnumerable<Trainer> trainer { get; set; }

        public IEnumerable<ScheduleTrainer> ScheduleTrainers { get; set; }
    }

    public class Scheduleclassviewmodel
    {
        public int scheduledclassid { get; set; }
    
        public int classid { get; set; }

        public string classname { get; set; }

        public int Membershiptype { get; set; }

        public string MembershipName { get; set; }

        public decimal price { get; set; }

        public System.DateTime? ScheduleDate { get; set; }

        public System.TimeSpan? ScheduleTime { get; set; }
    }
    public class viewscheduledclassviewmodel
    {
     
    }
}