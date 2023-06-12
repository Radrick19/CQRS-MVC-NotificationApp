using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace FastSchedule.Domain.Migrations
{
    /// <inheritdoc />
    public partial class addColorPropToTasks : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Color",
                table: "WeeklyTasks",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Color",
                table: "OnetimeTasks",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Color",
                table: "MonthlyTasks",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Color",
                table: "EverydayTasks",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Color",
                table: "WeeklyTasks");

            migrationBuilder.DropColumn(
                name: "Color",
                table: "OnetimeTasks");

            migrationBuilder.DropColumn(
                name: "Color",
                table: "MonthlyTasks");

            migrationBuilder.DropColumn(
                name: "Color",
                table: "EverydayTasks");
        }
    }
}
