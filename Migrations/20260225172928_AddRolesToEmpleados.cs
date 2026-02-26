using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace VideoClub.Migrations
{
    /// <inheritdoc />
    public partial class AddRolesToEmpleados : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "Rol",
                table: "Empleados",
                type: "integer",
                nullable: false,
                defaultValue: 0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Rol",
                table: "Empleados");
        }
    }
}
