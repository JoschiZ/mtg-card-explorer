using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CardExplorer.Migrations
{
    /// <inheritdoc />
    public partial class AddCurrentPage : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "CurrentPage",
                table: "Exploration",
                type: "integer",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CurrentPage",
                table: "Exploration");
        }
    }
}
