using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TimeFlow.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class changeTableAtributes : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Address",
                table: "ApplicationUserDetails",
                newName: "ZipCode");

            migrationBuilder.AddColumn<string>(
                name: "City",
                table: "ApplicationUserDetails",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Country",
                table: "ApplicationUserDetails",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Street",
                table: "ApplicationUserDetails",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "City",
                table: "ApplicationUserDetails");

            migrationBuilder.DropColumn(
                name: "Country",
                table: "ApplicationUserDetails");

            migrationBuilder.DropColumn(
                name: "Street",
                table: "ApplicationUserDetails");

            migrationBuilder.RenameColumn(
                name: "ZipCode",
                table: "ApplicationUserDetails",
                newName: "Address");
        }
    }
}
