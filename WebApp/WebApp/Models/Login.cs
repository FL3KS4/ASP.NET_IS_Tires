using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace WebApp_project.Models
{
    public class Login
    {
        [Display(Name = "Login:")]
        [Required(ErrorMessage = "Pole login nesmí být prázdné!")]
        public string login { get; set; }

        [Display(Name = "Heslo:")]
        [Required(ErrorMessage = "Pole heslo nesmí být prázdné!")]
        public string heslo { get; set; }
    }
}
