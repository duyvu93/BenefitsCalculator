using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace BenefitsCostCalculator.Models
{
    public class PersonModel
    {
        public int Id { get; set; }

        [Display(Name = "First Name")]
        [StringLength(20, MinimumLength = 1)]
        [Required]
        public string FirstName { get; set; }

        [Display(Name = "Last Name")]
        [StringLength(20, MinimumLength = 1)]
        [Required]
        public string LastName { get; set; }
        
        [DataType(DataType.Currency)]
        public double Cost { get; set; }
    }
}
