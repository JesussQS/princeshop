using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace princeshop.Data.Migrations
{
    public partial class Pedido_DetallePedido20Migration : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "t_pedido",
                columns: table => new
                {
                    id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    UserID = table.Column<string>(type: "text", nullable: true),
                    Total = table.Column<decimal>(type: "numeric", nullable: false),
                    pagoId = table.Column<int>(type: "integer", nullable: true),
                    Status = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_t_pedido", x => x.id);
                    table.ForeignKey(
                        name: "FK_t_pedido_t_pago_pagoId",
                        column: x => x.pagoId,
                        principalTable: "t_pago",
                        principalColumn: "id");
                });

            migrationBuilder.CreateTable(
                name: "t_detallePedido",
                columns: table => new
                {
                    id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    ProductoId = table.Column<int>(type: "integer", nullable: true),
                    Talla = table.Column<string>(type: "text", nullable: true),
                    Cantidad = table.Column<int>(type: "integer", nullable: false),
                    Precio = table.Column<decimal>(type: "numeric", nullable: false),
                    pedidoID = table.Column<int>(type: "integer", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_t_detallePedido", x => x.id);
                    table.ForeignKey(
                        name: "FK_t_detallePedido_t_pedido_pedidoID",
                        column: x => x.pedidoID,
                        principalTable: "t_pedido",
                        principalColumn: "id");
                    table.ForeignKey(
                        name: "FK_t_detallePedido_t_producto_ProductoId",
                        column: x => x.ProductoId,
                        principalTable: "t_producto",
                        principalColumn: "id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_t_detallePedido_pedidoID",
                table: "t_detallePedido",
                column: "pedidoID");

            migrationBuilder.CreateIndex(
                name: "IX_t_detallePedido_ProductoId",
                table: "t_detallePedido",
                column: "ProductoId");

            migrationBuilder.CreateIndex(
                name: "IX_t_pedido_pagoId",
                table: "t_pedido",
                column: "pagoId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "t_detallePedido");

            migrationBuilder.DropTable(
                name: "t_pedido");
        }
    }
}
