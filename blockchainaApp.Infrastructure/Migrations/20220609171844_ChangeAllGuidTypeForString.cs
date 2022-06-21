using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace blockchainaApp.Infrastructure.Migrations
{
    public partial class ChangeAllGuidTypeForString : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
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

            migrationBuilder.AlterColumn<string>(
                name: "WalletReceiver",
                table: "Transactions",
                type: "text",
                nullable: true,
                oldClrType: typeof(Guid),
                oldType: "uuid");

            migrationBuilder.AlterColumn<string>(
                name: "WalletId",
                table: "Transactions",
                type: "text",
                nullable: true,
                oldClrType: typeof(Guid),
                oldType: "uuid");

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
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Transactions_Wallets_WalletId",
                table: "Transactions");

            migrationBuilder.DropIndex(
                name: "IX_Transactions_WalletId",
                table: "Transactions");

            migrationBuilder.AlterColumn<Guid>(
                name: "WalletReceiver",
                table: "Transactions",
                type: "uuid",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"),
                oldClrType: typeof(string),
                oldType: "text",
                oldNullable: true);

            migrationBuilder.AlterColumn<Guid>(
                name: "WalletId",
                table: "Transactions",
                type: "uuid",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"),
                oldClrType: typeof(string),
                oldType: "text",
                oldNullable: true);

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
    }
}
