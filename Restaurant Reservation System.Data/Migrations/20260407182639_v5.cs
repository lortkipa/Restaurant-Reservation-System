using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Restaurant_Reservation_System.Data.Migrations
{
    /// <inheritdoc />
    public partial class v5 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "EmailJSConfigs",
                columns: new[] { "Id", "PublicKey", "ServiceId", "TemplateId", "UserId" },
                values: new object[,]
                {
                    { 2, null, null, null, 2 },
                    { 3, null, null, null, 3 },
                    { 4, null, null, null, 4 },
                    { 5, null, null, null, 5 }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "EmailJSConfigs",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "EmailJSConfigs",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "EmailJSConfigs",
                keyColumn: "Id",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "EmailJSConfigs",
                keyColumn: "Id",
                keyValue: 5);
        }
    }
}
