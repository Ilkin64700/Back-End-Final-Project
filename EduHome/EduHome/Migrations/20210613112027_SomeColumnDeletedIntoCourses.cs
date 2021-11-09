using Microsoft.EntityFrameworkCore.Migrations;

namespace EduHome.Migrations
{
    public partial class SomeColumnDeletedIntoCourses : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Courses_Features_FeatureId",
                table: "Courses");

            migrationBuilder.DropIndex(
                name: "IX_Courses_FeatureId",
                table: "Courses");

            migrationBuilder.DropColumn(
                name: "FeatureId",
                table: "Courses");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "FeatureId",
                table: "Courses",
                type: "int",
                nullable: false,
                defaultValue: 0);

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
    }
}
