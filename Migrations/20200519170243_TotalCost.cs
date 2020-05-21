using Microsoft.EntityFrameworkCore.Migrations;

namespace BenefitsCostCalculator.Migrations
{
    public partial class TotalCost : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<double>(
                name: "TotalCost",
                table: "EmployeeModel",
                nullable: false,
                defaultValue: 0.0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "TotalCost",
                table: "EmployeeModel");
        }
    }
}
