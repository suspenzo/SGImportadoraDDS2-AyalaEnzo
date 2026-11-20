using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace POS.Server.Migrations
{
    /// <inheritdoc />
    public partial class AjusteEntidadCategoriaYProductos : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "estado",
                table: "Products",
                newName: "Estado");

            migrationBuilder.RenameColumn(
                name: "nombre",
                table: "Categories",
                newName: "Nombre");

            migrationBuilder.RenameColumn(
                name: "descripcion",
                table: "Categories",
                newName: "Descripcion");

            migrationBuilder.AlterColumn<string>(
                name: "Marca",
                table: "Products",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Estado",
                table: "Products",
                newName: "estado");

            migrationBuilder.RenameColumn(
                name: "Nombre",
                table: "Categories",
                newName: "nombre");

            migrationBuilder.RenameColumn(
                name: "Descripcion",
                table: "Categories",
                newName: "descripcion");

            migrationBuilder.AlterColumn<int>(
                name: "Marca",
                table: "Products",
                type: "int",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");
        }
    }
}
