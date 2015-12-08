using System;
using System.Collections.Generic;
using Microsoft.Data.Entity.Migrations;

namespace EF7Samurai.Context.RC.Migrations
{
    public partial class newStringInSamurai : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(name: "FK_Quote_Samurai_SamuraiId", table: "Quote");
            migrationBuilder.DropForeignKey(name: "FK_SamuraiBattle_Battle_BattleId", table: "SamuraiBattle");
            migrationBuilder.DropForeignKey(name: "FK_SamuraiBattle_Samurai_SamuraiId", table: "SamuraiBattle");
            migrationBuilder.DropForeignKey(name: "FK_SecretIdentity_Samurai_SamuraiId", table: "SecretIdentity");
            migrationBuilder.DropForeignKey(name: "FK_Sword_Maker_MakerId", table: "Sword");
            migrationBuilder.AddColumn<string>(
                name: "NewString",
                table: "Samurai",
                nullable: true);
            migrationBuilder.AddForeignKey(
                name: "FK_Quote_Samurai_SamuraiId",
                table: "Quote",
                column: "SamuraiId",
                principalTable: "Samurai",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
            migrationBuilder.AddForeignKey(
                name: "FK_SamuraiBattle_Battle_BattleId",
                table: "SamuraiBattle",
                column: "BattleId",
                principalTable: "Battle",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
            migrationBuilder.AddForeignKey(
                name: "FK_SamuraiBattle_Samurai_SamuraiId",
                table: "SamuraiBattle",
                column: "SamuraiId",
                principalTable: "Samurai",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
            migrationBuilder.AddForeignKey(
                name: "FK_SecretIdentity_Samurai_SamuraiId",
                table: "SecretIdentity",
                column: "SamuraiId",
                principalTable: "Samurai",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
            migrationBuilder.AddForeignKey(
                name: "FK_Sword_Maker_MakerId",
                table: "Sword",
                column: "MakerId",
                principalTable: "Maker",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(name: "FK_Quote_Samurai_SamuraiId", table: "Quote");
            migrationBuilder.DropForeignKey(name: "FK_SamuraiBattle_Battle_BattleId", table: "SamuraiBattle");
            migrationBuilder.DropForeignKey(name: "FK_SamuraiBattle_Samurai_SamuraiId", table: "SamuraiBattle");
            migrationBuilder.DropForeignKey(name: "FK_SecretIdentity_Samurai_SamuraiId", table: "SecretIdentity");
            migrationBuilder.DropForeignKey(name: "FK_Sword_Maker_MakerId", table: "Sword");
            migrationBuilder.DropColumn(name: "NewString", table: "Samurai");
            migrationBuilder.AddForeignKey(
                name: "FK_Quote_Samurai_SamuraiId",
                table: "Quote",
                column: "SamuraiId",
                principalTable: "Samurai",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
            migrationBuilder.AddForeignKey(
                name: "FK_SamuraiBattle_Battle_BattleId",
                table: "SamuraiBattle",
                column: "BattleId",
                principalTable: "Battle",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
            migrationBuilder.AddForeignKey(
                name: "FK_SamuraiBattle_Samurai_SamuraiId",
                table: "SamuraiBattle",
                column: "SamuraiId",
                principalTable: "Samurai",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
            migrationBuilder.AddForeignKey(
                name: "FK_SecretIdentity_Samurai_SamuraiId",
                table: "SecretIdentity",
                column: "SamuraiId",
                principalTable: "Samurai",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
            migrationBuilder.AddForeignKey(
                name: "FK_Sword_Maker_MakerId",
                table: "Sword",
                column: "MakerId",
                principalTable: "Maker",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
