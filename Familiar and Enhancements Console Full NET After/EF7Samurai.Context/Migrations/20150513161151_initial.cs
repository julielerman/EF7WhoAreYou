using System.Collections.Generic;
using Microsoft.Data.Entity.Relational.Migrations;
using Microsoft.Data.Entity.Relational.Migrations.Builders;
using Microsoft.Data.Entity.Relational.Migrations.Operations;

namespace EF7Samurai.Context.Migrations
{
    public partial class initial : Migration
    {
        public override void Up(MigrationBuilder migration)
        {
            migration.CreateSequence(
                name: "DefaultSequence",
                type: "bigint",
                startWith: 1L,
                incrementBy: 10);
            migration.CreateTable(
                name: "Battle",
                columns: table => new
                {
                    EndDate = table.Column(type: "datetime2", nullable: false),
                    Id = table.Column(type: "int", nullable: false),
                    Name = table.Column(type: "nvarchar(max)", nullable: true),
                    StartDate = table.Column(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Battle", x => x.Id);
                });
            migration.CreateTable(
                name: "Samurai",
                columns: table => new
                {
                    Id = table.Column(type: "int", nullable: false),
                    Name = table.Column(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Samurai", x => x.Id);
                });
            migration.CreateTable(
                name: "Quote",
                columns: table => new
                {
                    Id = table.Column(type: "int", nullable: false),
                    SamuraiId = table.Column(type: "int", nullable: false),
                    Text = table.Column(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Quote", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Quote_Samurai_SamuraiId",
                        columns: x => x.SamuraiId,
                        referencedTable: "Samurai",
                        referencedColumn: "Id");
                });
        }
        
        public override void Down(MigrationBuilder migration)
        {
            migration.DropSequence("DefaultSequence");
            migration.DropTable("Battle");
            migration.DropTable("Quote");
            migration.DropTable("Samurai");
        }
    }
}
