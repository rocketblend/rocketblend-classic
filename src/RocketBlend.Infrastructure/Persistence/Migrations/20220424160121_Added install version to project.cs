using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace RocketBlend.Infrastructure.Persistence.Migrations;

/// <summary>
/// The added install version to project.
/// </summary>
public partial class Addedinstallversiontoproject : Migration
{
    /// <inheritdoc />
    protected override void Up(MigrationBuilder migrationBuilder)
    {
        migrationBuilder.AddColumn<Guid>(
            name: "InstallId",
            table: "Project",
            type: "TEXT",
            nullable: true);

        migrationBuilder.CreateIndex(
            name: "IX_Project_InstallId",
            table: "Project",
            column: "InstallId");

        migrationBuilder.AddForeignKey(
            name: "FK_Project_Install_InstallId",
            table: "Project",
            column: "InstallId",
            principalTable: "Install",
            principalColumn: "Id");
    }

    /// <inheritdoc />
    protected override void Down(MigrationBuilder migrationBuilder)
    {
        migrationBuilder.DropForeignKey(
            name: "FK_Project_Install_InstallId",
            table: "Project");

        migrationBuilder.DropIndex(
            name: "IX_Project_InstallId",
            table: "Project");

        migrationBuilder.DropColumn(
            name: "InstallId",
            table: "Project");
    }
}
