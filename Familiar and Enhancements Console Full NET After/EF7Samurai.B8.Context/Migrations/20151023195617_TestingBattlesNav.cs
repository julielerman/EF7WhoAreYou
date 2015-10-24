using System;
using System.Collections.Generic;
using Microsoft.Data.Entity.Migrations;
using Microsoft.Data.Entity.Metadata;

namespace EF7Samurai.B8.Context.Migrations
{
    public partial class TestingBattlesNav : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Location",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Name = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Location", x => x.Id);
                });
            migrationBuilder.AddColumn<int>(
                name: "LocationId",
                table: "Battle",
                nullable: true);
            migrationBuilder.AddForeignKey(
                name: "FK_Battle_Location_LocationId",
                table: "Battle",
                column: "LocationId",
                principalTable: "Location",
                principalColumn: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(name: "FK_Battle_Location_LocationId", table: "Battle");
            migrationBuilder.DropColumn(name: "LocationId", table: "Battle");
            migrationBuilder.DropTable("Location");
        }
    }
}
