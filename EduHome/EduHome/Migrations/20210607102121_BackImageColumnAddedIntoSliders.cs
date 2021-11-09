using Microsoft.EntityFrameworkCore.Migrations;

namespace EduHome.Migrations
{
    public partial class BackImageColumnAddedIntoSliders : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_EventSpeaker_Events_EventId",
                table: "EventSpeaker");

            migrationBuilder.DropForeignKey(
                name: "FK_EventSpeaker_Speakers_SpeakerId",
                table: "EventSpeaker");

            migrationBuilder.DropPrimaryKey(
                name: "PK_EventSpeaker",
                table: "EventSpeaker");

            migrationBuilder.RenameTable(
                name: "EventSpeaker",
                newName: "EventSpeakers");

            migrationBuilder.RenameIndex(
                name: "IX_EventSpeaker_SpeakerId",
                table: "EventSpeakers",
                newName: "IX_EventSpeakers_SpeakerId");

            migrationBuilder.RenameIndex(
                name: "IX_EventSpeaker_EventId",
                table: "EventSpeakers",
                newName: "IX_EventSpeakers_EventId");

            migrationBuilder.AddColumn<string>(
                name: "BackImage",
                table: "Sliders",
                maxLength: 100,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddPrimaryKey(
                name: "PK_EventSpeakers",
                table: "EventSpeakers",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_EventSpeakers_Events_EventId",
                table: "EventSpeakers",
                column: "EventId",
                principalTable: "Events",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_EventSpeakers_Speakers_SpeakerId",
                table: "EventSpeakers",
                column: "SpeakerId",
                principalTable: "Speakers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_EventSpeakers_Events_EventId",
                table: "EventSpeakers");

            migrationBuilder.DropForeignKey(
                name: "FK_EventSpeakers_Speakers_SpeakerId",
                table: "EventSpeakers");

            migrationBuilder.DropPrimaryKey(
                name: "PK_EventSpeakers",
                table: "EventSpeakers");

            migrationBuilder.DropColumn(
                name: "BackImage",
                table: "Sliders");

            migrationBuilder.RenameTable(
                name: "EventSpeakers",
                newName: "EventSpeaker");

            migrationBuilder.RenameIndex(
                name: "IX_EventSpeakers_SpeakerId",
                table: "EventSpeaker",
                newName: "IX_EventSpeaker_SpeakerId");

            migrationBuilder.RenameIndex(
                name: "IX_EventSpeakers_EventId",
                table: "EventSpeaker",
                newName: "IX_EventSpeaker_EventId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_EventSpeaker",
                table: "EventSpeaker",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_EventSpeaker_Events_EventId",
                table: "EventSpeaker",
                column: "EventId",
                principalTable: "Events",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_EventSpeaker_Speakers_SpeakerId",
                table: "EventSpeaker",
                column: "SpeakerId",
                principalTable: "Speakers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
