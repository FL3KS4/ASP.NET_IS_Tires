using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace WebApp_project.Models
{
    public class Eshop
    {
        public int ID { get; set; }

        [Display(Name = "Typ pneumatik")]
        [Required(ErrorMessage = "Pole typ pneumatik nesmí být prázdné!")]
        public int typ { get; set; }

        [Display(Name = "Značka a model")]
        [Required(ErrorMessage = "Pole značka a model nesmí být prázdné!")]
        [MaxLength(50)]
        public string znacka { get; set; }

        [Display(Name = "Rozměry")]
        [Required(ErrorMessage = "Pole rozměry nesmí být prázdné!")]
        [MaxLength(50,ErrorMessage = "Maximální délka je 10 chars!")]
        public string rozmery { get; set; }

        [Display(Name = "Počet")]
        [Required(ErrorMessage = "Pole počet nesmí být prázdné!")]
        public int pocet { get; set; }

        [Display(Name = "Cena")]
        [Required(ErrorMessage = "Pole cena nesmí být prázdné!")]
        public int cena { get; set; }


        [Display(Name = "Poznámka")]
        [MaxLength(200)]
        public string poznamka { get; set; }
    }
}
