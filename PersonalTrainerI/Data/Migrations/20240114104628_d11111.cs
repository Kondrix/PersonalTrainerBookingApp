using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PersonalTrainerI.Data.Migrations
{
    public partial class d11111 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "TrainerID",
                table: "AvailabilityViewModel");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "TrainerID",
                table: "AvailabilityViewModel",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }
    }
}
