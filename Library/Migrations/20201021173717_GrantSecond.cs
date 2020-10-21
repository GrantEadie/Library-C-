using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Library.Migrations
{
    public partial class GrantSecond : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CopyCheckoutDate",
                table: "Copies");

            migrationBuilder.AddColumn<DateTime>(
                name: "CopyCheckoutDate",
                table: "CopyPatron",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CopyCheckoutDate",
                table: "CopyPatron");

            migrationBuilder.AddColumn<DateTime>(
                name: "CopyCheckoutDate",
                table: "Copies",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));
        }
    }
}
