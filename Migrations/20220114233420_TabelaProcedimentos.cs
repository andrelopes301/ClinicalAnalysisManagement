using Microsoft.EntityFrameworkCore.Migrations;

namespace LabTestesOnline.Migrations
{
    public partial class TabelaProcedimentos : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Procedimentos",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    TipoProcedimento = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    isChecked = table.Column<bool>(type: "bit", nullable: false),
                    TestePossiveisId = table.Column<int>(type: "int", nullable: true),
                    TestesPossiveisId = table.Column<int>(type: "int", nullable: true),
                    TesteId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Procedimentos", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Procedimentos_Testes_TesteId",
                        column: x => x.TesteId,
                        principalTable: "Testes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Procedimentos_TestesPossiveis_TestesPossiveisId",
                        column: x => x.TestesPossiveisId,
                        principalTable: "TestesPossiveis",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "d4433e87-7618-47c5-a8cf-e89449266627", "AQAAAAEAACcQAAAAEI9Ok3MsDZ5or3DE4PK9ofXeIb1ClKHQoNpVjyY85t//ZF/QB+Ha2MYDvVz1tD+Bzg==", "11a3a5b7-e906-4f60-9de6-c49768d63c49" });

            migrationBuilder.CreateIndex(
                name: "IX_Procedimentos_TesteId",
                table: "Procedimentos",
                column: "TesteId");

            migrationBuilder.CreateIndex(
                name: "IX_Procedimentos_TestesPossiveisId",
                table: "Procedimentos",
                column: "TestesPossiveisId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Procedimentos");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "a9b54cf9-2715-4e7b-b95f-db77735b049b", "AQAAAAEAACcQAAAAEFXAfzzyNKMIUnMNk6hiWtASAKOzNgpwyKGPfha69yF2uIyWoRObiTuZS2NVpOAqwA==", "e4da11b9-e05a-40e1-98ea-334189536d27" });
        }
    }
}
