using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WoodAcademy.Migrations
{
    /// <inheritdoc />
    public partial class AllImages : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_KitchenImages",
                table: "KitchenImages");

            migrationBuilder.RenameTable(
                name: "KitchenImages",
                newName: "Images");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Images",
                table: "Images",
                column: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_Images",
                table: "Images");

            migrationBuilder.RenameTable(
                name: "Images",
                newName: "KitchenImages");

            migrationBuilder.AddPrimaryKey(
                name: "PK_KitchenImages",
                table: "KitchenImages",
                column: "Id");
        }
    }
}
