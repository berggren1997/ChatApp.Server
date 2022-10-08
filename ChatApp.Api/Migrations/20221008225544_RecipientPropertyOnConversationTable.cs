using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ChatApp.Api.Migrations
{
    public partial class RecipientPropertyOnConversationTable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Conversations_AspNetUsers_CreatedByAppUserId",
                table: "Conversations");

            migrationBuilder.AddColumn<int>(
                name: "RecipientId",
                table: "Conversations",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Conversations_RecipientId",
                table: "Conversations",
                column: "RecipientId");

            migrationBuilder.AddForeignKey(
                name: "FK_Conversations_AspNetUsers_CreatedByAppUserId",
                table: "Conversations",
                column: "CreatedByAppUserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Conversations_AspNetUsers_RecipientId",
                table: "Conversations",
                column: "RecipientId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Conversations_AspNetUsers_CreatedByAppUserId",
                table: "Conversations");

            migrationBuilder.DropForeignKey(
                name: "FK_Conversations_AspNetUsers_RecipientId",
                table: "Conversations");

            migrationBuilder.DropIndex(
                name: "IX_Conversations_RecipientId",
                table: "Conversations");

            migrationBuilder.DropColumn(
                name: "RecipientId",
                table: "Conversations");

            migrationBuilder.AddForeignKey(
                name: "FK_Conversations_AspNetUsers_CreatedByAppUserId",
                table: "Conversations",
                column: "CreatedByAppUserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
