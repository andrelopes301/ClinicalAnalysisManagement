using Microsoft.EntityFrameworkCore.Migrations;

namespace LabTestesOnline.Migrations
{
    public partial class MIgraDel : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AspNetUsers_CentrosAnalises_CentroAnaliseId",
                table: "AspNetUsers");

            migrationBuilder.DropForeignKey(
                name: "FK_CentrosAnalises_AspNetUsers_GestorId",
                table: "CentrosAnalises");

            migrationBuilder.DropForeignKey(
                name: "FK_Procedimentos_TestesPossiveis_TestesPossiveisId",
                table: "Procedimentos");

            migrationBuilder.DropForeignKey(
                name: "FK_Testes_AspNetUsers_ClienteId",
                table: "Testes");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "f24e253c-7e66-44f5-8354-22559ecec843", "AQAAAAEAACcQAAAAEL0jqk90kPoBi4liTDCTI2IKM2sKghj454g61fMyOj65r5Uu2gYNJhUtlz2+o4/pbQ==", "754c2fc4-d6d8-46c4-ab3d-7377706ee68f" });

            migrationBuilder.AddForeignKey(
                name: "FK_AspNetUsers_CentrosAnalises_CentroAnaliseId",
                table: "AspNetUsers",
                column: "CentroAnaliseId",
                principalTable: "CentrosAnalises",
                principalColumn: "Id",
                onDelete: ReferentialAction.SetNull);

            migrationBuilder.AddForeignKey(
                name: "FK_CentrosAnalises_AspNetUsers_GestorId",
                table: "CentrosAnalises",
                column: "GestorId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.SetNull);

            migrationBuilder.AddForeignKey(
                name: "FK_Procedimentos_TestesPossiveis_TestesPossiveisId",
                table: "Procedimentos",
                column: "TestesPossiveisId",
                principalTable: "TestesPossiveis",
                principalColumn: "Id",
                onDelete: ReferentialAction.SetNull);

            migrationBuilder.AddForeignKey(
                name: "FK_Testes_AspNetUsers_ClienteId",
                table: "Testes",
                column: "ClienteId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.SetNull);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AspNetUsers_CentrosAnalises_CentroAnaliseId",
                table: "AspNetUsers");

            migrationBuilder.DropForeignKey(
                name: "FK_CentrosAnalises_AspNetUsers_GestorId",
                table: "CentrosAnalises");

            migrationBuilder.DropForeignKey(
                name: "FK_Procedimentos_TestesPossiveis_TestesPossiveisId",
                table: "Procedimentos");

            migrationBuilder.DropForeignKey(
                name: "FK_Testes_AspNetUsers_ClienteId",
                table: "Testes");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "8e6e75ea-5f8e-48de-a7f7-28db5443e41a", "AQAAAAEAACcQAAAAEDDgybXI6yVA2NsTAYwNGlYE5n62MtwYvOqEAlJ7+t33fEWTt96SQpd0WX1fUUEnsg==", "22f0f1dc-c4b0-497e-b615-c0464882431a" });

            migrationBuilder.AddForeignKey(
                name: "FK_AspNetUsers_CentrosAnalises_CentroAnaliseId",
                table: "AspNetUsers",
                column: "CentroAnaliseId",
                principalTable: "CentrosAnalises",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_CentrosAnalises_AspNetUsers_GestorId",
                table: "CentrosAnalises",
                column: "GestorId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Procedimentos_TestesPossiveis_TestesPossiveisId",
                table: "Procedimentos",
                column: "TestesPossiveisId",
                principalTable: "TestesPossiveis",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Testes_AspNetUsers_ClienteId",
                table: "Testes",
                column: "ClienteId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
