using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class UserRoles : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Book_Users_BookedById",
                table: "Book");

            migrationBuilder.AddColumn<int>(
                name: "Role",
                table: "Users",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddForeignKey(
                name: "FK_Book_Users_BookedById",
                table: "Book",
                column: "BookedById",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Book_Users_BookedById",
                table: "Book");

            migrationBuilder.DropColumn(
                name: "Role",
                table: "Users");

            migrationBuilder.AddForeignKey(
                name: "FK_Book_Users_BookedById",
                table: "Book",
                column: "BookedById",
                principalTable: "Users",
                principalColumn: "Id");
        }
    }
}
