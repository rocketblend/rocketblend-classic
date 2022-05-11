using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace RocketBlend.Infrastructure.Persistence.Migrations;

/// <summary>
/// The initial migration.
/// </summary>
public partial class Init : Migration
{
    /// <inheritdoc />
    protected override void Up(MigrationBuilder migrationBuilder)
    {
        migrationBuilder.CreateTable(
            name: "Project",
            columns: table => new
            {
                Id = table.Column<Guid>(type: "TEXT", nullable: false),
                Name = table.Column<string>(type: "TEXT", maxLength: 64, nullable: false),
                Path = table.Column<string>(type: "TEXT", maxLength: 255, nullable: false),
                CreatedDateTime = table.Column<DateTimeOffset>(type: "TEXT", nullable: true),
                UpdatedDateTime = table.Column<DateTimeOffset>(type: "TEXT", nullable: true)
            },
            constraints: table =>
            {
                table.PrimaryKey("PK_Project", x => x.Id);
            });
    }

    /// <inheritdoc />
    protected override void Down(MigrationBuilder migrationBuilder)
    {
        migrationBuilder.DropTable(
            name: "Project");
    }
}
