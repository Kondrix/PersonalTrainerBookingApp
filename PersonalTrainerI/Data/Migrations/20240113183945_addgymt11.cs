using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PersonalTrainerI.Data.Migrations
{
    public partial class addgymt11 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "GymID",
                table: "Training",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Training_GymID",
                table: "Training",
                column: "GymID");

            migrationBuilder.AddForeignKey(
                name: "FK_Training_Gym_GymID",
                table: "Training",
                column: "GymID",
                principalTable: "Gym",
                principalColumn: "ID",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Training_Gym_GymID",
                table: "Training");

            migrationBuilder.DropIndex(
                name: "IX_Training_GymID",
                table: "Training");

            migrationBuilder.DropColumn(
                name: "GymID",
                table: "Training");
        }
    }
}
