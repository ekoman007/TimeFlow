using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TimeFlow.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class RoleMigration : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "ModifiedDate",
                table: "Roles",
                newName: "ModifiedOn");

            migrationBuilder.RenameColumn(
                name: "CreatedDate",
                table: "Roles",
                newName: "CreatedOn");

            migrationBuilder.AddColumn<int>(
                name: "Status",
                table: "Roles",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Status",
                table: "Roles");

            migrationBuilder.RenameColumn(
                name: "ModifiedOn",
                table: "Roles",
                newName: "ModifiedDate");

            migrationBuilder.RenameColumn(
                name: "CreatedOn",
                table: "Roles",
                newName: "CreatedDate");
        }
    }
}
