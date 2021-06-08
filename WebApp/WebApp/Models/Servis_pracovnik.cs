using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace WebApp_project.Models
{
    public class Servis_Pracovnik
    {
        public int ID { get; set; }

        [Display(Name = "Jméno:")]
        [Required(ErrorMessage = "Pole jméno nesmí být prázdné!")]
        public string jmeno { get; set; }
        [Display(Name = "Přijmení:")]
        [Required(ErrorMessage = "Pole prijmeni nesmí být prázdné!")]
        public string prijmeni { get; set; }

        [Display(Name="Login:")]
        [Required(ErrorMessage ="Pole login nesmí být prázdné!")]
        [MaxLength(7)]
        public string login { get; set; }

        [DisplayFormat(NullDisplayText = "", ApplyFormatInEditMode = true)]
        public DateTime? datum_narozeni { get; set; }

        [Display(Name = "Telefon:")]
        [Required(ErrorMessage = "Pole telefon nesmí být prázdné!")]
        public int telefon { get; set; }

        [Display(Name = "Heslo:")]
        [Required(ErrorMessage = "Pole heslo nesmí být prázdné!")]
        public string heslo { get; set; }
    }
}
