using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace LabTestesOnline.Data.Migrations
{
    public partial class Migration1 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Clientes",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Nome = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Sexo = table.Column<string>(type: "nvarchar(1)", nullable: false),
                    DataNascimento = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Email = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Password = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Localidade = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Morada = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Contacto = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Clientes", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Gestores",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Nome = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Email = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Password = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Gestores", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "CentrosAnalises",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Nome = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Localidade = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    numLimiteTestes = table.Column<int>(type: "int", nullable: false),
                    numLimiteAnalises = table.Column<int>(type: "int", nullable: false),
                    HorarioAbertura = table.Column<DateTime>(type: "datetime2", nullable: false),
                    HorarioEncerramento = table.Column<DateTime>(type: "datetime2", nullable: false),
                    GestorId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CentrosAnalises", x => x.Id);
                    table.ForeignKey(
                        name: "FK_CentrosAnalises_Gestores_GestorId",
                        column: x => x.GestorId,
                        principalTable: "Gestores",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "AnalisesPossiveis",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    TipoAnalise = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CentroAnaliseId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AnalisesPossiveis", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AnalisesPossiveis_CentrosAnalises_CentroAnaliseId",
                        column: x => x.CentroAnaliseId,
                        principalTable: "CentrosAnalises",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Tecnicos",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Nome = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Email = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Password = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CentroAnaliseId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Tecnicos", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Tecnicos_CentrosAnalises_CentroAnaliseId",
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
                    TipoAnalise = table.Column<string>(type: "nvarchar(max)", nullable: true),
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

            migrationBuilder.CreateTable(
                name: "Analises",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    TipoAnalise = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    DataInicio = table.Column<DateTime>(type: "datetime2", nullable: false),
                    DataFinal = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Resultado = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CentroAnaliseId = table.Column<int>(type: "int", nullable: true),
                    TecnicoResponsavelId = table.Column<int>(type: "int", nullable: true),
                    ClienteId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Analises", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Analises_CentrosAnalises_CentroAnaliseId",
                        column: x => x.CentroAnaliseId,
                        principalTable: "CentrosAnalises",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Analises_Clientes_ClienteId",
                        column: x => x.ClienteId,
                        principalTable: "Clientes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Analises_Tecnicos_TecnicoResponsavelId",
                        column: x => x.TecnicoResponsavelId,
                        principalTable: "Tecnicos",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Testes",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Tipo = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Resultado = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ClienteId = table.Column<int>(type: "int", nullable: true),
                    TecnicoResponsavelId = table.Column<int>(type: "int", nullable: true),
                    CentroAnaliseId = table.Column<int>(type: "int", nullable: true),
                    TecnicoId = table.Column<int>(type: "int", nullable: true),
                    Data = table.Column<DateTime>(type: "datetime2", nullable: false),
                    DataInicio = table.Column<DateTime>(type: "datetime2", nullable: false),
                    DataFinal = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Testes", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Testes_CentrosAnalises_CentroAnaliseId",
                        column: x => x.CentroAnaliseId,
                        principalTable: "CentrosAnalises",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Testes_Clientes_ClienteId",
                        column: x => x.ClienteId,
                        principalTable: "Clientes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Testes_Tecnicos_TecnicoId",
                        column: x => x.TecnicoId,
                        principalTable: "Tecnicos",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Analises_CentroAnaliseId",
                table: "Analises",
                column: "CentroAnaliseId");

            migrationBuilder.CreateIndex(
                name: "IX_Analises_ClienteId",
                table: "Analises",
                column: "ClienteId");

            migrationBuilder.CreateIndex(
                name: "IX_Analises_TecnicoResponsavelId",
                table: "Analises",
                column: "TecnicoResponsavelId");

            migrationBuilder.CreateIndex(
                name: "IX_AnalisesPossiveis_CentroAnaliseId",
                table: "AnalisesPossiveis",
                column: "CentroAnaliseId");

            migrationBuilder.CreateIndex(
                name: "IX_CentrosAnalises_GestorId",
                table: "CentrosAnalises",
                column: "GestorId");

            migrationBuilder.CreateIndex(
                name: "IX_Tecnicos_CentroAnaliseId",
                table: "Tecnicos",
                column: "CentroAnaliseId");

            migrationBuilder.CreateIndex(
                name: "IX_Testes_CentroAnaliseId",
                table: "Testes",
                column: "CentroAnaliseId");

            migrationBuilder.CreateIndex(
                name: "IX_Testes_ClienteId",
                table: "Testes",
                column: "ClienteId");

            migrationBuilder.CreateIndex(
                name: "IX_Testes_TecnicoId",
                table: "Testes",
                column: "TecnicoId");

            migrationBuilder.CreateIndex(
                name: "IX_TestesPossiveis_CentroAnaliseId",
                table: "TestesPossiveis",
                column: "CentroAnaliseId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Analises");

            migrationBuilder.DropTable(
                name: "AnalisesPossiveis");

            migrationBuilder.DropTable(
                name: "Testes");

            migrationBuilder.DropTable(
                name: "TestesPossiveis");

            migrationBuilder.DropTable(
                name: "Clientes");

            migrationBuilder.DropTable(
                name: "Tecnicos");

            migrationBuilder.DropTable(
                name: "CentrosAnalises");

            migrationBuilder.DropTable(
                name: "Gestores");
        }
    }
}
