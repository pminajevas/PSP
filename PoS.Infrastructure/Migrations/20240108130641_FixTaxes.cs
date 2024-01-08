using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PoS.Infrastructure.Migrations
{
    public partial class FixTaxes : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "TaxPercentage",
                table: "Taxes",
                newName: "TaxValue");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "TaxValue",
                table: "Taxes",
                newName: "TaxPercentage");
        }
    }
}
