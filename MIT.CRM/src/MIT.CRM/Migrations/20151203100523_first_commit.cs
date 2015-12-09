using System;
using System.Collections.Generic;
using Microsoft.Data.Entity.Migrations;
using Microsoft.Data.Entity.Metadata;

namespace MIT.CRM.Migrations
{
    public partial class first_commit : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(name: "FK_IdentityRoleClaim<string>_IdentityRole_RoleId", table: "AspNetRoleClaims");
            migrationBuilder.DropForeignKey(name: "FK_IdentityUserClaim<string>_ApplicationUser_UserId", table: "AspNetUserClaims");
            migrationBuilder.DropForeignKey(name: "FK_IdentityUserLogin<string>_ApplicationUser_UserId", table: "AspNetUserLogins");
            migrationBuilder.DropForeignKey(name: "FK_IdentityUserRole<string>_IdentityRole_RoleId", table: "AspNetUserRoles");
            migrationBuilder.DropForeignKey(name: "FK_IdentityUserRole<string>_ApplicationUser_UserId", table: "AspNetUserRoles");
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
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    departamento = table.Column<string>(nullable: true),
                    descricao = table.Column<string>(nullable: true),
                    empresaId = table.Column<string>(nullable: true),
                    responsavelId = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Departamento", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Departamento_Empresa_empresaId",
                        column: x => x.empresaId,
                        principalTable: "Empresa",
                        principalColumn: "codigo",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Departamento_ApplicationUser_responsavelId",
                        column: x => x.responsavelId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });
            migrationBuilder.CreateTable(
                name: "Funcionario",
                columns: table => new
                {
                    id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    categoria = table.Column<string>(nullable: true),
                    classificacao = table.Column<string>(nullable: true),
                    codigo = table.Column<string>(nullable: true),
                    dataAdmissao = table.Column<DateTime>(nullable: true),
                    dataClassificacao = table.Column<DateTime>(nullable: true),
                    dataFimContrato = table.Column<DateTime>(nullable: true),
                    dataNascimento = table.Column<DateTime>(nullable: true),
                    dataReadmissao = table.Column<DateTime>(nullable: true),
                    departamentoId = table.Column<int>(nullable: false),
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
                    telefone = table.Column<string>(nullable: true),
                    telefoneAlternativo = table.Column<string>(nullable: true),
                    telemovel = table.Column<string>(nullable: true),
                    utilizadorId = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Funcionario", x => x.id);
                    table.ForeignKey(
                        name: "FK_Funcionario_Departamento_departamentoId",
                        column: x => x.departamentoId,
                        principalTable: "Departamento",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Funcionario_Empresa_empresaId",
                        column: x => x.empresaId,
                        principalTable: "Empresa",
                        principalColumn: "codigo",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Funcionario_ApplicationUser_utilizadorId",
                        column: x => x.utilizadorId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });
            migrationBuilder.CreateTable(
                name: "Ferias",
                columns: table => new
                {
                    id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    dataFim = table.Column<DateTime>(nullable: false),
                    dataInicio = table.Column<DateTime>(nullable: false),
                    funcionarioId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Ferias", x => x.id);
                    table.ForeignKey(
                        name: "FK_Ferias_Funcionario_funcionarioId",
                        column: x => x.funcionarioId,
                        principalTable: "Funcionario",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
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
                    funcionarioId = table.Column<int>(nullable: false),
                    originouFalta = table.Column<bool>(nullable: false),
                    originouFaltaSubAlim = table.Column<bool>(nullable: false),
                    tipoMarcacao = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FuncFerias", x => x.id);
                    table.ForeignKey(
                        name: "FK_FuncFerias_Funcionario_funcionarioId",
                        column: x => x.funcionarioId,
                        principalTable: "Funcionario",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
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
                    funcionarioId = table.Column<int>(nullable: false),
                    totalDias = table.Column<double>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FuncInfFerias", x => x.id);
                    table.ForeignKey(
                        name: "FK_FuncInfFerias_Funcionario_funcionarioId",
                        column: x => x.funcionarioId,
                        principalTable: "Funcionario",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });
            migrationBuilder.CreateTable(
                name: "Ferias_Itens",
                columns: table => new
                {
                    id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    ano = table.Column<short>(nullable: false),
                    dataFeria = table.Column<DateTime>(nullable: false),
                    estado = table.Column<string>(nullable: true),
                    estadoGozo = table.Column<bool>(nullable: false),
                    feriasId = table.Column<int>(nullable: true),
                    funcionarioId = table.Column<int>(nullable: false),
                    originouFalta = table.Column<bool>(nullable: false),
                    originouFaltaSubAlim = table.Column<bool>(nullable: false),
                    tipo = table.Column<string>(nullable: true),
                    tipoMarcacao = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Ferias_Itens", x => x.id);
                    table.ForeignKey(
                        name: "FK_Ferias_Itens_Ferias_feriasId",
                        column: x => x.feriasId,
                        principalTable: "Ferias",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Ferias_Itens_Funcionario_funcionarioId",
                        column: x => x.funcionarioId,
                        principalTable: "Funcionario",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });
            migrationBuilder.CreateTable(
                name: "Historio_Ferias_Item",
                columns: table => new
                {
                    id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    data = table.Column<DateTime>(nullable: false),
                    estado = table.Column<string>(nullable: true),
                    ferias_item_id = table.Column<int>(nullable: false),
                    utilizadorId = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Historio_Ferias_Item", x => x.id);
                    table.ForeignKey(
                        name: "FK_Historio_Ferias_Item_Ferias_Itens_ferias_item_id",
                        column: x => x.ferias_item_id,
                        principalTable: "Ferias_Itens",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Historio_Ferias_Item_ApplicationUser_utilizadorId",
                        column: x => x.utilizadorId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });
            migrationBuilder.AlterColumn<string>(
                name: "UserId",
                table: "AspNetUserLogins",
                nullable: false);
            migrationBuilder.AlterColumn<string>(
                name: "UserId",
                table: "AspNetUserClaims",
                nullable: false);
            migrationBuilder.AlterColumn<string>(
                name: "RoleId",
                table: "AspNetRoleClaims",
                nullable: false);
            migrationBuilder.AddForeignKey(
                name: "FK_IdentityRoleClaim<string>_IdentityRole_RoleId",
                table: "AspNetRoleClaims",
                column: "RoleId",
                principalTable: "AspNetRoles",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
            migrationBuilder.AddForeignKey(
                name: "FK_IdentityUserClaim<string>_ApplicationUser_UserId",
                table: "AspNetUserClaims",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
            migrationBuilder.AddForeignKey(
                name: "FK_IdentityUserLogin<string>_ApplicationUser_UserId",
                table: "AspNetUserLogins",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
            migrationBuilder.AddForeignKey(
                name: "FK_IdentityUserRole<string>_IdentityRole_RoleId",
                table: "AspNetUserRoles",
                column: "RoleId",
                principalTable: "AspNetRoles",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
            migrationBuilder.AddForeignKey(
                name: "FK_IdentityUserRole<string>_ApplicationUser_UserId",
                table: "AspNetUserRoles",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(name: "FK_IdentityRoleClaim<string>_IdentityRole_RoleId", table: "AspNetRoleClaims");
            migrationBuilder.DropForeignKey(name: "FK_IdentityUserClaim<string>_ApplicationUser_UserId", table: "AspNetUserClaims");
            migrationBuilder.DropForeignKey(name: "FK_IdentityUserLogin<string>_ApplicationUser_UserId", table: "AspNetUserLogins");
            migrationBuilder.DropForeignKey(name: "FK_IdentityUserRole<string>_IdentityRole_RoleId", table: "AspNetUserRoles");
            migrationBuilder.DropForeignKey(name: "FK_IdentityUserRole<string>_ApplicationUser_UserId", table: "AspNetUserRoles");
            migrationBuilder.DropTable("FuncFerias");
            migrationBuilder.DropTable("FuncInfFerias");
            migrationBuilder.DropTable("Historio_Ferias_Item");
            migrationBuilder.DropTable("Ferias_Itens");
            migrationBuilder.DropTable("Ferias");
            migrationBuilder.DropTable("Funcionario");
            migrationBuilder.DropTable("Departamento");
            migrationBuilder.DropTable("Empresa");
            migrationBuilder.AlterColumn<string>(
                name: "UserId",
                table: "AspNetUserLogins",
                nullable: true);
            migrationBuilder.AlterColumn<string>(
                name: "UserId",
                table: "AspNetUserClaims",
                nullable: true);
            migrationBuilder.AlterColumn<string>(
                name: "RoleId",
                table: "AspNetRoleClaims",
                nullable: true);
            migrationBuilder.AddForeignKey(
                name: "FK_IdentityRoleClaim<string>_IdentityRole_RoleId",
                table: "AspNetRoleClaims",
                column: "RoleId",
                principalTable: "AspNetRoles",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
            migrationBuilder.AddForeignKey(
                name: "FK_IdentityUserClaim<string>_ApplicationUser_UserId",
                table: "AspNetUserClaims",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
            migrationBuilder.AddForeignKey(
                name: "FK_IdentityUserLogin<string>_ApplicationUser_UserId",
                table: "AspNetUserLogins",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
            migrationBuilder.AddForeignKey(
                name: "FK_IdentityUserRole<string>_IdentityRole_RoleId",
                table: "AspNetUserRoles",
                column: "RoleId",
                principalTable: "AspNetRoles",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
            migrationBuilder.AddForeignKey(
                name: "FK_IdentityUserRole<string>_ApplicationUser_UserId",
                table: "AspNetUserRoles",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
