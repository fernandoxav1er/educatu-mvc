using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace EducaTu.Migrations
{
    /// <inheritdoc />
    public partial class AtualizandoTabelaPlanoComValor : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<decimal>(
                name: "Valor",
                table: "Planos",
                type: "decimal(18,2)",
                nullable: false,
                defaultValue: 0m);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Valor",
                table: "Planos");
        }
    }
}
