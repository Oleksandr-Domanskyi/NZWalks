using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace NZwalks.API.Migrations
{
    /// <inheritdoc />
    public partial class seeddataregionanddifficylty : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Difficulties",
                columns: new[] { "Id", "Name" },
                values: new object[,]
                {
                    { new Guid("58dc1430-6932-4636-9458-967a3a634b6c"), "Medium" },
                    { new Guid("8c6e5452-83e2-44a9-97a6-4f73576b1c9b"), "Hard" },
                    { new Guid("f65d5aaa-307d-4e30-b129-d33104cf4aa6"), "Easy" }
                });

            migrationBuilder.InsertData(
                table: "Regions",
                columns: new[] { "Id", "Code", "Name", "ReoginImageURL" },
                values: new object[,]
                {
                    { new Guid("2dd0bceb-3795-4a47-92e1-f5bcef87d362"), "ZKL", "Zikland", "https://upload.wikimedia.org/wikipedia/commons/thumb/4/47/Auckland_skyline_from_harbor_bridge%2C_20_September_2019.jpg/1024px-Auckland_skyline_from_harbor_bridge%2C_20_September_2019.jpg" },
                    { new Guid("aed57327-1524-42ea-b48e-9485e97cf983"), "AKL", "Auckland", "https://upload.wikimedia.org/wikipedia/commons/thumb/4/47/Auckland_skyline_from_harbor_bridge%2C_20_September_2019.jpg/1024px-Auckland_skyline_from_harbor_bridge%2C_20_September_2019.jpg" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Difficulties",
                keyColumn: "Id",
                keyValue: new Guid("58dc1430-6932-4636-9458-967a3a634b6c"));

            migrationBuilder.DeleteData(
                table: "Difficulties",
                keyColumn: "Id",
                keyValue: new Guid("8c6e5452-83e2-44a9-97a6-4f73576b1c9b"));

            migrationBuilder.DeleteData(
                table: "Difficulties",
                keyColumn: "Id",
                keyValue: new Guid("f65d5aaa-307d-4e30-b129-d33104cf4aa6"));

            migrationBuilder.DeleteData(
                table: "Regions",
                keyColumn: "Id",
                keyValue: new Guid("2dd0bceb-3795-4a47-92e1-f5bcef87d362"));

            migrationBuilder.DeleteData(
                table: "Regions",
                keyColumn: "Id",
                keyValue: new Guid("aed57327-1524-42ea-b48e-9485e97cf983"));
        }
    }
}
