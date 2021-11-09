using Microsoft.EntityFrameworkCore.Migrations;

namespace EduHome.Migrations
{
    public partial class TeachersTableCreated : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Teachers",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(maxLength: 150, nullable: false),
                    Position = table.Column<string>(maxLength: 150, nullable: false),
                    About = table.Column<string>(maxLength: 1500, nullable: false),
                    Degree = table.Column<string>(maxLength: 50, nullable: false),
                    Experience = table.Column<string>(maxLength: 50, nullable: false),
                    Hobbies = table.Column<string>(maxLength: 50, nullable: false),
                    Faculty = table.Column<string>(maxLength: 50, nullable: false),
                    Mail = table.Column<string>(maxLength: 150, nullable: false),
                    Number = table.Column<string>(maxLength: 150, nullable: false),
                    Skype = table.Column<string>(maxLength: 150, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Teachers", x => x.Id);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Teachers");
        }
    }
}
