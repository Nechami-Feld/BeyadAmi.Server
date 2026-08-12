using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BeyadAmi.Server.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class RemoveDeviceType : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_DeviceTypes_DeviceCategories_CategoryId",
                table: "DeviceTypes");

            migrationBuilder.AddForeignKey(
                name: "FK_DeviceTypes_DeviceCategories_CategoryId",
                table: "DeviceTypes",
                column: "CategoryId",
                principalTable: "DeviceCategories",
                principalColumn: "CategoryId",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_DeviceTypes_DeviceCategories_CategoryId",
                table: "DeviceTypes");

            migrationBuilder.AddForeignKey(
                name: "FK_DeviceTypes_DeviceCategories_CategoryId",
                table: "DeviceTypes",
                column: "CategoryId",
                principalTable: "DeviceCategories",
                principalColumn: "CategoryId",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
