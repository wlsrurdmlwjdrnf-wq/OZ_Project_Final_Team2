using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TeamProjectServer.Migrations
{
    /// <inheritdoc />
    public partial class fix : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<float>(
                name: "CurGold",
                table: "playerInits",
                type: "real",
                nullable: false,
                defaultValue: 0f);

            migrationBuilder.AddColumn<float>(
                name: "CurGold",
                table: "playerAccountData",
                type: "real",
                nullable: false,
                defaultValue: 0f);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CurGold",
                table: "playerInits");

            migrationBuilder.DropColumn(
                name: "CurGold",
                table: "playerAccountData");
        }
    }
}
