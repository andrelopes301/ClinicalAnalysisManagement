using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace LabTestesOnline.Migrations
{
    public partial class MigrationInicial : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "CentroAnaliseId",
                table: "AspNetUsers",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Discriminator",
                table: "AspNetUsers",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.CreateTable(
                name: "CentrosAnalises",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Nome = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Localidade = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    NumLimiteTestes = table.Column<int>(type: "int", nullable: false),
                    HorarioAbertura = table.Column<DateTime>(type: "datetime2", nullable: false),
                    HorarioEncerramento = table.Column<DateTime>(type: "datetime2", nullable: false),
                    GestorId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CentrosAnalises", x => x.Id);
                    table.ForeignKey(
                        name: "FK_CentrosAnalises_AspNetUsers_GestorId",
                        column: x => x.GestorId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Testes",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    TipoTeste = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    DataInicio = table.Column<DateTime>(type: "datetime2", nullable: false),
                    DataFinal = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Resultado = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CentroAnaliseId = table.Column<int>(type: "int", nullable: true),
                    TecnicoResponsavelId = table.Column<int>(type: "int", nullable: true),
                    ClienteId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Testes", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Testes_AspNetUsers_ClienteId",
                        column: x => x.ClienteId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Testes_AspNetUsers_TecnicoResponsavelId",
                        column: x => x.TecnicoResponsavelId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Testes_CentrosAnalises_CentroAnaliseId",
                        column: x => x.CentroAnaliseId,
                        principalTable: "CentrosAnalises",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "TestesPossiveis",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    TipoTeste = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CentroAnaliseId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TestesPossiveis", x => x.Id);
                    table.ForeignKey(
                        name: "FK_TestesPossiveis_CentrosAnalises_CentroAnaliseId",
                        column: x => x.CentroAnaliseId,
                        principalTable: "CentrosAnalises",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUsers_CentroAnaliseId",
                table: "AspNetUsers",
                column: "CentroAnaliseId");

            migrationBuilder.CreateIndex(
                name: "IX_CentrosAnalises_GestorId",
                table: "CentrosAnalises",
                column: "GestorId");

            migrationBuilder.CreateIndex(
                name: "IX_Testes_CentroAnaliseId",
                table: "Testes",
                column: "CentroAnaliseId");

            migrationBuilder.CreateIndex(
                name: "IX_Testes_ClienteId",
                table: "Testes",
                column: "ClienteId");

            migrationBuilder.CreateIndex(
                name: "IX_Testes_TecnicoResponsavelId",
                table: "Testes",
                column: "TecnicoResponsavelId");

            migrationBuilder.CreateIndex(
                name: "IX_TestesPossiveis_CentroAnaliseId",
                table: "TestesPossiveis",
                column: "CentroAnaliseId");

            migrationBuilder.AddForeignKey(
                name: "FK_AspNetUsers_CentrosAnalises_CentroAnaliseId",
                table: "AspNetUsers",
                column: "CentroAnaliseId",
                principalTable: "CentrosAnalises",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AspNetUsers_CentrosAnalises_CentroAnaliseId",
                table: "AspNetUsers");

            migrationBuilder.DropTable(
                name: "Testes");

            migrationBuilder.DropTable(
                name: "TestesPossiveis");

            migrationBuilder.DropTable(
                name: "CentrosAnalises");

            migrationBuilder.DropIndex(
                name: "IX_AspNetUsers_CentroAnaliseId",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "CentroAnaliseId",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "Discriminator",
                table: "AspNetUsers");
        }
    }
}
