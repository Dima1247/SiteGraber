using Microsoft.EntityFrameworkCore.Migrations;

namespace GitarhusetCopyMaker.Migrations
{
    public partial class updatingtablesnames : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Categories_Categories_ParentCategoryId",
                table: "Categories");

            migrationBuilder.DropForeignKey(
                name: "FK_Picture_Products_ProductId",
                table: "Picture");

            migrationBuilder.DropForeignKey(
                name: "FK_Products_Categories_CategoryId",
                table: "Products");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Products",
                table: "Products");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Picture",
                table: "Picture");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Categories",
                table: "Categories");

            migrationBuilder.RenameTable(
                name: "Products",
                newName: "tblProduct");

            migrationBuilder.RenameTable(
                name: "Picture",
                newName: "tblPicture");

            migrationBuilder.RenameTable(
                name: "Categories",
                newName: "tblCategory");

            migrationBuilder.RenameIndex(
                name: "IX_Products_CategoryId",
                table: "tblProduct",
                newName: "IX_tblProduct_CategoryId");

            migrationBuilder.RenameIndex(
                name: "IX_Picture_ProductId",
                table: "tblPicture",
                newName: "IX_tblPicture_ProductId");

            migrationBuilder.RenameIndex(
                name: "IX_Categories_ParentCategoryId",
                table: "tblCategory",
                newName: "IX_tblCategory_ParentCategoryId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_tblProduct",
                table: "tblProduct",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_tblPicture",
                table: "tblPicture",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_tblCategory",
                table: "tblCategory",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_tblCategory_tblCategory_ParentCategoryId",
                table: "tblCategory",
                column: "ParentCategoryId",
                principalTable: "tblCategory",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_tblPicture_tblProduct_ProductId",
                table: "tblPicture",
                column: "ProductId",
                principalTable: "tblProduct",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_tblProduct_tblCategory_CategoryId",
                table: "tblProduct",
                column: "CategoryId",
                principalTable: "tblCategory",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_tblCategory_tblCategory_ParentCategoryId",
                table: "tblCategory");

            migrationBuilder.DropForeignKey(
                name: "FK_tblPicture_tblProduct_ProductId",
                table: "tblPicture");

            migrationBuilder.DropForeignKey(
                name: "FK_tblProduct_tblCategory_CategoryId",
                table: "tblProduct");

            migrationBuilder.DropPrimaryKey(
                name: "PK_tblProduct",
                table: "tblProduct");

            migrationBuilder.DropPrimaryKey(
                name: "PK_tblPicture",
                table: "tblPicture");

            migrationBuilder.DropPrimaryKey(
                name: "PK_tblCategory",
                table: "tblCategory");

            migrationBuilder.RenameTable(
                name: "tblProduct",
                newName: "Products");

            migrationBuilder.RenameTable(
                name: "tblPicture",
                newName: "Picture");

            migrationBuilder.RenameTable(
                name: "tblCategory",
                newName: "Categories");

            migrationBuilder.RenameIndex(
                name: "IX_tblProduct_CategoryId",
                table: "Products",
                newName: "IX_Products_CategoryId");

            migrationBuilder.RenameIndex(
                name: "IX_tblPicture_ProductId",
                table: "Picture",
                newName: "IX_Picture_ProductId");

            migrationBuilder.RenameIndex(
                name: "IX_tblCategory_ParentCategoryId",
                table: "Categories",
                newName: "IX_Categories_ParentCategoryId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Products",
                table: "Products",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Picture",
                table: "Picture",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Categories",
                table: "Categories",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Categories_Categories_ParentCategoryId",
                table: "Categories",
                column: "ParentCategoryId",
                principalTable: "Categories",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Picture_Products_ProductId",
                table: "Picture",
                column: "ProductId",
                principalTable: "Products",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Products_Categories_CategoryId",
                table: "Products",
                column: "CategoryId",
                principalTable: "Categories",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
