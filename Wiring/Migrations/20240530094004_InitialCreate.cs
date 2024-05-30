using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Wiring.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "HarnessDrawings",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Harness = table.Column<string>(type: "nvarchar(30)", maxLength: 30, nullable: false),
                    Harness_version = table.Column<string>(type: "nvarchar(30)", maxLength: 30, nullable: false),
                    Drawing = table.Column<string>(type: "nvarchar(30)", maxLength: 30, nullable: false),
                    Drawing_version = table.Column<string>(type: "nvarchar(30)", maxLength: 30, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_HarnessDrawings", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "HarnessWires",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Harness_ID = table.Column<int>(type: "int", nullable: false),
                    Length = table.Column<float>(type: "real", nullable: false),
                    Color = table.Column<string>(type: "nvarchar(30)", maxLength: 30, nullable: false),
                    Housing_1 = table.Column<string>(type: "nvarchar(30)", maxLength: 30, nullable: false),
                    Housing_2 = table.Column<string>(type: "nvarchar(30)", maxLength: 30, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_HarnessWires", x => x.ID);
                    table.ForeignKey(
                        name: "FK_HarnessWires_HarnessDrawings_Harness_ID",
                        column: x => x.Harness_ID,
                        principalTable: "HarnessDrawings",
                        principalColumn: "ID");
                });

            migrationBuilder.CreateIndex(
                name: "IX_HarnessWires_Harness_ID",
                table: "HarnessWires",
                column: "Harness_ID");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "HarnessWires");

            migrationBuilder.DropTable(
                name: "HarnessDrawings");
        }
    }
}
