using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Askify.Migrations
{
    /// <inheritdoc />
    public partial class removedProfCoverImgs : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CoverImage",
                table: "EndUsers");

            migrationBuilder.DropColumn(
                name: "ProfImage",
                table: "EndUsers");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "CoverImage",
                table: "EndUsers",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ProfImage",
                table: "EndUsers",
                type: "nvarchar(max)",
                nullable: true);
        }
    }
}
