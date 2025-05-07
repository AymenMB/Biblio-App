using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BibliothequeApp.Migrations
{
    /// <inheritdoc />
    public partial class AddCopiesAvailableToBook : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "CopiesAvailable",
                table: "Books",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CopiesAvailable",
                table: "Books");
        }
    }
}
