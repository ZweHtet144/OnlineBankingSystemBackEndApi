using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Banking_Project.Models
{
    public class Admin
    {
        [Required(ErrorMessage = "Please Enter UserName")]
        [Display(Name = "Enter UserName")]
        public string UserName { get; set; }

        [Required(ErrorMessage = "Please Enter Email")]
        [Display(Name = "Enter Email")]

        public string Email { get; set; }

        [Required(ErrorMessage = "Please Enter Password")]
        [Display(Name = "Enter Password")]
        [DataType(DataType.Password)]
        public string Password { get; set; }
    }
}