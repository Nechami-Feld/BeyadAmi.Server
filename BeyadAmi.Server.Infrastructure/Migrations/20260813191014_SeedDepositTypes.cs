using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace BeyadAmi.Server.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class SeedDepositTypes : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "DepositTypes",
                columns: new[] { "DepositTypeId", "DepositTypeName" },
                values: new object[,]
                {
                    { 1, "מזומן" },
                    { 2, "צ'ק" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "DepositTypes",
                keyColumn: "DepositTypeId",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "DepositTypes",
                keyColumn: "DepositTypeId",
                keyValue: 2);
        }
    }
}
