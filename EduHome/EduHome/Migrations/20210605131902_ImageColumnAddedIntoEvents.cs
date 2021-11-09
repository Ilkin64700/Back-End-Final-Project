using Microsoft.EntityFrameworkCore.Migrations;

namespace EduHome.Migrations
{
    public partial class ImageColumnAddedIntoEvents : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CourseImages_Events_EventId",
                table: "CourseImages");

            migrationBuilder.DropIndex(
                name: "IX_CourseImages_EventId",
                table: "CourseImages");

            migrationBuilder.DropColumn(
                name: "EventId",
                table: "CourseImages");

            migrationBuilder.AddColumn<string>(
                name: "Image",
                table: "Events",
                maxLength: 150,
                nullable: false,
                defaultValue: "");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Image",
                table: "Events");

            migrationBuilder.AddColumn<int>(
                name: "EventId",
                table: "CourseImages",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_CourseImages_EventId",
                table: "CourseImages",
                column: "EventId");

            migrationBuilder.AddForeignKey(
                name: "FK_CourseImages_Events_EventId",
                table: "CourseImages",
                column: "EventId",
                principalTable: "Events",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
