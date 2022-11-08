using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Grupp3_Elevator.Migrations
{
    public partial class AuthorToTechnician : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Author",
                table: "ErrandComments");

            migrationBuilder.AddColumn<Guid>(
                name: "TechnicianId",
                table: "ErrandComments",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.CreateIndex(
                name: "IX_ErrandComments_TechnicianId",
                table: "ErrandComments",
                column: "TechnicianId");

            migrationBuilder.AddForeignKey(
                name: "FK_ErrandComments_Technicians_TechnicianId",
                table: "ErrandComments",
                column: "TechnicianId",
                principalTable: "Technicians",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ErrandComments_Technicians_TechnicianId",
                table: "ErrandComments");

            migrationBuilder.DropIndex(
                name: "IX_ErrandComments_TechnicianId",
                table: "ErrandComments");

            migrationBuilder.DropColumn(
                name: "TechnicianId",
                table: "ErrandComments");

            migrationBuilder.AddColumn<string>(
                name: "Author",
                table: "ErrandComments",
                type: "nvarchar(200)",
                nullable: false,
                defaultValue: "");
        }
    }
}
