using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace WebApp_project.Models
{
    public class Zakaznik
    {
        public int ID { get; set; }

        [Display(Name = "Jméno:")]
        [Required(ErrorMessage = "Pole jméno nesmí být prázdné!")]
        public string jmeno { get; set; }

        [Display(Name = "Přijmení:")]
        [Required(ErrorMessage = "Pole příjmení nesmí být prázdné!")]
        public string prijmeni { get; set; }

        [Display(Name = "Adresa:")]
        public string adresa { get; set; }

        [Display(Name = "Telefon:")]
        public int telefon { get; set; }

        public int? typZakaznikID { get; set; }

        public int? pneuID { get; set; }


        [Display(Name = "Login:")]
        [Required(ErrorMessage = "Pole login nesmí být prázdné!")]
        public string login { get; set; }

        [Display(Name = "Heslo:")]
        [Required(ErrorMessage = "Pole heslo nesmí být prázdné!")]
        public string heslo { get; set; }
    }
}
