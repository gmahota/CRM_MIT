using System;
using System.Collections.Generic;
using Microsoft.Data.Entity.Migrations;
using Microsoft.Data.Entity.Metadata;

namespace CRM.Migrations
{
    public partial class ActulizacaoDadosFuncionarios : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Empresa",
                columns: table => new
                {
                    codigo = table.Column<string>(nullable: false),
                    LogoTipo = table.Column<byte[]>(nullable: true),
                    categoria = table.Column<string>(nullable: true),
                    codEmpresaPri = table.Column<string>(nullable: true),
                    conexao = table.Column<string>(nullable: true),
                    credentials = table.Column<string>(nullable: true),
                    email = table.Column<string>(nullable: true),
                    empresaPrimavera = table.Column<bool>(nullable: true),
                    enableSsl = table.Column<bool>(nullable: true),
                    host = table.Column<string>(nullable: true),
                    localidadeEmpresa = table.Column<string>(nullable: true),
                    morada = table.Column<string>(nullable: true),
                    moradaEmpresa = table.Column<string>(nullable: true),
                    nome = table.Column<string>(nullable: true),
                    nomeEmpresa = table.Column<string>(nullable: true),
                    nuit = table.Column<string>(nullable: true),
                    nuitEmpresa = table.Column<string>(nullable: true),
                    passwordUtilizadorPrimavera = table.Column<string>(nullable: true),
                    port = table.Column<int>(nullable: true),
                    telefoneEmpresa = table.Column<string>(nullable: true),
                    tipoEmpresa = table.Column<string>(nullable: true),
                    useDefaultCredentials = table.Column<bool>(nullable: true),
                    utilizadorPrimavera = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Empresa", x => x.codigo);
                });
            migrationBuilder.CreateTable(
                name: "Departamento",
                columns: table => new
                {
                    departamento = table.Column<string>(nullable: false),
                    descricao = table.Column<string>(nullable: true),
                    empresaId = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Departamento", x => x.departamento);
                    table.ForeignKey(
                        name: "FK_Departamento_Empresa_empresaId",
                        column: x => x.empresaId,
                        principalTable: "Empresa",
                        principalColumn: "codigo");
                });
            migrationBuilder.CreateTable(
                name: "Funcionario",
                columns: table => new
                {
                    codigo = table.Column<string>(nullable: false),
                    categoria = table.Column<string>(nullable: true),
                    classificacao = table.Column<string>(nullable: true),
                    dataAdmissao = table.Column<DateTime>(nullable: true),
                    dataClassificacao = table.Column<DateTime>(nullable: true),
                    dataFimContrato = table.Column<DateTime>(nullable: true),
                    dataNascimento = table.Column<DateTime>(nullable: true),
                    dataReadmissao = table.Column<DateTime>(nullable: true),
                    departamentoId = table.Column<string>(nullable: true),
                    distrito = table.Column<string>(nullable: true),
                    email = table.Column<string>(nullable: true),
                    empresaId = table.Column<string>(nullable: true),
                    estadoCivil = table.Column<string>(nullable: true),
                    habilitacao = table.Column<string>(nullable: true),
                    localidade = table.Column<string>(nullable: true),
                    nacionalidade = table.Column<string>(nullable: true),
                    naturalidade = table.Column<string>(nullable: true),
                    nome = table.Column<string>(nullable: true),
                    profissao = table.Column<string>(nullable: true),
                    sexo = table.Column<string>(nullable: true),
                    telefone = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Funcionario", x => x.codigo);
                    table.ForeignKey(
                        name: "FK_Funcionario_Departamento_departamentoId",
                        column: x => x.departamentoId,
                        principalTable: "Departamento",
                        principalColumn: "departamento");
                    table.ForeignKey(
                        name: "FK_Funcionario_Empresa_empresaId",
                        column: x => x.empresaId,
                        principalTable: "Empresa",
                        principalColumn: "codigo");
                });
            migrationBuilder.CreateTable(
                name: "FuncFerias",
                columns: table => new
                {
                    id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    ano = table.Column<short>(nullable: false),
                    dataFeria = table.Column<DateTime>(nullable: false),
                    estadoGozo = table.Column<bool>(nullable: false),
                    funcionarioId = table.Column<string>(nullable: true),
                    originouFalta = table.Column<bool>(nullable: false),
                    originouFaltaSubAlim = table.Column<bool>(nullable: false),
                    tipoMarcacao = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FuncFerias", x => x.id);
                    table.ForeignKey(
                        name: "FK_FuncFerias_Funcionario_funcionarioId",
                        column: x => x.funcionarioId,
                        principalTable: "Funcionario",
                        principalColumn: "codigo");
                });
            migrationBuilder.CreateTable(
                name: "FuncInfFerias",
                columns: table => new
                {
                    id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    ano = table.Column<short>(nullable: false),
                    diasAdicionais = table.Column<double>(nullable: false),
                    diasAnoAnterior = table.Column<double>(nullable: false),
                    diasDireito = table.Column<double>(nullable: false),
                    diasFeriasJaPagas = table.Column<double>(nullable: false),
                    diasJaGozados = table.Column<double>(nullable: false),
                    diasPorGozar = table.Column<double>(nullable: false),
                    diasPorMarcar = table.Column<double>(nullable: false),
                    funcSemFerias = table.Column<bool>(nullable: false),
                    funcionarioId = table.Column<string>(nullable: true),
                    totalDias = table.Column<double>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FuncInfFerias", x => x.id);
                    table.ForeignKey(
                        name: "FK_FuncInfFerias_Funcionario_funcionarioId",
                        column: x => x.funcionarioId,
                        principalTable: "Funcionario",
                        principalColumn: "codigo");
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable("FuncFerias");
            migrationBuilder.DropTable("FuncInfFerias");
            migrationBuilder.DropTable("Funcionario");
            migrationBuilder.DropTable("Departamento");
            migrationBuilder.DropTable("Empresa");
        }
    }
}
