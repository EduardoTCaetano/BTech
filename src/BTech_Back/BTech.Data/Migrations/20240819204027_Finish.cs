using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace BlitzTech.Data.Migrations
{
    /// <inheritdoc />
    public partial class Finish : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
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
                    { "6f050d74-e173-46e0-b0dc-67fcf37c4db8", null, "User", "USER" },
                    { "a2400655-adc0-4515-ae1c-bddcb6555c06", null, "Admin", "ADMIN" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "6f050d74-e173-46e0-b0dc-67fcf37c4db8");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "a2400655-adc0-4515-ae1c-bddcb6555c06");

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "507f22ca-bedd-4d2f-9361-fb462f00211a", null, "Admin", "ADMIN" },
                    { "58bd1b28-bb7e-4fe4-8ebf-5c4a4678c196", null, "User", "USER" }
                });
        }
    }
}
