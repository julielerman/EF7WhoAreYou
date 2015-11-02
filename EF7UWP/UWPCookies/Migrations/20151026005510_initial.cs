using System;
using System.Collections.Generic;
using Microsoft.Data.Entity.Migrations;

namespace UWPCookies.Migrations
{
    public partial class initial : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "CookieBinge",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    HowMany = table.Column<int>(nullable: false),
                    TimeOccurred = table.Column<DateTime>(nullable: false),
                    WorthIt = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CookieBinge", x => x.Id);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable("CookieBinge");
        }
    }
}
