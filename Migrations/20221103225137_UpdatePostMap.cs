using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BlogNet.Migrations
{
    public partial class UpdatePostMap : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Post_Author",
                table: "Post");

            migrationBuilder.DropIndex(
                name: "IX_Post_AuthorIdId",
                table: "Post");

            migrationBuilder.DropColumn(
                name: "AuthorIdId",
                table: "Post");

            migrationBuilder.RenameColumn(
                name: "Summary",
                table: "Post",
                newName: "AuthorId");

            migrationBuilder.AlterColumn<DateTime>(
                name: "LastUpdateDate",
                table: "Post",
                type: "SMALLDATETIME",
                maxLength: 60,
                nullable: false,
                defaultValue: new DateTime(2022, 11, 3, 22, 51, 36, 805, DateTimeKind.Utc).AddTicks(9603),
                oldClrType: typeof(DateTime),
                oldType: "SMALLDATETIME",
                oldMaxLength: 60,
                oldDefaultValue: new DateTime(2022, 11, 2, 22, 38, 2, 870, DateTimeKind.Utc).AddTicks(2243));

            migrationBuilder.AddColumn<string>(
                name: "UserId",
                table: "Post",
                type: "VARCHAR(80)",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Post_UserId",
                table: "Post",
                column: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_Post_User_UserId",
                table: "Post",
                column: "UserId",
                principalTable: "User",
                principalColumn: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Post_User_UserId",
                table: "Post");

            migrationBuilder.DropIndex(
                name: "IX_Post_UserId",
                table: "Post");

            migrationBuilder.DropColumn(
                name: "UserId",
                table: "Post");

            migrationBuilder.RenameColumn(
                name: "AuthorId",
                table: "Post",
                newName: "Summary");

            migrationBuilder.AlterColumn<DateTime>(
                name: "LastUpdateDate",
                table: "Post",
                type: "SMALLDATETIME",
                maxLength: 60,
                nullable: false,
                defaultValue: new DateTime(2022, 11, 2, 22, 38, 2, 870, DateTimeKind.Utc).AddTicks(2243),
                oldClrType: typeof(DateTime),
                oldType: "SMALLDATETIME",
                oldMaxLength: 60,
                oldDefaultValue: new DateTime(2022, 11, 3, 22, 51, 36, 805, DateTimeKind.Utc).AddTicks(9603));

            migrationBuilder.AddColumn<string>(
                name: "AuthorIdId",
                table: "Post",
                type: "VARCHAR(80)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.CreateIndex(
                name: "IX_Post_AuthorIdId",
                table: "Post",
                column: "AuthorIdId");

            migrationBuilder.AddForeignKey(
                name: "FK_Post_Author",
                table: "Post",
                column: "AuthorIdId",
                principalTable: "User",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
