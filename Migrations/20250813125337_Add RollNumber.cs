using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MyMVCMappingDEMO.Migrations
{
    /// <inheritdoc />
    public partial class AddRollNumber : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "MRollNo",
                table: "MechEmployees",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "MRollNo",
                table: "MechEmployees");
        }
    }
}
