using Microsoft.EntityFrameworkCore.Migrations;

namespace SESCAP.Ecommerce.Migrations
{
    public partial class CriaTabelaCadastroLoginSescAp : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "CADASTRO_LOGIN_SESCAP",
                columns: table => new
                {
                    ID = table.Column<int>(nullable: false)
                        .Annotation("Db2:Identity", "1, 1"),
                    MATRICULA = table.Column<string>(maxLength: 13, nullable: false),
                    EMAIL = table.Column<string>(maxLength: 255, nullable: false),
                    CPF = table.Column<string>(maxLength: 14, nullable: false),
                    SENHA = table.Column<string>(maxLength: 255, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CADASTRO_LOGIN_SESCAP", x => x.ID);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "CADASTRO_LOGIN_SESCAP");
        }
    }
}
