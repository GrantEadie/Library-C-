using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Library.Migrations
{
    public partial class SeventhInningStretch : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_BookCopy_Patrons_PatronId",
                table: "BookCopy");

            migrationBuilder.DropForeignKey(
                name: "FK_BookCopy_Patrons_PatronId1",
                table: "BookCopy");

            migrationBuilder.DropIndex(
                name: "IX_BookCopy_PatronId",
                table: "BookCopy");

            migrationBuilder.DropIndex(
                name: "IX_BookCopy_PatronId1",
                table: "BookCopy");

            migrationBuilder.DropColumn(
                name: "PatronId",
                table: "BookCopy");

            migrationBuilder.DropColumn(
                name: "PatronId1",
                table: "BookCopy");

            migrationBuilder.AddColumn<DateTime>(
                name: "CopyCheckoutDate",
                table: "Copies",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CopyCheckoutDate",
                table: "Copies");

            migrationBuilder.AddColumn<int>(
                name: "PatronId",
                table: "BookCopy",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "PatronId1",
                table: "BookCopy",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_BookCopy_PatronId",
                table: "BookCopy",
                column: "PatronId");

            migrationBuilder.CreateIndex(
                name: "IX_BookCopy_PatronId1",
                table: "BookCopy",
                column: "PatronId1");

            migrationBuilder.AddForeignKey(
                name: "FK_BookCopy_Patrons_PatronId",
                table: "BookCopy",
                column: "PatronId",
                principalTable: "Patrons",
                principalColumn: "PatronId",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_BookCopy_Patrons_PatronId1",
                table: "BookCopy",
                column: "PatronId1",
                principalTable: "Patrons",
                principalColumn: "PatronId",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
