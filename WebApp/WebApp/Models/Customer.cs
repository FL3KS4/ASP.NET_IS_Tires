using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace WebApp_project.Models
{
    public class Customer
    {

        public int guestID { get; set; }

        [Display(Name = "Jméno:")]
        [Required(ErrorMessage = "Pole jméno nesmí být prázdné!")]
        public string name { get; set; }

        [Display(Name = "Přijmení:")]
        [Required(ErrorMessage = "Pole přijmení nesmí být prázdné!")]
        public string lastName { get; set; }

        [Display(Name = "Telefon:")]
        [Required(ErrorMessage = "Pole telefon nesmí být prázdné!")]

        public int telephoneNum { get; set; }


        public int vehicleID { get; set; }

        [Display(Name = "Značka a model:")]
        [Required(ErrorMessage = "Pole značka a model nesmí být prázdné!")]
        public string brand { get; set; }


        [Display(Name = "Servisní typ:")]
        [Required(ErrorMessage = "Pole servisní typ nesmí být prázdné!")]
        public string serviceType { get; set; }

        [Display(Name = "Typ vozidla:")]
        [Required(ErrorMessage = "Pole typ vozidla nesmí být prázdné!")]
        public string vehicleType { get; set; }

        [Display(Name = "Datum:")]
        [Required(ErrorMessage = "Pole datum nesmí být prázdné!")]
        public DateTime date { get; set; }

        [Display(Name = "Čas:")]
        [Required(ErrorMessage = "Pole čas nesmí být prázdné!")]
        public DateTime time { get; set; }

    }
}
