using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Askify.Migrations
{
    /// <inheritdoc />
    public partial class UserAnswerRelationship : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "ReceiverId",
                table: "Answer",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "SenderId",
                table: "Answer",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Answer_ReceiverId",
                table: "Answer",
                column: "ReceiverId");

            migrationBuilder.CreateIndex(
                name: "IX_Answer_SenderId",
                table: "Answer",
                column: "SenderId");

            migrationBuilder.AddForeignKey(
                name: "FK_Answer_EndUsers_ReceiverId",
                table: "Answer",
                column: "ReceiverId",
                principalTable: "EndUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Answer_EndUsers_SenderId",
                table: "Answer",
                column: "SenderId",
                principalTable: "EndUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Answer_EndUsers_ReceiverId",
                table: "Answer");

            migrationBuilder.DropForeignKey(
                name: "FK_Answer_EndUsers_SenderId",
                table: "Answer");

            migrationBuilder.DropIndex(
                name: "IX_Answer_ReceiverId",
                table: "Answer");

            migrationBuilder.DropIndex(
                name: "IX_Answer_SenderId",
                table: "Answer");

            migrationBuilder.DropColumn(
                name: "ReceiverId",
                table: "Answer");

            migrationBuilder.DropColumn(
                name: "SenderId",
                table: "Answer");
        }
    }
}
