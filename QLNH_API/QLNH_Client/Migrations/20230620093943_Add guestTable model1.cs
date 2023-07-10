using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace QLNH_Client.Migrations
{
    public partial class AddguestTablemodel1 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_GuestTable_Locations_LocationId",
                table: "GuestTable");

            migrationBuilder.DropForeignKey(
                name: "FK_GuestTable_Restaurants_RestaurantId",
                table: "GuestTable");

            migrationBuilder.DropForeignKey(
                name: "FK_GuestTable_Statuses_StatusId",
                table: "GuestTable");

            migrationBuilder.DropPrimaryKey(
                name: "PK_GuestTable",
                table: "GuestTable");

            migrationBuilder.RenameTable(
                name: "GuestTable",
                newName: "GuestTables");

            migrationBuilder.RenameIndex(
                name: "IX_GuestTable_StatusId",
                table: "GuestTables",
                newName: "IX_GuestTables_StatusId");

            migrationBuilder.RenameIndex(
                name: "IX_GuestTable_RestaurantId",
                table: "GuestTables",
                newName: "IX_GuestTables_RestaurantId");

            migrationBuilder.RenameIndex(
                name: "IX_GuestTable_LocationId",
                table: "GuestTables",
                newName: "IX_GuestTables_LocationId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_GuestTables",
                table: "GuestTables",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_GuestTables_Locations_LocationId",
                table: "GuestTables",
                column: "LocationId",
                principalTable: "Locations",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_GuestTables_Restaurants_RestaurantId",
                table: "GuestTables",
                column: "RestaurantId",
                principalTable: "Restaurants",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_GuestTables_Statuses_StatusId",
                table: "GuestTables",
                column: "StatusId",
                principalTable: "Statuses",
                principalColumn: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_GuestTables_Locations_LocationId",
                table: "GuestTables");

            migrationBuilder.DropForeignKey(
                name: "FK_GuestTables_Restaurants_RestaurantId",
                table: "GuestTables");

            migrationBuilder.DropForeignKey(
                name: "FK_GuestTables_Statuses_StatusId",
                table: "GuestTables");

            migrationBuilder.DropPrimaryKey(
                name: "PK_GuestTables",
                table: "GuestTables");

            migrationBuilder.RenameTable(
                name: "GuestTables",
                newName: "GuestTable");

            migrationBuilder.RenameIndex(
                name: "IX_GuestTables_StatusId",
                table: "GuestTable",
                newName: "IX_GuestTable_StatusId");

            migrationBuilder.RenameIndex(
                name: "IX_GuestTables_RestaurantId",
                table: "GuestTable",
                newName: "IX_GuestTable_RestaurantId");

            migrationBuilder.RenameIndex(
                name: "IX_GuestTables_LocationId",
                table: "GuestTable",
                newName: "IX_GuestTable_LocationId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_GuestTable",
                table: "GuestTable",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_GuestTable_Locations_LocationId",
                table: "GuestTable",
                column: "LocationId",
                principalTable: "Locations",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_GuestTable_Restaurants_RestaurantId",
                table: "GuestTable",
                column: "RestaurantId",
                principalTable: "Restaurants",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_GuestTable_Statuses_StatusId",
                table: "GuestTable",
                column: "StatusId",
                principalTable: "Statuses",
                principalColumn: "Id");
        }
    }
}
