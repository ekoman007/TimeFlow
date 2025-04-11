using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TimeFlow.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class changeUserRelationShqip : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_UserDetails_Company_CompanyId",
                table: "UserDetails");

            migrationBuilder.DropIndex(
                name: "IX_UserDetails_CompanyId",
                table: "UserDetails");

            migrationBuilder.DropColumn(
                name: "CompanyId",
                table: "UserDetails");

            migrationBuilder.AddColumn<int>(
                name: "UserDetailsId",
                table: "Company",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Company_UserDetailsId",
                table: "Company",
                column: "UserDetailsId");

            migrationBuilder.AddForeignKey(
                name: "FK_Company_UserDetails_UserDetailsId",
                table: "Company",
                column: "UserDetailsId",
                principalTable: "UserDetails",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Company_UserDetails_UserDetailsId",
                table: "Company");

            migrationBuilder.DropIndex(
                name: "IX_Company_UserDetailsId",
                table: "Company");

            migrationBuilder.DropColumn(
                name: "UserDetailsId",
                table: "Company");

            migrationBuilder.AddColumn<int>(
                name: "CompanyId",
                table: "UserDetails",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_UserDetails_CompanyId",
                table: "UserDetails",
                column: "CompanyId");

            migrationBuilder.AddForeignKey(
                name: "FK_UserDetails_Company_CompanyId",
                table: "UserDetails",
                column: "CompanyId",
                principalTable: "Company",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
