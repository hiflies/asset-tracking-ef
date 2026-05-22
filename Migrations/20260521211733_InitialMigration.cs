using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace AssetTracking.Migrations
{
    /// <inheritdoc />
    public partial class InitialMigration : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Currencies",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Currencies", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Offices",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CurrencyId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Offices", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Offices_Currencies_CurrencyId",
                        column: x => x.CurrencyId,
                        principalTable: "Currencies",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Assets",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Brand = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Model = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Price = table.Column<double>(type: "float", nullable: false),
                    OfficeId = table.Column<int>(type: "int", nullable: false),
                    SerialNumber = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    PurchaseDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    WarrantyExpirationDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Type = table.Column<string>(type: "nvarchar(8)", maxLength: 8, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Assets", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Assets_Offices_OfficeId",
                        column: x => x.OfficeId,
                        principalTable: "Offices",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "Currencies",
                columns: new[] { "Id", "Name" },
                values: new object[,]
                {
                    { 1, "USD" },
                    { 2, "EUR" },
                    { 3, "SEK" },
                    { 4, "CHF" },
                    { 5, "TRY" }
                });

            migrationBuilder.InsertData(
                table: "Offices",
                columns: new[] { "Id", "CurrencyId", "Name" },
                values: new object[,]
                {
                    { 1, 1, "USA" },
                    { 2, 2, "Germany" },
                    { 3, 3, "Sweden" },
                    { 4, 4, "Switzerland" },
                    { 5, 5, "Türkiye" }
                });

            migrationBuilder.InsertData(
                table: "Assets",
                columns: new[] { "Id", "Brand", "Model", "OfficeId", "Price", "PurchaseDate", "SerialNumber", "Type", "WarrantyExpirationDate" },
                values: new object[,]
                {
                    { 1, "Motorola", "X3", 1, 200.0, new DateTime(2023, 9, 20, 0, 0, 0, 0, DateTimeKind.Unspecified), "SR-MBL-1", "Mobile", new DateTime(2026, 9, 20, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 2, "Motorola", "X3", 1, 400.0, new DateTime(2023, 10, 20, 0, 0, 0, 0, DateTimeKind.Unspecified), "SR-MBL-2", "Mobile", new DateTime(2026, 10, 20, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 3, "Motorola", "X2", 1, 400.0, new DateTime(2024, 3, 20, 0, 0, 0, 0, DateTimeKind.Unspecified), "SR-MBL-3", "Mobile", new DateTime(2027, 3, 20, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 4, "Samsung", "Galaxy 10", 3, 4500.0, new DateTime(2023, 11, 20, 0, 0, 0, 0, DateTimeKind.Unspecified), "SR-MBL-4", "Mobile", new DateTime(2026, 11, 20, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 5, "Samsung", "Galaxy 10", 3, 4500.0, new DateTime(2023, 12, 20, 0, 0, 0, 0, DateTimeKind.Unspecified), "SR-MBL-5", "Mobile", new DateTime(2026, 12, 20, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 6, "Sony", "XPeria 7", 3, 3000.0, new DateTime(2023, 9, 20, 0, 0, 0, 0, DateTimeKind.Unspecified), "SR-MBL-6", "Mobile", new DateTime(2026, 9, 20, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 7, "Sony", "XPeria 7", 3, 3000.0, new DateTime(2023, 10, 20, 0, 0, 0, 0, DateTimeKind.Unspecified), "SR-MBL-7", "Mobile", new DateTime(2026, 10, 20, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 8, "Siemens", "Brick", 2, 220.0, new DateTime(2024, 5, 20, 0, 0, 0, 0, DateTimeKind.Unspecified), "SR-MBL-8", "Mobile", new DateTime(2027, 5, 20, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 9, "Dell", "Desktop 900", 1, 100.0, new DateTime(2023, 3, 20, 0, 0, 0, 0, DateTimeKind.Unspecified), "SR-CMP-9", "Computer", new DateTime(2026, 3, 20, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 10, "Dell", "Desktop 900", 1, 100.0, new DateTime(2023, 4, 20, 0, 0, 0, 0, DateTimeKind.Unspecified), "SR-CMP-10", "Computer", new DateTime(2026, 4, 20, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 11, "Lenovo", "X100", 1, 300.0, new DateTime(2023, 6, 20, 0, 0, 0, 0, DateTimeKind.Unspecified), "SR-CMP-11", "Computer", new DateTime(2026, 6, 20, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 12, "Lenovo", "X200", 1, 300.0, new DateTime(2023, 9, 20, 0, 0, 0, 0, DateTimeKind.Unspecified), "SR-CMP-12", "Computer", new DateTime(2026, 9, 20, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 13, "Lenovo", "X300", 1, 500.0, new DateTime(2024, 2, 20, 0, 0, 0, 0, DateTimeKind.Unspecified), "SR-CMP-13", "Computer", new DateTime(2027, 2, 20, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 14, "Dell", "Optiplex 100", 3, 1500.0, new DateTime(2023, 12, 20, 0, 0, 0, 0, DateTimeKind.Unspecified), "SR-CMP-14", "Computer", new DateTime(2026, 12, 20, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 15, "Dell", "Optiplex 200", 3, 1400.0, new DateTime(2024, 1, 20, 0, 0, 0, 0, DateTimeKind.Unspecified), "SR-CMP-15", "Computer", new DateTime(2027, 1, 20, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 16, "Dell", "Optiplex 300", 3, 1300.0, new DateTime(2024, 2, 20, 0, 0, 0, 0, DateTimeKind.Unspecified), "SR-CMP-16", "Computer", new DateTime(2027, 2, 20, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 17, "Asus", "ROG 600", 2, 1600.0, new DateTime(2024, 7, 20, 0, 0, 0, 0, DateTimeKind.Unspecified), "SR-CMP-17", "Computer", new DateTime(2027, 7, 20, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 18, "Asus", "ROG 500", 2, 1200.0, new DateTime(2023, 9, 20, 0, 0, 0, 0, DateTimeKind.Unspecified), "SR-CMP-18", "Computer", new DateTime(2026, 9, 20, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 19, "Asus", "ROG 500", 2, 1200.0, new DateTime(2023, 8, 20, 0, 0, 0, 0, DateTimeKind.Unspecified), "SR-CMP-19", "Computer", new DateTime(2026, 8, 20, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 20, "Asus", "ROG 500", 2, 1300.0, new DateTime(2023, 7, 20, 0, 0, 0, 0, DateTimeKind.Unspecified), "SR-CMP-20", "Computer", new DateTime(2026, 7, 20, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 21, "Motorola", "X4", 4, 400.0, new DateTime(2023, 10, 20, 0, 0, 0, 0, DateTimeKind.Unspecified), "SR-MBL-21", "Mobile", new DateTime(2026, 10, 20, 0, 0, 0, 0, DateTimeKind.Unspecified) }
                });

            migrationBuilder.CreateIndex(
                name: "IX_Assets_OfficeId",
                table: "Assets",
                column: "OfficeId");

            migrationBuilder.CreateIndex(
                name: "IX_Offices_CurrencyId",
                table: "Offices",
                column: "CurrencyId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Assets");

            migrationBuilder.DropTable(
                name: "Offices");

            migrationBuilder.DropTable(
                name: "Currencies");
        }
    }
}
