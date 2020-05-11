using Microsoft.EntityFrameworkCore.Migrations;

namespace HolyLibraryBackend.Migrations
{
    public partial class AddBackBorrowerInCollection : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "BorrowerId",
                table: "Collections",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Collections_BorrowerId",
                table: "Collections",
                column: "BorrowerId");

            migrationBuilder.AddForeignKey(
                name: "FK_Collections_Users_BorrowerId",
                table: "Collections",
                column: "BorrowerId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Collections_Users_BorrowerId",
                table: "Collections");

            migrationBuilder.DropIndex(
                name: "IX_Collections_BorrowerId",
                table: "Collections");

            migrationBuilder.DropColumn(
                name: "BorrowerId",
                table: "Collections");
        }
    }
}
