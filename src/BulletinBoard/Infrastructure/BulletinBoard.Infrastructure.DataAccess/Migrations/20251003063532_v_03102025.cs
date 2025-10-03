using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BulletinBoard.Infrastructure.DataAccess.Migrations
{
    /// <inheritdoc />
    public partial class v_03102025 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_BulletinRating_BulletinId",
                schema: "public",
                table: "BulletinRating");

            migrationBuilder.CreateIndex(
                name: "IX_BulletinRating_BulletinId",
                schema: "public",
                table: "BulletinRating",
                column: "BulletinId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_BulletinRating_BulletinId",
                schema: "public",
                table: "BulletinRating");

            migrationBuilder.CreateIndex(
                name: "IX_BulletinRating_BulletinId",
                schema: "public",
                table: "BulletinRating",
                column: "BulletinId",
                unique: true);
        }
    }
}
