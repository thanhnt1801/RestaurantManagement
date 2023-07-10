using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace QLNH_Client.Migrations
{
    public partial class UpdaterelationshipUserRestaurant : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "UpdatedUserId",
                table: "Restaurants",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Restaurants_UpdatedUserId",
                table: "Restaurants",
                column: "UpdatedUserId");

            migrationBuilder.AddForeignKey(
                name: "FK_Restaurants_Users_UpdatedUserId",
                table: "Restaurants",
                column: "UpdatedUserId",
                principalTable: "Users",
                principalColumn: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Restaurants_Users_UpdatedUserId",
                table: "Restaurants");

            migrationBuilder.DropIndex(
                name: "IX_Restaurants_UpdatedUserId",
                table: "Restaurants");

            migrationBuilder.DropColumn(
                name: "UpdatedUserId",
                table: "Restaurants");
        }
    }
}
