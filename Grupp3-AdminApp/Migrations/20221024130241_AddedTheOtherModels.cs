using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Grupp3_Elevator.Migrations
{
    public partial class AddedTheOtherModels : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Elevators",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(250)", nullable: false),
                    Status = table.Column<string>(type: "nvarchar(100)", nullable: false),
                    DoorStatus = table.Column<bool>(type: "bit", nullable: false),
                    CurrentLevel = table.Column<int>(type: "int", nullable: false),
                    TargetLevel = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Elevators", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Technicians",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(200)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Technicians", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Errands",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Title = table.Column<string>(type: "nvarchar(300)", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(2000)", nullable: false),
                    Status = table.Column<string>(type: "nvarchar(100)", nullable: false),
                    LastEdited = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreatedBy = table.Column<string>(type: "nvarchar(200)", nullable: false),
                    TechnicianId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ElevatorModelId = table.Column<Guid>(type: "uniqueidentifier", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Errands", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Errands_Elevators_ElevatorModelId",
                        column: x => x.ElevatorModelId,
                        principalTable: "Elevators",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Errands_Technicians_TechnicianId",
                        column: x => x.TechnicianId,
                        principalTable: "Technicians",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ErrandComments",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Content = table.Column<string>(type: "nvarchar(2000)", nullable: false),
                    Author = table.Column<string>(type: "nvarchar(200)", nullable: false),
                    PostedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ErrandModelId = table.Column<Guid>(type: "uniqueidentifier", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ErrandComments", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ErrandComments_Errands_ErrandModelId",
                        column: x => x.ErrandModelId,
                        principalTable: "Errands",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_ErrandComments_ErrandModelId",
                table: "ErrandComments",
                column: "ErrandModelId");

            migrationBuilder.CreateIndex(
                name: "IX_Errands_ElevatorModelId",
                table: "Errands",
                column: "ElevatorModelId");

            migrationBuilder.CreateIndex(
                name: "IX_Errands_TechnicianId",
                table: "Errands",
                column: "TechnicianId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ErrandComments");

            migrationBuilder.DropTable(
                name: "Errands");

            migrationBuilder.DropTable(
                name: "Elevators");

            migrationBuilder.DropTable(
                name: "Technicians");
        }
    }
}
