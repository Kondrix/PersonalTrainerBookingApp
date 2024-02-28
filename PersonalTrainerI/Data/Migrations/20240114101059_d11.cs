using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PersonalTrainerI.Data.Migrations
{
    public partial class d11 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "AvailabilityEnd",
                table: "PersonalTrainers");

            migrationBuilder.DropColumn(
                name: "AvailabilityStart",
                table: "PersonalTrainers");

            migrationBuilder.CreateTable(
                name: "AvailabilityViewModel",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    TrainerID = table.Column<int>(type: "int", nullable: false),
                    Date = table.Column<DateTime>(type: "datetime2", nullable: false),
                    StartTime = table.Column<TimeSpan>(type: "time", nullable: false),
                    EndTime = table.Column<TimeSpan>(type: "time", nullable: false),
                    PersonalTrainerID = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AvailabilityViewModel", x => x.ID);
                    table.ForeignKey(
                        name: "FK_AvailabilityViewModel_PersonalTrainers_PersonalTrainerID",
                        column: x => x.PersonalTrainerID,
                        principalTable: "PersonalTrainers",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_AvailabilityViewModel_PersonalTrainerID",
                table: "AvailabilityViewModel",
                column: "PersonalTrainerID");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AvailabilityViewModel");

            migrationBuilder.AddColumn<DateTime>(
                name: "AvailabilityEnd",
                table: "PersonalTrainers",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<DateTime>(
                name: "AvailabilityStart",
                table: "PersonalTrainers",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));
        }
    }
}
