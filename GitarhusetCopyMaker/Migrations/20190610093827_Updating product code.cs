using Microsoft.EntityFrameworkCore.Migrations;

namespace GitarhusetCopyMaker.Migrations
{
    public partial class Updatingproductcode : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Article",
                table: "tblProduct");

            migrationBuilder.AddColumn<string>(
                name: "Code",
                table: "tblProduct",
                maxLength: 50,
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Code",
                table: "tblProduct");

            migrationBuilder.AddColumn<string>(
                name: "Article",
                table: "tblProduct",
                maxLength: 50,
                nullable: false,
                defaultValue: "");
        }
    }
}
