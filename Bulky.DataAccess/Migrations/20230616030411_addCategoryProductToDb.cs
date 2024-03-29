﻿using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Bulky.DataAccess.Migrations
{
    /// <inheritdoc />
    public partial class addCategoryProductToDb : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Categories",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(30)", maxLength: 30, nullable: false),
                    DisplayOrder = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Categories", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Products",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Title = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ISBN = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Author = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ListPrice = table.Column<double>(type: "float", nullable: false),
                    Price = table.Column<double>(type: "float", nullable: false),
                    Price50 = table.Column<double>(type: "float", nullable: false),
                    Price100 = table.Column<double>(type: "float", nullable: false),
                    CategoryId = table.Column<int>(type: "int", nullable: false),
                    ImageUrl = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Products", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Products_Categories_CategoryId",
                        column: x => x.CategoryId,
                        principalTable: "Categories",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "Categories",
                columns: new[] { "Id", "DisplayOrder", "Name" },
                values: new object[,]
                {
                    { 1, 1, "Action" },
                    { 2, 2, "SciFi" },
                    { 3, 3, "History" }
                });

            migrationBuilder.InsertData(
                table: "Products",
                columns: new[] { "Id", "Author", "CategoryId", "Description", "ISBN", "ImageUrl", "ListPrice", "Price", "Price100", "Price50", "Title" },
                values: new object[,]
                {
                    { 1, "Billy Spark", 1, "Present vitae sodales libero . Praesent Molestie orce augue , vitae euismod velit sollicitudin ac. praesent vestibulum facilisis nibh ut ultricies.\r\n\r\n Numc Malesuala viverra ipsum sit amet tinciduct. ", "SWD9999001", "WhatsApp Image 2023-06-04 at 1.07.41 PM", 99.0, 90.0, 80.0, 85.0, "Fortune of time" },
                    { 2, "Nancy Hoover", 1, "Present vitae sodales libero . Praesent Molestie orce augue , vitae euismod velit sollicitudin ac. praesent vestibulum facilisis nibh ut ultricies.\r\n\r\n Numc Malesuala viverra ipsum sit amet tinciduct. ", "CAR777777701", "Node js", 25.0, 23.0, 20.0, 22.0, "Dark Skies" },
                    { 3, "Julian Button", 1, "Present vitae sodales libero . Praesent Molestie orce augue , vitae euismod velit sollicitudin ac. praesent vestibulum facilisis nibh ut ultricies.\r\n\r\n Numc Malesuala viverra ipsum sit amet tinciduct. ", "RITO5555501", "C language", 55.0, 50.0, 35.0, 40.0, "Vanish in the Sunset" },
                    { 4, "Abby Muscles", 2, "Present vitae sodales libero . Praesent Molestie orce augue , vitae euismod velit sollicitudin ac. praesent vestibulum facilisis nibh ut ultricies.\r\n\r\n Numc Malesuala viverra ipsum sit amet tinciduct. ", "WS3333333301", "Asp .net core 6", 70.0, 65.0, 55.0, 60.0, "Cotton Candy" },
                    { 5, "Ron Parker", 2, "Present vitae sodales libero . Praesent Molestie orce augue , vitae euismod velit sollicitudin ac. praesent vestibulum facilisis nibh ut ultricies.\r\n\r\n Numc Malesuala viverra ipsum sit amet tinciduct. ", "SOTJ1111111101", "asp.net SharePoint", 30.0, 27.0, 20.0, 25.0, "Rock in the Ocean" },
                    { 6, "Laura Phantom", 3, "Present vitae sodales libero . Praesent Molestie orce augue , vitae euismod velit sollicitudin ac. praesent vestibulum facilisis nibh ut ultricies.\r\n\r\n Numc Malesuala viverra ipsum sit amet tinciduct. ", "FOT000000001", "Asp.net core 6 and Angular", 25.0, 23.0, 20.0, 22.0, "Leaves and Wonders" }
                });

            migrationBuilder.CreateIndex(
                name: "IX_Products_CategoryId",
                table: "Products",
                column: "CategoryId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Products");

            migrationBuilder.DropTable(
                name: "Categories");
        }
    }
}
