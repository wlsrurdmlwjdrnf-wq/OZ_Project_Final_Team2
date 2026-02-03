using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace TeamProjectServer.Migrations
{
    /// <inheritdoc />
    public partial class PlayerAccountData : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "playerAccountData",
                columns: table => new
                {
                    ID = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Email = table.Column<string>(type: "text", nullable: false),
                    Password = table.Column<string>(type: "text", nullable: false),
                    PlayerDataID = table.Column<int>(type: "integer", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_playerAccountData", x => x.ID);
                    table.ForeignKey(
                        name: "FK_playerAccountData_playerInits_PlayerDataID",
                        column: x => x.PlayerDataID,
                        principalTable: "playerInits",
                        principalColumn: "ID");
                });

            migrationBuilder.CreateIndex(
                name: "IX_playerAccountData_PlayerDataID",
                table: "playerAccountData",
                column: "PlayerDataID");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "playerAccountData");
        }
    }
}
