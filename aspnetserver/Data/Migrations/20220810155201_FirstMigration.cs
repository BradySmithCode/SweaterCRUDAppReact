using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace aspnetserver.Data.Migrations
{
    public partial class FirstMigration : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Sweaters",
                columns: table => new
                {
                    SweaterId = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    manufacturer = table.Column<string>(type: "TEXT", maxLength: 25, nullable: false),
                    quantity = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Sweaters", x => x.SweaterId);
                });

            migrationBuilder.InsertData(
                table: "Sweaters",
                columns: new[] { "SweaterId", "manufacturer", "quantity" },
                values: new object[] { 1, "Sweater 1", 1 });

            migrationBuilder.InsertData(
                table: "Sweaters",
                columns: new[] { "SweaterId", "manufacturer", "quantity" },
                values: new object[] { 2, "Sweater 2", 2 });

            migrationBuilder.InsertData(
                table: "Sweaters",
                columns: new[] { "SweaterId", "manufacturer", "quantity" },
                values: new object[] { 3, "Sweater 3", 3 });

            migrationBuilder.InsertData(
                table: "Sweaters",
                columns: new[] { "SweaterId", "manufacturer", "quantity" },
                values: new object[] { 4, "Sweater 4", 4 });

            migrationBuilder.InsertData(
                table: "Sweaters",
                columns: new[] { "SweaterId", "manufacturer", "quantity" },
                values: new object[] { 5, "Sweater 5", 5 });

            migrationBuilder.InsertData(
                table: "Sweaters",
                columns: new[] { "SweaterId", "manufacturer", "quantity" },
                values: new object[] { 6, "Sweater 6", 6 });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Sweaters");
        }
    }
}
