using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WikiMovies.Migrations
{
    public partial class migrationInitial : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Categorias",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    NameCategory = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Status = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Categorias", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Peliculas",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Age = table.Column<int>(type: "int", nullable: false),
                    Director = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CategoryId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Peliculas", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Peliculas_Categorias_CategoryId",
                        column: x => x.CategoryId,
                        principalTable: "Categorias",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "Categorias",
                columns: new[] { "Id", "NameCategory", "Status" },
                values: new object[,]
                {
                    { 1, "Accion", true },
                    { 2, "Drama", true },
                    { 3, "Ciencia Ficcion", true },
                    { 4, "Anime", true },
                    { 5, "Comedia", true }
                });

            migrationBuilder.InsertData(
                table: "Peliculas",
                columns: new[] { "Id", "Age", "CategoryId", "Director", "Name" },
                values: new object[] { 1, 0, 3, null, "Constantine" });

            migrationBuilder.InsertData(
                table: "Peliculas",
                columns: new[] { "Id", "Age", "CategoryId", "Director", "Name" },
                values: new object[] { 2, 0, 2, null, "Hasta el ultimo hombre" });

            migrationBuilder.InsertData(
                table: "Peliculas",
                columns: new[] { "Id", "Age", "CategoryId", "Director", "Name" },
                values: new object[] { 3, 0, 5, null, "Jonny English" });

            migrationBuilder.CreateIndex(
                name: "IX_Peliculas_CategoryId",
                table: "Peliculas",
                column: "CategoryId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Peliculas");

            migrationBuilder.DropTable(
                name: "Categorias");
        }
    }
}
