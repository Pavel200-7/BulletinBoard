using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BulletinBoard.Infrastructure.DataAccess.Migrations
{
    /// <inheritdoc />
    public partial class ratingImproved : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_BulletinRating_ViewsCount",
                schema: "public",
                table: "BulletinRating");

            migrationBuilder.DropColumn(
                name: "ViewsCount",
                schema: "public",
                table: "BulletinRating");

            migrationBuilder.AlterColumn<int>(
                name: "Rating",
                schema: "public",
                table: "BulletinRating",
                type: "int",
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "numeric(3,2)");

            migrationBuilder.AddColumn<DateTime>(
                name: "CreatedAt",
                schema: "public",
                table: "BulletinRating",
                type: "timestamp with time zone",
                nullable: false,
                defaultValueSql: "CURRENT_TIMESTAMP");

            migrationBuilder.AddColumn<Guid>(
                name: "UserId",
                schema: "public",
                table: "BulletinRating",
                type: "uuid",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.CreateTable(
                name: "BulletinViewsCount",
                schema: "public",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    BulletinId = table.Column<Guid>(type: "uuid", nullable: false),
                    ViewsCount = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BulletinViewsCount", x => x.Id);
                    table.ForeignKey(
                        name: "FK_BulletinViewsCount_BulletinMain_BulletinId",
                        column: x => x.BulletinId,
                        principalSchema: "public",
                        principalTable: "BulletinMain",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_BulletinRating_UserId",
                schema: "public",
                table: "BulletinRating",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_BulletinViewsCount_BulletinId",
                schema: "public",
                table: "BulletinViewsCount",
                column: "BulletinId",
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "FK_BulletinRating_BulletinUser_UserId",
                schema: "public",
                table: "BulletinRating",
                column: "UserId",
                principalSchema: "public",
                principalTable: "BulletinUser",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_BulletinRating_BulletinUser_UserId",
                schema: "public",
                table: "BulletinRating");

            migrationBuilder.DropTable(
                name: "BulletinViewsCount",
                schema: "public");

            migrationBuilder.DropIndex(
                name: "IX_BulletinRating_UserId",
                schema: "public",
                table: "BulletinRating");

            migrationBuilder.DropColumn(
                name: "CreatedAt",
                schema: "public",
                table: "BulletinRating");

            migrationBuilder.DropColumn(
                name: "UserId",
                schema: "public",
                table: "BulletinRating");

            migrationBuilder.AlterColumn<decimal>(
                name: "Rating",
                schema: "public",
                table: "BulletinRating",
                type: "numeric(3,2)",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddColumn<int>(
                name: "ViewsCount",
                schema: "public",
                table: "BulletinRating",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_BulletinRating_ViewsCount",
                schema: "public",
                table: "BulletinRating",
                column: "ViewsCount");
        }
    }
}
