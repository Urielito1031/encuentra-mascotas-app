using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Infraestructure.Migrations
{
    /// <inheritdoc />
    public partial class RenombrarTablaImagenesEnDbContext : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_ImagenBusquedaTemporals",
                table: "ImagenBusquedaTemporals");

            migrationBuilder.RenameTable(
                name: "ImagenBusquedaTemporals",
                newName: "ImagenesBusquedaTemporal");

            migrationBuilder.RenameIndex(
                name: "IX_ImagenBusquedaTemporals_FechaExpiracion_EliminadoAutomatico",
                table: "ImagenesBusquedaTemporal",
                newName: "IX_ImagenesBusquedaTemporal_FechaExpiracion_EliminadoAutomatico");

            migrationBuilder.AddPrimaryKey(
                name: "PK_ImagenesBusquedaTemporal",
                table: "ImagenesBusquedaTemporal",
                column: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_ImagenesBusquedaTemporal",
                table: "ImagenesBusquedaTemporal");

            migrationBuilder.RenameTable(
                name: "ImagenesBusquedaTemporal",
                newName: "ImagenBusquedaTemporals");

            migrationBuilder.RenameIndex(
                name: "IX_ImagenesBusquedaTemporal_FechaExpiracion_EliminadoAutomatico",
                table: "ImagenBusquedaTemporals",
                newName: "IX_ImagenBusquedaTemporals_FechaExpiracion_EliminadoAutomatico");

            migrationBuilder.AddPrimaryKey(
                name: "PK_ImagenBusquedaTemporals",
                table: "ImagenBusquedaTemporals",
                column: "Id");
        }
    }
}
