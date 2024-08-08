using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace BlitzTech.Data.Migrations
{
    /// <inheritdoc />
    public partial class ProductUpdate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "02136a2c-6e4f-42a2-a0c8-9c9cbf407a00");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "c24917c6-3cee-48c0-aded-8c6a9a15b8e6");

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "28cb86f1-051f-4fb8-a841-3e7d96ed824e", null, "Admin", "ADMIN" },
                    { "5b8d655b-cdf2-4caa-ba9d-aa6caa74a933", null, "User", "USER" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
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
                    { "02136a2c-6e4f-42a2-a0c8-9c9cbf407a00", null, "User", "USER" },
                    { "c24917c6-3cee-48c0-aded-8c6a9a15b8e6", null, "Admin", "ADMIN" }
                });
        }
    }
}
