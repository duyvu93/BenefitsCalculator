using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using System.Transactions;

namespace BenefitsCostCalculator.Models
{
    public class EmployeeModel : PersonModel
    {
        [Display(Name = "Number of Dependents")]
        public int NumberOfDependents { get; set; }
        public List<DependentModel> Dependents { get; set; }
        [Display(Name = "Total Cost per Year")]
        public double TotalCost { get; set; }
    }
}
