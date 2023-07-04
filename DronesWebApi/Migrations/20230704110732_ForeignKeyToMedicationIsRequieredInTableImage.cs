using Microsoft.EntityFrameworkCore.Migrations;

namespace DronesWebApi.Migrations
{
    public partial class ForeignKeyToMedicationIsRequieredInTableImage : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Images_Medications_MedicationCode",
                table: "Images");

            migrationBuilder.AlterColumn<string>(
                name: "MedicationCode",
                table: "Images",
                type: "TEXT",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "TEXT",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Images_Medications_MedicationCode",
                table: "Images",
                column: "MedicationCode",
                principalTable: "Medications",
                principalColumn: "Code",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Images_Medications_MedicationCode",
                table: "Images");

            migrationBuilder.AlterColumn<string>(
                name: "MedicationCode",
                table: "Images",
                type: "TEXT",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "TEXT");

            migrationBuilder.AddForeignKey(
                name: "FK_Images_Medications_MedicationCode",
                table: "Images",
                column: "MedicationCode",
                principalTable: "Medications",
                principalColumn: "Code",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
