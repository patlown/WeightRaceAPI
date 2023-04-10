using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WeightRaceAPI.Migrations
{
    public partial class AddStatsToUser : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<double>(
                name: "DayChange",
                table: "Users",
                type: "double",
                nullable: true);

            migrationBuilder.AddColumn<double>(
                name: "TotalChange",
                table: "Users",
                type: "double",
                nullable: true);

            migrationBuilder.AddColumn<double>(
                name: "WeekChange",
                table: "Users",
                type: "double",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "DayChange",
                table: "Users");

            migrationBuilder.DropColumn(
                name: "TotalChange",
                table: "Users");

            migrationBuilder.DropColumn(
                name: "WeekChange",
                table: "Users");
        }
    }
}
