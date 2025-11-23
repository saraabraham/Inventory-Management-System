using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace InventoryAPI.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Suppliers",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Name = table.Column<string>(type: "TEXT", maxLength: 200, nullable: false),
                    ContactPerson = table.Column<string>(type: "TEXT", nullable: false),
                    Email = table.Column<string>(type: "TEXT", maxLength: 100, nullable: false),
                    Phone = table.Column<string>(type: "TEXT", nullable: false),
                    Address = table.Column<string>(type: "TEXT", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "TEXT", nullable: false),
                    IsActive = table.Column<bool>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Suppliers", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Products",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    SKU = table.Column<string>(type: "TEXT", maxLength: 50, nullable: false),
                    Name = table.Column<string>(type: "TEXT", maxLength: 200, nullable: false),
                    Description = table.Column<string>(type: "TEXT", nullable: false),
                    Category = table.Column<string>(type: "TEXT", nullable: false),
                    Price = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    StockQuantity = table.Column<int>(type: "INTEGER", nullable: false),
                    MinimumStock = table.Column<int>(type: "INTEGER", nullable: false),
                    SupplierId = table.Column<int>(type: "INTEGER", nullable: false),
                    Location = table.Column<string>(type: "TEXT", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "TEXT", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "TEXT", nullable: false),
                    IsActive = table.Column<bool>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Products", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Products_Suppliers_SupplierId",
                        column: x => x.SupplierId,
                        principalTable: "Suppliers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "StockTransactions",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    ProductId = table.Column<int>(type: "INTEGER", nullable: false),
                    Type = table.Column<int>(type: "INTEGER", nullable: false),
                    Quantity = table.Column<int>(type: "INTEGER", nullable: false),
                    Reference = table.Column<string>(type: "TEXT", nullable: true),
                    Notes = table.Column<string>(type: "TEXT", nullable: true),
                    TransactionDate = table.Column<DateTime>(type: "TEXT", nullable: false),
                    PerformedBy = table.Column<string>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_StockTransactions", x => x.Id);
                    table.ForeignKey(
                        name: "FK_StockTransactions_Products_ProductId",
                        column: x => x.ProductId,
                        principalTable: "Products",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "Suppliers",
                columns: new[] { "Id", "Address", "ContactPerson", "CreatedAt", "Email", "IsActive", "Name", "Phone" },
                values: new object[,]
                {
                    { 1, "Stockholm, Sweden", "Erik Larsson", new DateTime(2025, 11, 23, 11, 34, 0, 198, DateTimeKind.Utc).AddTicks(3010), "erik@scandi.com", true, "Scandinavian Furniture Co.", "+46-123-456" },
                    { 2, "Gdansk, Poland", "Anna Kowalski", new DateTime(2025, 11, 23, 11, 34, 0, 198, DateTimeKind.Utc).AddTicks(3020), "anna@baltic.com", true, "Baltic Wood Supplies", "+48-987-654" }
                });

            migrationBuilder.InsertData(
                table: "Products",
                columns: new[] { "Id", "Category", "CreatedAt", "Description", "IsActive", "Location", "MinimumStock", "Name", "Price", "SKU", "StockQuantity", "SupplierId", "UpdatedAt" },
                values: new object[,]
                {
                    { 1, "Storage", new DateTime(2025, 11, 23, 11, 34, 0, 198, DateTimeKind.Utc).AddTicks(3070), "Adjustable shelves", true, "Warehouse A-1", 20, "BILLY Bookcase", 79.99m, "BILLY-001", 150, 1, new DateTime(2025, 11, 23, 11, 34, 0, 198, DateTimeKind.Utc).AddTicks(3080) },
                    { 2, "Furniture", new DateTime(2025, 11, 23, 11, 34, 0, 198, DateTimeKind.Utc).AddTicks(3080), "3-seat sofa with removable cover", true, "Warehouse B-3", 5, "EKTORP Sofa", 599.99m, "EKTORP-001", 25, 1, new DateTime(2025, 11, 23, 11, 34, 0, 198, DateTimeKind.Utc).AddTicks(3080) },
                    { 3, "Tables", new DateTime(2025, 11, 23, 11, 34, 0, 198, DateTimeKind.Utc).AddTicks(3080), "Easy to assemble", true, "Warehouse A-2", 30, "LACK Coffee Table", 49.99m, "LACK-001", 200, 2, new DateTime(2025, 11, 23, 11, 34, 0, 198, DateTimeKind.Utc).AddTicks(3080) },
                    { 4, "Bedroom", new DateTime(2025, 11, 23, 11, 34, 0, 198, DateTimeKind.Utc).AddTicks(3090), "Queen size with storage", true, "Warehouse C-1", 10, "MALM Bed Frame", 299.99m, "MALM-001", 15, 2, new DateTime(2025, 11, 23, 11, 34, 0, 198, DateTimeKind.Utc).AddTicks(3090) }
                });

            migrationBuilder.CreateIndex(
                name: "IX_Products_SKU",
                table: "Products",
                column: "SKU",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Products_SupplierId",
                table: "Products",
                column: "SupplierId");

            migrationBuilder.CreateIndex(
                name: "IX_StockTransactions_ProductId",
                table: "StockTransactions",
                column: "ProductId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "StockTransactions");

            migrationBuilder.DropTable(
                name: "Products");

            migrationBuilder.DropTable(
                name: "Suppliers");
        }
    }
}
