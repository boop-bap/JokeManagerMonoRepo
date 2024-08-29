using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace JokeAPI.Migrations
{
    /// <inheritdoc />
    public partial class RemoveDisplayNamee : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "DisplayName",
                table: "Users");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "DisplayName",
                table: "Users",
                type: "text",
                nullable: false,
                defaultValue: "");
        }
    }
}
