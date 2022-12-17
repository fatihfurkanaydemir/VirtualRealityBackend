using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace VirtualReality.Migrations
{
    /// <inheritdoc />
    public partial class AddFileld : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_Houses",
                table: "Houses");

            migrationBuilder.DropColumn(
                name: "HouseId",
                table: "Houses");

            migrationBuilder.RenameColumn(
                name: "SaleOrRent",
                table: "Houses",
                newName: "Id");

            migrationBuilder.AlterColumn<int>(
                name: "Id",
                table: "Houses",
                type: "integer",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "integer")
                .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);

            migrationBuilder.AddPrimaryKey(
                name: "PK_Houses",
                table: "Houses",
                column: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_Houses",
                table: "Houses");

            migrationBuilder.RenameColumn(
                name: "Id",
                table: "Houses",
                newName: "SaleOrRent");

            migrationBuilder.AlterColumn<int>(
                name: "SaleOrRent",
                table: "Houses",
                type: "integer",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "integer")
                .OldAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);

            migrationBuilder.AddColumn<int>(
                name: "HouseId",
                table: "Houses",
                type: "integer",
                nullable: false,
                defaultValue: 0)
                .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);

            migrationBuilder.AddPrimaryKey(
                name: "PK_Houses",
                table: "Houses",
                column: "HouseId");
        }
    }
}
