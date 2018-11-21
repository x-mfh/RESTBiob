using Microsoft.EntityFrameworkCore.Migrations;

namespace Biob.Data.Data.Migrations
{
    public partial class NoDefaultValuesAndIdsToGuids : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<bool>(
                name: "ThreeDee",
                table: "Showtimes",
                nullable: false,
                oldClrType: typeof(bool),
                oldDefaultValue: false);

            migrationBuilder.AlterColumn<bool>(
                name: "ThreeDee",
                table: "Halls",
                nullable: false,
                oldClrType: typeof(bool),
                oldDefaultValue: false);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<bool>(
                name: "ThreeDee",
                table: "Showtimes",
                nullable: false,
                defaultValue: false,
                oldClrType: typeof(bool));

            migrationBuilder.AlterColumn<bool>(
                name: "ThreeDee",
                table: "Halls",
                nullable: false,
                defaultValue: false,
                oldClrType: typeof(bool));
        }
    }
}
