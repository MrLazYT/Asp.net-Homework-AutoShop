using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace AutoShop.Migrations
{
    /// <inheritdoc />
    public partial class Init : Migration
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
                    Description = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Categories", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Cars",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Model = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Color = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Year = table.Column<int>(type: "int", nullable: false),
                    CategoryId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Cars", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Cars_Categories_CategoryId",
                        column: x => x.CategoryId,
                        principalTable: "Categories",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "Categories",
                columns: new[] { "Id", "Description", "Name" },
                values: new object[,]
                {
                    { 1, "Standard 4-door passenger cars", "Sedan" },
                    { 2, "Sport Utility Vehicles", "SUV" },
                    { 3, "Two-door sports cars", "Coupe" },
                    { 4, "Cars with retractable roofs", "Convertible" },
                    { 5, "Compact cars with hatch-style trunk", "Hatchback" }
                });

            migrationBuilder.InsertData(
                table: "Cars",
                columns: new[] { "Id", "CategoryId", "Color", "Model", "Year" },
                values: new object[,]
                {
                    { 1, 1, "White", "Toyota Camry", 2020 },
                    { 2, 1, "Blue", "Honda Civic", 2019 },
                    { 3, 2, "Black", "Ford Explorer", 2021 },
                    { 4, 2, "Red", "Chevrolet Tahoe", 2022 },
                    { 5, 3, "Silver", "BMW 2 Series", 2018 },
                    { 6, 3, "Gray", "Mercedes-Benz C-Class", 2017 },
                    { 7, 4, "Green", "Mazda MX-5", 2021 },
                    { 8, 4, "Yellow", "Ford Mustang", 2020 },
                    { 9, 5, "White", "Volkswagen Golf", 2019 },
                    { 10, 5, "Black", "Honda Fit", 2020 }
                });

            migrationBuilder.CreateIndex(
                name: "IX_Cars_CategoryId",
                table: "Cars",
                column: "CategoryId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Cars");

            migrationBuilder.DropTable(
                name: "Categories");
        }
    }
}
