using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Concesionario.WebApi.Migrations
{
    /// <inheritdoc />
    public partial class EditTipoDePagoID : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "TipoDePagoId",
                table: "TiposDePagos",
                newName: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Id",
                table: "TiposDePagos",
                newName: "TipoDePagoId");
        }
    }
}
