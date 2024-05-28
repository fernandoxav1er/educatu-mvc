using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace EducaTu.Migrations
{
    /// <inheritdoc />
    public partial class CriandoTabelaCursos : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<int>(
                name: "Perfil",
                table: "Usuario",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.CreateTable(
                name: "Cursos",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    NomeCurso = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    DescricaoCurso = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    IFrameVideo = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    TipoImagem = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    AutorCurso = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CriadoPor = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Cursos", x => x.Id);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Cursos");

            migrationBuilder.AlterColumn<int>(
                name: "Perfil",
                table: "Usuario",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);
        }
    }
}
