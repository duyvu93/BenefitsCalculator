using BenefitsCostCalculator.Models;
using Microsoft.EntityFrameworkCore;

namespace BenefitsCostCalculator.Data
{
    public class EmployeeContext : DbContext
    {
        public EmployeeContext(DbContextOptions<EmployeeContext> options)
            : base(options)
        {
        }

        public DbSet<EmployeeModel> EmployeeModel { get; set; }
        public DbSet<DependentModel> DependentModel { get; set; }
    }
}
