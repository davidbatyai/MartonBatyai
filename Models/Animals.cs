using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace MindenMancs.Models
{
    public class Animals
    {
        [Key]
        [Display(Name = "Chip")]
        public int ChipID { get; set; }
        [Display(Name = "Név")]
        public string Name { get; set; }
        [Display(Name = "Faj")]
        public string Species { get; set; }
        [Display(Name = "Fajta")]
        public string Breed { get; set; }
        [Display(Name = "Nem")]
        public string Sex { get; set; }
        [Display(Name = "Kor")]
        public string Born { get; set; }
        [Display(Name = "Súly")]
        [Column(TypeName = "decimal(18,2)")]
        public decimal Weight { get; set; }
        [Display(Name = "Magasság")]
        [Column(TypeName = "decimal(18,2)")]
        public decimal Height { get; set; }
        [Display(Name = "Szín")]
        public string Colour { get; set; }
        [Display(Name = "Szőrzet")]
        public string Coat { get; set; }
        [Display(Name = "Leírás")]

        public string Description { get; set; }
        [Display(Name = "Kép")]
        public byte Image { get; set; }
        [Display(Name = "Menhely")]
        public string ShelterId { get; set; }

    }
}
