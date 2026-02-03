using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TeamProjectServer.Migrations
{
    /// <inheritdoc />
    public partial class AddPlayerAccountData : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Email",
                table: "playerInits");

            migrationBuilder.DropColumn(
                name: "Password",
                table: "playerInits");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Email",
                table: "playerInits",
                type: "character varying(50)",
                maxLength: 50,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Password",
                table: "playerInits",
                type: "text",
                nullable: false,
                defaultValue: "");
        }
    }
}
