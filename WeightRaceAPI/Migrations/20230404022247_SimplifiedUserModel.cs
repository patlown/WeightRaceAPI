using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WeightRaceAPI.Migrations
{
    /// <inheritdoc />
    public partial class SimplifiedUserModel : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
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

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
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
    }
}
