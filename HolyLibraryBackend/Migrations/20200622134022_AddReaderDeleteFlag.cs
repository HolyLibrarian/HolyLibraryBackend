using Microsoft.EntityFrameworkCore.Migrations;

namespace HolyLibraryBackend.Migrations
{
    public partial class AddReaderDeleteFlag : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "Deleteflag",
                table: "Users",
                nullable: false,
                defaultValue: false);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Deleteflag",
                table: "Users");
        }
    }
}
