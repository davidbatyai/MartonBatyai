using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace MindenMancs.Models
{
    public class Shelters
    {
        public int Id { get; set; }
        [Display(Name = "Név")]
        public string Name { get; set; }
        [Display(Name = "Város")]
        public string City { get; set; }
        [Display(Name = "Cím")]
        public string Address { get; set; }
        [Display(Name = "E-mail")]
        public string Email { get; set; }
        [Display(Name = "Telefon")]
        public string PhoneNumber { get; set; }
        [Display(Name = "Adószám")]
        public string TaxNumber { get; set; }

    }
}
