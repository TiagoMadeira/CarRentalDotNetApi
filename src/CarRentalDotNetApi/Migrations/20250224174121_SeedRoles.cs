using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace api.Migrations
{
    /// <inheritdoc />
    public partial class SeedRoles : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "5e438c8e-df84-4f39-80af-82306f2764f9", "6ac343b0-00ef-4a1c-8f64-68daaca77b5b", "Admin", "ADMIN" },
                    { "a51a1291-d383-43f3-9522-cb9324885d11", "1450449e-a75a-446c-9d00-866ed5f529f7", "User", "USER" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "5e438c8e-df84-4f39-80af-82306f2764f9");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "a51a1291-d383-43f3-9522-cb9324885d11");
        }
    }
}
