using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace RocketBlend.Infrastructure.Persistence.Migrations;

/// <summary>
/// The create build and install tables.
/// </summary>
public partial class BuildandInstalltables : Migration
{
    /// <inheritdoc />
    protected override void Up(MigrationBuilder migrationBuilder)
    {
        migrationBuilder.CreateTable(
            name: "Build",
            columns: table => new
            {
                Id = table.Column<Guid>(type: "TEXT", nullable: false),
                Name = table.Column<string>(type: "TEXT", maxLength: 256, nullable: false),
                Tag = table.Column<string>(type: "TEXT", maxLength: 256, nullable: false),
                Hash = table.Column<string>(type: "TEXT", maxLength: 256, nullable: false),
                DownloadUrl = table.Column<string>(type: "TEXT", maxLength: 256, nullable: false),
                Filesize = table.Column<string>(type: "TEXT", maxLength: 64, nullable: false),
                CreatedDateTime = table.Column<DateTimeOffset>(type: "TEXT", nullable: true),
                UpdatedDateTime = table.Column<DateTimeOffset>(type: "TEXT", nullable: true)
            },
            constraints: table =>
            {
                table.PrimaryKey("PK_Build", x => x.Id);
            });

        migrationBuilder.CreateTable(
            name: "Install",
            columns: table => new
            {
                Id = table.Column<Guid>(type: "TEXT", nullable: false),
                BuildId = table.Column<Guid>(type: "TEXT", nullable: false),
                FileName = table.Column<string>(type: "TEXT", maxLength: 256, nullable: false),
                FileLocation = table.Column<string>(type: "TEXT", maxLength: 512, nullable: false),
                LaunchArgs = table.Column<string>(type: "TEXT", maxLength: 512, nullable: false),
                CreatedDateTime = table.Column<DateTimeOffset>(type: "TEXT", nullable: true),
                UpdatedDateTime = table.Column<DateTimeOffset>(type: "TEXT", nullable: true)
            },
            constraints: table =>
            {
                table.PrimaryKey("PK_Install", x => x.Id);
                table.ForeignKey(
                    name: "FK_Install_Build_BuildId",
                    column: x => x.BuildId,
                    principalTable: "Build",
                    principalColumn: "Id",
                    onDelete: ReferentialAction.Cascade);
            });

        migrationBuilder.CreateIndex(
            name: "IX_Install_BuildId",
            table: "Install",
            column: "BuildId");
    }

    /// <inheritdoc />
    protected override void Down(MigrationBuilder migrationBuilder)
    {
        migrationBuilder.DropTable(
            name: "Install");

        migrationBuilder.DropTable(
            name: "Build");
    }
}
