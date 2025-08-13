using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MyMVCMappingDEMO.Migrations
{
    /// <inheritdoc />
    public partial class AddCompany : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "MSalary",
                table: "MechEmployees");

            migrationBuilder.AddColumn<string>(
                name: "MCompany",
                table: "MechEmployees",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "MNotes",
                table: "MechEmployees",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "MYearPassed",
                table: "MechEmployees",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "MCompany",
                table: "MechEmployees");

            migrationBuilder.DropColumn(
                name: "MNotes",
                table: "MechEmployees");

            migrationBuilder.DropColumn(
                name: "MYearPassed",
                table: "MechEmployees");

            migrationBuilder.AddColumn<float>(
                name: "MSalary",
                table: "MechEmployees",
                type: "real",
                nullable: true);
        }
    }
}
