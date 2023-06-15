using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace FastSchedule.Domain.Migrations
{
    /// <inheritdoc />
    public partial class addRepeatTypeFieldToTask : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "RemindType",
                table: "Tasks",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "RemindType",
                table: "Tasks");
        }
    }
}
