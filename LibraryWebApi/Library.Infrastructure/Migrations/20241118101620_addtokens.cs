using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Library.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class addtokens : Migration
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
                    { "884a35f5-19cf-411f-85c5-e1c680a7f1b6", null, "Admin", "ADMIN" },
                    { "c57bf5d5-87e1-49ee-a897-2fb2032a4801", null, "User", "USER" }
                });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "b74ddd14-6340-4840-95c2-db12554843e5",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "RefreshToken", "SecurityStamp", "TokenExpires" },
                values: new object[] { "066cccb5-0120-4fc0-807a-4079ea1125c5", "AQAAAAIAAYagAAAAEEhIAlO7272FqV8R5JaUVB03WbeGPqPgeATF2vE2MXbxDXhzlKCg6bcvMpf+EVFBBQ==", "", "9c206400-f42f-401a-873c-85fbd81f2a83", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "884a35f5-19cf-411f-85c5-e1c680a7f1b6");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "c57bf5d5-87e1-49ee-a897-2fb2032a4801");

            migrationBuilder.DropColumn(
                name: "RefreshToken",
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
