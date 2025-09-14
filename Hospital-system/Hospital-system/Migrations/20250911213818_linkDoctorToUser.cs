using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Hospital_system.Migrations
{
    /// <inheritdoc />
    public partial class linkDoctorToUser : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "UserId",
                table: "doctors",
                type: "nvarchar(450)",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_doctors_UserId",
                table: "doctors",
                column: "UserId",
                unique: true,
                filter: "[UserId] IS NOT NULL");

            migrationBuilder.AddForeignKey(
                name: "FK_doctors_AspNetUsers_UserId",
                table: "doctors",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_doctors_AspNetUsers_UserId",
                table: "doctors");

            migrationBuilder.DropIndex(
                name: "IX_doctors_UserId",
                table: "doctors");

            migrationBuilder.DropColumn(
                name: "UserId",
                table: "doctors");
        }
    }
}
