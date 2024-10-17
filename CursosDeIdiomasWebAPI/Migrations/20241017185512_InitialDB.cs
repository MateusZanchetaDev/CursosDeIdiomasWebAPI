using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CursosDeIdiomasWebAPI.Migrations
{
    /// <inheritdoc />
    public partial class InitialDB : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Alunos",
                columns: table => new
                {
                    CPF = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Nome = table.Column<string>(type: "nvarchar(150)", maxLength: 150, nullable: false),
                    Email = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Alunos", x => x.CPF);
                });

            migrationBuilder.CreateTable(
                name: "Turmas",
                columns: table => new
                {
                    Codigo = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Nivel = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    AlunoCPF = table.Column<string>(type: "nvarchar(450)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Turmas", x => x.Codigo);
                    table.ForeignKey(
                        name: "FK_Turmas_Alunos_AlunoCPF",
                        column: x => x.AlunoCPF,
                        principalTable: "Alunos",
                        principalColumn: "CPF");
                });

            migrationBuilder.CreateIndex(
                name: "IX_Turmas_AlunoCPF",
                table: "Turmas",
                column: "AlunoCPF");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Turmas");

            migrationBuilder.DropTable(
                name: "Alunos");
        }
    }
}
