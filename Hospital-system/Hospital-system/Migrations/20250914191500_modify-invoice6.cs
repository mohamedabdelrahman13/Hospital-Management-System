using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Hospital_system.Migrations
{
    /// <inheritdoc />
    public partial class modifyinvoice6 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_invoices_doctors_DoctorId",
                table: "invoices");

            migrationBuilder.DropIndex(
                name: "IX_invoices_DoctorId",
                table: "invoices");

            migrationBuilder.DropColumn(
                name: "DoctorId",
                table: "invoices");

            migrationBuilder.AddColumn<string>(
                name: "DoctorUserId",
                table: "invoices",
                type: "nvarchar(450)",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_invoices_DoctorUserId",
                table: "invoices",
                column: "DoctorUserId");

            migrationBuilder.AddForeignKey(
                name: "FK_invoices_AspNetUsers_DoctorUserId",
                table: "invoices",
                column: "DoctorUserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_invoices_AspNetUsers_DoctorUserId",
                table: "invoices");

            migrationBuilder.DropIndex(
                name: "IX_invoices_DoctorUserId",
                table: "invoices");

            migrationBuilder.DropColumn(
                name: "DoctorUserId",
                table: "invoices");

            migrationBuilder.AddColumn<string>(
                name: "DoctorId",
                table: "invoices",
                type: "nvarchar(450)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.CreateIndex(
                name: "IX_invoices_DoctorId",
                table: "invoices",
                column: "DoctorId");

            migrationBuilder.AddForeignKey(
                name: "FK_invoices_doctors_DoctorId",
                table: "invoices",
                column: "DoctorId",
                principalTable: "doctors",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
