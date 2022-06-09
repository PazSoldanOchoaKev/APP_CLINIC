using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace App.Infraestructure.Migrations
{
    public partial class Appointment : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Appointment",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    PetId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    TypeProcedures = table.Column<int>(type: "int", nullable: false),
                    DateAppointment = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Hour = table.Column<TimeSpan>(type: "time", nullable: false),
                    ListSizes = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Appointment", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Appointment_Pets_PetId",
                        column: x => x.PetId,
                        principalTable: "Pets",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Appointment_PetId",
                table: "Appointment",
                column: "PetId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Appointment");
        }
    }
}
