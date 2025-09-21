using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BulletinBoard.Infrastructure.DataAccess.Migrations
{
    /// <inheritdoc />
    public partial class SomeWhereAddedCascadeOnDelete : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_BulletinCharacteristic_BulletinCategory_CategoryId",
                schema: "public",
                table: "BulletinCharacteristic");

            migrationBuilder.DropForeignKey(
                name: "FK_BulletinCharacteristicValue_BulletinCharacteristic_Characte~",
                schema: "public",
                table: "BulletinCharacteristicValue");

            migrationBuilder.AddForeignKey(
                name: "FK_BulletinCharacteristic_BulletinCategory_CategoryId",
                schema: "public",
                table: "BulletinCharacteristic",
                column: "CategoryId",
                principalSchema: "public",
                principalTable: "BulletinCategory",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_BulletinCharacteristicValue_BulletinCharacteristic_Characte~",
                schema: "public",
                table: "BulletinCharacteristicValue",
                column: "CharacteristicId",
                principalSchema: "public",
                principalTable: "BulletinCharacteristic",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_BulletinCharacteristic_BulletinCategory_CategoryId",
                schema: "public",
                table: "BulletinCharacteristic");

            migrationBuilder.DropForeignKey(
                name: "FK_BulletinCharacteristicValue_BulletinCharacteristic_Characte~",
                schema: "public",
                table: "BulletinCharacteristicValue");

            migrationBuilder.AddForeignKey(
                name: "FK_BulletinCharacteristic_BulletinCategory_CategoryId",
                schema: "public",
                table: "BulletinCharacteristic",
                column: "CategoryId",
                principalSchema: "public",
                principalTable: "BulletinCategory",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_BulletinCharacteristicValue_BulletinCharacteristic_Characte~",
                schema: "public",
                table: "BulletinCharacteristicValue",
                column: "CharacteristicId",
                principalSchema: "public",
                principalTable: "BulletinCharacteristic",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
