using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace AutoShop.Migrations
{
    /// <inheritdoc />
    public partial class AddedStorageSystemAndSoldCount : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Cars",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Cars",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Cars",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "Cars",
                keyColumn: "Id",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "Cars",
                keyColumn: "Id",
                keyValue: 5);

            migrationBuilder.DeleteData(
                table: "Cars",
                keyColumn: "Id",
                keyValue: 6);

            migrationBuilder.DeleteData(
                table: "Cars",
                keyColumn: "Id",
                keyValue: 7);

            migrationBuilder.DeleteData(
                table: "Cars",
                keyColumn: "Id",
                keyValue: 8);

            migrationBuilder.DeleteData(
                table: "Cars",
                keyColumn: "Id",
                keyValue: 9);

            migrationBuilder.DeleteData(
                table: "Cars",
                keyColumn: "Id",
                keyValue: 10);

            migrationBuilder.AddColumn<int>(
                name: "SoldCount",
                table: "Cars",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "StorageItemId",
                table: "Cars",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddUniqueConstraint(
                name: "AK_Cars_StorageItemId",
                table: "Cars",
                column: "StorageItemId");

            migrationBuilder.CreateTable(
                name: "StorageItems",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CarId = table.Column<int>(type: "int", nullable: false),
                    Count = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_StorageItems", x => x.Id);
                    table.ForeignKey(
                        name: "FK_StorageItems_Cars_CarId",
                        column: x => x.CarId,
                        principalTable: "Cars",
                        principalColumn: "StorageItemId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "Cars",
                columns: new[] { "Id", "CategoryId", "Color", "ImagePath", "Model", "Price", "SoldCount", "StorageItemId", "Year" },
                values: new object[,]
                {
                    { 1, 1, "White", "https://images.dealer.com/autodata/us/color/2020/USD00TOC021D0/040.jpg", "Toyota Camry", 28007m, 287, 1, 2020 },
                    { 2, 1, "Blue", "https://di-uploads-pod11.dealerinspire.com/hondaofkirkland/uploads/2019/08/2019-Civic-Type-R-Aegean-Blue.png", "Honda Civic", 20000m, 512, 2, 2019 },
                    { 3, 2, "Black", "https://img.autobytel.com/chrome/colormatched_01/white/640/cc_2021fos10_01_640/cc_2021fos100055_01_640_um.jpg", "Ford Explorer", 32999m, 297, 3, 2021 },
                    { 4, 2, "Red", "https://dealer-content.s3.amazonaws.com/images/models/chevrolet/2022/tahoe/colors/auburn-metallic.png", "Chevrolet Tahoe", 52990m, 263, 4, 2022 },
                    { 5, 3, "Silver", "https://static.tcimg.net/vehicles/gallery/1d9ae78296ed295e/2018-BMW-2_Series.jpg", "BMW 2 Series", 9999m, 823, 5, 2018 },
                    { 6, 3, "Gray", "https://di-uploads-pod5.dealerinspire.com/mercedesbenzofpalmsprings/uploads/2017/05/2017-C-300-Sedan-Selenite-Grey-Metallic-1.png", "Mercedes-Benz C-Class", 9300m, 768, 6, 2017 },
                    { 7, 4, "Green", "https://www.miataforumz.com/attachments/miata-general-discussion-36/1958d1317909812-mazda-mx-5-miata-gets-black-tuned-japan-black-tuned-roadster-628.jpg", "Mazda MX-5", 27999m, 324, 7, 2021 },
                    { 8, 4, "Yellow", "https://i.ebayimg.com/images/g/BsAAAOSwpaZbmWIz/s-l1200.jpg", "Ford Mustang", 65000m, 234, 8, 2020 },
                    { 9, 5, "White", "https://www.stablevehiclecontracts.co.uk/uploads/2-2020-vw-golf-gti-mk8-pure-white.jpg", "Volkswagen Golf", 23000m, 438, 9, 2019 },
                    { 10, 5, "Black", "https://platform.cstatic-images.com/xlarge/in/v2/stock_photos/4f7a628e-4f6c-4ea4-b32f-3bdb82cbd18b/dd77a9fa-afc6-4dc4-baa2-bdddb8383dac.png", "Honda Fit", 17100m, 642, 10, 2020 }
                });

            migrationBuilder.InsertData(
                table: "StorageItems",
                columns: new[] { "Id", "CarId", "Count" },
                values: new object[,]
                {
                    { 1, 1, 0 },
                    { 2, 2, 2 },
                    { 3, 3, 2 },
                    { 4, 4, 2 },
                    { 5, 5, 2 },
                    { 6, 6, 2 },
                    { 7, 7, 2 },
                    { 8, 8, 2 },
                    { 9, 9, 5 },
                    { 10, 10, 2 }
                });

            migrationBuilder.CreateIndex(
                name: "IX_StorageItems_CarId",
                table: "StorageItems",
                column: "CarId",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "StorageItems");

            migrationBuilder.DropUniqueConstraint(
                name: "AK_Cars_StorageItemId",
                table: "Cars");

            migrationBuilder.DropColumn(
                name: "SoldCount",
                table: "Cars");

            migrationBuilder.DropColumn(
                name: "StorageItemId",
                table: "Cars");
        }
    }
}
