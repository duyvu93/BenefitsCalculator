using Microsoft.EntityFrameworkCore.Migrations;

namespace BenefitsCostCalculator.Migrations
{
    public partial class InitialDB : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "EmployeeModel",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    FirstName = table.Column<string>(nullable: true),
                    LastName = table.Column<string>(nullable: true),
                    NumberOfDependents = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EmployeeModel", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "DependentModel",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    FirstName = table.Column<string>(nullable: true),
                    LastName = table.Column<string>(nullable: true),
                    ParentId = table.Column<int>(nullable: false),
                    EmployeeModelId = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DependentModel", x => x.Id);
                    table.ForeignKey(
                        name: "FK_DependentModel_EmployeeModel_EmployeeModelId",
                        column: x => x.EmployeeModelId,
                        principalTable: "EmployeeModel",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_DependentModel_EmployeeModelId",
                table: "DependentModel",
                column: "EmployeeModelId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "DependentModel");

            migrationBuilder.DropTable(
                name: "EmployeeModel");
        }
    }
}
