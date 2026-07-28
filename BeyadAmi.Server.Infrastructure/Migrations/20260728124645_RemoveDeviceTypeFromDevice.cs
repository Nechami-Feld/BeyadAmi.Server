using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BeyadAmi.Server.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class RemoveDeviceTypeFromDevice : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Devices_DeviceTypes_DeviceTypeId",
                table: "Devices");

            migrationBuilder.AlterColumn<int>(
                name: "DeviceTypeId",
                table: "Devices",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddColumn<int>(
                name: "CategoryId",
                table: "Devices",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Devices_CategoryId",
                table: "Devices",
                column: "CategoryId");

            migrationBuilder.AddForeignKey(
                name: "FK_Devices_DeviceCategories_CategoryId",
                table: "Devices",
                column: "CategoryId",
                principalTable: "DeviceCategories",
                principalColumn: "CategoryId",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Devices_DeviceTypes_DeviceTypeId",
                table: "Devices",
                column: "DeviceTypeId",
                principalTable: "DeviceTypes",
                principalColumn: "DeviceTypeId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Devices_DeviceCategories_CategoryId",
                table: "Devices");

            migrationBuilder.DropForeignKey(
                name: "FK_Devices_DeviceTypes_DeviceTypeId",
                table: "Devices");

            migrationBuilder.DropIndex(
                name: "IX_Devices_CategoryId",
                table: "Devices");

            migrationBuilder.DropColumn(
                name: "CategoryId",
                table: "Devices");

            migrationBuilder.AlterColumn<int>(
                name: "DeviceTypeId",
                table: "Devices",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Devices_DeviceTypes_DeviceTypeId",
                table: "Devices",
                column: "DeviceTypeId",
                principalTable: "DeviceTypes",
                principalColumn: "DeviceTypeId",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
