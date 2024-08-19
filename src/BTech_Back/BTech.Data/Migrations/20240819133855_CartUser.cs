using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace BlitzTech.Data.Migrations
{
    /// <inheritdoc />
    public partial class CartUser : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "349afa2c-1450-4a96-86b7-a9f5cdb89517");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "5be5fcf8-02d1-4161-97e9-3200c8cd906a");

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "507f22ca-bedd-4d2f-9361-fb462f00211a", null, "Admin", "ADMIN" },
                    { "58bd1b28-bb7e-4fe4-8ebf-5c4a4678c196", null, "User", "USER" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "507f22ca-bedd-4d2f-9361-fb462f00211a");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "58bd1b28-bb7e-4fe4-8ebf-5c4a4678c196");

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "349afa2c-1450-4a96-86b7-a9f5cdb89517", null, "User", "USER" },
                    { "5be5fcf8-02d1-4161-97e9-3200c8cd906a", null, "Admin", "ADMIN" }
                });
        }
    }
}
