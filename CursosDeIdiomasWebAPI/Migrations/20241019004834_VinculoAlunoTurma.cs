using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CursosDeIdiomasWebAPI.Migrations
{
    /// <inheritdoc />
    public partial class VinculoAlunoTurma : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "AlunoCPF",
                table: "Turmas");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "AlunoCPF",
                table: "Turmas",
                type: "nvarchar(max)",
                nullable: true);
        }
    }
}
