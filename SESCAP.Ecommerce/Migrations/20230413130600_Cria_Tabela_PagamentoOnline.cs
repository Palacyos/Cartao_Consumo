using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace SESCAP.Ecommerce.Migrations
{
    public partial class Cria_Tabela_PagamentoOnline : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "PAGAMENTO_ONLINE",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("Db2:Identity", "1, 1"),
                    OrderId = table.Column<string>(maxLength: 60, nullable: true),
                    FormaPgto = table.Column<string>(maxLength: 20, nullable: true),
                    Total = table.Column<decimal>(nullable: false),
                    Transacao = table.Column<string>(maxLength: 3000, nullable: true),
                    DataPgto = table.Column<DateTime>(nullable: false),
                    Status = table.Column<string>(maxLength: 50, nullable: true),
                    SQMATRIC = table.Column<int>(nullable: true),
                    CDUOP = table.Column<int>(nullable: true)
                },
                 constraints: table =>
                 {
                     table.PrimaryKey("PK_PAGAMENTO_ONLINE", x => x.Id);
                     table.ForeignKey(
                         name: "FK_PAGAMENTO_ONLINE_CLIENTELA_SQMATRIC_CDUOP",
                         columns: x => new { x.SQMATRIC, x.CDUOP },
                         principalTable: "CLIENTELA",
                         principalColumns: new[] { "SQMATRIC", "CDUOP" },
                         onDelete: ReferentialAction.Restrict);
                 });

            migrationBuilder.CreateIndex(
                name: "IX_PAGAMENTO_ONLINE_SQMATRIC_CDUOP",
                table: "PAGAMENTO_ONLINE",
                columns: new[] { "SQMATRIC", "CDUOP" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "PAGAMENTO_ONLINE");
        }
    }
}
