using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Renovation.API.Migrations
{
    /// <inheritdoc />
    public partial class SeedingdataforRoomTypes : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "RoomType",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RoomType", x => x.Id);
                });

            migrationBuilder.InsertData(
                table: "RoomType",
                columns: new[] { "Id", "Name" },
                values: new object[,]
                {
                    { new Guid("0b0b7f6b-604e-44aa-a33a-3e8da049e416"), "Kitchen" },
                    { new Guid("0f7dd051-61a2-4dae-960a-bd526526351f"), "Toilet" },
                    { new Guid("557b823d-d64f-4178-b187-4f05759b1d5f"), "Bedroom" },
                    { new Guid("b0eb59b9-7519-4fd2-9b6f-b3cf88ca820a"), "LivingRoom" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "RoomType");
        }
    }
}
