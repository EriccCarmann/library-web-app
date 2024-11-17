using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Library.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class addedtokens : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "01b46134-712e-4943-b910-25f886088938");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "d4bb38f0-6f82-4ca2-ab82-9e76cec19a73");

            migrationBuilder.AddColumn<string>(
                name: "RefreshToken",
                table: "AspNetUsers",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<DateTime>(
                name: "TokenCreated",
                table: "AspNetUsers",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<DateTime>(
                name: "TokenExpires",
                table: "AspNetUsers",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "5c3cd7a0-8634-49a1-a0b6-e983a1beb262", null, "Admin", "ADMIN" },
                    { "9ab7061b-4189-47d4-afea-ee7645e77a0c", null, "User", "USER" }
                });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "b74ddd14-6340-4840-95c2-db12554843e5",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "RefreshToken", "SecurityStamp", "TokenCreated", "TokenExpires" },
                values: new object[] { "e9a61552-c201-4065-82dc-e0fada7381bc", "AQAAAAIAAYagAAAAEDUvY8E8vSbLb+HcTKAwsNV+EOlOlLr26G96nWtkrm1kM3W4MbcBd19X0jpE/HSvDA==", "", "c1564100-e1d1-4920-945a-a7a8d4745c2e", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "5c3cd7a0-8634-49a1-a0b6-e983a1beb262");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "9ab7061b-4189-47d4-afea-ee7645e77a0c");

            migrationBuilder.DropColumn(
                name: "RefreshToken",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "TokenCreated",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "TokenExpires",
                table: "AspNetUsers");

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "01b46134-712e-4943-b910-25f886088938", null, "Admin", "ADMIN" },
                    { "d4bb38f0-6f82-4ca2-ab82-9e76cec19a73", null, "User", "USER" }
                });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "b74ddd14-6340-4840-95c2-db12554843e5",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "b5c5f502-08fd-4669-a0f0-02ba1add07cd", "AQAAAAIAAYagAAAAEKSdTFjrEm3rMkpi+mejqpsR1DrdQIoK0+DEd792Hx2mBtEqs75Rzmhh1LS0POwHMQ==", "9331fffd-ef67-4cc2-b0b3-76c70309b451" });
        }
    }
}
