using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace BeyadAmi.Server.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class AddDeviceTemplate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Devices_DeviceTypes_DeviceTypeId",
                table: "Devices");

            migrationBuilder.DropForeignKey(
                name: "FK_DeviceTemplates_DeviceTypes_DeviceTypeId",
                table: "DeviceTemplates");

            migrationBuilder.DropTable(
                name: "DeviceTypes");

            migrationBuilder.DropIndex(
                name: "IX_Devices_DeviceTypeId",
                table: "Devices");

            migrationBuilder.DropColumn(
                name: "FilePath",
                table: "DeviceTemplates");

            migrationBuilder.DropColumn(
                name: "DeviceTypeId",
                table: "Devices");

            migrationBuilder.RenameColumn(
                name: "DeviceTypeId",
                table: "DeviceTemplates",
                newName: "DeviceCategoryId");

            migrationBuilder.RenameColumn(
                name: "TemplateId",
                table: "DeviceTemplates",
                newName: "DeviceTemplateId");

            migrationBuilder.RenameIndex(
                name: "IX_DeviceTemplates_DeviceTypeId",
                table: "DeviceTemplates",
                newName: "IX_DeviceTemplates_DeviceCategoryId");

            migrationBuilder.AddColumn<string>(
                name: "TemplateText",
                table: "DeviceTemplates",
                type: "character varying(2000)",
                maxLength: 2000,
                nullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_DeviceTemplates_DeviceCategories_DeviceCategoryId",
                table: "DeviceTemplates",
                column: "DeviceCategoryId",
                principalTable: "DeviceCategories",
                principalColumn: "CategoryId",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_DeviceTemplates_DeviceCategories_DeviceCategoryId",
                table: "DeviceTemplates");

            migrationBuilder.DropColumn(
                name: "TemplateText",
                table: "DeviceTemplates");

            migrationBuilder.RenameColumn(
                name: "DeviceCategoryId",
                table: "DeviceTemplates",
                newName: "DeviceTypeId");

            migrationBuilder.RenameColumn(
                name: "DeviceTemplateId",
                table: "DeviceTemplates",
                newName: "TemplateId");

            migrationBuilder.RenameIndex(
                name: "IX_DeviceTemplates_DeviceCategoryId",
                table: "DeviceTemplates",
                newName: "IX_DeviceTemplates_DeviceTypeId");

            migrationBuilder.AddColumn<string>(
                name: "FilePath",
                table: "DeviceTemplates",
                type: "character varying(500)",
                maxLength: 500,
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "DeviceTypeId",
                table: "Devices",
                type: "integer",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "DeviceTypes",
                columns: table => new
                {
                    DeviceTypeId = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    CategoryId = table.Column<int>(type: "integer", nullable: false),
                    BasicInfo = table.Column<string>(type: "text", nullable: true),
                    Company = table.Column<string>(type: "text", nullable: true),
                    DeviceName = table.Column<string>(type: "text", nullable: true),
                    Model = table.Column<string>(type: "text", nullable: true),
                    Rules = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DeviceTypes", x => x.DeviceTypeId);
                    table.ForeignKey(
                        name: "FK_DeviceTypes_DeviceCategories_CategoryId",
                        column: x => x.CategoryId,
                        principalTable: "DeviceCategories",
                        principalColumn: "CategoryId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Devices_DeviceTypeId",
                table: "Devices",
                column: "DeviceTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_DeviceTypes_CategoryId",
                table: "DeviceTypes",
                column: "CategoryId");

            migrationBuilder.AddForeignKey(
                name: "FK_Devices_DeviceTypes_DeviceTypeId",
                table: "Devices",
                column: "DeviceTypeId",
                principalTable: "DeviceTypes",
                principalColumn: "DeviceTypeId");

            migrationBuilder.AddForeignKey(
                name: "FK_DeviceTemplates_DeviceTypes_DeviceTypeId",
                table: "DeviceTemplates",
                column: "DeviceTypeId",
                principalTable: "DeviceTypes",
                principalColumn: "DeviceTypeId",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
