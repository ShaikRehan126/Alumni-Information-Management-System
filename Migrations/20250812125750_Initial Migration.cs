using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MyMVCMappingDEMO.Migrations
{
    /// <inheritdoc />
    public partial class InitialMigration : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "MechEmployees",
                columns: table => new
                {
                    MId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    MName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    MAddress = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    MEmail = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    MPhone = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    MDesignation = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    MDepartment = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    MSalary = table.Column<float>(type: "real", nullable: true),
                    ImagePath = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MechEmployees", x => x.MId);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "MechEmployees");
        }
    }
}
