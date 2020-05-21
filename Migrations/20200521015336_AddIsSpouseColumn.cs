using Microsoft.EntityFrameworkCore.Migrations;

namespace BenefitsCostCalculator.Migrations
{
    public partial class AddIsSpouseColumn : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "IsSpouse",
                table: "DependentModel",
                nullable: false,
                defaultValue: false);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsSpouse",
                table: "DependentModel");
        }
    }
}
