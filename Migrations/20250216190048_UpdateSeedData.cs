using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace MusicStore.Migrations
{
    /// <inheritdoc />
    public partial class UpdateSeedData : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Artists",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Bio = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Artists", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Customers",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    FirstName = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    LastName = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Email = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Phone = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: true),
                    TotalSpent = table.Column<decimal>(type: "decimal(18,2)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Customers", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Discounts",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Percentage = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    StartDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    EndDate = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Discounts", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Genres",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Genres", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Promotions",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    DiscountPercentage = table.Column<decimal>(type: "decimal(5,2)", nullable: false),
                    StartDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    EndDate = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Promotions", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Publishers",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Publishers", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Users",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Username = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Password = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Orders",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    OrderDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    TotalPrice = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    CustomerId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Orders", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Orders_Customers_CustomerId",
                        column: x => x.CustomerId,
                        principalTable: "Customers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "VinylRecords",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    Year = table.Column<int>(type: "int", nullable: false),
                    Tracks = table.Column<int>(type: "int", nullable: false),
                    SalePrice = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    SellingPrice = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    ReleaseDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Stock = table.Column<int>(type: "int", nullable: false),
                    Sales = table.Column<int>(type: "int", nullable: false),
                    ArtistId = table.Column<int>(type: "int", nullable: false),
                    PublisherId = table.Column<int>(type: "int", nullable: true),
                    GenreId = table.Column<int>(type: "int", nullable: true),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_VinylRecords", x => x.Id);
                    table.ForeignKey(
                        name: "FK_VinylRecords_Artists_ArtistId",
                        column: x => x.ArtistId,
                        principalTable: "Artists",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_VinylRecords_Genres_GenreId",
                        column: x => x.GenreId,
                        principalTable: "Genres",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.SetNull);
                    table.ForeignKey(
                        name: "FK_VinylRecords_Publishers_PublisherId",
                        column: x => x.PublisherId,
                        principalTable: "Publishers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.SetNull);
                });

            migrationBuilder.CreateTable(
                name: "OrderDetails",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Quantity = table.Column<int>(type: "int", nullable: false),
                    Price = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    OrderId = table.Column<int>(type: "int", nullable: false),
                    VinylRecordId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_OrderDetails", x => x.Id);
                    table.ForeignKey(
                        name: "FK_OrderDetails_Orders_OrderId",
                        column: x => x.OrderId,
                        principalTable: "Orders",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_OrderDetails_VinylRecords_VinylRecordId",
                        column: x => x.VinylRecordId,
                        principalTable: "VinylRecords",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Reservations",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ReservedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    VinylRecordId = table.Column<int>(type: "int", nullable: false),
                    CustomerId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Reservations", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Reservations_Customers_CustomerId",
                        column: x => x.CustomerId,
                        principalTable: "Customers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Reservations_VinylRecords_VinylRecordId",
                        column: x => x.VinylRecordId,
                        principalTable: "VinylRecords",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Sales",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    VinylRecordId = table.Column<int>(type: "int", nullable: false),
                    Quantity = table.Column<int>(type: "int", nullable: false),
                    SalePrice = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    CustomerId = table.Column<int>(type: "int", nullable: false),
                    SaleDate = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Sales", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Sales_Customers_CustomerId",
                        column: x => x.CustomerId,
                        principalTable: "Customers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Sales_VinylRecords_VinylRecordId",
                        column: x => x.VinylRecordId,
                        principalTable: "VinylRecords",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "VinylRecordPromotions",
                columns: table => new
                {
                    PromotionsId = table.Column<int>(type: "int", nullable: false),
                    VinylRecordsId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_VinylRecordPromotions", x => new { x.PromotionsId, x.VinylRecordsId });
                    table.ForeignKey(
                        name: "FK_VinylRecordPromotions_Promotions_PromotionsId",
                        column: x => x.PromotionsId,
                        principalTable: "Promotions",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_VinylRecordPromotions_VinylRecords_VinylRecordsId",
                        column: x => x.VinylRecordsId,
                        principalTable: "VinylRecords",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "Artists",
                columns: new[] { "Id", "Bio", "Name" },
                values: new object[,]
                {
                    { 1, "English rock band formed in Liverpool in 1960.", "The Beatles" },
                    { 2, "American singer, songwriter, and dancer.", "Michael Jackson" },
                    { 3, "American jazz trumpeter, bandleader, and composer.", "Miles Davis" },
                    { 4, "American singer, songwriter, and actress.", "Beyoncé" },
                    { 5, "American singer-songwriter known for narrative songwriting.", "Taylor Swift" },
                    { 6, "English rock band known for progressive rock.", "Pink Floyd" },
                    { 7, "Jamaican singer, songwriter, and musician.", "Bob Marley" },
                    { 8, "English singer, songwriter, and actor.", "David Bowie" },
                    { 9, "American singer, songwriter, and actress.", "Madonna" },
                    { 10, "English singer-songwriter.", "Adele" }
                });

            migrationBuilder.InsertData(
                table: "Customers",
                columns: new[] { "Id", "Email", "FirstName", "LastName", "Phone", "TotalSpent" },
                values: new object[,]
                {
                    { 1, "johndoe@example.com", "John", "Doe", "123-456-7890", 500.00m },
                    { 2, "janesmith@example.com", "Jane", "Smith", "234-567-8901", 1000.00m },
                    { 3, "robertjohnson@example.com", "Robert", "Johnson", "345-678-9012", 150.00m },
                    { 4, "emilydavis@example.com", "Emily", "Davis", "456-789-0123", 250.00m },
                    { 5, "michaelmiller@example.com", "Michael", "Miller", "567-890-1234", 300.00m },
                    { 6, "sophiawilliams@example.com", "Sophia", "Williams", "678-901-2345", 450.00m },
                    { 7, "liambrown@example.com", "Liam", "Brown", "789-012-3456", 1200.00m },
                    { 8, "oliviajones@example.com", "Olivia", "Jones", "890-123-4567", 700.00m },
                    { 9, "ethangarcia@example.com", "Ethan", "Garcia", "901-234-5678", 600.00m },
                    { 10, "avamartinez@example.com", "Ava", "Martinez", "012-345-6789", 800.00m }
                });

            migrationBuilder.InsertData(
                table: "Discounts",
                columns: new[] { "Id", "EndDate", "Name", "Percentage", "StartDate" },
                values: new object[,]
                {
                    { 1, new DateTime(2025, 2, 6, 21, 0, 48, 122, DateTimeKind.Local).AddTicks(1646), "Black Friday", 50.00m, new DateTime(2025, 2, 1, 21, 0, 48, 122, DateTimeKind.Local).AddTicks(1644) },
                    { 2, new DateTime(2025, 2, 21, 21, 0, 48, 122, DateTimeKind.Local).AddTicks(1649), "New Year Sale", 30.00m, new DateTime(2025, 2, 11, 21, 0, 48, 122, DateTimeKind.Local).AddTicks(1647) },
                    { 3, new DateTime(2025, 3, 18, 21, 0, 48, 122, DateTimeKind.Local).AddTicks(1652), "Summer Clearance", 25.00m, new DateTime(2025, 2, 21, 21, 0, 48, 122, DateTimeKind.Local).AddTicks(1650) },
                    { 4, new DateTime(2025, 2, 6, 21, 0, 48, 122, DateTimeKind.Local).AddTicks(1654), "Winter Deals", 40.00m, new DateTime(2025, 1, 27, 21, 0, 48, 122, DateTimeKind.Local).AddTicks(1653) },
                    { 5, new DateTime(2025, 3, 28, 21, 0, 48, 122, DateTimeKind.Local).AddTicks(1657), "Easter Special", 15.00m, new DateTime(2025, 2, 26, 21, 0, 48, 122, DateTimeKind.Local).AddTicks(1656) }
                });

            migrationBuilder.InsertData(
                table: "Genres",
                columns: new[] { "Id", "Name" },
                values: new object[,]
                {
                    { 1, "Rock" },
                    { 2, "Pop" },
                    { 3, "Jazz" },
                    { 4, "Classical" },
                    { 5, "Hip-Hop" },
                    { 6, "Electronic" },
                    { 7, "Country" },
                    { 8, "Blues" },
                    { 9, "Reggae" },
                    { 10, "Indie" }
                });

            migrationBuilder.InsertData(
                table: "Promotions",
                columns: new[] { "Id", "DiscountPercentage", "EndDate", "Name", "StartDate" },
                values: new object[,]
                {
                    { 1, 10.00m, new DateTime(2025, 2, 26, 21, 0, 48, 122, DateTimeKind.Local).AddTicks(1620), "Jazz Week", new DateTime(2025, 2, 6, 21, 0, 48, 122, DateTimeKind.Local).AddTicks(1571) },
                    { 2, 15.00m, new DateTime(2025, 2, 21, 21, 0, 48, 122, DateTimeKind.Local).AddTicks(1624), "Rock Fest", new DateTime(2025, 2, 11, 21, 0, 48, 122, DateTimeKind.Local).AddTicks(1623) },
                    { 3, 20.00m, new DateTime(2025, 3, 8, 21, 0, 48, 122, DateTimeKind.Local).AddTicks(1627), "Pop Mania", new DateTime(2025, 2, 17, 21, 0, 48, 122, DateTimeKind.Local).AddTicks(1626) }
                });

            migrationBuilder.InsertData(
                table: "Publishers",
                columns: new[] { "Id", "Name" },
                values: new object[,]
                {
                    { 1, "Universal Music" },
                    { 2, "Sony Music" },
                    { 3, "Warner Music" },
                    { 4, "EMI" },
                    { 5, "Atlantic Records" }
                });

            migrationBuilder.InsertData(
                table: "VinylRecords",
                columns: new[] { "Id", "ArtistId", "CreatedAt", "GenreId", "Name", "PublisherId", "ReleaseDate", "SalePrice", "Sales", "SellingPrice", "Stock", "Tracks", "Year" },
                values: new object[,]
                {
                    { 1, 1, new DateTime(2025, 2, 16, 19, 0, 48, 122, DateTimeKind.Utc).AddTicks(1677), 1, "Abbey Road", 1, new DateTime(1969, 9, 26, 0, 0, 0, 0, DateTimeKind.Unspecified), 19.99m, 500, 20.99m, 100, 17, 1969 },
                    { 2, 2, new DateTime(2025, 2, 16, 19, 0, 48, 122, DateTimeKind.Utc).AddTicks(1686), 2, "Thriller", 2, new DateTime(1982, 11, 30, 0, 0, 0, 0, DateTimeKind.Unspecified), 24.99m, 700, 25.99m, 150, 9, 1982 },
                    { 3, 3, new DateTime(2025, 2, 16, 19, 0, 48, 122, DateTimeKind.Utc).AddTicks(1688), 3, "Kind of Blue", 3, new DateTime(1959, 8, 17, 0, 0, 0, 0, DateTimeKind.Unspecified), 29.99m, 400, 30.99m, 80, 5, 1959 },
                    { 4, 4, new DateTime(2025, 2, 16, 19, 0, 48, 122, DateTimeKind.Utc).AddTicks(1690), 2, "Lemonade", 1, new DateTime(2016, 4, 23, 0, 0, 0, 0, DateTimeKind.Unspecified), 21.99m, 600, 22.99m, 200, 12, 2016 },
                    { 5, 5, new DateTime(2025, 2, 16, 19, 0, 48, 122, DateTimeKind.Utc).AddTicks(1693), 2, "1989", 2, new DateTime(2014, 10, 27, 0, 0, 0, 0, DateTimeKind.Unspecified), 17.99m, 800, 18.99m, 250, 13, 2014 }
                });

            migrationBuilder.CreateIndex(
                name: "IX_OrderDetails_OrderId",
                table: "OrderDetails",
                column: "OrderId");

            migrationBuilder.CreateIndex(
                name: "IX_OrderDetails_VinylRecordId",
                table: "OrderDetails",
                column: "VinylRecordId");

            migrationBuilder.CreateIndex(
                name: "IX_Orders_CustomerId",
                table: "Orders",
                column: "CustomerId");

            migrationBuilder.CreateIndex(
                name: "IX_Reservations_CustomerId",
                table: "Reservations",
                column: "CustomerId");

            migrationBuilder.CreateIndex(
                name: "IX_Reservations_VinylRecordId",
                table: "Reservations",
                column: "VinylRecordId");

            migrationBuilder.CreateIndex(
                name: "IX_Sales_CustomerId",
                table: "Sales",
                column: "CustomerId");

            migrationBuilder.CreateIndex(
                name: "IX_Sales_VinylRecordId",
                table: "Sales",
                column: "VinylRecordId");

            migrationBuilder.CreateIndex(
                name: "IX_VinylRecordPromotions_VinylRecordsId",
                table: "VinylRecordPromotions",
                column: "VinylRecordsId");

            migrationBuilder.CreateIndex(
                name: "IX_VinylRecords_ArtistId",
                table: "VinylRecords",
                column: "ArtistId");

            migrationBuilder.CreateIndex(
                name: "IX_VinylRecords_GenreId",
                table: "VinylRecords",
                column: "GenreId");

            migrationBuilder.CreateIndex(
                name: "IX_VinylRecords_PublisherId",
                table: "VinylRecords",
                column: "PublisherId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Discounts");

            migrationBuilder.DropTable(
                name: "OrderDetails");

            migrationBuilder.DropTable(
                name: "Reservations");

            migrationBuilder.DropTable(
                name: "Sales");

            migrationBuilder.DropTable(
                name: "Users");

            migrationBuilder.DropTable(
                name: "VinylRecordPromotions");

            migrationBuilder.DropTable(
                name: "Orders");

            migrationBuilder.DropTable(
                name: "Promotions");

            migrationBuilder.DropTable(
                name: "VinylRecords");

            migrationBuilder.DropTable(
                name: "Customers");

            migrationBuilder.DropTable(
                name: "Artists");

            migrationBuilder.DropTable(
                name: "Genres");

            migrationBuilder.DropTable(
                name: "Publishers");
        }
    }
}
