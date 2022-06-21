using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace blockchainaApp.Infrastructure.Migrations
{
    public partial class ChangePrimaryKeyType : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Transactions_Wallets_WalletId",
                table: "Transactions");

            migrationBuilder.DropIndex(
                name: "IX_Transactions_WalletId",
                table: "Transactions");

            migrationBuilder.AlterColumn<string>(
                name: "Id",
                table: "Wallets",
                type: "text",
                nullable: false,
                oldClrType: typeof(Guid),
                oldType: "uuid");

            migrationBuilder.AlterColumn<string>(
                name: "Id",
                table: "Transactions",
                type: "text",
                nullable: false,
                oldClrType: typeof(Guid),
                oldType: "uuid");

            migrationBuilder.AddColumn<string>(
                name: "WalletId1",
                table: "Transactions",
                type: "text",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Transactions_WalletId1",
                table: "Transactions",
                column: "WalletId1");

            migrationBuilder.AddForeignKey(
                name: "FK_Transactions_Wallets_WalletId1",
                table: "Transactions",
                column: "WalletId1",
                principalTable: "Wallets",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Transactions_Wallets_WalletId1",
                table: "Transactions");

            migrationBuilder.DropIndex(
                name: "IX_Transactions_WalletId1",
                table: "Transactions");

            migrationBuilder.DropColumn(
                name: "WalletId1",
                table: "Transactions");

            migrationBuilder.AlterColumn<Guid>(
                name: "Id",
                table: "Wallets",
                type: "uuid",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "text");

            migrationBuilder.AlterColumn<Guid>(
                name: "Id",
                table: "Transactions",
                type: "uuid",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "text");

            migrationBuilder.CreateIndex(
                name: "IX_Transactions_WalletId",
                table: "Transactions",
                column: "WalletId");

            migrationBuilder.AddForeignKey(
                name: "FK_Transactions_Wallets_WalletId",
                table: "Transactions",
                column: "WalletId",
                principalTable: "Wallets",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
