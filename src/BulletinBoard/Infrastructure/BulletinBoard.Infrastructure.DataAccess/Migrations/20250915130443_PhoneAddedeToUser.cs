using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BulletinBoard.Infrastructure.DataAccess.Migrations
{
    /// <inheritdoc />
    public partial class PhoneAddedeToUser : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "BulletinCharacteristicСomparison",
                schema: "public");

            migrationBuilder.AddColumn<string>(
                name: "Phone",
                schema: "public",
                table: "BulletinUser",
                type: "varchar(20)",
                maxLength: 20,
                nullable: true);

            migrationBuilder.CreateTable(
                name: "BulletinCharacteristicComparison",
                schema: "public",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    BulletinId = table.Column<Guid>(type: "uuid", nullable: false),
                    CharacteristicId = table.Column<Guid>(type: "uuid", nullable: false),
                    CharacteristicValueId = table.Column<Guid>(type: "uuid", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BulletinCharacteristicComparison", x => x.Id);
                    table.ForeignKey(
                        name: "FK_BulletinCharacteristicComparison_BulletinCharacteristicValu~",
                        column: x => x.CharacteristicValueId,
                        principalSchema: "public",
                        principalTable: "BulletinCharacteristicValue",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_BulletinCharacteristicComparison_BulletinCharacteristic_Cha~",
                        column: x => x.CharacteristicId,
                        principalSchema: "public",
                        principalTable: "BulletinCharacteristic",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_BulletinCharacteristicComparison_BulletinMain_BulletinId",
                        column: x => x.BulletinId,
                        principalSchema: "public",
                        principalTable: "BulletinMain",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_BulletinCharacteristicComparison_BulletinId",
                schema: "public",
                table: "BulletinCharacteristicComparison",
                column: "BulletinId");

            migrationBuilder.CreateIndex(
                name: "IX_BulletinCharacteristicComparison_BulletinId_CharacteristicId",
                schema: "public",
                table: "BulletinCharacteristicComparison",
                columns: new[] { "BulletinId", "CharacteristicId" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_BulletinCharacteristicComparison_CharacteristicId",
                schema: "public",
                table: "BulletinCharacteristicComparison",
                column: "CharacteristicId");

            migrationBuilder.CreateIndex(
                name: "IX_BulletinCharacteristicComparison_CharacteristicValueId",
                schema: "public",
                table: "BulletinCharacteristicComparison",
                column: "CharacteristicValueId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "BulletinCharacteristicComparison",
                schema: "public");

            migrationBuilder.DropColumn(
                name: "Phone",
                schema: "public",
                table: "BulletinUser");

            migrationBuilder.CreateTable(
                name: "BulletinCharacteristicСomparison",
                schema: "public",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    BulletinId = table.Column<Guid>(type: "uuid", nullable: false),
                    CharacteristicId = table.Column<Guid>(type: "uuid", nullable: false),
                    CharacteristicValueId = table.Column<Guid>(type: "uuid", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BulletinCharacteristicСomparison", x => x.Id);
                    table.ForeignKey(
                        name: "FK_BulletinCharacteristicСomparison_BulletinCharacteristicValu~",
                        column: x => x.CharacteristicValueId,
                        principalSchema: "public",
                        principalTable: "BulletinCharacteristicValue",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_BulletinCharacteristicСomparison_BulletinCharacteristic_Cha~",
                        column: x => x.CharacteristicId,
                        principalSchema: "public",
                        principalTable: "BulletinCharacteristic",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_BulletinCharacteristicСomparison_BulletinMain_BulletinId",
                        column: x => x.BulletinId,
                        principalSchema: "public",
                        principalTable: "BulletinMain",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_BulletinCharacteristicСomparison_BulletinId",
                schema: "public",
                table: "BulletinCharacteristicСomparison",
                column: "BulletinId");

            migrationBuilder.CreateIndex(
                name: "IX_BulletinCharacteristicСomparison_BulletinId_CharacteristicId",
                schema: "public",
                table: "BulletinCharacteristicСomparison",
                columns: new[] { "BulletinId", "CharacteristicId" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_BulletinCharacteristicСomparison_CharacteristicId",
                schema: "public",
                table: "BulletinCharacteristicСomparison",
                column: "CharacteristicId");

            migrationBuilder.CreateIndex(
                name: "IX_BulletinCharacteristicСomparison_CharacteristicValueId",
                schema: "public",
                table: "BulletinCharacteristicСomparison",
                column: "CharacteristicValueId");
        }
    }
}
