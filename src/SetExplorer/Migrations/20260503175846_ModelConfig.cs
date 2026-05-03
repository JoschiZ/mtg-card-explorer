using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SetExplorer.Migrations
{
    /// <inheritdoc />
    public partial class ModelConfig : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CardCollection_AspNetUsers_ApplicationUserId",
                table: "CardCollection");

            migrationBuilder.DropForeignKey(
                name: "FK_Exploration_AspNetUsers_ApplicationUserId",
                table: "Exploration");

            migrationBuilder.DropIndex(
                name: "IX_Exploration_ApplicationUserId",
                table: "Exploration");

            migrationBuilder.DropIndex(
                name: "IX_CardCollection_ApplicationUserId",
                table: "CardCollection");

            migrationBuilder.DropColumn(
                name: "ApplicationUserId",
                table: "Exploration");

            migrationBuilder.DropColumn(
                name: "ApplicationUserId",
                table: "CardCollection");

            migrationBuilder.CreateIndex(
                name: "IX_Exploration_UserId",
                table: "Exploration",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_CardCollection_UserId",
                table: "CardCollection",
                column: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_CardCollection_AspNetUsers_UserId",
                table: "CardCollection",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Exploration_AspNetUsers_UserId",
                table: "Exploration",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CardCollection_AspNetUsers_UserId",
                table: "CardCollection");

            migrationBuilder.DropForeignKey(
                name: "FK_Exploration_AspNetUsers_UserId",
                table: "Exploration");

            migrationBuilder.DropIndex(
                name: "IX_Exploration_UserId",
                table: "Exploration");

            migrationBuilder.DropIndex(
                name: "IX_CardCollection_UserId",
                table: "CardCollection");

            migrationBuilder.AddColumn<Guid>(
                name: "ApplicationUserId",
                table: "Exploration",
                type: "TEXT",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "ApplicationUserId",
                table: "CardCollection",
                type: "TEXT",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Exploration_ApplicationUserId",
                table: "Exploration",
                column: "ApplicationUserId");

            migrationBuilder.CreateIndex(
                name: "IX_CardCollection_ApplicationUserId",
                table: "CardCollection",
                column: "ApplicationUserId");

            migrationBuilder.AddForeignKey(
                name: "FK_CardCollection_AspNetUsers_ApplicationUserId",
                table: "CardCollection",
                column: "ApplicationUserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Exploration_AspNetUsers_ApplicationUserId",
                table: "Exploration",
                column: "ApplicationUserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id");
        }
    }
}
