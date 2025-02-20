using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace api.Migrations
{
    /// <inheritdoc />
    public partial class VehicleRentalOneToMany : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Rentals_Vehicles_VehicleId",
                table: "Rentals");

            migrationBuilder.DropIndex(
                name: "IX_Rentals_VehicleId",
                table: "Rentals");

            migrationBuilder.AlterColumn<int>(
                name: "VehicleId",
                table: "Rentals",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AddColumn<int>(
                name: "RentalId",
                table: "Rentals",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Rentals_RentalId",
                table: "Rentals",
                column: "RentalId");

            migrationBuilder.AddForeignKey(
                name: "FK_Rentals_Vehicles_RentalId",
                table: "Rentals",
                column: "RentalId",
                principalTable: "Vehicles",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Rentals_Vehicles_RentalId",
                table: "Rentals");

            migrationBuilder.DropIndex(
                name: "IX_Rentals_RentalId",
                table: "Rentals");

            migrationBuilder.DropColumn(
                name: "RentalId",
                table: "Rentals");

            migrationBuilder.AlterColumn<int>(
                name: "VehicleId",
                table: "Rentals",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.CreateIndex(
                name: "IX_Rentals_VehicleId",
                table: "Rentals",
                column: "VehicleId");

            migrationBuilder.AddForeignKey(
                name: "FK_Rentals_Vehicles_VehicleId",
                table: "Rentals",
                column: "VehicleId",
                principalTable: "Vehicles",
                principalColumn: "Id");
        }
    }
}
