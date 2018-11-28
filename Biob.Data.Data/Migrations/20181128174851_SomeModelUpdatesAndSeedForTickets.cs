using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Biob.Data.Data.Migrations
{
    public partial class SomeModelUpdatesAndSeedForTickets : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Showtimes_Halls_HallId",
                table: "Showtimes");

            migrationBuilder.DropColumn(
                name: "HallSeatId",
                table: "Tickets");

            migrationBuilder.DropColumn(
                name: "Reserved",
                table: "Tickets");

            migrationBuilder.AlterColumn<decimal>(
                name: "Price",
                table: "Tickets",
                nullable: false,
                oldClrType: typeof(int));

            migrationBuilder.AddColumn<Guid>(
                name: "SeatId",
                table: "Tickets",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.InsertData(
                table: "Tickets",
                columns: new[] { "Id", "CreatedOn", "CustomerId", "DeletedOn", "IsDeleted", "ModifiedOn", "Paid", "Price", "SeatId", "ShowtimeId" },
                values: new object[,]
                {
                    { new Guid("303814ca-54f0-4fbb-955b-7ffd33b10b9d"), new DateTimeOffset(new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), new Guid("64c986df-a168-40cb-b5ea-ab2b20069a08"), null, false, null, false, 250m, new Guid("5fd7f7c4-d90f-4d60-8878-067af214a0dc"), new Guid("092ca7c5-ae83-4a52-a38b-cfc7c8e40e9a") },
                    { new Guid("ce442ad4-37a4-43f4-9a6d-5f7ab15df011"), new DateTimeOffset(new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), new Guid("64c986df-a168-40cb-b5ea-ab2b20069a08"), null, false, null, true, 250m, new Guid("603ab124-4be6-40fd-9a5e-49bb4a5730db"), new Guid("092ca7c5-ae83-4a52-a38b-cfc7c8e40e9a") },
                    { new Guid("7f36e8e7-b5cd-43ef-a71d-8cfa2355d8ab"), new DateTimeOffset(new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), new Guid("64c986df-a168-40cb-b5ea-ab2b20069a08"), null, false, null, false, 300m, new Guid("173595a9-917d-4df9-9a6d-1a5d5b46104e"), new Guid("5e0d5ad3-22b0-4bdc-808c-62b8f50d0796") },
                    { new Guid("a9aee74e-c644-4fc3-9a27-946d7c4cd037"), new DateTimeOffset(new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), new Guid("64c986df-a168-40cb-b5ea-ab2b20069a08"), null, false, null, false, 300m, new Guid("70fe1293-99c5-43aa-82df-fd0beaa8076a"), new Guid("5e0d5ad3-22b0-4bdc-808c-62b8f50d0796") }
                });

            migrationBuilder.CreateIndex(
                name: "IX_Tickets_SeatId",
                table: "Tickets",
                column: "SeatId");

            migrationBuilder.CreateIndex(
                name: "IX_Tickets_ShowtimeId",
                table: "Tickets",
                column: "ShowtimeId");

            migrationBuilder.AddForeignKey(
                name: "FK_Showtimes_Halls_HallId",
                table: "Showtimes",
                column: "HallId",
                principalTable: "Halls",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Tickets_Seats_SeatId",
                table: "Tickets",
                column: "SeatId",
                principalTable: "Seats",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Tickets_Showtimes_ShowtimeId",
                table: "Tickets",
                column: "ShowtimeId",
                principalTable: "Showtimes",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Showtimes_Halls_HallId",
                table: "Showtimes");

            migrationBuilder.DropForeignKey(
                name: "FK_Tickets_Seats_SeatId",
                table: "Tickets");

            migrationBuilder.DropForeignKey(
                name: "FK_Tickets_Showtimes_ShowtimeId",
                table: "Tickets");

            migrationBuilder.DropIndex(
                name: "IX_Tickets_SeatId",
                table: "Tickets");

            migrationBuilder.DropIndex(
                name: "IX_Tickets_ShowtimeId",
                table: "Tickets");

            migrationBuilder.DeleteData(
                table: "Tickets",
                keyColumn: "Id",
                keyValue: new Guid("303814ca-54f0-4fbb-955b-7ffd33b10b9d"));

            migrationBuilder.DeleteData(
                table: "Tickets",
                keyColumn: "Id",
                keyValue: new Guid("7f36e8e7-b5cd-43ef-a71d-8cfa2355d8ab"));

            migrationBuilder.DeleteData(
                table: "Tickets",
                keyColumn: "Id",
                keyValue: new Guid("a9aee74e-c644-4fc3-9a27-946d7c4cd037"));

            migrationBuilder.DeleteData(
                table: "Tickets",
                keyColumn: "Id",
                keyValue: new Guid("ce442ad4-37a4-43f4-9a6d-5f7ab15df011"));

            migrationBuilder.DropColumn(
                name: "SeatId",
                table: "Tickets");

            migrationBuilder.AlterColumn<int>(
                name: "Price",
                table: "Tickets",
                nullable: false,
                oldClrType: typeof(decimal));

            migrationBuilder.AddColumn<int>(
                name: "HallSeatId",
                table: "Tickets",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<bool>(
                name: "Reserved",
                table: "Tickets",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddForeignKey(
                name: "FK_Showtimes_Halls_HallId",
                table: "Showtimes",
                column: "HallId",
                principalTable: "Halls",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
