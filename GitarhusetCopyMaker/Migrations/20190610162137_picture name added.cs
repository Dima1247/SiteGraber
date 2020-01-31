using Microsoft.EntityFrameworkCore.Migrations;

namespace GitarhusetCopyMaker.Migrations
{
    public partial class picturenameadded : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Name",
                table: "tblPicture",
                nullable: false,
                defaultValue: "");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Name",
                table: "tblPicture");
        }
    }
}
