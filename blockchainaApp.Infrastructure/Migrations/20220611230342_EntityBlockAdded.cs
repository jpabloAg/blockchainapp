using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace blockchainaApp.Infrastructure.Migrations
{
    public partial class EntityBlockAdded : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "HashBlock",
                table: "Transactions",
                newName: "BlockId");

            migrationBuilder.CreateTable(
                name: "Block",
                columns: table => new
                {
                    Id = table.Column<string>(type: "text", nullable: false),
                    previousBlockHash = table.Column<string>(type: "text", nullable: true),
                    TimeStamp = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    Height = table.Column<int>(type: "integer", nullable: false),
                    Nonce = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Block", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Transactions_BlockId",
                table: "Transactions",
                column: "BlockId");

            migrationBuilder.AddForeignKey(
                name: "FK_Transactions_Block_BlockId",
                table: "Transactions",
                column: "BlockId",
                principalTable: "Block",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Transactions_Block_BlockId",
                table: "Transactions");

            migrationBuilder.DropTable(
                name: "Block");

            migrationBuilder.DropIndex(
                name: "IX_Transactions_BlockId",
                table: "Transactions");

            migrationBuilder.RenameColumn(
                name: "BlockId",
                table: "Transactions",
                newName: "HashBlock");
        }
    }
}
