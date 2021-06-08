using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace WebApp_project.Models
{
    public class OrderCustomer
    {
        [Display(Name = "Jméno:")]
        [Required(ErrorMessage = "Pole jméno nemuže být prázdné!")]
        public string name { get; set; }

        [Display(Name = "Přijmení:")]
        [Required(ErrorMessage = "Pole přijmení nemuže být prázdné!")]
        public string lastname { get; set; }

        [Display(Name = "Email:")]
        [Required(ErrorMessage = "Pole email nemuže být prázdné!")]
        [EmailAddress]
        public string email { get; set; }

        [Display(Name = "Telefonní číslo:")]
        [Required(ErrorMessage = "Pole telefonní číslo nemuže být prázdné!")]
        public string phoneNum { get; set; }

        [Display(Name = "Město:")]
        [Required(ErrorMessage = "Pole město nemuže být prázdné!")]
        public string city { get; set; }

        [Display(Name = "Adresa:")]
        [Required(ErrorMessage = "Pole adresa nemuže být prázdné!")]
        public string adress { get; set; }

        [Display(Name = "PSČ:")]
        [Required(ErrorMessage = "Pole PSČ nemuže být prázdné!")]
        public string pCode { get; set; }

        [Display(Name = "Typ platby:")]
        [Required(ErrorMessage = "Pole typ platby  nemuže být prázdné!")]
        public string paymentType { get; set; }

        [Display(Name = "Typ přepravy:")]
        [Required(ErrorMessage = "Pole typ přepravy  nemuže být prázdné!")]
        public string transportType { get; set; }


    }
}
