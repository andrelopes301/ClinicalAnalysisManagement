using Microsoft.EntityFrameworkCore.Migrations;

namespace LabTestesOnline.Migrations
{
    public partial class MigraRequired2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "3c8f49bd-8267-444e-9bc8-f69792ca00a7", "AQAAAAEAACcQAAAAEAVvg0rlQvbAFDbHauZxxXAFQ962MWR1+b642kZ0UK1hMfIwuNjWM2POPzT3Cao2yA==", "aa6e5570-c694-4035-9186-31685c7d2aac" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "433ac27c-d9cd-4c61-a68f-9cab0acb3dc8", "AQAAAAEAACcQAAAAEOK16VHV+MeTw8IGubJQCaKbgTmenzsEEO46sBE6e3LOSfFRK2IMwYtqXJSivVgkbA==", "f6519e95-8e9a-4529-8958-a8f53c7ecc8a" });
        }
    }
}
