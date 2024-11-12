using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace BTech.Data.Migrations
{
    /// <inheritdoc />
    public partial class AddProdAndImage : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
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
                name: "ProductName",
                table: "OrderItems",
                newName: "NameProd");

            migrationBuilder.AddColumn<string>(
                name: "Image",
                table: "OrderItems",
                type: "TEXT",
                nullable: true);

            migrationBuilder.Sql(@"INSERT OR IGNORE INTO AspNetRoles (Id, ConcurrencyStamp, Name, NormalizedName)
                                   VALUES ('0c6f7c27-f066-4606-9e9c-f06bf5c2e193', NULL, 'Master', 'Master');");

            migrationBuilder.Sql(@"INSERT OR IGNORE INTO AspNetRoles (Id, ConcurrencyStamp, Name, NormalizedName)
                                   VALUES ('cea64c4f-ab10-4d1b-a945-04dc572e2c5c', NULL, 'Order', 'ORDER');");

            migrationBuilder.Sql(@"INSERT OR IGNORE INTO AspNetRoles (Id, ConcurrencyStamp, Name, NormalizedName)
                                   VALUES ('cfcf83b2-7b2d-4d04-9e66-e35921fa67c1', NULL, 'User', 'USER');");

            migrationBuilder.Sql(@"INSERT OR IGNORE INTO AspNetRoles (Id, ConcurrencyStamp, Name, NormalizedName)
                                   VALUES ('e28742ba-d25c-45da-ac38-fb83db047b8d', NULL, 'Admin', 'ADMIN');");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "0c6f7c27-f066-4606-9e9c-f06bf5c2e193");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "cea64c4f-ab10-4d1b-a945-04dc572e2c5c");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "cfcf83b2-7b2d-4d04-9e66-e35921fa67c1");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "e28742ba-d25c-45da-ac38-fb83db047b8d");

            migrationBuilder.DropColumn(
                name: "Image",
                table: "OrderItems");

            migrationBuilder.RenameColumn(
                name: "NameProd",
                table: "OrderItems",
                newName: "ProductName");

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
    }
}
