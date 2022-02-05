using Microsoft.EntityFrameworkCore.Migrations;

namespace LabTestesOnline.Migrations
{
    public partial class MigraRequired3 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "Contacto",
                table: "AspNetUsers",
                type: "nvarchar(9)",
                maxLength: 9,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "4a7780a2-614d-41a8-a4f3-a230c3ab8d6e", "AQAAAAEAACcQAAAAEBv1W9ZwkgzWvKIafaG9QXlDKNagmxPtNFLPpWf02WSpUDIc9HiVWW00GV/kbXPweQ==", "7e706595-277c-490b-9c31-228339185da6" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "Contacto",
                table: "AspNetUsers",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(9)",
                oldMaxLength: 9);

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "3c8f49bd-8267-444e-9bc8-f69792ca00a7", "AQAAAAEAACcQAAAAEAVvg0rlQvbAFDbHauZxxXAFQ962MWR1+b642kZ0UK1hMfIwuNjWM2POPzT3Cao2yA==", "aa6e5570-c694-4035-9186-31685c7d2aac" });
        }
    }
}
