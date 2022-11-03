using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Grupp3_Elevator.Migrations
{
    public partial class redid_models : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CurrentLevel",
                table: "Elevators");

            migrationBuilder.DropColumn(
                name: "DoorStatus",
                table: "Elevators");

            migrationBuilder.DropColumn(
                name: "Name",
                table: "Elevators");

            migrationBuilder.DropColumn(
                name: "Status",
                table: "Elevators");

            migrationBuilder.DropColumn(
                name: "TargetLevel",
                table: "Elevators");

            migrationBuilder.AddColumn<string>(
                name: "ConnectionString",
                table: "Elevators",
                type: "nvarchar(1000)",
                nullable: false,
                defaultValue: "");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ConnectionString",
                table: "Elevators");

            migrationBuilder.AddColumn<int>(
                name: "CurrentLevel",
                table: "Elevators",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<bool>(
                name: "DoorStatus",
                table: "Elevators",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<string>(
                name: "Name",
                table: "Elevators",
                type: "nvarchar(250)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Status",
                table: "Elevators",
                type: "nvarchar(100)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<int>(
                name: "TargetLevel",
                table: "Elevators",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }
    }
}
