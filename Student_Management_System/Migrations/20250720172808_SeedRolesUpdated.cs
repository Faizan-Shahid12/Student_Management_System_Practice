using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Student_Management_System.Migrations
{
    /// <inheritdoc />
    public partial class SeedRolesUpdated : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "100d68bc-2dc6-4930-8d02-cc544e50ed72");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "5c4af957-b61b-412b-babe-bc35c3e0649f");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "a01f3169-1ee9-41d8-93ec-1815698d6e1e");

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "A1", "1", "Admin", "ADMIN" },
                    { "A2", "2", "Student", "STUDENT" },
                    { "A3", "3", "Teacher", "TEACHER" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "A1");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "A2");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "A3");

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "100d68bc-2dc6-4930-8d02-cc544e50ed72", "3", "Teacher", "Teacher" },
                    { "5c4af957-b61b-412b-babe-bc35c3e0649f", "1", "Admin", "Admin" },
                    { "a01f3169-1ee9-41d8-93ec-1815698d6e1e", "2", "Student", "Student" }
                });
        }
    }
}
