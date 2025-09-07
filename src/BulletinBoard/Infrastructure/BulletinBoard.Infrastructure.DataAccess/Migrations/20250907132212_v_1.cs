using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BulletinBoard.Infrastructure.DataAccess.Migrations
{
    /// <inheritdoc />
    public partial class v_1 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_BulletinCategory_BulletinCategory_ParentCategoryId",
                schema: "public",
                table: "BulletinCategory");

            migrationBuilder.AddForeignKey(
                name: "FK_BulletinCategory_BulletinCategory_ParentCategoryId",
                schema: "public",
                table: "BulletinCategory",
                column: "ParentCategoryId",
                principalSchema: "public",
                principalTable: "BulletinCategory",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_BulletinCategory_BulletinCategory_ParentCategoryId",
                schema: "public",
                table: "BulletinCategory");

            migrationBuilder.AddForeignKey(
                name: "FK_BulletinCategory_BulletinCategory_ParentCategoryId",
                schema: "public",
                table: "BulletinCategory",
                column: "ParentCategoryId",
                principalSchema: "public",
                principalTable: "BulletinCategory",
                principalColumn: "Id");
        }
    }
}
