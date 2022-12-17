using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace VirtualReality.Migrations
{
    /// <inheritdoc />
    public partial class AddFileId : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "SaleOrRent",
                table: "Houses",
                type: "integer",
                nullable: false,
                defaultValue: 0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "SaleOrRent",
                table: "Houses");
        }
    }
}
