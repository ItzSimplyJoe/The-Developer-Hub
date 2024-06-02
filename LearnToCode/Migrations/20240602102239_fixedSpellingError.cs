using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DeveloperHub.Migrations
{
    /// <inheritdoc />
    public partial class fixedSpellingError : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Orangisation",
                table: "Users",
                newName: "Organisation");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Organisation",
                table: "Users",
                newName: "Orangisation");
        }
    }
}
