using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace api.Migrations
{
    /// <inheritdoc />
    public partial class AppUserRentalOneToMany : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "AppUserId",
                table: "Rentals",
                type: "nvarchar(450)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.CreateIndex(
                name: "IX_Rentals_AppUserId",
                table: "Rentals",
                column: "AppUserId");

            migrationBuilder.AddForeignKey(
                name: "FK_Rentals_AspNetUsers_AppUserId",
                table: "Rentals",
                column: "AppUserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Rentals_AspNetUsers_AppUserId",
                table: "Rentals");

            migrationBuilder.DropIndex(
                name: "IX_Rentals_AppUserId",
                table: "Rentals");

            migrationBuilder.DropColumn(
                name: "AppUserId",
                table: "Rentals");
        }
    }
}
