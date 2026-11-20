using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace POS.Server.Migrations
{
    /// <inheritdoc />
    public partial class AddTablaClientes : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Products_Categories_categoriaCategoryID",
                table: "Products");

            migrationBuilder.RenameColumn(
                name: "sku",
                table: "Products",
                newName: "Sku");

            migrationBuilder.RenameColumn(
                name: "nombre",
                table: "Products",
                newName: "Nombre");

            migrationBuilder.RenameColumn(
                name: "modelo",
                table: "Products",
                newName: "Modelo");

            migrationBuilder.RenameColumn(
                name: "marca",
                table: "Products",
                newName: "Marca");

            migrationBuilder.RenameColumn(
                name: "descripcion",
                table: "Products",
                newName: "Descripcion");

            migrationBuilder.RenameColumn(
                name: "productID",
                table: "Products",
                newName: "ProductID");

            migrationBuilder.RenameColumn(
                name: "precio_venta",
                table: "Products",
                newName: "PrecioVenta");

            migrationBuilder.RenameColumn(
                name: "id_categoria",
                table: "Products",
                newName: "estado");

            migrationBuilder.RenameColumn(
                name: "categoriaCategoryID",
                table: "Products",
                newName: "CategoryID");

            migrationBuilder.RenameIndex(
                name: "IX_Products_categoriaCategoryID",
                table: "Products",
                newName: "IX_Products_CategoryID");

            migrationBuilder.AddColumn<int>(
                name: "Estado",
                table: "Users",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateTable(
                name: "Clients",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Nombre = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    APaterno = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    AMaterno = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    NumDocumento = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Email = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Telefono = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Estado = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Clients", x => x.Id);
                });

            migrationBuilder.AddForeignKey(
                name: "FK_Products_Categories_CategoryID",
                table: "Products",
                column: "CategoryID",
                principalTable: "Categories",
                principalColumn: "CategoryID",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Products_Categories_CategoryID",
                table: "Products");

            migrationBuilder.DropTable(
                name: "Clients");

            migrationBuilder.DropColumn(
                name: "Estado",
                table: "Users");

            migrationBuilder.RenameColumn(
                name: "Sku",
                table: "Products",
                newName: "sku");

            migrationBuilder.RenameColumn(
                name: "Nombre",
                table: "Products",
                newName: "nombre");

            migrationBuilder.RenameColumn(
                name: "Modelo",
                table: "Products",
                newName: "modelo");

            migrationBuilder.RenameColumn(
                name: "Marca",
                table: "Products",
                newName: "marca");

            migrationBuilder.RenameColumn(
                name: "Descripcion",
                table: "Products",
                newName: "descripcion");

            migrationBuilder.RenameColumn(
                name: "ProductID",
                table: "Products",
                newName: "productID");

            migrationBuilder.RenameColumn(
                name: "estado",
                table: "Products",
                newName: "id_categoria");

            migrationBuilder.RenameColumn(
                name: "PrecioVenta",
                table: "Products",
                newName: "precio_venta");

            migrationBuilder.RenameColumn(
                name: "CategoryID",
                table: "Products",
                newName: "categoriaCategoryID");

            migrationBuilder.RenameIndex(
                name: "IX_Products_CategoryID",
                table: "Products",
                newName: "IX_Products_categoriaCategoryID");

            migrationBuilder.AddForeignKey(
                name: "FK_Products_Categories_categoriaCategoryID",
                table: "Products",
                column: "categoriaCategoryID",
                principalTable: "Categories",
                principalColumn: "CategoryID",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
