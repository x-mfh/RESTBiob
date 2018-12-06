using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Biob.Data.Data.Migrations
{
    public partial class RemovedCustomer : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CustomerId",
                table: "Tickets");

            migrationBuilder.AlterColumn<string>(
                name: "Title",
                table: "Movies",
                maxLength: 100,
                nullable: false,
                oldClrType: typeof(string),
                oldMaxLength: 255);

            migrationBuilder.AlterColumn<string>(
                name: "Producer",
                table: "Movies",
                maxLength: 100,
                nullable: true,
                oldClrType: typeof(string),
                oldMaxLength: 255,
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Actors",
                table: "Movies",
                maxLength: 255,
                nullable: true,
                oldClrType: typeof(string),
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "GenreName",
                table: "Genres",
                maxLength: 30,
                nullable: false,
                oldClrType: typeof(string));
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
                name: "CustomerId",
                table: "Tickets",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AlterColumn<string>(
                name: "Title",
                table: "Movies",
                maxLength: 255,
                nullable: false,
                oldClrType: typeof(string),
                oldMaxLength: 100);

            migrationBuilder.AlterColumn<string>(
                name: "Producer",
                table: "Movies",
                maxLength: 255,
                nullable: true,
                oldClrType: typeof(string),
                oldMaxLength: 100,
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Actors",
                table: "Movies",
                nullable: true,
                oldClrType: typeof(string),
                oldMaxLength: 255,
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "GenreName",
                table: "Genres",
                nullable: false,
                oldClrType: typeof(string),
                oldMaxLength: 30);

            migrationBuilder.UpdateData(
                table: "Tickets",
                keyColumn: "Id",
                keyValue: new Guid("303814ca-54f0-4fbb-955b-7ffd33b10b9d"),
                column: "CustomerId",
                value: new Guid("64c986df-a168-40cb-b5ea-ab2b20069a08"));

            migrationBuilder.UpdateData(
                table: "Tickets",
                keyColumn: "Id",
                keyValue: new Guid("7f36e8e7-b5cd-43ef-a71d-8cfa2355d8ab"),
                column: "CustomerId",
                value: new Guid("64c986df-a168-40cb-b5ea-ab2b20069a08"));

            migrationBuilder.UpdateData(
                table: "Tickets",
                keyColumn: "Id",
                keyValue: new Guid("a9aee74e-c644-4fc3-9a27-946d7c4cd037"),
                column: "CustomerId",
                value: new Guid("64c986df-a168-40cb-b5ea-ab2b20069a08"));

            migrationBuilder.UpdateData(
                table: "Tickets",
                keyColumn: "Id",
                keyValue: new Guid("ce442ad4-37a4-43f4-9a6d-5f7ab15df011"),
                column: "CustomerId",
                value: new Guid("64c986df-a168-40cb-b5ea-ab2b20069a08"));
        }
    }
}
