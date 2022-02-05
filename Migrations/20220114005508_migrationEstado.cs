using Microsoft.EntityFrameworkCore.Migrations;

namespace LabTestesOnline.Migrations
{
    public partial class migrationEstado : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "Estado",
                table: "Testes",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "a9b54cf9-2715-4e7b-b95f-db77735b049b", "AQAAAAEAACcQAAAAEFXAfzzyNKMIUnMNk6hiWtASAKOzNgpwyKGPfha69yF2uIyWoRObiTuZS2NVpOAqwA==", "e4da11b9-e05a-40e1-98ea-334189536d27" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Estado",
                table: "Testes");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "16367c17-aee7-495a-a29d-552e7f77efbf", "AQAAAAEAACcQAAAAEOayMXNaODVVi7MQkyGvY4beeI71vAFcY2oHa2qxBmP+ndwmikIj7W71ieEHwzpoKw==", "520a3adf-910d-4c1c-94f6-908e3d8603d2" });
        }
    }
}
