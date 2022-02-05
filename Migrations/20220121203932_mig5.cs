using Microsoft.EntityFrameworkCore.Migrations;

namespace LabTestesOnline.Migrations
{
    public partial class mig5 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "766663b8-83c8-4c37-a9bd-d64343ac6455", "AQAAAAEAACcQAAAAEDKiKQMzAjMNZdJK9IDOAIO+QVjexSfOYs3F8Q1amNj0xbrVNZETTHRISZbovmaPyg==", "918b67d5-d997-4e8c-a0ba-f0b9ba683320" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "4a7780a2-614d-41a8-a4f3-a230c3ab8d6e", "AQAAAAEAACcQAAAAEBv1W9ZwkgzWvKIafaG9QXlDKNagmxPtNFLPpWf02WSpUDIc9HiVWW00GV/kbXPweQ==", "7e706595-277c-490b-9c31-228339185da6" });
        }
    }
}
