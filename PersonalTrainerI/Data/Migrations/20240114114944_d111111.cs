using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PersonalTrainerI.Data.Migrations
{
    public partial class d111111 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AvailabilityViewModel_PersonalTrainers_PersonalTrainerID",
                table: "AvailabilityViewModel");

            migrationBuilder.DropPrimaryKey(
                name: "PK_AvailabilityViewModel",
                table: "AvailabilityViewModel");

            migrationBuilder.RenameTable(
                name: "AvailabilityViewModel",
                newName: "Availability");

            migrationBuilder.RenameIndex(
                name: "IX_AvailabilityViewModel_PersonalTrainerID",
                table: "Availability",
                newName: "IX_Availability_PersonalTrainerID");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Availability",
                table: "Availability",
                column: "ID");

            migrationBuilder.AddForeignKey(
                name: "FK_Availability_PersonalTrainers_PersonalTrainerID",
                table: "Availability",
                column: "PersonalTrainerID",
                principalTable: "PersonalTrainers",
                principalColumn: "ID",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Availability_PersonalTrainers_PersonalTrainerID",
                table: "Availability");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Availability",
                table: "Availability");

            migrationBuilder.RenameTable(
                name: "Availability",
                newName: "AvailabilityViewModel");

            migrationBuilder.RenameIndex(
                name: "IX_Availability_PersonalTrainerID",
                table: "AvailabilityViewModel",
                newName: "IX_AvailabilityViewModel_PersonalTrainerID");

            migrationBuilder.AddPrimaryKey(
                name: "PK_AvailabilityViewModel",
                table: "AvailabilityViewModel",
                column: "ID");

            migrationBuilder.AddForeignKey(
                name: "FK_AvailabilityViewModel_PersonalTrainers_PersonalTrainerID",
                table: "AvailabilityViewModel",
                column: "PersonalTrainerID",
                principalTable: "PersonalTrainers",
                principalColumn: "ID",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
