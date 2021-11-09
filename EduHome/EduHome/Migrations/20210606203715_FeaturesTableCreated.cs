using Microsoft.EntityFrameworkCore.Migrations;

namespace EduHome.Migrations
{
    public partial class FeaturesTableCreated : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Features",
                table: "Courses");

            migrationBuilder.AddColumn<int>(
                name: "FeatureId",
                table: "Courses",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateTable(
                name: "Features",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Start = table.Column<string>(nullable: false),
                    Duration = table.Column<string>(nullable: false),
                    ClassDuration = table.Column<string>(nullable: false),
                    SkillLevel = table.Column<string>(nullable: false),
                    Language = table.Column<string>(nullable: false),
                    Student = table.Column<string>(nullable: false),
                    Assesment = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Features", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Courses_FeatureId",
                table: "Courses",
                column: "FeatureId");

            migrationBuilder.AddForeignKey(
                name: "FK_Courses_Features_FeatureId",
                table: "Courses",
                column: "FeatureId",
                principalTable: "Features",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Courses_Features_FeatureId",
                table: "Courses");

            migrationBuilder.DropTable(
                name: "Features");

            migrationBuilder.DropIndex(
                name: "IX_Courses_FeatureId",
                table: "Courses");

            migrationBuilder.DropColumn(
                name: "FeatureId",
                table: "Courses");

            migrationBuilder.AddColumn<string>(
                name: "Features",
                table: "Courses",
                type: "nvarchar(1500)",
                maxLength: 1500,
                nullable: false,
                defaultValue: "");
        }
    }
}
