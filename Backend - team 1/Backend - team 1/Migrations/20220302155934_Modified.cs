using System.Collections.Generic;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Backend___team_1.Migrations
{
    public partial class Modified : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CoverImage",
                table: "Workshops");

            migrationBuilder.DropColumn(
                name: "Presentation",
                table: "Workshops");

            migrationBuilder.DropColumn(
                name: "Role",
                table: "Users");

            migrationBuilder.DropColumn(
                name: "Photo",
                table: "UserProfiles");

            migrationBuilder.AddColumn<List<string>>(
                name: "Roles",
                table: "Users",
                type: "text[]",
                nullable: false);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Roles",
                table: "Users");

            migrationBuilder.AddColumn<string>(
                name: "CoverImage",
                table: "Workshops",
                type: "text",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Presentation",
                table: "Workshops",
                type: "text",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Role",
                table: "Users",
                type: "text",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Photo",
                table: "UserProfiles",
                type: "text",
                nullable: false,
                defaultValue: "");
        }
    }
}
