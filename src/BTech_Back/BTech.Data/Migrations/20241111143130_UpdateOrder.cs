using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace BTech.Data.Migrations
{
    /// <inheritdoc />
    public partial class UpdateOrder : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "94366daa-1b8e-455b-a272-7e29663ee87a");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "a6dfd124-c7dc-412b-886e-c8e966fe67ef");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "c24c4bee-c6e2-4867-bfa8-c6aa5b25ef1a");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "fd706c7a-8e0e-45d7-a1e1-eed043271c47");

            migrationBuilder.RenameColumn(
                name: "UnitPrice",
                table: "OrderItems",
                newName: "Price");

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "2f555bcb-aba0-43d8-830f-5a8ba301cd45", null, "Master", "Master" },
                    { "325846ba-fc57-4f3c-9add-77afa5e763bb", null, "Order", "ORDER" },
                    { "421ed962-d89c-4049-b033-55acd1a81b5e", null, "User", "USER" },
                    { "e3fbdb01-e4c5-44e9-a864-c5098e986b55", null, "Admin", "ADMIN" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "2f555bcb-aba0-43d8-830f-5a8ba301cd45");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "325846ba-fc57-4f3c-9add-77afa5e763bb");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "421ed962-d89c-4049-b033-55acd1a81b5e");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "e3fbdb01-e4c5-44e9-a864-c5098e986b55");

            migrationBuilder.RenameColumn(
                name: "Price",
                table: "OrderItems",
                newName: "UnitPrice");

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "94366daa-1b8e-455b-a272-7e29663ee87a", null, "Admin", "ADMIN" },
                    { "a6dfd124-c7dc-412b-886e-c8e966fe67ef", null, "Master", "Master" },
                    { "c24c4bee-c6e2-4867-bfa8-c6aa5b25ef1a", null, "User", "USER" },
                    { "fd706c7a-8e0e-45d7-a1e1-eed043271c47", null, "Order", "ORDER" }
                });
        }
    }
}
