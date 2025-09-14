using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Hospital_system.Migrations
{
    /// <inheritdoc />
    public partial class modifyAppointment : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Appointments_doctors_doctorID",
                table: "Appointments");

            migrationBuilder.RenameColumn(
                name: "doctorID",
                table: "Appointments",
                newName: "DoctorUserID");

            migrationBuilder.RenameIndex(
                name: "IX_Appointments_doctorID",
                table: "Appointments",
                newName: "IX_Appointments_DoctorUserID");

            migrationBuilder.AddForeignKey(
                name: "FK_Appointments_AspNetUsers_DoctorUserID",
                table: "Appointments",
                column: "DoctorUserID",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Appointments_AspNetUsers_DoctorUserID",
                table: "Appointments");

            migrationBuilder.RenameColumn(
                name: "DoctorUserID",
                table: "Appointments",
                newName: "doctorID");

            migrationBuilder.RenameIndex(
                name: "IX_Appointments_DoctorUserID",
                table: "Appointments",
                newName: "IX_Appointments_doctorID");

            migrationBuilder.AddForeignKey(
                name: "FK_Appointments_doctors_doctorID",
                table: "Appointments",
                column: "doctorID",
                principalTable: "doctors",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
