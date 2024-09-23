using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AutoShop.Migrations
{
    /// <inheritdoc />
    public partial class updatedCarClass : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "Categories",
                type: "nvarchar(20)",
                maxLength: 20,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(30)",
                oldMaxLength: 30);

            migrationBuilder.AlterColumn<string>(
                name: "Description",
                table: "Categories",
                type: "nvarchar(150)",
                maxLength: 150,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(100)",
                oldMaxLength: 100);

            migrationBuilder.AlterColumn<string>(
                name: "Model",
                table: "Cars",
                type: "nvarchar(30)",
                maxLength: 30,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AddColumn<string>(
                name: "ImagePath",
                table: "Cars",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<decimal>(
                name: "Price",
                table: "Cars",
                type: "decimal(18,2)",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.UpdateData(
                table: "Cars",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "ImagePath", "Price" },
                values: new object[] { "https://images.dealer.com/autodata/us/color/2020/USD00TOC021D0/040.jpg", 28007m });

            migrationBuilder.UpdateData(
                table: "Cars",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "ImagePath", "Price" },
                values: new object[] { "https://di-uploads-pod11.dealerinspire.com/hondaofkirkland/uploads/2019/08/2019-Civic-Type-R-Aegean-Blue.png", 20000m });

            migrationBuilder.UpdateData(
                table: "Cars",
                keyColumn: "Id",
                keyValue: 3,
                columns: new[] { "ImagePath", "Price" },
                values: new object[] { "https://img.autobytel.com/chrome/colormatched_01/white/640/cc_2021fos10_01_640/cc_2021fos100055_01_640_um.jpg", 32999m });

            migrationBuilder.UpdateData(
                table: "Cars",
                keyColumn: "Id",
                keyValue: 4,
                columns: new[] { "ImagePath", "Price" },
                values: new object[] { "https://dealer-content.s3.amazonaws.com/images/models/chevrolet/2022/tahoe/colors/auburn-metallic.png", 52990m });

            migrationBuilder.UpdateData(
                table: "Cars",
                keyColumn: "Id",
                keyValue: 5,
                columns: new[] { "ImagePath", "Price" },
                values: new object[] { "https://static.tcimg.net/vehicles/gallery/1d9ae78296ed295e/2018-BMW-2_Series.jpg", 9999m });

            migrationBuilder.UpdateData(
                table: "Cars",
                keyColumn: "Id",
                keyValue: 6,
                columns: new[] { "ImagePath", "Price" },
                values: new object[] { "https://di-uploads-pod5.dealerinspire.com/mercedesbenzofpalmsprings/uploads/2017/05/2017-C-300-Sedan-Selenite-Grey-Metallic-1.png", 9300m });

            migrationBuilder.UpdateData(
                table: "Cars",
                keyColumn: "Id",
                keyValue: 7,
                columns: new[] { "ImagePath", "Price" },
                values: new object[] { "https://www.miataforumz.com/attachments/miata-general-discussion-36/1958d1317909812-mazda-mx-5-miata-gets-black-tuned-japan-black-tuned-roadster-628.jpg", 27999m });

            migrationBuilder.UpdateData(
                table: "Cars",
                keyColumn: "Id",
                keyValue: 8,
                columns: new[] { "ImagePath", "Price" },
                values: new object[] { "https://i.ebayimg.com/images/g/BsAAAOSwpaZbmWIz/s-l1200.jpg", 65000m });

            migrationBuilder.UpdateData(
                table: "Cars",
                keyColumn: "Id",
                keyValue: 9,
                columns: new[] { "ImagePath", "Price" },
                values: new object[] { "https://www.stablevehiclecontracts.co.uk/uploads/2-2020-vw-golf-gti-mk8-pure-white.jpg", 23000m });

            migrationBuilder.UpdateData(
                table: "Cars",
                keyColumn: "Id",
                keyValue: 10,
                columns: new[] { "ImagePath", "Price" },
                values: new object[] { "https://platform.cstatic-images.com/xlarge/in/v2/stock_photos/4f7a628e-4f6c-4ea4-b32f-3bdb82cbd18b/dd77a9fa-afc6-4dc4-baa2-bdddb8383dac.png", 17100m });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ImagePath",
                table: "Cars");

            migrationBuilder.DropColumn(
                name: "Price",
                table: "Cars");

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "Categories",
                type: "nvarchar(30)",
                maxLength: 30,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(20)",
                oldMaxLength: 20);

            migrationBuilder.AlterColumn<string>(
                name: "Description",
                table: "Categories",
                type: "nvarchar(100)",
                maxLength: 100,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(150)",
                oldMaxLength: 150);

            migrationBuilder.AlterColumn<string>(
                name: "Model",
                table: "Cars",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(30)",
                oldMaxLength: 30);
        }
    }
}
