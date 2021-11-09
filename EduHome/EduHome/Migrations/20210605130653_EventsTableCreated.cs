using Microsoft.EntityFrameworkCore.Migrations;

namespace EduHome.Migrations
{
    public partial class EventsTableCreated : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "EventId",
                table: "CourseTags",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "EventId",
                table: "CourseImages",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "Events",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CategoryId = table.Column<int>(nullable: false),
                    SpeakerId = table.Column<int>(nullable: false),
                    Name = table.Column<string>(maxLength: 150, nullable: false),
                    Date = table.Column<string>(nullable: false),
                    Time = table.Column<string>(nullable: false),
                    Venue = table.Column<string>(maxLength: 500, nullable: false),
                    Desc = table.Column<string>(maxLength: 1500, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Events", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Events_Categories_CategoryId",
                        column: x => x.CategoryId,
                        principalTable: "Categories",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Events_Speakers_SpeakerId",
                        column: x => x.SpeakerId,
                        principalTable: "Speakers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_CourseTags_EventId",
                table: "CourseTags",
                column: "EventId");

            migrationBuilder.CreateIndex(
                name: "IX_CourseImages_EventId",
                table: "CourseImages",
                column: "EventId");

            migrationBuilder.CreateIndex(
                name: "IX_Events_CategoryId",
                table: "Events",
                column: "CategoryId");

            migrationBuilder.CreateIndex(
                name: "IX_Events_SpeakerId",
                table: "Events",
                column: "SpeakerId");

            migrationBuilder.AddForeignKey(
                name: "FK_CourseImages_Events_EventId",
                table: "CourseImages",
                column: "EventId",
                principalTable: "Events",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_CourseTags_Events_EventId",
                table: "CourseTags",
                column: "EventId",
                principalTable: "Events",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CourseImages_Events_EventId",
                table: "CourseImages");

            migrationBuilder.DropForeignKey(
                name: "FK_CourseTags_Events_EventId",
                table: "CourseTags");

            migrationBuilder.DropTable(
                name: "Events");

            migrationBuilder.DropIndex(
                name: "IX_CourseTags_EventId",
                table: "CourseTags");

            migrationBuilder.DropIndex(
                name: "IX_CourseImages_EventId",
                table: "CourseImages");

            migrationBuilder.DropColumn(
                name: "EventId",
                table: "CourseTags");

            migrationBuilder.DropColumn(
                name: "EventId",
                table: "CourseImages");
        }
    }
}
