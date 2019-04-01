﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace Fitness.Models.Viewmodel
{
    public class Fitnessviewmodel
    {
    }

    public class Loginviewmodel
    {
        [Required(ErrorMessage = "PLease enter User name")]
        [StringLength(50)]
        public string UserName { get; set; }

        [Required(ErrorMessage = "PLease enter Password")]
        [DataType(DataType.Password)]
        [StringLength(50)]
        public string Password { get; set; }
    }

    public class Resgisterviewmodel
    {
        [Required(ErrorMessage = "Please enter first name")]
        [StringLength(100)]
        [Display(Name = "First Name")]
        public string FirstName { get; set; }

        [Required(ErrorMessage = "Please enter Last name")]
        [StringLength(100)]
        [Display(Name = "Last Name")]
        public string LastName { get; set; }

        [Required(ErrorMessage = "Please enter Date of birth")]
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
        [Display(Name = "City")]
        public int ZipCode { get; set; }

        [Required(ErrorMessage = "PLease enter User name")]
        [StringLength(50)]
        [Display(Name ="UserName")]
        public string UserName { get; set; }

        [Required(ErrorMessage = "PLease enter Password")]
        [DataType(DataType.Password)]
        [StringLength(50)]
        [Display(Name ="Password")]
        public string Password { get; set; }

        [Required(ErrorMessage = "PLease enter Confirm Password")]
        [DataType(DataType.Password)]
        [StringLength(50)]
        [Compare("Password")]
        [Display(Name ="Confirm Password")]
        public string ConfirmPassword { get; set; }

    }
}