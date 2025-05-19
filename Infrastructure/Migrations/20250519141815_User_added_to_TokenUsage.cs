using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class User_added_to_TokenUsage : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "UserId",
                table: "TokenUsages",
                type: "varchar(255)",
                nullable: false,
                defaultValue: "")
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateIndex(
                name: "IX_TokenUsages_UserId",
                table: "TokenUsages",
                column: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_TokenUsages_AspNetUsers_UserId",
                table: "TokenUsages",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_TokenUsages_AspNetUsers_UserId",
                table: "TokenUsages");

            migrationBuilder.DropIndex(
                name: "IX_TokenUsages_UserId",
                table: "TokenUsages");

            migrationBuilder.DropColumn(
                name: "UserId",
                table: "TokenUsages");
        }
    }
}
