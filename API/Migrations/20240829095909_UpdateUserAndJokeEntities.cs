using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace JokeAPI.Migrations
{
    /// <inheritdoc />
    public partial class UpdateUserAndJokeEntities : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "UserId",
                table: "Jokes",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Jokes_UserId",
                table: "Jokes",
                column: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_Jokes_Users_UserId",
                table: "Jokes",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Jokes_Users_UserId",
                table: "Jokes");

            migrationBuilder.DropIndex(
                name: "IX_Jokes_UserId",
                table: "Jokes");

            migrationBuilder.DropColumn(
                name: "UserId",
                table: "Jokes");
        }
    }
}
