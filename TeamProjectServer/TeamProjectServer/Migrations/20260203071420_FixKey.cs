using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace TeamProjectServer.Migrations
{
    /// <inheritdoc />
    public partial class FixKey : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_playerAccountData_playerInits_PlayerDataID",
                table: "playerAccountData");

            migrationBuilder.DropIndex(
                name: "IX_playerAccountData_PlayerDataID",
                table: "playerAccountData");

            migrationBuilder.DropColumn(
                name: "PlayerDataID",
                table: "playerAccountData");

            migrationBuilder.AlterColumn<int>(
                name: "ID",
                table: "Weapon",
                type: "integer",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "integer")
                .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);

            migrationBuilder.AlterColumn<int>(
                name: "ID",
                table: "stages",
                type: "integer",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "integer")
                .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);

            migrationBuilder.AlterColumn<int>(
                name: "ID",
                table: "skills",
                type: "integer",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "integer")
                .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);

            migrationBuilder.AlterColumn<int>(
                name: "ID",
                table: "playerInits",
                type: "integer",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "integer")
                .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);

            migrationBuilder.AddColumn<float>(
                name: "ATKPower",
                table: "playerAccountData",
                type: "real",
                nullable: false,
                defaultValue: 0f);

            migrationBuilder.AddColumn<float>(
                name: "ATKSpeed",
                table: "playerAccountData",
                type: "real",
                nullable: false,
                defaultValue: 0f);

            migrationBuilder.AddColumn<float>(
                name: "CriticalDamage",
                table: "playerAccountData",
                type: "real",
                nullable: false,
                defaultValue: 0f);

            migrationBuilder.AddColumn<float>(
                name: "CriticalRate",
                table: "playerAccountData",
                type: "real",
                nullable: false,
                defaultValue: 0f);

            migrationBuilder.AddColumn<float>(
                name: "EXPMultiplier",
                table: "playerAccountData",
                type: "real",
                nullable: false,
                defaultValue: 0f);

            migrationBuilder.AddColumn<float>(
                name: "GoldMultiplier",
                table: "playerAccountData",
                type: "real",
                nullable: false,
                defaultValue: 0f);

            migrationBuilder.AddColumn<float>(
                name: "HPRegenPerSec",
                table: "playerAccountData",
                type: "real",
                nullable: false,
                defaultValue: 0f);

            migrationBuilder.AddColumn<DateTime>(
                name: "LastLoginTime",
                table: "playerAccountData",
                type: "timestamp with time zone",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<int>(
                name: "Level",
                table: "playerAccountData",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<float>(
                name: "MPRegenPerSec",
                table: "playerAccountData",
                type: "real",
                nullable: false,
                defaultValue: 0f);

            migrationBuilder.AddColumn<float>(
                name: "MaxHP",
                table: "playerAccountData",
                type: "real",
                nullable: false,
                defaultValue: 0f);

            migrationBuilder.AddColumn<float>(
                name: "MaxMP",
                table: "playerAccountData",
                type: "real",
                nullable: false,
                defaultValue: 0f);

            migrationBuilder.AddColumn<float>(
                name: "MoveSpeed",
                table: "playerAccountData",
                type: "real",
                nullable: false,
                defaultValue: 0f);

            migrationBuilder.AddColumn<string>(
                name: "Name",
                table: "playerAccountData",
                type: "text",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<int>(
                name: "Tier",
                table: "playerAccountData",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AlterColumn<int>(
                name: "ID",
                table: "artifacts",
                type: "integer",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "integer")
                .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);

            migrationBuilder.AlterColumn<int>(
                name: "ID",
                table: "accessorys",
                type: "integer",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "integer")
                .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ATKPower",
                table: "playerAccountData");

            migrationBuilder.DropColumn(
                name: "ATKSpeed",
                table: "playerAccountData");

            migrationBuilder.DropColumn(
                name: "CriticalDamage",
                table: "playerAccountData");

            migrationBuilder.DropColumn(
                name: "CriticalRate",
                table: "playerAccountData");

            migrationBuilder.DropColumn(
                name: "EXPMultiplier",
                table: "playerAccountData");

            migrationBuilder.DropColumn(
                name: "GoldMultiplier",
                table: "playerAccountData");

            migrationBuilder.DropColumn(
                name: "HPRegenPerSec",
                table: "playerAccountData");

            migrationBuilder.DropColumn(
                name: "LastLoginTime",
                table: "playerAccountData");

            migrationBuilder.DropColumn(
                name: "Level",
                table: "playerAccountData");

            migrationBuilder.DropColumn(
                name: "MPRegenPerSec",
                table: "playerAccountData");

            migrationBuilder.DropColumn(
                name: "MaxHP",
                table: "playerAccountData");

            migrationBuilder.DropColumn(
                name: "MaxMP",
                table: "playerAccountData");

            migrationBuilder.DropColumn(
                name: "MoveSpeed",
                table: "playerAccountData");

            migrationBuilder.DropColumn(
                name: "Name",
                table: "playerAccountData");

            migrationBuilder.DropColumn(
                name: "Tier",
                table: "playerAccountData");

            migrationBuilder.AlterColumn<int>(
                name: "ID",
                table: "Weapon",
                type: "integer",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "integer")
                .OldAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);

            migrationBuilder.AlterColumn<int>(
                name: "ID",
                table: "stages",
                type: "integer",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "integer")
                .OldAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);

            migrationBuilder.AlterColumn<int>(
                name: "ID",
                table: "skills",
                type: "integer",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "integer")
                .OldAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);

            migrationBuilder.AlterColumn<int>(
                name: "ID",
                table: "playerInits",
                type: "integer",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "integer")
                .OldAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);

            migrationBuilder.AddColumn<int>(
                name: "PlayerDataID",
                table: "playerAccountData",
                type: "integer",
                nullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "ID",
                table: "artifacts",
                type: "integer",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "integer")
                .OldAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);

            migrationBuilder.AlterColumn<int>(
                name: "ID",
                table: "accessorys",
                type: "integer",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "integer")
                .OldAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);

            migrationBuilder.CreateIndex(
                name: "IX_playerAccountData_PlayerDataID",
                table: "playerAccountData",
                column: "PlayerDataID");

            migrationBuilder.AddForeignKey(
                name: "FK_playerAccountData_playerInits_PlayerDataID",
                table: "playerAccountData",
                column: "PlayerDataID",
                principalTable: "playerInits",
                principalColumn: "ID");
        }
    }
}
