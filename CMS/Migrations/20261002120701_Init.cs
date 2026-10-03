using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace CMS.Migrations
{
    /// <inheritdoc />
    public partial class Init : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Customers",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    FullName = table.Column<string>(type: "nvarchar(150)", maxLength: 150, nullable: false),
                    Email = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    PhoneNumber = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Customers", x => x.Id);
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
                name: "Movies",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Title = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    Director = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Duration = table.Column<int>(type: "int", nullable: false),
                    ReleaseYear = table.Column<int>(type: "int", nullable: false),
                    Rating = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    TicketPrice = table.Column<decimal>(type: "decimal(10,2)", precision: 10, scale: 2, nullable: false),
                    AvailableSeats = table.Column<int>(type: "int", nullable: false),
                    GenreId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Movies", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Movies_Genres_GenreId",
                        column: x => x.GenreId,
                        principalTable: "Genres",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Bookings",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    BookingDate = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValueSql: "GETDATE()"),
                    NumberOfTickets = table.Column<int>(type: "int", nullable: false),
                    TotalPrice = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    CustomerId = table.Column<int>(type: "int", nullable: false),
                    MovieId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Bookings", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Bookings_Customers_CustomerId",
                        column: x => x.CustomerId,
                        principalTable: "Customers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Bookings_Movies_MovieId",
                        column: x => x.MovieId,
                        principalTable: "Movies",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "Customers",
                columns: new[] { "Id", "Email", "FullName", "PhoneNumber" },
                values: new object[,]
                {
                    { 1, "mariam.ahmed@example.com", "Mariam Ahmed", "01010000001" },
                    { 2, "adam.nasser@example.com", "Adam Nasser", "01010000002" },
                    { 3, "layla.fathy@example.com", "Layla Fathy", "01010000003" },
                    { 4, "ziad.mostafa@example.com", "Ziad Mostafa", "01010000004" },
                    { 5, "farah.mahmoud@example.com", "Farah Mahmoud", "01010000005" }
                });

            migrationBuilder.InsertData(
                table: "Genres",
                columns: new[] { "Id", "Name" },
                values: new object[,]
                {
                    { 1, "Action" },
                    { 2, "Comedy" },
                    { 3, "Drama" },
                    { 4, "Science Fiction" },
                    { 5, "Animation" }
                });

            migrationBuilder.InsertData(
                table: "Movies",
                columns: new[] { "Id", "AvailableSeats", "Director", "Duration", "GenreId", "Rating", "ReleaseYear", "TicketPrice", "Title" },
                values: new object[,]
                {
                    { 1, 80, "Nora Hassan", 118, 1, 7.8m, 2022, 12.50m, "Skyline Pursuit" },
                    { 2, 65, "Omar Khaled", 132, 4, 8.2m, 2024, 14.00m, "The Last Signal" },
                    { 3, 90, "Maya Farouk", 104, 3, 7.1m, 2021, 10.00m, "A Quiet Promise" },
                    { 4, 110, "Karim Adel", 96, 2, 6.9m, 2020, 9.50m, "Weekend Mix-Up" },
                    { 5, 55, "Lina Samir", 145, 4, 8.5m, 2025, 15.00m, "Orbit Nine" },
                    { 6, 75, "Salma Nabil", 101, 5, 7.6m, 2023, 11.00m, "The Paper Kingdom" },
                    { 7, 95, "Youssef Tarek", 126, 3, 7.3m, 2019, 10.50m, "Crossing Paths" },
                    { 8, 70, "Hana Mostafa", 110, 1, 8.0m, 2026, 13.25m, "Midnight Circuit" }
                });

            migrationBuilder.InsertData(
                table: "Bookings",
                columns: new[] { "Id", "BookingDate", "CustomerId", "MovieId", "NumberOfTickets", "TotalPrice" },
                values: new object[,]
                {
                    { 1, new DateTime(2026, 9, 20, 0, 0, 0, 0, DateTimeKind.Unspecified), 1, 1, 2, 25.00m },
                    { 2, new DateTime(2026, 9, 21, 0, 0, 0, 0, DateTimeKind.Unspecified), 2, 2, 1, 14.00m },
                    { 3, new DateTime(2026, 9, 22, 0, 0, 0, 0, DateTimeKind.Unspecified), 3, 3, 3, 30.00m },
                    { 4, new DateTime(2026, 9, 23, 0, 0, 0, 0, DateTimeKind.Unspecified), 4, 5, 2, 30.00m },
                    { 5, new DateTime(2026, 9, 24, 0, 0, 0, 0, DateTimeKind.Unspecified), 5, 6, 1, 11.00m }
                });

            migrationBuilder.CreateIndex(
                name: "IX_Bookings_CustomerId",
                table: "Bookings",
                column: "CustomerId");

            migrationBuilder.CreateIndex(
                name: "IX_Bookings_MovieId",
                table: "Bookings",
                column: "MovieId");

            migrationBuilder.CreateIndex(
                name: "IX_Customers_Email",
                table: "Customers",
                column: "Email",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Genres_Name",
                table: "Genres",
                column: "Name",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Movies_GenreId",
                table: "Movies",
                column: "GenreId");

            migrationBuilder.CreateIndex(
                name: "IX_Movies_Title",
                table: "Movies",
                column: "Title",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Bookings");

            migrationBuilder.DropTable(
                name: "Customers");

            migrationBuilder.DropTable(
                name: "Movies");

            migrationBuilder.DropTable(
                name: "Genres");
        }
    }
}
