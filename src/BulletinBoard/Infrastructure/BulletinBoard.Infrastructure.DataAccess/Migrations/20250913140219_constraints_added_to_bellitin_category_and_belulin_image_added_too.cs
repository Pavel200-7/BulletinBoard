using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BulletinBoard.Infrastructure.DataAccess.Migrations
{
    /// <inheritdoc />
    public partial class constraints_added_to_bellitin_category_and_belulin_image_added_too : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<bool>(
                name: "IsLeafy",
                schema: "public",
                table: "BulletinCategory",
                type: "bool",
                nullable: false,
                defaultValue: false,
                oldClrType: typeof(bool),
                oldType: "boolean");

            migrationBuilder.AlterColumn<string>(
                name: "CategoryName",
                schema: "public",
                table: "BulletinCategory",
                type: "varchar(50)",
                maxLength: 50,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "text");

            migrationBuilder.CreateTable(
                name: "BulletinImage",
                schema: "public",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    BelletinId = table.Column<Guid>(type: "uuid", nullable: false),
                    IsMain = table.Column<bool>(type: "bool", nullable: false, defaultValue: false),
                    Name = table.Column<string>(type: "varchar(255)", maxLength: 255, nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false, defaultValueSql: "CURRENT_TIMESTAMP"),
                    Path = table.Column<string>(type: "varchar(500)", maxLength: 500, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BulletinImage", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_BulletinImage_BelletinId",
                schema: "public",
                table: "BulletinImage",
                column: "BelletinId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "BulletinImage",
                schema: "public");

            migrationBuilder.AlterColumn<bool>(
                name: "IsLeafy",
                schema: "public",
                table: "BulletinCategory",
                type: "boolean",
                nullable: false,
                oldClrType: typeof(bool),
                oldType: "bool",
                oldDefaultValue: false);

            migrationBuilder.AlterColumn<string>(
                name: "CategoryName",
                schema: "public",
                table: "BulletinCategory",
                type: "text",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "varchar(50)",
                oldMaxLength: 50);
        }
    }
}
