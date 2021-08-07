using Microsoft.EntityFrameworkCore.Migrations;

namespace MY_CALS_BACKEND.Migrations
{
    public partial class addNewColumnToReviewTB : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "Nbr_Stars",
                table: "Reviews",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Nbr_Stars",
                table: "Reviews");
        }
    }
}
