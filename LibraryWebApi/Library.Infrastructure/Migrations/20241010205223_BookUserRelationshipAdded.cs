using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Library.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class BookUserRelationshipAdded : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "1bb00cc7-4263-4174-b2c4-0eec720dbb4b");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "dc6d46a5-4aa3-41de-9040-ee3012ed22af");

            migrationBuilder.AlterColumn<DateTime>(
                name: "TakeDateTime",
                table: "Book",
                type: "datetime2",
                nullable: true,
                oldClrType: typeof(DateTime),
                oldType: "datetime2");

            migrationBuilder.AddColumn<string>(
                name: "UserId",
                table: "Book",
                type: "nvarchar(450)",
                nullable: true);

            migrationBuilder.AlterColumn<DateOnly>(
                name: "DateOfBirth",
                table: "Author",
                type: "date",
                nullable: true,
                oldClrType: typeof(DateOnly),
                oldType: "date");

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "9897b089-a4eb-45c5-8b34-e12758a52981", null, "Admin", "ADMIN" },
                    { "d4a4b9a2-2bab-47fc-bad0-b942ac8f282d", null, "User", "USER" }
                });

            migrationBuilder.CreateIndex(
                name: "IX_Book_UserId",
                table: "Book",
                column: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_Book_AspNetUsers_UserId",
                table: "Book",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Book_AspNetUsers_UserId",
                table: "Book");

            migrationBuilder.DropIndex(
                name: "IX_Book_UserId",
                table: "Book");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "9897b089-a4eb-45c5-8b34-e12758a52981");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "d4a4b9a2-2bab-47fc-bad0-b942ac8f282d");

            migrationBuilder.DropColumn(
                name: "UserId",
                table: "Book");

            migrationBuilder.AlterColumn<DateTime>(
                name: "TakeDateTime",
                table: "Book",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldNullable: true);

            migrationBuilder.AlterColumn<DateOnly>(
                name: "DateOfBirth",
                table: "Author",
                type: "date",
                nullable: false,
                defaultValue: new DateOnly(1, 1, 1),
                oldClrType: typeof(DateOnly),
                oldType: "date",
                oldNullable: true);

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "1bb00cc7-4263-4174-b2c4-0eec720dbb4b", null, "Admin", "ADMIN" },
                    { "dc6d46a5-4aa3-41de-9040-ee3012ed22af", null, "User", "USER" }
                });
        }
    }
}
