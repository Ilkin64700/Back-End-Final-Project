using Microsoft.EntityFrameworkCore.Migrations;

namespace EduHome.Migrations
{
    public partial class BlogsTableCreated : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CourseTags_Events_EventId",
                table: "CourseTags");

            migrationBuilder.DropIndex(
                name: "IX_CourseTags_EventId",
                table: "CourseTags");

            migrationBuilder.DropColumn(
                name: "EventId",
                table: "CourseTags");

            migrationBuilder.CreateTable(
                name: "Blogs",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CategoryId = table.Column<int>(nullable: false),
                    Name = table.Column<string>(maxLength: 150, nullable: false),
                    Image = table.Column<string>(maxLength: 150, nullable: false),
                    Desc = table.Column<string>(maxLength: 1500, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Blogs", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Blogs_Categories_CategoryId",
                        column: x => x.CategoryId,
                        principalTable: "Categories",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Blogs_CategoryId",
                table: "Blogs",
                column: "CategoryId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Blogs");

            migrationBuilder.AddColumn<int>(
                name: "EventId",
                table: "CourseTags",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_CourseTags_EventId",
                table: "CourseTags",
                column: "EventId");

            migrationBuilder.AddForeignKey(
                name: "FK_CourseTags_Events_EventId",
                table: "CourseTags",
                column: "EventId",
                principalTable: "Events",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
