using Microsoft.EntityFrameworkCore.Migrations;

namespace EduHome.Migrations
{
    public partial class SomeColumnAddedIntoSettings : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "DetailImage",
                table: "CourseImages");

            migrationBuilder.AlterColumn<string>(
                name: "Image",
                table: "Testimonials",
                maxLength: 100,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(100)",
                oldMaxLength: 100);

            migrationBuilder.AlterColumn<string>(
                name: "Image",
                table: "Sliders",
                maxLength: 100,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(100)",
                oldMaxLength: 100);

            migrationBuilder.AlterColumn<string>(
                name: "BackImage",
                table: "Sliders",
                maxLength: 100,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(100)",
                oldMaxLength: 100);

            migrationBuilder.AddColumn<string>(
                name: "Adress",
                table: "Settings",
                maxLength: 35,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "FooterDesc",
                table: "Settings",
                maxLength: 500,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "InstagramURL",
                table: "Settings",
                maxLength: 35,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Phone2",
                table: "Settings",
                maxLength: 35,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Phone3",
                table: "Settings",
                maxLength: 35,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "PinterestURL",
                table: "Settings",
                maxLength: 35,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "TwitterURL",
                table: "Settings",
                maxLength: 35,
                nullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Image",
                table: "Promotions",
                maxLength: 100,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(100)",
                oldMaxLength: 100);

            migrationBuilder.AlterColumn<string>(
                name: "Image",
                table: "Events",
                maxLength: 150,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(150)",
                oldMaxLength: 150);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Adress",
                table: "Settings");

            migrationBuilder.DropColumn(
                name: "FooterDesc",
                table: "Settings");

            migrationBuilder.DropColumn(
                name: "InstagramURL",
                table: "Settings");

            migrationBuilder.DropColumn(
                name: "Phone2",
                table: "Settings");

            migrationBuilder.DropColumn(
                name: "Phone3",
                table: "Settings");

            migrationBuilder.DropColumn(
                name: "PinterestURL",
                table: "Settings");

            migrationBuilder.DropColumn(
                name: "TwitterURL",
                table: "Settings");

            migrationBuilder.AlterColumn<string>(
                name: "Image",
                table: "Testimonials",
                type: "nvarchar(100)",
                maxLength: 100,
                nullable: false,
                oldClrType: typeof(string),
                oldMaxLength: 100,
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Image",
                table: "Sliders",
                type: "nvarchar(100)",
                maxLength: 100,
                nullable: false,
                oldClrType: typeof(string),
                oldMaxLength: 100,
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "BackImage",
                table: "Sliders",
                type: "nvarchar(100)",
                maxLength: 100,
                nullable: false,
                oldClrType: typeof(string),
                oldMaxLength: 100,
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Image",
                table: "Promotions",
                type: "nvarchar(100)",
                maxLength: 100,
                nullable: false,
                oldClrType: typeof(string),
                oldMaxLength: 100,
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Image",
                table: "Events",
                type: "nvarchar(150)",
                maxLength: 150,
                nullable: false,
                oldClrType: typeof(string),
                oldMaxLength: 150,
                oldNullable: true);

            migrationBuilder.AddColumn<string>(
                name: "DetailImage",
                table: "CourseImages",
                type: "nvarchar(100)",
                maxLength: 100,
                nullable: false,
                defaultValue: "");
        }
    }
}
