using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace RocketBlend.Infrastructure.Persistence.Migrations
{
    public partial class Filenametofilepath : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "FileLocation",
                table: "Project",
                newName: "FilePath");

            migrationBuilder.RenameColumn(
                name: "FileLocation",
                table: "Install",
                newName: "FilePath");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "FilePath",
                table: "Project",
                newName: "FileLocation");

            migrationBuilder.RenameColumn(
                name: "FilePath",
                table: "Install",
                newName: "FileLocation");
        }
    }
}
