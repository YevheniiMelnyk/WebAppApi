using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace WebApplication_API.Migrations
{
    /// <inheritdoc />
    public partial class AddDataToHotelsTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Hotels",
                columns: new[] { "Id", "CreatedDate", "Description", "ImageUrl", "Name", "Rate", "UpdateDate" },
                values: new object[,]
                {
                    { 1, new DateTime(2023, 7, 12, 12, 35, 44, 356, DateTimeKind.Local).AddTicks(9857), "Luxury hotel in Te Aro with indoor pool and restaurant", "https://images.trvl-media.com/lodging/2000000/1380000/1376400/1376357/b26bd2e2.jpg?impolicy=resizecrop&rw=1200&ra=fit", "QT Wellington", 100.0, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 2, new DateTime(2023, 7, 12, 12, 35, 44, 356, DateTimeKind.Local).AddTicks(9917), "Hotel description", "https://images.trvl-media.com/lodging/1000000/980000/977400/977353/ba82cfaa.jpg?impolicy=resizecrop&rw=1200&ra=fit", "Naumi Studio Wellington", 175.0, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 3, new DateTime(2023, 7, 12, 12, 35, 44, 356, DateTimeKind.Local).AddTicks(9922), "Suburban hotel with outdoor pool, near Villa Maria Auckland Winery", "https://images.trvl-media.com/lodging/11000000/10070000/10062500/10062481/f07817e8.jpg?impolicy=resizecrop&rw=1200&ra=fit", "Naumi Auckland Airport", 200.0, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 4, new DateTime(2023, 7, 12, 12, 35, 44, 356, DateTimeKind.Local).AddTicks(9927), "Wellington upmarket aparthotel with 24-hour fitness", "https://images.trvl-media.com/lodging/67000000/66530000/66522700/66522603/fdaeade6.jpg?impolicy=resizecrop&rw=1200&ra=fit", "Ramada by Wyndham Wellington Taranaki Street", 150.0, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 5, new DateTime(2023, 7, 12, 12, 35, 44, 356, DateTimeKind.Local).AddTicks(9932), "Luxury hotel with spa, near Sky Tower", "https://images.trvl-media.com/lodging/2000000/1390000/1386200/1386172/e654fdfb.jpg?impolicy=resizecrop&rw=1200&ra=fit", "The Grand by SkyCity", 250.0, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 6, new DateTime(2023, 7, 12, 12, 35, 44, 356, DateTimeKind.Local).AddTicks(9936), "Luxury hotel with restaurant, near Princes Warf Visitor Information Centre", "https://images.trvl-media.com/lodging/1000000/10000/5500/5465/d4144184.jpg?impolicy=resizecrop&rw=1200&ra=fit", "M Social Auckland", 130.0, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Hotels",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Hotels",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Hotels",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "Hotels",
                keyColumn: "Id",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "Hotels",
                keyColumn: "Id",
                keyValue: 5);

            migrationBuilder.DeleteData(
                table: "Hotels",
                keyColumn: "Id",
                keyValue: 6);
        }
    }
}
