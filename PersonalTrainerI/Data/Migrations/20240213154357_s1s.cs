using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PersonalTrainerI.Data.Migrations
{
    public partial class s1s : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "AspNetUserId",
                table: "PersonalTrainers");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "AspNetUserId",
                table: "PersonalTrainers",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }
    }
}
