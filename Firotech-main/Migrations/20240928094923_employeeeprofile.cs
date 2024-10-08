using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Firotechbd.Migrations
{
    public partial class employeeeprofile : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_BlogPosts_BlogTags_TagId",
                table: "BlogPosts");

            migrationBuilder.DropTable(
                name: "BlogPostCategory");

            migrationBuilder.DropIndex(
                name: "IX_BlogPosts_TagId",
                table: "BlogPosts");

            migrationBuilder.DropColumn(
                name: "TagId",
                table: "BlogPosts");

            migrationBuilder.CreateTable(
                name: "EmployeePubProfile",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Slug = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    EmployeeProfileImageUrl = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true),
                    EmployeeName = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    EmployeePhone = table.Column<string>(type: "nvarchar(30)", maxLength: 30, nullable: true),
                    EmployeeEmail = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    EmployeeAddress = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true),
                    EmployeePosition = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    EmployeeStatus = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: true),
                    EmployeeAboutme = table.Column<string>(type: "nvarchar(1000)", maxLength: 1000, nullable: true),
                    Employeefb = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    Employeewp = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    Employeex = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    EmployeeMailHot = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    EmployeeLinkedin = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    IsActive = table.Column<bool>(type: "bit", nullable: false),
                    CreateBy = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CreateAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdateAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Updateby = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EmployeePubProfile", x => x.Id);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "EmployeePubProfile");

            migrationBuilder.AddColumn<int>(
                name: "TagId",
                table: "BlogPosts",
                type: "int",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "BlogPostCategory",
                columns: table => new
                {
                    BlogPostsId = table.Column<int>(type: "int", nullable: false),
                    CategoriesId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BlogPostCategory", x => new { x.BlogPostsId, x.CategoriesId });
                    table.ForeignKey(
                        name: "FK_BlogPostCategory_BlogCategories_CategoriesId",
                        column: x => x.CategoriesId,
                        principalTable: "BlogCategories",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_BlogPostCategory_BlogPosts_BlogPostsId",
                        column: x => x.BlogPostsId,
                        principalTable: "BlogPosts",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_BlogPosts_TagId",
                table: "BlogPosts",
                column: "TagId");

            migrationBuilder.CreateIndex(
                name: "IX_BlogPostCategory_CategoriesId",
                table: "BlogPostCategory",
                column: "CategoriesId");

            migrationBuilder.AddForeignKey(
                name: "FK_BlogPosts_BlogTags_TagId",
                table: "BlogPosts",
                column: "TagId",
                principalTable: "BlogTags",
                principalColumn: "Id");
        }
    }
}
