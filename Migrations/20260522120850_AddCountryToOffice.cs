using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AssetTracking.Migrations
{
    /// <inheritdoc />
    public partial class AddCountryToOffice : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Country",
                table: "Offices",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.UpdateData(
                table: "Offices",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "Country", "Name" },
                values: new object[] { "USA", "Lexicon USA" });

            migrationBuilder.UpdateData(
                table: "Offices",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "Country", "Name" },
                values: new object[] { "Sweden", "Lexicon Germany" });

            migrationBuilder.UpdateData(
                table: "Offices",
                keyColumn: "Id",
                keyValue: 3,
                columns: new[] { "Country", "Name" },
                values: new object[] { "Sweden", "Lexicon Sweden" });

            migrationBuilder.UpdateData(
                table: "Offices",
                keyColumn: "Id",
                keyValue: 4,
                columns: new[] { "Country", "Name" },
                values: new object[] { "Switzerland", "Lexicon Switzerland" });

            migrationBuilder.UpdateData(
                table: "Offices",
                keyColumn: "Id",
                keyValue: 5,
                columns: new[] { "Country", "Name" },
                values: new object[] { "Türkiye", "Lexicon Türkiye" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Country",
                table: "Offices");

            migrationBuilder.UpdateData(
                table: "Offices",
                keyColumn: "Id",
                keyValue: 1,
                column: "Name",
                value: "USA");

            migrationBuilder.UpdateData(
                table: "Offices",
                keyColumn: "Id",
                keyValue: 2,
                column: "Name",
                value: "Germany");

            migrationBuilder.UpdateData(
                table: "Offices",
                keyColumn: "Id",
                keyValue: 3,
                column: "Name",
                value: "Sweden");

            migrationBuilder.UpdateData(
                table: "Offices",
                keyColumn: "Id",
                keyValue: 4,
                column: "Name",
                value: "Switzerland");

            migrationBuilder.UpdateData(
                table: "Offices",
                keyColumn: "Id",
                keyValue: 5,
                column: "Name",
                value: "Türkiye");
        }
    }
}
