using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace VideoClub.Migrations
{
    /// <inheritdoc />
    public partial class RemoveDiasDeRenta : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Dias_de_renta",
                table: "Articulos");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "Dias_de_renta",
                table: "Articulos",
                type: "integer",
                nullable: false,
                defaultValue: 0);
        }
    }
}
