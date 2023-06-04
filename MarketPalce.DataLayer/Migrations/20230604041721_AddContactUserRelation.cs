using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MarketPlace.DataLayer.Migrations
{
    public partial class AddContactUserRelation : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateIndex(
                name: "IX_ContactUses_UserId",
                table: "ContactUses",
                column: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_ContactUses_Users_UserId",
                table: "ContactUses",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ContactUses_Users_UserId",
                table: "ContactUses");

            migrationBuilder.DropIndex(
                name: "IX_ContactUses_UserId",
                table: "ContactUses");
        }
    }
}
