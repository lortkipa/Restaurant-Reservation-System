using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Restaurant_Reservation_System.Data.Migrations
{
    /// <inheritdoc />
    public partial class mig_final : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Menus",
                columns: new[] { "Id", "Name", "RestaurantId" },
                values: new object[,]
                {
                    { 1, "Main Dishes", 1 },
                    { 2, "Drinks", 1 },
                    { 3, "Traditional Meals", 2 },
                    { 4, "Wine & Drinks", 2 },
                    { 5, "Garden Specials", 3 },
                    { 6, "Desserts", 3 },
                    { 7, "Lobio & Beans", 4 },
                    { 8, "Extras", 4 },
                    { 9, "Khachapuri", 5 },
                    { 10, "Drinks", 5 },
                    { 11, "Georgian Classics", 6 },
                    { 12, "Drinks", 6 }
                });

            migrationBuilder.InsertData(
                table: "MenuItems",
                columns: new[] { "Id", "IsAvaiable", "MenuId", "Name", "Price" },
                values: new object[,]
                {
                    { 1, true, 1, "Chashushuli", 18.5m },
                    { 2, true, 1, "Ojakhuri", 16.0m },
                    { 3, true, 2, "Red Wine", 12.0m },
                    { 4, true, 2, "Craft Beer", 8.5m },
                    { 5, true, 3, "Kharcho", 20.0m },
                    { 6, true, 3, "Chkmeruli", 19.5m },
                    { 7, true, 4, "White Wine", 13.0m },
                    { 8, true, 4, "Mineral Water", 3.0m },
                    { 9, true, 5, "Mtsvadi", 17.0m },
                    { 10, true, 5, "Badrijani Nigvzit", 11.0m },
                    { 11, true, 6, "Churchkhela", 6.0m },
                    { 12, true, 6, "Honey Cake", 7.5m },
                    { 13, true, 7, "Lobio (Clay Pot)", 9.0m },
                    { 14, true, 7, "Lobio with Mchadi", 11.0m },
                    { 15, true, 8, "Pickled Vegetables", 5.5m },
                    { 16, true, 8, "Cornbread (Mchadi)", 3.5m },
                    { 17, true, 9, "Adjarian Khachapuri", 14.0m },
                    { 18, true, 9, "Imeretian Khachapuri", 12.0m },
                    { 19, true, 10, "Lemonade", 4.0m },
                    { 20, true, 10, "Beer", 6.5m },
                    { 21, true, 11, "Khinkali (10 pcs)", 13.0m },
                    { 22, true, 11, "Chakapuli", 18.0m },
                    { 23, true, 12, "Red Wine", 11.0m },
                    { 24, true, 12, "Cola", 3.0m }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "MenuItems",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "MenuItems",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "MenuItems",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "MenuItems",
                keyColumn: "Id",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "MenuItems",
                keyColumn: "Id",
                keyValue: 5);

            migrationBuilder.DeleteData(
                table: "MenuItems",
                keyColumn: "Id",
                keyValue: 6);

            migrationBuilder.DeleteData(
                table: "MenuItems",
                keyColumn: "Id",
                keyValue: 7);

            migrationBuilder.DeleteData(
                table: "MenuItems",
                keyColumn: "Id",
                keyValue: 8);

            migrationBuilder.DeleteData(
                table: "MenuItems",
                keyColumn: "Id",
                keyValue: 9);

            migrationBuilder.DeleteData(
                table: "MenuItems",
                keyColumn: "Id",
                keyValue: 10);

            migrationBuilder.DeleteData(
                table: "MenuItems",
                keyColumn: "Id",
                keyValue: 11);

            migrationBuilder.DeleteData(
                table: "MenuItems",
                keyColumn: "Id",
                keyValue: 12);

            migrationBuilder.DeleteData(
                table: "MenuItems",
                keyColumn: "Id",
                keyValue: 13);

            migrationBuilder.DeleteData(
                table: "MenuItems",
                keyColumn: "Id",
                keyValue: 14);

            migrationBuilder.DeleteData(
                table: "MenuItems",
                keyColumn: "Id",
                keyValue: 15);

            migrationBuilder.DeleteData(
                table: "MenuItems",
                keyColumn: "Id",
                keyValue: 16);

            migrationBuilder.DeleteData(
                table: "MenuItems",
                keyColumn: "Id",
                keyValue: 17);

            migrationBuilder.DeleteData(
                table: "MenuItems",
                keyColumn: "Id",
                keyValue: 18);

            migrationBuilder.DeleteData(
                table: "MenuItems",
                keyColumn: "Id",
                keyValue: 19);

            migrationBuilder.DeleteData(
                table: "MenuItems",
                keyColumn: "Id",
                keyValue: 20);

            migrationBuilder.DeleteData(
                table: "MenuItems",
                keyColumn: "Id",
                keyValue: 21);

            migrationBuilder.DeleteData(
                table: "MenuItems",
                keyColumn: "Id",
                keyValue: 22);

            migrationBuilder.DeleteData(
                table: "MenuItems",
                keyColumn: "Id",
                keyValue: 23);

            migrationBuilder.DeleteData(
                table: "MenuItems",
                keyColumn: "Id",
                keyValue: 24);

            migrationBuilder.DeleteData(
                table: "Menus",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Menus",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Menus",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "Menus",
                keyColumn: "Id",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "Menus",
                keyColumn: "Id",
                keyValue: 5);

            migrationBuilder.DeleteData(
                table: "Menus",
                keyColumn: "Id",
                keyValue: 6);

            migrationBuilder.DeleteData(
                table: "Menus",
                keyColumn: "Id",
                keyValue: 7);

            migrationBuilder.DeleteData(
                table: "Menus",
                keyColumn: "Id",
                keyValue: 8);

            migrationBuilder.DeleteData(
                table: "Menus",
                keyColumn: "Id",
                keyValue: 9);

            migrationBuilder.DeleteData(
                table: "Menus",
                keyColumn: "Id",
                keyValue: 10);

            migrationBuilder.DeleteData(
                table: "Menus",
                keyColumn: "Id",
                keyValue: 11);

            migrationBuilder.DeleteData(
                table: "Menus",
                keyColumn: "Id",
                keyValue: 12);
        }
    }
}
