using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WoodAcademy.Migrations
{
    /// <inheritdoc />
    public partial class AllView : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_CorImages",
                table: "CorImages");

            migrationBuilder.RenameTable(
                name: "CorImages",
                newName: "CorridorImages");

            migrationBuilder.AddPrimaryKey(
                name: "PK_CorridorImages",
                table: "CorridorImages",
                column: "Id");

            migrationBuilder.CreateTable(
                name: "BathImages",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ImagePath = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BathImages", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "DoorImages",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ImagePath = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DoorImages", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "TvImages",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ImagePath = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TvImages", x => x.Id);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "BathImages");

            migrationBuilder.DropTable(
                name: "DoorImages");

            migrationBuilder.DropTable(
                name: "TvImages");

            migrationBuilder.DropPrimaryKey(
                name: "PK_CorridorImages",
                table: "CorridorImages");

            migrationBuilder.RenameTable(
                name: "CorridorImages",
                newName: "CorImages");

            migrationBuilder.AddPrimaryKey(
                name: "PK_CorImages",
                table: "CorImages",
                column: "Id");
        }
    }
}
