using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace lab5.Migrations
{
    public partial class CreateInitial : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Managers",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    FirstName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    LastName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Location = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Age = table.Column<int>(type: "int", nullable: false),
                    YearsExp = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Managers", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "SuperHeroes",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    FirstName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    LastName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Age = table.Column<int>(type: "int", nullable: false),
                    Place = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ManagerId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SuperHeroes", x => x.Id);
                    table.ForeignKey("Fk_Manager", x => x.ManagerId, "Managers", onUpdate: ReferentialAction.Cascade, onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "SuperPowers",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Type = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    HasAntidote = table.Column<bool>(type: "bit", nullable: false),
                    IsControlled = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SuperPowers", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "SuperHeroPowers",
                columns: table => new
                {
                    SuperHeroId = table.Column<int>(type: "int", nullable: false),
                    SuperPowerId = table.Column<int>(type: "int", nullable: false),
                    Score = table.Column<int>(type: "int", nullable: false),
                    UseTime = table.Column<int>(type: "int", nullable: false),
                    RecoveryTime = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SuperHeroPowers", x => new { x.SuperHeroId, x.SuperPowerId });
                    table.ForeignKey("FK_SuperHero", x => x.SuperHeroId, "SuperHeroes", onUpdate: ReferentialAction.Cascade, onDelete: ReferentialAction.Cascade);
                    table.ForeignKey("FK_SuperPower", x => x.SuperPowerId, "SuperPowers", onUpdate: ReferentialAction.Cascade, onDelete: ReferentialAction.Cascade);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "SuperHeroPowers");

            migrationBuilder.DropTable(
                name: "SuperHeroes");

            migrationBuilder.DropTable(
                name: "Managers");

            migrationBuilder.DropTable(
                name: "SuperPowers");
        }
    }
}
