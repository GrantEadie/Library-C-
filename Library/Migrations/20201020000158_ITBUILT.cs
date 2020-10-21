using Microsoft.EntityFrameworkCore.Migrations;

namespace Library.Migrations
{
    public partial class ITBUILT : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "LastName",
                table: "Patrons",
                newName: "PatronLastName");

            migrationBuilder.RenameColumn(
                name: "FirstName",
                table: "Patrons",
                newName: "PatronFirstName");

            migrationBuilder.RenameColumn(
                name: "Description",
                table: "Genres",
                newName: "GenreName");

            migrationBuilder.RenameColumn(
                name: "Name",
                table: "Copies",
                newName: "UserId");

            migrationBuilder.RenameColumn(
                name: "Name",
                table: "Authors",
                newName: "UserId");

            migrationBuilder.AddColumn<string>(
                name: "GenreDescription",
                table: "Genres",
                nullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "UserId",
                table: "Copies",
                nullable: true,
                oldClrType: typeof(string),
                oldNullable: true);

            migrationBuilder.AddColumn<string>(
                name: "CopyName",
                table: "Copies",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "BookName",
                table: "Books",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "PatronId1",
                table: "BookCopy",
                nullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "UserId",
                table: "Authors",
                nullable: true,
                oldClrType: typeof(string),
                oldNullable: true);

            migrationBuilder.AddColumn<string>(
                name: "AuthorName",
                table: "Authors",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Copies_UserId",
                table: "Copies",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_BookCopy_PatronId1",
                table: "BookCopy",
                column: "PatronId1");

            migrationBuilder.CreateIndex(
                name: "IX_Authors_UserId",
                table: "Authors",
                column: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_Authors_AspNetUsers_UserId",
                table: "Authors",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_BookCopy_Patrons_PatronId1",
                table: "BookCopy",
                column: "PatronId1",
                principalTable: "Patrons",
                principalColumn: "PatronId",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Copies_AspNetUsers_UserId",
                table: "Copies",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Authors_AspNetUsers_UserId",
                table: "Authors");

            migrationBuilder.DropForeignKey(
                name: "FK_BookCopy_Patrons_PatronId1",
                table: "BookCopy");

            migrationBuilder.DropForeignKey(
                name: "FK_Copies_AspNetUsers_UserId",
                table: "Copies");

            migrationBuilder.DropIndex(
                name: "IX_Copies_UserId",
                table: "Copies");

            migrationBuilder.DropIndex(
                name: "IX_BookCopy_PatronId1",
                table: "BookCopy");

            migrationBuilder.DropIndex(
                name: "IX_Authors_UserId",
                table: "Authors");

            migrationBuilder.DropColumn(
                name: "GenreDescription",
                table: "Genres");

            migrationBuilder.DropColumn(
                name: "CopyName",
                table: "Copies");

            migrationBuilder.DropColumn(
                name: "BookName",
                table: "Books");

            migrationBuilder.DropColumn(
                name: "PatronId1",
                table: "BookCopy");

            migrationBuilder.DropColumn(
                name: "AuthorName",
                table: "Authors");

            migrationBuilder.RenameColumn(
                name: "PatronLastName",
                table: "Patrons",
                newName: "LastName");

            migrationBuilder.RenameColumn(
                name: "PatronFirstName",
                table: "Patrons",
                newName: "FirstName");

            migrationBuilder.RenameColumn(
                name: "GenreName",
                table: "Genres",
                newName: "Description");

            migrationBuilder.RenameColumn(
                name: "UserId",
                table: "Copies",
                newName: "Name");

            migrationBuilder.RenameColumn(
                name: "UserId",
                table: "Authors",
                newName: "Name");

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "Copies",
                nullable: true,
                oldClrType: typeof(string),
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "Authors",
                nullable: true,
                oldClrType: typeof(string),
                oldNullable: true);
        }
    }
}
