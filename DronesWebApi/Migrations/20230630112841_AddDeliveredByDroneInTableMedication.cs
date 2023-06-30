using Microsoft.EntityFrameworkCore.Migrations;

namespace DronesWebApi.Migrations
{
    public partial class AddDeliveredByDroneInTableMedication : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "DeliveredByDroneId",
                table: "Medications",
                type: "INTEGER",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Medications_DeliveredByDroneId",
                table: "Medications",
                column: "DeliveredByDroneId");

            migrationBuilder.AddForeignKey(
                name: "FK_Medications_Drones_DeliveredByDroneId",
                table: "Medications",
                column: "DeliveredByDroneId",
                principalTable: "Drones",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Medications_Drones_DeliveredByDroneId",
                table: "Medications");

            migrationBuilder.DropIndex(
                name: "IX_Medications_DeliveredByDroneId",
                table: "Medications");

            migrationBuilder.DropColumn(
                name: "DeliveredByDroneId",
                table: "Medications");
        }
    }
}
