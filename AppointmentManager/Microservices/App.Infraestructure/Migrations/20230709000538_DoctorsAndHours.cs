using Microsoft.EntityFrameworkCore.Migrations;

namespace App.Infraestructure.Migrations
{
    public partial class DoctorsAndHours : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Users_Access_AccessId",
                table: "Users");

            migrationBuilder.AlterColumn<string>(
                name: "AccessId",
                table: "Users",
                type: "nvarchar(450)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)");

            migrationBuilder.AddForeignKey(
                name: "FK_Users_Access_AccessId",
                table: "Users",
                column: "AccessId",
                principalTable: "Access",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Users_Access_AccessId",
                table: "Users");

            migrationBuilder.AlterColumn<string>(
                name: "AccessId",
                table: "Users",
                type: "nvarchar(450)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(450)",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Users_Access_AccessId",
                table: "Users",
                column: "AccessId",
                principalTable: "Access",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
