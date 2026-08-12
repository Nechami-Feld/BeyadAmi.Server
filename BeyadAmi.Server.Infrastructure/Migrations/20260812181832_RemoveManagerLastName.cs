using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BeyadAmi.Server.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class RemoveManagerLastName : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ManagerLastName",
                table: "Branches");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "ManagerLastName",
                table: "Branches",
                type: "nvarchar(100)",
                maxLength: 100,
                nullable: true);
        }
    }
}
