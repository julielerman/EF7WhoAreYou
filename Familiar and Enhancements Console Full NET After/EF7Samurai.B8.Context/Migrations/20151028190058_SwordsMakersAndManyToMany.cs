using System;
using System.Collections.Generic;
using Microsoft.Data.Entity.Migrations;
using Microsoft.Data.Entity.Metadata;

namespace EF7Samurai.B8.Context.Migrations
{
    public partial class SwordsMakersAndManyToMany : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Maker",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Name = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Maker", x => x.Id);
                });
            migrationBuilder.CreateTable(
                name: "SamuraiBattle",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    BattleId = table.Column<int>(nullable: false),
                    DateJoined = table.Column<DateTime>(nullable: false),
                    SamuraiId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SamuraiBattle", x => x.Id);
                    table.ForeignKey(
                        name: "FK_SamuraiBattle_Battle_BattleId",
                        column: x => x.BattleId,
                        principalTable: "Battle",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_SamuraiBattle_Samurai_SamuraiId",
                        column: x => x.SamuraiId,
                        principalTable: "Samurai",
                        principalColumn: "Id");
                });
            migrationBuilder.CreateTable(
                name: "Sword",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    MakerId = table.Column<int>(nullable: false),
                    SamuraId = table.Column<int>(nullable: false),
                    SamuraiId = table.Column<int>(nullable: true),
                    WeightGrams = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Sword", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Sword_Maker_MakerId",
                        column: x => x.MakerId,
                        principalTable: "Maker",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Sword_Samurai_SamuraiId",
                        column: x => x.SamuraiId,
                        principalTable: "Samurai",
                        principalColumn: "Id");
                });
            migrationBuilder.AddColumn<Guid>(
                name: "AlternateKey",
                table: "Samurai",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(name: "AlternateKey", table: "Samurai");
            migrationBuilder.DropTable("SamuraiBattle");
            migrationBuilder.DropTable("Sword");
            migrationBuilder.DropTable("Maker");
        }
    }
}
