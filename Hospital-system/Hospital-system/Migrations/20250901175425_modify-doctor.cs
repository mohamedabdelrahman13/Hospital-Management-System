using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Hospital_system.Migrations
{
    /// <inheritdoc />
    public partial class modifydoctor : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "DoctorID",
                table: "consultationHours",
                type: "nvarchar(450)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.CreateIndex(
                name: "IX_consultationHours_DoctorID",
                table: "consultationHours",
                column: "DoctorID");

            migrationBuilder.AddForeignKey(
                name: "FK_consultationHours_doctors_DoctorID",
                table: "consultationHours",
                column: "DoctorID",
                principalTable: "doctors",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_consultationHours_doctors_DoctorID",
                table: "consultationHours");

            migrationBuilder.DropIndex(
                name: "IX_consultationHours_DoctorID",
                table: "consultationHours");

            migrationBuilder.DropColumn(
                name: "DoctorID",
                table: "consultationHours");
        }
    }
}
