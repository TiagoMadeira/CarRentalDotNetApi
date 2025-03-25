using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace api.Migrations
{
    /// <inheritdoc />
    public partial class RentalBlockedDateOneToOneCorrection : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_BlockedDates_Rentals_RentalId",
                table: "BlockedDates");

            migrationBuilder.DropIndex(
                name: "IX_BlockedDates_RentalId",
                table: "BlockedDates");

            migrationBuilder.DropColumn(
                name: "RentalId",
                table: "BlockedDates");

            migrationBuilder.CreateIndex(
                name: "IX_Rentals_BlockedDateId",
                table: "Rentals",
                column: "BlockedDateId",
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Rentals_BlockedDates_BlockedDateId",
                table: "Rentals",
                column: "BlockedDateId",
                principalTable: "BlockedDates",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Rentals_BlockedDates_BlockedDateId",
                table: "Rentals");

            migrationBuilder.DropIndex(
                name: "IX_Rentals_BlockedDateId",
                table: "Rentals");

            migrationBuilder.AddColumn<int>(
                name: "RentalId",
                table: "BlockedDates",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_BlockedDates_RentalId",
                table: "BlockedDates",
                column: "RentalId",
                unique: true,
                filter: "[RentalId] IS NOT NULL");

            migrationBuilder.AddForeignKey(
                name: "FK_BlockedDates_Rentals_RentalId",
                table: "BlockedDates",
                column: "RentalId",
                principalTable: "Rentals",
                principalColumn: "Id");
        }
    }
}
