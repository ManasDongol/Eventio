using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Eventing.ApiService.Migrations
{
    /// <inheritdoc />
    public partial class RemoveCreatedByUSER : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_events_users_CreatedBy",
                table: "events");

            migrationBuilder.DropIndex(
                name: "IX_events_CreatedBy",
                table: "events");

            migrationBuilder.AddColumn<Guid>(
                name: "UsersId",
                table: "events",
                type: "uuid",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_events_UsersId",
                table: "events",
                column: "UsersId");

            migrationBuilder.AddForeignKey(
                name: "FK_events_users_UsersId",
                table: "events",
                column: "UsersId",
                principalTable: "users",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_events_users_UsersId",
                table: "events");

            migrationBuilder.DropIndex(
                name: "IX_events_UsersId",
                table: "events");

            migrationBuilder.DropColumn(
                name: "UsersId",
                table: "events");

            migrationBuilder.CreateIndex(
                name: "IX_events_CreatedBy",
                table: "events",
                column: "CreatedBy");

            migrationBuilder.AddForeignKey(
                name: "FK_events_users_CreatedBy",
                table: "events",
                column: "CreatedBy",
                principalTable: "users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
