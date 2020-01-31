using Microsoft.EntityFrameworkCore.Migrations;

namespace GitarhusetCopyMaker.Migrations
{
    public partial class addedurls : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Url",
                table: "tblProduct",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Url",
                table: "tblCategory",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Url",
                table: "tblProduct");

            migrationBuilder.DropColumn(
                name: "Url",
                table: "tblCategory");
        }
    }
}
