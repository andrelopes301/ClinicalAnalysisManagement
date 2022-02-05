using Microsoft.EntityFrameworkCore.Migrations;

namespace LabTestesOnline.Migrations
{
    public partial class MigrationTec : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "26f5a74b-430b-44d5-ac1c-0a269ff5dc19", "AQAAAAEAACcQAAAAEDlSICNcB7ldjJ7omikFq78NreMf6/CyYNKtDESHy3oynuOI9bpyczh1K5K3W2DPsQ==", "279a5db8-b1b4-49c0-bc3b-dd39077fd875" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "d4433e87-7618-47c5-a8cf-e89449266627", "AQAAAAEAACcQAAAAEI9Ok3MsDZ5or3DE4PK9ofXeIb1ClKHQoNpVjyY85t//ZF/QB+Ha2MYDvVz1tD+Bzg==", "11a3a5b7-e906-4f60-9de6-c49768d63c49" });
        }
    }
}
