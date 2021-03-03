using Microsoft.EntityFrameworkCore.Migrations;

namespace CapstoneTranslator.Migrations
{
    public partial class UpdateMigration : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Pay",
                table: "Employers",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Schedule",
                table: "Employers",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Pay",
                table: "Employers");

            migrationBuilder.DropColumn(
                name: "Schedule",
                table: "Employers");
        }
    }
}
