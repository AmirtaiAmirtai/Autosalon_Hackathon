using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SellCar.Database.Migration
{
    public partial class AddedSellerAge : Microsoft.EntityFrameworkCore.Migrations.Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "Age",
                table: "Sellers",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Age",
                table: "Sellers");
        }
    }
}
