using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace RocketBlend.Infrastructure.Persistence.Migrations
{
    public partial class Updatedprojectmodel : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Project_Install_InstallId",
                table: "Project");

            migrationBuilder.DropColumn(
                name: "Path",
                table: "Project");

            migrationBuilder.AlterColumn<Guid>(
                name: "InstallId",
                table: "Project",
                type: "TEXT",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"),
                oldClrType: typeof(Guid),
                oldType: "TEXT",
                oldNullable: true);

            migrationBuilder.AddColumn<string>(
                name: "FileLocation",
                table: "Project",
                type: "TEXT",
                maxLength: 512,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "FileName",
                table: "Project",
                type: "TEXT",
                maxLength: 256,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddForeignKey(
                name: "FK_Project_Install_InstallId",
                table: "Project",
                column: "InstallId",
                principalTable: "Install",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Project_Install_InstallId",
                table: "Project");

            migrationBuilder.DropColumn(
                name: "FileLocation",
                table: "Project");

            migrationBuilder.DropColumn(
                name: "FileName",
                table: "Project");

            migrationBuilder.AlterColumn<Guid>(
                name: "InstallId",
                table: "Project",
                type: "TEXT",
                nullable: true,
                oldClrType: typeof(Guid),
                oldType: "TEXT");

            migrationBuilder.AddColumn<string>(
                name: "Path",
                table: "Project",
                type: "TEXT",
                maxLength: 255,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddForeignKey(
                name: "FK_Project_Install_InstallId",
                table: "Project",
                column: "InstallId",
                principalTable: "Install",
                principalColumn: "Id");
        }
    }
}
