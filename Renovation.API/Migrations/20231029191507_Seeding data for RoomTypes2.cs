using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Renovation.API.Migrations
{
    /// <inheritdoc />
    public partial class SeedingdataforRoomTypes2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_RoomType",
                table: "RoomType");

            migrationBuilder.RenameTable(
                name: "RoomType",
                newName: "RoomTypes");

            migrationBuilder.AddPrimaryKey(
                name: "PK_RoomTypes",
                table: "RoomTypes",
                column: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_RoomTypes",
                table: "RoomTypes");

            migrationBuilder.RenameTable(
                name: "RoomTypes",
                newName: "RoomType");

            migrationBuilder.AddPrimaryKey(
                name: "PK_RoomType",
                table: "RoomType",
                column: "Id");
        }
    }
}
