using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SetExplorer.Migrations
{
    /// <inheritdoc />
    public partial class Changes : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CardCardCollection_Card_CardsId",
                table: "CardCardCollection");

            migrationBuilder.DropForeignKey(
                name: "FK_CardExploration_Card_SeenCardsId",
                table: "CardExploration");

            migrationBuilder.DropTable(
                name: "Card");

            migrationBuilder.CreateTable(
                name: "Cards",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Cards", x => x.Id);
                });

            migrationBuilder.AddForeignKey(
                name: "FK_CardCardCollection_Cards_CardsId",
                table: "CardCardCollection",
                column: "CardsId",
                principalTable: "Cards",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_CardExploration_Cards_SeenCardsId",
                table: "CardExploration",
                column: "SeenCardsId",
                principalTable: "Cards",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CardCardCollection_Cards_CardsId",
                table: "CardCardCollection");

            migrationBuilder.DropForeignKey(
                name: "FK_CardExploration_Cards_SeenCardsId",
                table: "CardExploration");

            migrationBuilder.DropTable(
                name: "Cards");

            migrationBuilder.CreateTable(
                name: "Card",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Card", x => x.Id);
                });

            migrationBuilder.AddForeignKey(
                name: "FK_CardCardCollection_Card_CardsId",
                table: "CardCardCollection",
                column: "CardsId",
                principalTable: "Card",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_CardExploration_Card_SeenCardsId",
                table: "CardExploration",
                column: "SeenCardsId",
                principalTable: "Card",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
