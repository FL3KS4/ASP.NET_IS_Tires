using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace WebApp_project.Models
{
    public class Vozidlo
    {
        public int? ID { get; set; }
        [Display(Name = "Značka a model:")]
        [MaxLength(40)]
        [Required(ErrorMessage = "Pole značka a model nesmí být prázdné!")]
        public string znacka { get; set; }
        [Display(Name = "SPZ:")]
        [Required(ErrorMessage = "Pole spz nesmí být prázdné!")]
        [MaxLength(7)]
        public string spz { get; set; }

        [Display(Name = "Typ vozidla:")]
        [Required(ErrorMessage = "Pole typ vozidla nesmí být prázdné!")]
        public int? typVozidlaID { get; set; }

        public int? pneuID_predni { get; set; }
        public int? pneuID_zadni { get; set; }

    }

    public class VozidloPneu
    {
        public int? ID { get; set; }
        [Display(Name = "Značka a model:")]
        [MaxLength(40)]
        [Required(ErrorMessage = "Pole značka a model nesmí být prázdné!")]
        public string znacka { get; set; }
        [Display(Name = "SPZ:")]
        [Required(ErrorMessage = "Pole spz nesmí být prázdné!")]
        [MaxLength(7)]
        public string spz { get; set; }

        [Display(Name = "Typ vozidla:")]
        [Required(ErrorMessage = "Pole typ vozidla nesmí být prázdné!")]
        public int? typVozidlaID { get; set; }

        public int? pneuID_predni { get; set; }
        public int? pneuID_zadni { get; set; }

        public string predniP { get; set; }
        public string zadniP { get; set; }
        public string rozmery { get; set; }
    }
    public class Zakaznik_vozidlo_relation
    {
        public int zakaznikID { get; set; }
        public int vozidloID { get; set; }
    }
}
