using Microsoft.EntityFrameworkCore.Migrations;

namespace blockchainaApp.Infrastructure.Migrations
{
    public partial class RootHashpropertyAdded : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "RootHash",
                table: "Block",
                type: "text",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "RootHash",
                table: "Block");
        }
    }
}
