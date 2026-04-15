using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace VideoClub.Migrations
{
    /// <inheritdoc />
    public partial class ActualizarDatosTipoArticulo : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql("UPDATE \"Tipos_de_Articulos\" SET \"Tipo\" = 'Pelicula' WHERE \"Tipo\" = '1';");
            migrationBuilder.Sql("UPDATE \"Tipos_de_Articulos\" SET \"Tipo\" = 'CD Musica' WHERE \"Tipo\" = '2';");
            migrationBuilder.Sql("UPDATE \"Tipos_de_Articulos\" SET \"Tipo\" = 'Libro' WHERE \"Tipo\" = '3';");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {

        }
    }
}
