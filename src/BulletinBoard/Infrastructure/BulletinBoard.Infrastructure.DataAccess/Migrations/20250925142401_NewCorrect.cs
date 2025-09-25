using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BulletinBoard.Infrastructure.DataAccess.Migrations
{
    /// <inheritdoc />
    public partial class NewCorrect : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Name",
                schema: "public",
                table: "BulletinImage");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Name",
                schema: "public",
                table: "BulletinImage",
                type: "varchar(255)",
                maxLength: 255,
                nullable: false,
                defaultValue: "");
        }
    }
}
