using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace Work.Models
{
    public class Login
    {
        [Key]
        [Display(Name = "Email")]
        public string email { get; set; }
        [Display(Name = "Password")]
        public string password { get; set; }
    }
}