using Microsoft.EntityFrameworkCore.Migrations;

namespace Library.Migrations
{
    public partial class TheNumberSix : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Description",
                table: "Books",
                newName: "BookDescription");

            migrationBuilder.AddColumn<int>(
                name: "NumberOfBooks",
                table: "Books",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "NumberOfBooks",
                table: "Books");

            migrationBuilder.RenameColumn(
                name: "BookDescription",
                table: "Books",
                newName: "Description");
        }
    }
}
