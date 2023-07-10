using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace QLNH_Client.Migrations
{
    public partial class Addguestmodel1 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CreatedBy",
                table: "Guests");

            migrationBuilder.DropColumn(
                name: "UpdatedBy",
                table: "Guests");

            migrationBuilder.AddColumn<int>(
                name: "CreatedById",
                table: "Guests",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "UpdatedById",
                table: "Guests",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Guests_CreatedById",
                table: "Guests",
                column: "CreatedById");

            migrationBuilder.CreateIndex(
                name: "IX_Guests_UpdatedById",
                table: "Guests",
                column: "UpdatedById");

            migrationBuilder.AddForeignKey(
                name: "FK_Guests_Users_CreatedById",
                table: "Guests",
                column: "CreatedById",
                principalTable: "Users",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Guests_Users_UpdatedById",
                table: "Guests",
                column: "UpdatedById",
                principalTable: "Users",
                principalColumn: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Guests_Users_CreatedById",
                table: "Guests");

            migrationBuilder.DropForeignKey(
                name: "FK_Guests_Users_UpdatedById",
                table: "Guests");

            migrationBuilder.DropIndex(
                name: "IX_Guests_CreatedById",
                table: "Guests");

            migrationBuilder.DropIndex(
                name: "IX_Guests_UpdatedById",
                table: "Guests");

            migrationBuilder.DropColumn(
                name: "CreatedById",
                table: "Guests");

            migrationBuilder.DropColumn(
                name: "UpdatedById",
                table: "Guests");

            migrationBuilder.AddColumn<int>(
                name: "CreatedBy",
                table: "Guests",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "UpdatedBy",
                table: "Guests",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }
    }
}
