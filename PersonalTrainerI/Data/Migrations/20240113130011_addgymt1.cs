using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PersonalTrainerI.Data.Migrations
{
    public partial class addgymt1 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_PersonalTrainers_Gym_GymID",
                table: "PersonalTrainers");

            migrationBuilder.DropIndex(
                name: "IX_PersonalTrainers_GymID",
                table: "PersonalTrainers");

            migrationBuilder.DropColumn(
                name: "GymID",
                table: "PersonalTrainers");

            migrationBuilder.CreateTable(
                name: "GymTrainer",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    PersonalTrainerID = table.Column<int>(type: "int", nullable: false),
                    GymID = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_GymTrainer", x => x.ID);
                    table.ForeignKey(
                        name: "FK_GymTrainer_Gym_GymID",
                        column: x => x.GymID,
                        principalTable: "Gym",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_GymTrainer_PersonalTrainers_PersonalTrainerID",
                        column: x => x.PersonalTrainerID,
                        principalTable: "PersonalTrainers",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_GymTrainer_GymID",
                table: "GymTrainer",
                column: "GymID");

            migrationBuilder.CreateIndex(
                name: "IX_GymTrainer_PersonalTrainerID",
                table: "GymTrainer",
                column: "PersonalTrainerID");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "GymTrainer");

            migrationBuilder.AddColumn<int>(
                name: "GymID",
                table: "PersonalTrainers",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_PersonalTrainers_GymID",
                table: "PersonalTrainers",
                column: "GymID");

            migrationBuilder.AddForeignKey(
                name: "FK_PersonalTrainers_Gym_GymID",
                table: "PersonalTrainers",
                column: "GymID",
                principalTable: "Gym",
                principalColumn: "ID",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
