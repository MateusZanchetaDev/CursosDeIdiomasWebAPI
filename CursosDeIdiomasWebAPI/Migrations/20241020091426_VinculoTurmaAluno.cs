using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CursosDeIdiomasWebAPI.Migrations
{
    /// <inheritdoc />
    public partial class VinculoTurmaAluno : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Alunos_Turmas_CodigoTurma",
                table: "Alunos");

            migrationBuilder.DropIndex(
                name: "IX_Alunos_CodigoTurma",
                table: "Alunos");

            migrationBuilder.AlterColumn<string>(
                name: "CodigoTurma",
                table: "Alunos",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)");

            migrationBuilder.CreateTable(
                name: "AlunoTurma",
                columns: table => new
                {
                    listAlunosCPF = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    listTurmasCodigo = table.Column<string>(type: "nvarchar(450)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AlunoTurma", x => new { x.listAlunosCPF, x.listTurmasCodigo });
                    table.ForeignKey(
                        name: "FK_AlunoTurma_Alunos_listAlunosCPF",
                        column: x => x.listAlunosCPF,
                        principalTable: "Alunos",
                        principalColumn: "CPF",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_AlunoTurma_Turmas_listTurmasCodigo",
                        column: x => x.listTurmasCodigo,
                        principalTable: "Turmas",
                        principalColumn: "Codigo",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_AlunoTurma_listTurmasCodigo",
                table: "AlunoTurma",
                column: "listTurmasCodigo");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AlunoTurma");

            migrationBuilder.AlterColumn<string>(
                name: "CodigoTurma",
                table: "Alunos",
                type: "nvarchar(450)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.CreateIndex(
                name: "IX_Alunos_CodigoTurma",
                table: "Alunos",
                column: "CodigoTurma");

            migrationBuilder.AddForeignKey(
                name: "FK_Alunos_Turmas_CodigoTurma",
                table: "Alunos",
                column: "CodigoTurma",
                principalTable: "Turmas",
                principalColumn: "Codigo",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
