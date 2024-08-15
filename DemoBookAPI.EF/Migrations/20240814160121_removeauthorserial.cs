using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DemoBookAPI.EF.Migrations
{
    /// <inheritdoc />
    public partial class removeauthorserial : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Serial",
                table: "Authors");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<decimal>(
                name: "Serial",
                table: "Authors",
                type: "decimal(18,2)",
                nullable: false,
                defaultValue: 0m);
        }
    }
}
