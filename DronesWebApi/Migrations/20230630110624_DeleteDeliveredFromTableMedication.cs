using Microsoft.EntityFrameworkCore.Migrations;

namespace DronesWebApi.Migrations
{
    public partial class DeleteDeliveredFromTableMedication : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Delivered",
                table: "Medications");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "Delivered",
                table: "Medications",
                type: "INTEGER",
                nullable: false,
                defaultValue: false);
        }
    }
}
