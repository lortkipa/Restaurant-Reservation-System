using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Restaurant_Reservation_System.Data.Migrations
{
    /// <inheritdoc />
    public partial class v3 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "DeveloperInfos",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    PersonId = table.Column<int>(type: "int", nullable: false),
                    Role = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    GithubLink = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true),
                    LinkedinLink = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true),
                    PortfolioLink = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DeveloperInfos", x => x.Id);
                    table.ForeignKey(
                        name: "FK_DeveloperInfos_Persons_PersonId",
                        column: x => x.PersonId,
                        principalTable: "Persons",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "DeveloperInfos",
                columns: new[] { "Id", "GithubLink", "LinkedinLink", "PersonId", "PortfolioLink", "Role" },
                values: new object[,]
                {
                    { 1, "https://github.com/lortkipa", "https://www.linkedin.com/in/nikoloz-lortkipanidze-2b4263329/", 1, null, "Team Leader | Full Stack Developer" },
                    { 2, null, null, 2, null, "Full Stack Developer" },
                    { 3, null, null, 3, null, "Full Stack Developer" },
                    { 4, null, null, 4, null, "Full Stack Developer" },
                    { 5, null, null, 5, null, "Front End Developer" }
                });

            migrationBuilder.CreateIndex(
                name: "IX_DeveloperInfos_PersonId",
                table: "DeveloperInfos",
                column: "PersonId",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "DeveloperInfos");
        }
    }
}
