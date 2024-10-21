using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Library.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class coveradded : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "1984c4c8-702e-4405-9df2-58c693ef6b42");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "ec5b3928-81e1-4a8f-8303-5581523e1f8b");

            migrationBuilder.AddColumn<byte[]>(
                name: "Cover",
                table: "Book",
                type: "varbinary(max)",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "ReturnDateTime",
                table: "Book",
                type: "datetime2",
                nullable: true);

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "31f24957-d884-4ac1-9459-aaf9a06b2c50", null, "User", "USER" },
                    { "9dd6dab6-a5be-404d-a7a5-9f1246fbe1f0", null, "Admin", "ADMIN" }
                });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "b74ddd14-6340-4840-95c2-db12554843e5",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "2a30f341-970f-4d45-a5b2-646ad78d22ed", "AQAAAAIAAYagAAAAENZiSnOskP76dfLUK1oj/dxE0Zet0iaGYXFT43va80yrG4l9urHubFubPpNx44NSiw==", "de201f04-59f7-40ce-85e9-29cd3603a11b" });

            migrationBuilder.UpdateData(
                table: "Book",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "Cover", "ReturnDateTime" },
                values: new object[] { null, null });

            migrationBuilder.UpdateData(
                table: "Book",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "Cover", "ReturnDateTime" },
                values: new object[] { null, null });

            migrationBuilder.UpdateData(
                table: "Book",
                keyColumn: "Id",
                keyValue: 3,
                columns: new[] { "Cover", "ReturnDateTime" },
                values: new object[] { null, null });

            migrationBuilder.UpdateData(
                table: "Book",
                keyColumn: "Id",
                keyValue: 4,
                columns: new[] { "Cover", "ReturnDateTime" },
                values: new object[] { null, null });

            migrationBuilder.UpdateData(
                table: "Book",
                keyColumn: "Id",
                keyValue: 5,
                columns: new[] { "Cover", "ReturnDateTime" },
                values: new object[] { null, null });

            migrationBuilder.UpdateData(
                table: "Book",
                keyColumn: "Id",
                keyValue: 6,
                columns: new[] { "Cover", "ReturnDateTime" },
                values: new object[] { null, null });

            migrationBuilder.UpdateData(
                table: "Book",
                keyColumn: "Id",
                keyValue: 7,
                columns: new[] { "Cover", "ReturnDateTime" },
                values: new object[] { null, null });

            migrationBuilder.UpdateData(
                table: "Book",
                keyColumn: "Id",
                keyValue: 8,
                columns: new[] { "Cover", "ReturnDateTime" },
                values: new object[] { null, null });

            migrationBuilder.UpdateData(
                table: "Book",
                keyColumn: "Id",
                keyValue: 9,
                columns: new[] { "Cover", "ReturnDateTime" },
                values: new object[] { null, null });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "31f24957-d884-4ac1-9459-aaf9a06b2c50");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "9dd6dab6-a5be-404d-a7a5-9f1246fbe1f0");

            migrationBuilder.DropColumn(
                name: "Cover",
                table: "Book");

            migrationBuilder.DropColumn(
                name: "ReturnDateTime",
                table: "Book");

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "1984c4c8-702e-4405-9df2-58c693ef6b42", null, "Admin", "ADMIN" },
                    { "ec5b3928-81e1-4a8f-8303-5581523e1f8b", null, "User", "USER" }
                });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "b74ddd14-6340-4840-95c2-db12554843e5",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "a47a307a-e15b-437c-913a-6a265939c99c", "AQAAAAIAAYagAAAAEAh5nQE2mojGs/Dt4WVT4kla6JIMNIeCvBNG9xmsePQLHoy3bB2Ov8Y6Ll/yEaV3tA==", "3782421c-4ba5-43e6-84ae-22c54adb8702" });
        }
    }
}
