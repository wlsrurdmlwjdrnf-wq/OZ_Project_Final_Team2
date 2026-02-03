using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TeamProjectServer.Migrations
{
    /// <inheritdoc />
    public partial class fixAddIsUnique : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateIndex(
                name: "IX_playerAccountData_Email",
                table: "playerAccountData",
                column: "Email",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_playerAccountData_Email",
                table: "playerAccountData");
        }
    }
}
