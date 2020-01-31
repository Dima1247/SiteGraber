using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace GitarhusetCopyMaker.Migrations
{
    public partial class UpdatedPictures : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "LargeImage",
                table: "Products");

            migrationBuilder.DropColumn(
                name: "MainImage",
                table: "Products");

            migrationBuilder.DropColumn(
                name: "SmallImage",
                table: "Products");

            migrationBuilder.CreateTable(
                name: "Picture",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Url = table.Column<string>(nullable: false),
                    Type = table.Column<int>(nullable: false),
                    Position = table.Column<int>(nullable: false),
                    ProductId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Picture", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Picture_Products_ProductId",
                        column: x => x.ProductId,
                        principalTable: "Products",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Picture_ProductId",
                table: "Picture",
                column: "ProductId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Picture");

            migrationBuilder.AddColumn<string>(
                name: "LargeImage",
                table: "Products",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "MainImage",
                table: "Products",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "SmallImage",
                table: "Products",
                nullable: true);
        }
    }
}
