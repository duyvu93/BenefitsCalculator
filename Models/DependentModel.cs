using System.ComponentModel.DataAnnotations;

namespace BenefitsCostCalculator.Models
{
    public class DependentModel : PersonModel
    {
        public int ParentId { get; set; }
        public bool IsSpouse { get; set; }
    }
}
