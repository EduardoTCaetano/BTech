using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace BlitzTech.Data.Migrations
{
    /// <inheritdoc />
    public partial class updatecategoryproduct : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "28cb86f1-051f-4fb8-a841-3e7d96ed824e");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "5b8d655b-cdf2-4caa-ba9d-aa6caa74a933");

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "53ce7c74-8d85-4f02-87f4-3f146d80f776", null, "User", "USER" },
                    { "6d21a588-53f4-4a3b-b5da-ab5421ffbaa1", null, "Admin", "ADMIN" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "53ce7c74-8d85-4f02-87f4-3f146d80f776");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "6d21a588-53f4-4a3b-b5da-ab5421ffbaa1");

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "28cb86f1-051f-4fb8-a841-3e7d96ed824e", null, "Admin", "ADMIN" },
                    { "5b8d655b-cdf2-4caa-ba9d-aa6caa74a933", null, "User", "USER" }
                });
        }
    }
}
