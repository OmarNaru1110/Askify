﻿using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Askify.Migrations
{
    /// <inheritdoc />
    public partial class v1 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Notifications_Answers_AnswerId",
                table: "Notifications");

            migrationBuilder.DropForeignKey(
                name: "FK_Notifications_EndUsers_ReceiverId",
                table: "Notifications");

            migrationBuilder.AddForeignKey(
                name: "FK_Notifications_Answers_AnswerId",
                table: "Notifications",
                column: "AnswerId",
                principalTable: "Answers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Notifications_EndUsers_ReceiverId",
                table: "Notifications",
                column: "ReceiverId",
                principalTable: "EndUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Notifications_Answers_AnswerId",
                table: "Notifications");

            migrationBuilder.DropForeignKey(
                name: "FK_Notifications_EndUsers_ReceiverId",
                table: "Notifications");

            migrationBuilder.AddForeignKey(
                name: "FK_Notifications_Answers_AnswerId",
                table: "Notifications",
                column: "AnswerId",
                principalTable: "Answers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Notifications_EndUsers_ReceiverId",
                table: "Notifications",
                column: "ReceiverId",
                principalTable: "EndUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
