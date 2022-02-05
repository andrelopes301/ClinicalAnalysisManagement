using Microsoft.EntityFrameworkCore.Migrations;

namespace LabTestesOnline.Migrations
{
    public partial class procedimentosv2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "TestePossiveisId",
                table: "Procedimentos");

            migrationBuilder.AlterColumn<string>(
                name: "Contacto",
                table: "AspNetUsers",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "ConcurrencyStamp", "Contacto", "PasswordHash", "SecurityStamp" },
                values: new object[] { "8e6e75ea-5f8e-48de-a7f7-28db5443e41a", null, "AQAAAAEAACcQAAAAEDDgybXI6yVA2NsTAYwNGlYE5n62MtwYvOqEAlJ7+t33fEWTt96SQpd0WX1fUUEnsg==", "22f0f1dc-c4b0-497e-b615-c0464882431a" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "TestePossiveisId",
                table: "Procedimentos",
                type: "int",
                nullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "Contacto",
                table: "AspNetUsers",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "ConcurrencyStamp", "Contacto", "PasswordHash", "SecurityStamp" },
                values: new object[] { "26f5a74b-430b-44d5-ac1c-0a269ff5dc19", 0, "AQAAAAEAACcQAAAAEDlSICNcB7ldjJ7omikFq78NreMf6/CyYNKtDESHy3oynuOI9bpyczh1K5K3W2DPsQ==", "279a5db8-b1b4-49c0-bc3b-dd39077fd875" });
        }
    }
}
