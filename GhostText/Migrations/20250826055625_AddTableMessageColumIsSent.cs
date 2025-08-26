using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace GhostText.Migrations
{
    /// <inheritdoc />
    public partial class AddTableMessageColumIsSent : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "IsSent",
                table: "Messages",
                type: "boolean",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<DateTime>(
                name: "SentAt",
                table: "Messages",
                type: "timestamp with time zone",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "ix_messages_issent_createdate",
                table: "Messages",
                columns: new[] { "IsSent", "CreateDate" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "ix_messages_issent_createdate",
                table: "Messages");

            migrationBuilder.DropColumn(
                name: "IsSent",
                table: "Messages");

            migrationBuilder.DropColumn(
                name: "SentAt",
                table: "Messages");
        }
    }
}
