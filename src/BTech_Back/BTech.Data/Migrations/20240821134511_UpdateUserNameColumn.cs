using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace BlitzTech.Data.Migrations
{
    /// <inheritdoc />
    public partial class UpdateUserNameColumn : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
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
                    { "a574ca7f-f91b-4359-89ba-87b38f7d3dfb", null, "User", "USER" },
                    { "fe848c55-6417-43e9-bd45-d93d0003ec50", null, "Admin", "ADMIN" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "a574ca7f-f91b-4359-89ba-87b38f7d3dfb");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "fe848c55-6417-43e9-bd45-d93d0003ec50");

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "6f050d74-e173-46e0-b0dc-67fcf37c4db8", null, "User", "USER" },
                    { "a2400655-adc0-4515-ae1c-bddcb6555c06", null, "Admin", "ADMIN" }
                });
        }
    }
}
