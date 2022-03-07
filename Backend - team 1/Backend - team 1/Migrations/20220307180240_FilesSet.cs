using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Backend___team_1.Migrations
{
    public partial class FilesSet : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "CoverImageId",
                table: "Workshops",
                type: "text",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "PresentationId",
                table: "Workshops",
                type: "text",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "PhotoId",
                table: "UserProfiles",
                type: "text",
                nullable: false,
                defaultValue: "");

            migrationBuilder.CreateTable(
                name: "Files",
                columns: table => new
                {
                    Id = table.Column<string>(type: "text", nullable: false),
                    Name = table.Column<string>(type: "text", nullable: false),
                    Path = table.Column<string>(type: "text", nullable: false),
                    Extension = table.Column<string>(type: "text", nullable: false),
                    Size = table.Column<long>(type: "bigint", nullable: false),
                    Created = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    Updated = table.Column<DateTime>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Files", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Workshops_CoverImageId",
                table: "Workshops",
                column: "CoverImageId");

            migrationBuilder.CreateIndex(
                name: "IX_Workshops_PresentationId",
                table: "Workshops",
                column: "PresentationId");

            migrationBuilder.CreateIndex(
                name: "IX_UserProfiles_PhotoId",
                table: "UserProfiles",
                column: "PhotoId");

            migrationBuilder.AddForeignKey(
                name: "FK_UserProfiles_Files_PhotoId",
                table: "UserProfiles",
                column: "PhotoId",
                principalTable: "Files",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Workshops_Files_CoverImageId",
                table: "Workshops",
                column: "CoverImageId",
                principalTable: "Files",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Workshops_Files_PresentationId",
                table: "Workshops",
                column: "PresentationId",
                principalTable: "Files",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_UserProfiles_Files_PhotoId",
                table: "UserProfiles");

            migrationBuilder.DropForeignKey(
                name: "FK_Workshops_Files_CoverImageId",
                table: "Workshops");

            migrationBuilder.DropForeignKey(
                name: "FK_Workshops_Files_PresentationId",
                table: "Workshops");

            migrationBuilder.DropTable(
                name: "Files");

            migrationBuilder.DropIndex(
                name: "IX_Workshops_CoverImageId",
                table: "Workshops");

            migrationBuilder.DropIndex(
                name: "IX_Workshops_PresentationId",
                table: "Workshops");

            migrationBuilder.DropIndex(
                name: "IX_UserProfiles_PhotoId",
                table: "UserProfiles");

            migrationBuilder.DropColumn(
                name: "CoverImageId",
                table: "Workshops");

            migrationBuilder.DropColumn(
                name: "PresentationId",
                table: "Workshops");

            migrationBuilder.DropColumn(
                name: "PhotoId",
                table: "UserProfiles");
        }
    }
}
