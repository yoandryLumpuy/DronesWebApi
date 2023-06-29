using Microsoft.EntityFrameworkCore.Migrations;

namespace DronesWebApi.Migrations
{
    public partial class InitialModel : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "DroneModels",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Name = table.Column<string>(type: "TEXT", maxLength: 100, nullable: false),
                    LightweightInGrams = table.Column<int>(type: "INTEGER", nullable: false),
                    MiddleweightInGrams = table.Column<int>(type: "INTEGER", nullable: false),
                    CruiserweightInGrams = table.Column<int>(type: "INTEGER", nullable: false),
                    HeavyweightInGrams = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DroneModels", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Drones",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    SerialNumber = table.Column<string>(type: "TEXT", maxLength: 100, nullable: false),
                    ModelId = table.Column<int>(type: "INTEGER", nullable: false),
                    WeightLimitInGrams = table.Column<int>(type: "INTEGER", nullable: false),
                    BatteryCapacityInPercentage = table.Column<int>(type: "INTEGER", nullable: false),
                    State = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Drones", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Drones_DroneModels_ModelId",
                        column: x => x.ModelId,
                        principalTable: "DroneModels",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Medications",
                columns: table => new
                {
                    Code = table.Column<string>(type: "TEXT", maxLength: 100, nullable: false),
                    Name = table.Column<string>(type: "TEXT", maxLength: 100, nullable: false),
                    WeightInGrams = table.Column<int>(type: "INTEGER", nullable: false),
                    Delivered = table.Column<bool>(type: "INTEGER", nullable: false),
                    DroneId = table.Column<int>(type: "INTEGER", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Medications", x => x.Code);
                    table.ForeignKey(
                        name: "FK_Medications_Drones_DroneId",
                        column: x => x.DroneId,
                        principalTable: "Drones",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Drones_ModelId",
                table: "Drones",
                column: "ModelId");

            migrationBuilder.CreateIndex(
                name: "IX_Medications_DroneId",
                table: "Medications",
                column: "DroneId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Medications");

            migrationBuilder.DropTable(
                name: "Drones");

            migrationBuilder.DropTable(
                name: "DroneModels");
        }
    }
}
