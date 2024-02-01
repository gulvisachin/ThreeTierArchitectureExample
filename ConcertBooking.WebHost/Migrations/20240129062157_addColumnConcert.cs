using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ConcertBooking.WebHost.Migrations
{
    public partial class addColumnConcert : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "ImageUrl",
                table: "Concerts",
                type: "nvarchar(max)",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ImageUrl",
                table: "Concerts");
        }
    }
}
