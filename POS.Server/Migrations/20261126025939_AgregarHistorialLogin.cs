using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace POS.Server.Migrations
{
    /// <inheritdoc />
    public partial class AgregarHistorialLogin : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_DetallesVenta_Products_ProductoProductID",
                table: "DetallesVenta");

            migrationBuilder.DropForeignKey(
                name: "FK_UnidadesFisicas_Products_ProductoProductID",
                table: "UnidadesFisicas");

            migrationBuilder.DropForeignKey(
                name: "FK_Ventas_Clients_ClienteId",
                table: "Ventas");

            migrationBuilder.DropForeignKey(
                name: "FK_Ventas_Users_UsuarioId",
                table: "Ventas");

            migrationBuilder.DropIndex(
                name: "IX_Ventas_ClienteId",
                table: "Ventas");

            migrationBuilder.DropIndex(
                name: "IX_Ventas_UsuarioId",
                table: "Ventas");

            migrationBuilder.DropIndex(
                name: "IX_UnidadesFisicas_ProductoProductID",
                table: "UnidadesFisicas");

            migrationBuilder.DropIndex(
                name: "IX_DetallesVenta_ProductoProductID",
                table: "DetallesVenta");

            migrationBuilder.DropColumn(
                name: "ClienteId",
                table: "Ventas");

            migrationBuilder.DropColumn(
                name: "UsuarioId",
                table: "Ventas");

            migrationBuilder.DropColumn(
                name: "ProductoProductID",
                table: "UnidadesFisicas");

            migrationBuilder.DropColumn(
                name: "ProductoProductID",
                table: "DetallesVenta");

            migrationBuilder.CreateTable(
                name: "LoginHistories",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserId = table.Column<int>(type: "int", nullable: false),
                    LoginTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    LogoutTime = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LoginHistories", x => x.Id);
                    table.ForeignKey(
                        name: "FK_LoginHistories_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Ventas_IdCliente",
                table: "Ventas",
                column: "IdCliente");

            migrationBuilder.CreateIndex(
                name: "IX_Ventas_IdUsuario",
                table: "Ventas",
                column: "IdUsuario");

            migrationBuilder.CreateIndex(
                name: "IX_UnidadesFisicas_IdProducto",
                table: "UnidadesFisicas",
                column: "IdProducto");

            migrationBuilder.CreateIndex(
                name: "IX_DetallesVenta_IdProducto",
                table: "DetallesVenta",
                column: "IdProducto");

            migrationBuilder.CreateIndex(
                name: "IX_LoginHistories_UserId",
                table: "LoginHistories",
                column: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_DetallesVenta_Products_IdProducto",
                table: "DetallesVenta",
                column: "IdProducto",
                principalTable: "Products",
                principalColumn: "ProductID",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_UnidadesFisicas_Products_IdProducto",
                table: "UnidadesFisicas",
                column: "IdProducto",
                principalTable: "Products",
                principalColumn: "ProductID",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Ventas_Clients_IdCliente",
                table: "Ventas",
                column: "IdCliente",
                principalTable: "Clients",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Ventas_Users_IdUsuario",
                table: "Ventas",
                column: "IdUsuario",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_DetallesVenta_Products_IdProducto",
                table: "DetallesVenta");

            migrationBuilder.DropForeignKey(
                name: "FK_UnidadesFisicas_Products_IdProducto",
                table: "UnidadesFisicas");

            migrationBuilder.DropForeignKey(
                name: "FK_Ventas_Clients_IdCliente",
                table: "Ventas");

            migrationBuilder.DropForeignKey(
                name: "FK_Ventas_Users_IdUsuario",
                table: "Ventas");

            migrationBuilder.DropTable(
                name: "LoginHistories");

            migrationBuilder.DropIndex(
                name: "IX_Ventas_IdCliente",
                table: "Ventas");

            migrationBuilder.DropIndex(
                name: "IX_Ventas_IdUsuario",
                table: "Ventas");

            migrationBuilder.DropIndex(
                name: "IX_UnidadesFisicas_IdProducto",
                table: "UnidadesFisicas");

            migrationBuilder.DropIndex(
                name: "IX_DetallesVenta_IdProducto",
                table: "DetallesVenta");

            migrationBuilder.AddColumn<int>(
                name: "ClienteId",
                table: "Ventas",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "UsuarioId",
                table: "Ventas",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "ProductoProductID",
                table: "UnidadesFisicas",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "ProductoProductID",
                table: "DetallesVenta",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Ventas_ClienteId",
                table: "Ventas",
                column: "ClienteId");

            migrationBuilder.CreateIndex(
                name: "IX_Ventas_UsuarioId",
                table: "Ventas",
                column: "UsuarioId");

            migrationBuilder.CreateIndex(
                name: "IX_UnidadesFisicas_ProductoProductID",
                table: "UnidadesFisicas",
                column: "ProductoProductID");

            migrationBuilder.CreateIndex(
                name: "IX_DetallesVenta_ProductoProductID",
                table: "DetallesVenta",
                column: "ProductoProductID");

            migrationBuilder.AddForeignKey(
                name: "FK_DetallesVenta_Products_ProductoProductID",
                table: "DetallesVenta",
                column: "ProductoProductID",
                principalTable: "Products",
                principalColumn: "ProductID");

            migrationBuilder.AddForeignKey(
                name: "FK_UnidadesFisicas_Products_ProductoProductID",
                table: "UnidadesFisicas",
                column: "ProductoProductID",
                principalTable: "Products",
                principalColumn: "ProductID");

            migrationBuilder.AddForeignKey(
                name: "FK_Ventas_Clients_ClienteId",
                table: "Ventas",
                column: "ClienteId",
                principalTable: "Clients",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Ventas_Users_UsuarioId",
                table: "Ventas",
                column: "UsuarioId",
                principalTable: "Users",
                principalColumn: "Id");
        }
    }
}
