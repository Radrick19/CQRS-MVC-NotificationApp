using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace FastSchedule.Domain.Migrations
{
    /// <inheritdoc />
    public partial class updateTesksEntities : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "DailyTasks");

            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DropColumn(
                name: "EventDaysOfWeek",
                table: "WeeklyTasks");

            migrationBuilder.DropColumn(
                name: "EventDaysOfMonth",
                table: "MonthlyTasks");

            migrationBuilder.AddColumn<int>(
                name: "EventDayOfWeek",
                table: "WeeklyTasks",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "EventDayOfMonth",
                table: "MonthlyTasks",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateTable(
                name: "EverydayTasks",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Guid = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Label = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    UserId = table.Column<int>(type: "int", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    EventTime = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PreNotifyTime = table.Column<TimeSpan>(type: "time", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EverydayTasks", x => x.Id);
                    table.ForeignKey(
                        name: "FK_EverydayTasks_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "OnetimeTasks",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    EventDay = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Guid = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Label = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    UserId = table.Column<int>(type: "int", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    EventTime = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PreNotifyTime = table.Column<long>(type: "bigint", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_OnetimeTasks", x => x.Id);
                    table.ForeignKey(
                        name: "FK_OnetimeTasks_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_EverydayTasks_UserId",
                table: "EverydayTasks",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_OnetimeTasks_Guid",
                table: "OnetimeTasks",
                column: "Guid",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_OnetimeTasks_UserId",
                table: "OnetimeTasks",
                column: "UserId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "EverydayTasks");

            migrationBuilder.DropTable(
                name: "OnetimeTasks");

            migrationBuilder.DropColumn(
                name: "EventDayOfWeek",
                table: "WeeklyTasks");

            migrationBuilder.DropColumn(
                name: "EventDayOfMonth",
                table: "MonthlyTasks");

            migrationBuilder.AddColumn<string>(
                name: "EventDaysOfWeek",
                table: "WeeklyTasks",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "EventDaysOfMonth",
                table: "MonthlyTasks",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.CreateTable(
                name: "DailyTasks",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserId = table.Column<int>(type: "int", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    EventDay = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    EventTime = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Guid = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Label = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    PreNotifyTime = table.Column<long>(type: "bigint", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DailyTasks", x => x.Id);
                    table.ForeignKey(
                        name: "FK_DailyTasks_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "Id", "Email", "Guid", "Login", "Password", "Salt" },
                values: new object[] { 1, "Radricksh@gmail.com", new Guid("2c8c670b-fb85-4a83-8159-c9efd2066a69"), "Radrick", "259", "-12312312" });

            migrationBuilder.InsertData(
                table: "DailyTasks",
                columns: new[] { "Id", "Description", "EventDay", "EventTime", "Guid", "Label", "PreNotifyTime", "UserId" },
                values: new object[] { 1, null, "08.06.2023", "16:00", new Guid("3b99ff1a-ff3a-4503-a3b1-cb491c18fc20"), "Do some work", 36000000000L, 1 });

            migrationBuilder.CreateIndex(
                name: "IX_DailyTasks_Guid",
                table: "DailyTasks",
                column: "Guid",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_DailyTasks_UserId",
                table: "DailyTasks",
                column: "UserId");
        }
    }
}
