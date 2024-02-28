using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PersonalTrainerI.Data.Migrations
{
    public partial class _11 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Gym",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    City = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    GymName = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    Street = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    StreetNumber = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Gym", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "PersonalTrainers",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    GymID = table.Column<int>(type: "int", nullable: false),
                    FirstName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    LastName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Rating = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    TrainingType = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    AvailabilityStart = table.Column<DateTime>(type: "datetime2", nullable: false),
                    AvailabilityEnd = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PersonalTrainers", x => x.ID);
                    table.ForeignKey(
                        name: "FK_PersonalTrainers_Gym_GymID",
                        column: x => x.GymID,
                        principalTable: "Gym",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Training",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserID = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    TrainerID = table.Column<int>(type: "int", nullable: false),
                    StartDateTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Duration = table.Column<TimeSpan>(type: "time", nullable: false),
                    TrainingType = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    TrainingPackage = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    PersonalTrainersID = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Training", x => x.ID);
                    table.ForeignKey(
                        name: "FK_Training_AspNetUsers_UserID",
                        column: x => x.UserID,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Training_PersonalTrainers_PersonalTrainersID",
                        column: x => x.PersonalTrainersID,
                        principalTable: "PersonalTrainers",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_PersonalTrainers_GymID",
                table: "PersonalTrainers",
                column: "GymID");

            migrationBuilder.CreateIndex(
                name: "IX_Training_PersonalTrainersID",
                table: "Training",
                column: "PersonalTrainersID");

            migrationBuilder.CreateIndex(
                name: "IX_Training_UserID",
                table: "Training",
                column: "UserID");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Training");

            migrationBuilder.DropTable(
                name: "PersonalTrainers");

            migrationBuilder.DropTable(
                name: "Gym");
        }
    }
}
