using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PersonalTrainerI.Data.Migrations
{
    public partial class sa1 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "IsVerified",
                table: "PersonalTrainers",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<string>(
                name: "UserId",
                table: "PersonalTrainers",
                type: "nvarchar(450)",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_PersonalTrainers_UserId",
                table: "PersonalTrainers",
                column: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_PersonalTrainers_AspNetUsers_UserId",
                table: "PersonalTrainers",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_PersonalTrainers_AspNetUsers_UserId",
                table: "PersonalTrainers");

            migrationBuilder.DropIndex(
                name: "IX_PersonalTrainers_UserId",
                table: "PersonalTrainers");

            migrationBuilder.DropColumn(
                name: "IsVerified",
                table: "PersonalTrainers");

            migrationBuilder.DropColumn(
                name: "UserId",
                table: "PersonalTrainers");
        }
    }
}
