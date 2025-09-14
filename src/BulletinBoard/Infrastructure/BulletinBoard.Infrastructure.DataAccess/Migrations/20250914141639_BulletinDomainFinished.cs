using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BulletinBoard.Infrastructure.DataAccess.Migrations
{
    /// <inheritdoc />
    public partial class BulletinDomainFinished : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.EnsureSchema(
                name: "public");

            migrationBuilder.CreateTable(
                name: "BulletinCategory",
                schema: "public",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    ParentCategoryId = table.Column<Guid>(type: "uuid", nullable: true),
                    CategoryName = table.Column<string>(type: "varchar(50)", maxLength: 50, nullable: false),
                    IsLeafy = table.Column<bool>(type: "boolean", nullable: false, defaultValue: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BulletinCategory", x => x.Id);
                    table.ForeignKey(
                        name: "FK_BulletinCategory_BulletinCategory_ParentCategoryId",
                        column: x => x.ParentCategoryId,
                        principalSchema: "public",
                        principalTable: "BulletinCategory",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "BulletinUser",
                schema: "public",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    FullName = table.Column<string>(type: "varchar(128)", maxLength: 128, nullable: false),
                    Blocked = table.Column<bool>(type: "boolean", nullable: false, defaultValue: false),
                    Latitude = table.Column<double>(type: "numeric(9,6)", nullable: false),
                    Longitude = table.Column<double>(type: "numeric(9,6)", nullable: false),
                    FormattedAddress = table.Column<string>(type: "varchar(255)", maxLength: 255, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BulletinUser", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "BulletinCharacteristic",
                schema: "public",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Name = table.Column<string>(type: "varchar(80)", maxLength: 80, nullable: false),
                    CategoryId = table.Column<Guid>(type: "uuid", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BulletinCharacteristic", x => x.Id);
                    table.ForeignKey(
                        name: "FK_BulletinCharacteristic_BulletinCategory_CategoryId",
                        column: x => x.CategoryId,
                        principalSchema: "public",
                        principalTable: "BulletinCategory",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "BulletinMain",
                schema: "public",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    UserId = table.Column<Guid>(type: "uuid", nullable: false),
                    Title = table.Column<string>(type: "varchar(100)", maxLength: 100, nullable: false),
                    Description = table.Column<string>(type: "text", nullable: false),
                    CategoryId = table.Column<Guid>(type: "uuid", nullable: false),
                    Price = table.Column<decimal>(type: "numeric(18,2)", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false, defaultValueSql: "CURRENT_TIMESTAMP"),
                    Hidden = table.Column<bool>(type: "boolean", nullable: false, defaultValue: false),
                    Closed = table.Column<bool>(type: "boolean", nullable: false, defaultValue: false),
                    Blocked = table.Column<bool>(type: "boolean", nullable: false, defaultValue: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BulletinMain", x => x.Id);
                    table.ForeignKey(
                        name: "FK_BulletinMain_BulletinCategory_CategoryId",
                        column: x => x.CategoryId,
                        principalSchema: "public",
                        principalTable: "BulletinCategory",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_BulletinMain_BulletinUser_UserId",
                        column: x => x.UserId,
                        principalSchema: "public",
                        principalTable: "BulletinUser",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "BulletinCharacteristicValue",
                schema: "public",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    CharacteristicId = table.Column<Guid>(type: "uuid", nullable: false),
                    Value = table.Column<string>(type: "varchar(50)", maxLength: 50, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BulletinCharacteristicValue", x => x.Id);
                    table.ForeignKey(
                        name: "FK_BulletinCharacteristicValue_BulletinCharacteristic_Characte~",
                        column: x => x.CharacteristicId,
                        principalSchema: "public",
                        principalTable: "BulletinCharacteristic",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "BulletinImage",
                schema: "public",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    BulletinId = table.Column<Guid>(type: "uuid", nullable: false),
                    IsMain = table.Column<bool>(type: "boolean", nullable: false, defaultValue: false),
                    Name = table.Column<string>(type: "varchar(255)", maxLength: 255, nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false, defaultValueSql: "CURRENT_TIMESTAMP")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BulletinImage", x => x.Id);
                    table.ForeignKey(
                        name: "FK_BulletinImage_BulletinMain_BulletinId",
                        column: x => x.BulletinId,
                        principalSchema: "public",
                        principalTable: "BulletinMain",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "BulletinRating",
                schema: "public",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    BulletinId = table.Column<Guid>(type: "uuid", nullable: false),
                    Rating = table.Column<decimal>(type: "numeric(3,2)", nullable: false),
                    ViewsCount = table.Column<int>(type: "int", nullable: false, defaultValue: 0)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BulletinRating", x => x.Id);
                    table.ForeignKey(
                        name: "FK_BulletinRating_BulletinMain_BulletinId",
                        column: x => x.BulletinId,
                        principalSchema: "public",
                        principalTable: "BulletinMain",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

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
                name: "IX_BulletinCategory_ParentCategoryId",
                schema: "public",
                table: "BulletinCategory",
                column: "ParentCategoryId");

            migrationBuilder.CreateIndex(
                name: "IX_BulletinCategory_ParentId_Name",
                schema: "public",
                table: "BulletinCategory",
                columns: new[] { "ParentCategoryId", "CategoryName" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_BulletinCharacteristic_CategoryId",
                schema: "public",
                table: "BulletinCharacteristic",
                column: "CategoryId");

            migrationBuilder.CreateIndex(
                name: "IX_BulletinCharacteristic_CategoryId_Name",
                schema: "public",
                table: "BulletinCharacteristic",
                columns: new[] { "CategoryId", "Name" },
                unique: true);

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

            migrationBuilder.CreateIndex(
                name: "IX_BulletinCharacteristicValue_CharacteristicId",
                schema: "public",
                table: "BulletinCharacteristicValue",
                column: "CharacteristicId");

            migrationBuilder.CreateIndex(
                name: "IX_BulletinCharacteristicValue_CharacteristicId_Value",
                schema: "public",
                table: "BulletinCharacteristicValue",
                columns: new[] { "CharacteristicId", "Value" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_BulletinImage_BelletinId",
                schema: "public",
                table: "BulletinImage",
                column: "BulletinId");

            migrationBuilder.CreateIndex(
                name: "IX_BulletinMain_CategoryId",
                schema: "public",
                table: "BulletinMain",
                column: "CategoryId");

            migrationBuilder.CreateIndex(
                name: "IX_BulletinMain_CreatedAt",
                schema: "public",
                table: "BulletinMain",
                column: "CreatedAt");

            migrationBuilder.CreateIndex(
                name: "IX_BulletinMain_UserId",
                schema: "public",
                table: "BulletinMain",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_BulletinMain_Visibility",
                schema: "public",
                table: "BulletinMain",
                columns: new[] { "Hidden", "Closed", "Blocked" });

            migrationBuilder.CreateIndex(
                name: "IX_BulletinRating_BulletinId",
                schema: "public",
                table: "BulletinRating",
                column: "BulletinId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_BulletinRating_Rating",
                schema: "public",
                table: "BulletinRating",
                column: "Rating");

            migrationBuilder.CreateIndex(
                name: "IX_BulletinRating_ViewsCount",
                schema: "public",
                table: "BulletinRating",
                column: "ViewsCount");

            migrationBuilder.CreateIndex(
                name: "IX_BulletinUser_Blocked",
                schema: "public",
                table: "BulletinUser",
                column: "Blocked");

            migrationBuilder.CreateIndex(
                name: "IX_BulletinUser_Location",
                schema: "public",
                table: "BulletinUser",
                columns: new[] { "Latitude", "Longitude" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "BulletinCharacteristicСomparison",
                schema: "public");

            migrationBuilder.DropTable(
                name: "BulletinImage",
                schema: "public");

            migrationBuilder.DropTable(
                name: "BulletinRating",
                schema: "public");

            migrationBuilder.DropTable(
                name: "BulletinCharacteristicValue",
                schema: "public");

            migrationBuilder.DropTable(
                name: "BulletinMain",
                schema: "public");

            migrationBuilder.DropTable(
                name: "BulletinCharacteristic",
                schema: "public");

            migrationBuilder.DropTable(
                name: "BulletinUser",
                schema: "public");

            migrationBuilder.DropTable(
                name: "BulletinCategory",
                schema: "public");
        }
    }
}
