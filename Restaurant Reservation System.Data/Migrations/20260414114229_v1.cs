using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Restaurant_Reservation_System.Data.Migrations
{
    /// <inheritdoc />
    public partial class v1 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Persons",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    FirstName = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    LastName = table.Column<string>(type: "nvarchar(30)", maxLength: 30, nullable: false),
                    Phone = table.Column<string>(type: "nvarchar(15)", maxLength: 15, nullable: false),
                    Address = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Persons", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Restaurants",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(25)", maxLength: 25, nullable: false),
                    Location = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Description = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Email = table.Column<string>(type: "nvarchar(254)", maxLength: 254, nullable: false),
                    TotalTables = table.Column<int>(type: "int", nullable: false),
                    SeatsPerTable = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Restaurants", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Roles",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Roles", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Statuses",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(25)", maxLength: 25, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Statuses", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Users",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    PersonId = table.Column<int>(type: "int", nullable: false),
                    Username = table.Column<string>(type: "nvarchar(25)", maxLength: 25, nullable: false),
                    PasswordHash = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Email = table.Column<string>(type: "nvarchar(254)", maxLength: 254, nullable: false),
                    RegistrationDate = table.Column<DateTime>(type: "date", nullable: false),
                    ImageUrl = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Users_Persons_PersonId",
                        column: x => x.PersonId,
                        principalTable: "Persons",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Menus",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    RestaurantId = table.Column<int>(type: "int", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(25)", maxLength: 25, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Menus", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Menus_Restaurants_RestaurantId",
                        column: x => x.RestaurantId,
                        principalTable: "Restaurants",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "EmailJSConfigs",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserId = table.Column<int>(type: "int", nullable: false),
                    ServiceId = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    TemplateId = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    PublicKey = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EmailJSConfigs", x => x.Id);
                    table.ForeignKey(
                        name: "FK_EmailJSConfigs_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Reservations",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CustomerId = table.Column<int>(type: "int", nullable: false),
                    RestaurantId = table.Column<int>(type: "int", nullable: false),
                    StatusId = table.Column<int>(type: "int", nullable: false),
                    Date = table.Column<DateTime>(type: "datetime", nullable: false),
                    TableNumber = table.Column<int>(type: "int", nullable: false),
                    GuestCount = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Reservations", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Reservations_Restaurants_RestaurantId",
                        column: x => x.RestaurantId,
                        principalTable: "Restaurants",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Reservations_Statuses_StatusId",
                        column: x => x.StatusId,
                        principalTable: "Statuses",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Reservations_Users_CustomerId",
                        column: x => x.CustomerId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "RoleUsers",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserId = table.Column<int>(type: "int", nullable: false),
                    RoleId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RoleUsers", x => x.Id);
                    table.ForeignKey(
                        name: "FK_RoleUsers_Roles_RoleId",
                        column: x => x.RoleId,
                        principalTable: "Roles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_RoleUsers_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "MenuItems",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    MenuId = table.Column<int>(type: "int", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(25)", maxLength: 25, nullable: false),
                    Price = table.Column<decimal>(type: "decimal(6,2)", nullable: false),
                    IsAvaiable = table.Column<bool>(type: "bit", nullable: false),
                    ImageUrl = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MenuItems", x => x.Id);
                    table.ForeignKey(
                        name: "FK_MenuItems_Menus_MenuId",
                        column: x => x.MenuId,
                        principalTable: "Menus",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "Persons",
                columns: new[] { "Id", "Address", "FirstName", "LastName", "Phone" },
                values: new object[,]
                {
                    { 1, "Near Lisi Lake", "Nikoloz", "Lortkipanidze", "577711701" },
                    { 2, "Address #2", "Temo", "Totoshvili", "577711702" },
                    { 3, "Address #3", "Davit", "Papava", "577711703" },
                    { 4, "Address #4", "Demetre", "Kvirikashvili", "577711704" },
                    { 5, "Address #5", "Saba", "Dolidze", "577711705" }
                });

            migrationBuilder.InsertData(
                table: "Restaurants",
                columns: new[] { "Id", "Description", "Email", "Location", "Name", "SeatsPerTable", "TotalTables" },
                values: new object[,]
                {
                    { 1, "Modern Georgian cuisine with creative twists and a cozy, artsy atmosphere.", "shavilomi@example.com", "Tbilisi, 30 Zurab Kvlividze St", "Shavi Lomi", 2, 4 },
                    { 2, "Historic recipes from a 19th-century cookbook served in an elegant setting.", "barbarestan@example.com", "Tbilisi, 132 Davit Aghmashenebeli Ave", "Barbarestan", 3, 12 },
                    { 3, "Charming garden restaurant known for traditional dishes and romantic vibes.", "ketoandkote@example.com", "Tbilisi, 3 Mikheil Zandukeli St", "Keto and Kote", 5, 10 },
                    { 4, "Famous for authentic lobio and rustic Georgian comfort food.", "salobie@example.com", "Mtskheta, 1 Samtavro St", "Salobie Bia", 2, 8 },
                    { 5, "Casual spot popular for Adjarian khachapuri and local favorites.", "machakhela@example.com", "Batumi, 26 May 6 St", "Machakhela", 4, 3 },
                    { 6, "Cozy restaurant offering classic Georgian meals in a warm setting.", "heartofbatumi@example.com", "Batumi, 11 Gen. Mazniashvili St", "Heart of Batumi", 4, 12 }
                });

            migrationBuilder.InsertData(
                table: "Roles",
                columns: new[] { "Id", "Name" },
                values: new object[,]
                {
                    { 1, "Admin" },
                    { 2, "Worker" },
                    { 3, "Customer" }
                });

            migrationBuilder.InsertData(
                table: "Statuses",
                columns: new[] { "Id", "Name" },
                values: new object[,]
                {
                    { 1, "Pending" },
                    { 2, "Confirmed" },
                    { 3, "Canceled" }
                });

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
                table: "Users",
                columns: new[] { "Id", "Email", "ImageUrl", "PasswordHash", "PersonId", "RegistrationDate", "Username" },
                values: new object[,]
                {
                    { 1, "nikusha191208@gmail.com", "uploads/users/nikoloz-lortkipanidze.jpg", "jGl25bVBBBW96Qi9Te4V37Fnqchz/Eu4qB9vKrRIqRg=", 1, new DateTime(2026, 3, 20, 0, 0, 0, 0, DateTimeKind.Unspecified), "NikolozLortki" },
                    { 2, "totoshvili@gmail.com", null, "jGl25bVBBBW96Qi9Te4V37Fnqchz/Eu4qB9vKrRIqRg=", 2, new DateTime(2026, 3, 20, 0, 0, 0, 0, DateTimeKind.Unspecified), "Temo_totoshvili" },
                    { 3, "papava@gmail.com", null, "jGl25bVBBBW96Qi9Te4V37Fnqchz/Eu4qB9vKrRIqRg=", 3, new DateTime(2026, 3, 20, 0, 0, 0, 0, DateTimeKind.Unspecified), "DatoPapava" },
                    { 4, "kvirrik@gmail.com", null, "jGl25bVBBBW96Qi9Te4V37Fnqchz/Eu4qB9vKrRIqRg=", 4, new DateTime(2026, 3, 20, 0, 0, 0, 0, DateTimeKind.Unspecified), "Kvirrik" },
                    { 5, "SabaDolidze@gmail.com", null, "jGl25bVBBBW96Qi9Te4V37Fnqchz/Eu4qB9vKrRIqRg=", 5, new DateTime(2026, 3, 20, 0, 0, 0, 0, DateTimeKind.Unspecified), "SabaDolidze" }
                });

            migrationBuilder.InsertData(
                table: "EmailJSConfigs",
                columns: new[] { "Id", "PublicKey", "ServiceId", "TemplateId", "UserId" },
                values: new object[,]
                {
                    { 1, "90LyXpeSeVnNPQeFJ", "service_kqw395h", "template_75iei9r", 1 },
                    { 2, null, null, null, 2 },
                    { 3, null, null, null, 3 },
                    { 4, null, null, null, 4 },
                    { 5, null, null, null, 5 }
                });

            migrationBuilder.InsertData(
                table: "MenuItems",
                columns: new[] { "Id", "ImageUrl", "IsAvaiable", "MenuId", "Name", "Price" },
                values: new object[,]
                {
                    { 1, null, true, 1, "Chashushuli", 18.5m },
                    { 2, null, true, 1, "Ojakhuri", 16.0m },
                    { 3, null, true, 2, "Red Wine", 12.0m },
                    { 4, null, true, 2, "Craft Beer", 8.5m },
                    { 5, null, true, 3, "Kharcho", 20.0m },
                    { 6, null, false, 3, "Chkmeruli", 19.5m },
                    { 7, null, false, 4, "White Wine", 13.0m },
                    { 8, null, true, 4, "Mineral Water", 3.0m },
                    { 9, null, true, 5, "Mtsvadi", 17.0m },
                    { 10, null, false, 5, "Badrijani Nigvzit", 11.0m },
                    { 11, null, true, 6, "Churchkhela", 6.0m },
                    { 12, null, true, 6, "Honey Cake", 7.5m },
                    { 13, null, true, 7, "Lobio (Clay Pot)", 9.0m },
                    { 14, null, true, 7, "Lobio with Mchadi", 11.0m },
                    { 15, null, true, 8, "Pickled Vegetables", 5.5m },
                    { 16, null, true, 8, "Cornbread (Mchadi)", 3.5m },
                    { 17, null, true, 9, "Adjarian Khachapuri", 14.0m },
                    { 18, null, true, 9, "Imeretian Khachapuri", 12.0m },
                    { 19, null, true, 10, "Lemonade", 4.0m },
                    { 20, null, true, 10, "Beer", 6.5m },
                    { 21, null, true, 11, "Khinkali (10 pcs)", 13.0m },
                    { 22, null, true, 11, "Chakapuli", 18.0m },
                    { 23, null, true, 12, "Red Wine", 11.0m },
                    { 24, null, true, 12, "Cola", 3.0m }
                });

            migrationBuilder.InsertData(
                table: "RoleUsers",
                columns: new[] { "Id", "RoleId", "UserId" },
                values: new object[,]
                {
                    { 1, 1, 1 },
                    { 2, 1, 2 },
                    { 3, 1, 3 },
                    { 4, 1, 4 },
                    { 5, 1, 5 }
                });

            migrationBuilder.CreateIndex(
                name: "IX_EmailJSConfigs_PublicKey",
                table: "EmailJSConfigs",
                column: "PublicKey",
                unique: true,
                filter: "[PublicKey] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_EmailJSConfigs_ServiceId",
                table: "EmailJSConfigs",
                column: "ServiceId",
                unique: true,
                filter: "[ServiceId] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_EmailJSConfigs_TemplateId",
                table: "EmailJSConfigs",
                column: "TemplateId",
                unique: true,
                filter: "[TemplateId] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_EmailJSConfigs_UserId",
                table: "EmailJSConfigs",
                column: "UserId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_MenuItems_MenuId",
                table: "MenuItems",
                column: "MenuId");

            migrationBuilder.CreateIndex(
                name: "IX_Menus_RestaurantId",
                table: "Menus",
                column: "RestaurantId");

            migrationBuilder.CreateIndex(
                name: "IX_Reservations_CustomerId",
                table: "Reservations",
                column: "CustomerId");

            migrationBuilder.CreateIndex(
                name: "IX_Reservations_RestaurantId",
                table: "Reservations",
                column: "RestaurantId");

            migrationBuilder.CreateIndex(
                name: "IX_Reservations_StatusId",
                table: "Reservations",
                column: "StatusId");

            migrationBuilder.CreateIndex(
                name: "IX_Restaurants_Name",
                table: "Restaurants",
                column: "Name",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Roles_Name",
                table: "Roles",
                column: "Name",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_RoleUsers_RoleId",
                table: "RoleUsers",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "IX_RoleUsers_UserId",
                table: "RoleUsers",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_Statuses_Name",
                table: "Statuses",
                column: "Name",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Users_PersonId",
                table: "Users",
                column: "PersonId",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "EmailJSConfigs");

            migrationBuilder.DropTable(
                name: "MenuItems");

            migrationBuilder.DropTable(
                name: "Reservations");

            migrationBuilder.DropTable(
                name: "RoleUsers");

            migrationBuilder.DropTable(
                name: "Menus");

            migrationBuilder.DropTable(
                name: "Statuses");

            migrationBuilder.DropTable(
                name: "Roles");

            migrationBuilder.DropTable(
                name: "Users");

            migrationBuilder.DropTable(
                name: "Restaurants");

            migrationBuilder.DropTable(
                name: "Persons");
        }
    }
}
