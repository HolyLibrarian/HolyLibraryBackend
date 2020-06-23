using Microsoft.EntityFrameworkCore.Migrations;

namespace HolyLibraryBackend.Migrations
{
    public partial class AddColletionDeleteFlag : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "DeleteFlag",
                table: "Collections",
                nullable: false,
                defaultValue: false);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "DeleteFlag",
                table: "Collections");
        }
    }
}
