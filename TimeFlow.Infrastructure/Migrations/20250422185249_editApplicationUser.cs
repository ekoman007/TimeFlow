using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TimeFlow.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class editApplicationUser : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "IsBussines",
                table: "ApplicationUsers",
                type: "bit",
                nullable: false,
                defaultValue: false);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsBussines",
                table: "ApplicationUsers");
        }
    }
}
