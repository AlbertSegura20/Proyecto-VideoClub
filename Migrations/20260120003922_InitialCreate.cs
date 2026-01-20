using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace VideoClub.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Clientes",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Nombre = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false),
                    Cedula = table.Column<string>(type: "text", nullable: false),
                    NumeroTarjetaCR = table.Column<string>(type: "text", nullable: false),
                    Limite_Credito = table.Column<double>(type: "double precision", nullable: false),
                    Tipo_Persona = table.Column<int>(type: "integer", nullable: false),
                    Monto_Entrega_Tardia = table.Column<double>(type: "double precision", nullable: false),
                    Estado = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Clientes", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Elenco",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Nombre = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false),
                    Estado = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Elenco", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Empleados",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Nombre = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false),
                    Cedula = table.Column<string>(type: "text", nullable: false),
                    TandaLabor = table.Column<string>(type: "text", nullable: false),
                    PorcentajeComision = table.Column<double>(type: "double precision", nullable: false),
                    Fecha_Ingreso = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    Estado = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Empleados", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Géneros",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Descripcion = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: true),
                    Estado = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Géneros", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Idiomas",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Descripcion = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: true),
                    Estado = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Idiomas", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Tipos_de_Articulos",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Descripcion = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: true),
                    Estado = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Tipos_de_Articulos", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Articulos",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Titulo = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: true),
                    Tipos_de_ArticulosId = table.Column<int>(type: "integer", nullable: false),
                    IdiomaId = table.Column<int>(type: "integer", nullable: false),
                    Renta_por_dia = table.Column<double>(type: "double precision", nullable: false),
                    Dias_de_renta = table.Column<int>(type: "integer", nullable: false),
                    Monto_Entrega_Tardia = table.Column<double>(type: "double precision", nullable: false),
                    Estado = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Articulos", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Articulos_Idiomas_IdiomaId",
                        column: x => x.IdiomaId,
                        principalTable: "Idiomas",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Articulos_Tipos_de_Articulos_Tipos_de_ArticulosId",
                        column: x => x.Tipos_de_ArticulosId,
                        principalTable: "Tipos_de_Articulos",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ElencoArticulo",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    ArticuloId = table.Column<int>(type: "integer", nullable: false),
                    ElencoId = table.Column<int>(type: "integer", nullable: false),
                    Rol = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ElencoArticulo", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ElencoArticulo_Articulos_ArticuloId",
                        column: x => x.ArticuloId,
                        principalTable: "Articulos",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ElencoArticulo_Elenco_ElencoId",
                        column: x => x.ElencoId,
                        principalTable: "Elenco",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Rentas",
                columns: table => new
                {
                    NoRenta = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    EmpleadoId = table.Column<int>(type: "integer", nullable: false),
                    ArticuloId = table.Column<int>(type: "integer", nullable: false),
                    ClienteId = table.Column<int>(type: "integer", nullable: false),
                    FechaRenta = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    FechaDevolucion = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    MontoPorDia = table.Column<decimal>(type: "numeric", nullable: false),
                    CantidadDias = table.Column<int>(type: "integer", nullable: false),
                    Comentario = table.Column<string>(type: "text", nullable: true),
                    Estado = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Rentas", x => x.NoRenta);
                    table.ForeignKey(
                        name: "FK_Rentas_Articulos_ArticuloId",
                        column: x => x.ArticuloId,
                        principalTable: "Articulos",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Rentas_Clientes_ClienteId",
                        column: x => x.ClienteId,
                        principalTable: "Clientes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Rentas_Empleados_EmpleadoId",
                        column: x => x.EmpleadoId,
                        principalTable: "Empleados",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Articulos_IdiomaId",
                table: "Articulos",
                column: "IdiomaId");

            migrationBuilder.CreateIndex(
                name: "IX_Articulos_Tipos_de_ArticulosId",
                table: "Articulos",
                column: "Tipos_de_ArticulosId");

            migrationBuilder.CreateIndex(
                name: "IX_ElencoArticulo_ArticuloId",
                table: "ElencoArticulo",
                column: "ArticuloId");

            migrationBuilder.CreateIndex(
                name: "IX_ElencoArticulo_ElencoId",
                table: "ElencoArticulo",
                column: "ElencoId");

            migrationBuilder.CreateIndex(
                name: "IX_Rentas_ArticuloId",
                table: "Rentas",
                column: "ArticuloId");

            migrationBuilder.CreateIndex(
                name: "IX_Rentas_ClienteId",
                table: "Rentas",
                column: "ClienteId");

            migrationBuilder.CreateIndex(
                name: "IX_Rentas_EmpleadoId",
                table: "Rentas",
                column: "EmpleadoId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ElencoArticulo");

            migrationBuilder.DropTable(
                name: "Géneros");

            migrationBuilder.DropTable(
                name: "Rentas");

            migrationBuilder.DropTable(
                name: "Elenco");

            migrationBuilder.DropTable(
                name: "Articulos");

            migrationBuilder.DropTable(
                name: "Clientes");

            migrationBuilder.DropTable(
                name: "Empleados");

            migrationBuilder.DropTable(
                name: "Idiomas");

            migrationBuilder.DropTable(
                name: "Tipos_de_Articulos");
        }
    }
}
