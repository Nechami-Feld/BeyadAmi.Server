using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace BeyadAmi.Server.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class AddCompanyToDevice : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Loans_Devices_DeviceId",
                table: "Loans");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Devices",
                table: "Devices");

            migrationBuilder.DropColumn(
                name: "Company",
                table: "Devices");

            migrationBuilder.AlterColumn<int>(
                name: "DeviceId",
                table: "Devices",
                type: "integer",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "integer")
                .OldAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);

            migrationBuilder.AddColumn<int>(
                name: "CompanyId",
                table: "Devices",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddPrimaryKey(
                name: "PK_Devices",
                table: "Devices",
                column: "CompanyId");

            migrationBuilder.AddForeignKey(
                name: "FK_Devices_Company_CompanyId",
                table: "Devices",
                column: "CompanyId",
                principalTable: "Company",
                principalColumn: "CompanyId",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Loans_Devices_DeviceId",
                table: "Loans",
                column: "DeviceId",
                principalTable: "Devices",
                principalColumn: "CompanyId",
                onDelete: ReferentialAction.Restrict);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Devices_Company_CompanyId",
                table: "Devices");

            migrationBuilder.DropForeignKey(
                name: "FK_Loans_Devices_DeviceId",
                table: "Loans");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Devices",
                table: "Devices");

            migrationBuilder.DropColumn(
                name: "CompanyId",
                table: "Devices");

            migrationBuilder.AlterColumn<int>(
                name: "DeviceId",
                table: "Devices",
                type: "integer",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "integer")
                .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);

            migrationBuilder.AddColumn<string>(
                name: "Company",
                table: "Devices",
                type: "character varying(100)",
                maxLength: 100,
                nullable: true);

            migrationBuilder.AddPrimaryKey(
                name: "PK_Devices",
                table: "Devices",
                column: "DeviceId");

            migrationBuilder.AddForeignKey(
                name: "FK_Loans_Devices_DeviceId",
                table: "Loans",
                column: "DeviceId",
                principalTable: "Devices",
                principalColumn: "DeviceId",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
