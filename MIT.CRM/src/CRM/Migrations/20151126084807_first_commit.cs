using System;
using System.Collections.Generic;
using Microsoft.Data.Entity.Migrations;
using Microsoft.Data.Entity.Metadata;

namespace CRM.Migrations
{
    public partial class first_commit : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "AspNetUsers",
                columns: table => new
                {
                    Id = table.Column<string>(nullable: false),
                    AccessFailedCount = table.Column<int>(nullable: false),
                    ConcurrencyStamp = table.Column<string>(nullable: true),
                    Email = table.Column<string>(nullable: true),
                    EmailConfirmed = table.Column<bool>(nullable: false),
                    LockoutEnabled = table.Column<bool>(nullable: false),
                    LockoutEnd = table.Column<DateTimeOffset>(nullable: true),
                    NormalizedEmail = table.Column<string>(nullable: true),
                    NormalizedUserName = table.Column<string>(nullable: true),
                    PasswordHash = table.Column<string>(nullable: true),
                    PhoneNumber = table.Column<string>(nullable: true),
                    PhoneNumberConfirmed = table.Column<bool>(nullable: false),
                    SecurityStamp = table.Column<string>(nullable: true),
                    TwoFactorEnabled = table.Column<bool>(nullable: false),
                    UserName = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ApplicationUser", x => x.Id);
                });
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
                name: "AspNetRoles",
                columns: table => new
                {
                    Id = table.Column<string>(nullable: false),
                    ConcurrencyStamp = table.Column<string>(nullable: true),
                    Name = table.Column<string>(nullable: true),
                    NormalizedName = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_IdentityRole", x => x.Id);
                });
            migrationBuilder.CreateTable(
                name: "AspNetUserClaims",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    ClaimType = table.Column<string>(nullable: true),
                    ClaimValue = table.Column<string>(nullable: true),
                    UserId = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_IdentityUserClaim<string>", x => x.Id);
                    table.ForeignKey(
                        name: "FK_IdentityUserClaim<string>_ApplicationUser_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id");
                });
            migrationBuilder.CreateTable(
                name: "AspNetUserLogins",
                columns: table => new
                {
                    LoginProvider = table.Column<string>(nullable: false),
                    ProviderKey = table.Column<string>(nullable: false),
                    ProviderDisplayName = table.Column<string>(nullable: true),
                    UserId = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_IdentityUserLogin<string>", x => new { x.LoginProvider, x.ProviderKey });
                    table.ForeignKey(
                        name: "FK_IdentityUserLogin<string>_ApplicationUser_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id");
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
                        principalColumn: "codigo");
                    table.ForeignKey(
                        name: "FK_Departamento_ApplicationUser_responsavelId",
                        column: x => x.responsavelId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id");
                });
            migrationBuilder.CreateTable(
                name: "AspNetRoleClaims",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    ClaimType = table.Column<string>(nullable: true),
                    ClaimValue = table.Column<string>(nullable: true),
                    RoleId = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_IdentityRoleClaim<string>", x => x.Id);
                    table.ForeignKey(
                        name: "FK_IdentityRoleClaim<string>_IdentityRole_RoleId",
                        column: x => x.RoleId,
                        principalTable: "AspNetRoles",
                        principalColumn: "Id");
                });
            migrationBuilder.CreateTable(
                name: "AspNetUserRoles",
                columns: table => new
                {
                    UserId = table.Column<string>(nullable: false),
                    RoleId = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_IdentityUserRole<string>", x => new { x.UserId, x.RoleId });
                    table.ForeignKey(
                        name: "FK_IdentityUserRole<string>_IdentityRole_RoleId",
                        column: x => x.RoleId,
                        principalTable: "AspNetRoles",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_IdentityUserRole<string>_ApplicationUser_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id");
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
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Funcionario_Empresa_empresaId",
                        column: x => x.empresaId,
                        principalTable: "Empresa",
                        principalColumn: "codigo");
                    table.ForeignKey(
                        name: "FK_Funcionario_ApplicationUser_utilizadorId",
                        column: x => x.utilizadorId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id");
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
                    tipoMarcacao = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FuncFerias", x => x.id);
                    table.ForeignKey(
                        name: "FK_FuncFerias_Funcionario_funcionarioId",
                        column: x => x.funcionarioId,
                        principalTable: "Funcionario",
                        principalColumn: "id");
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
                        principalColumn: "id");
                });
            migrationBuilder.CreateIndex(
                name: "EmailIndex",
                table: "AspNetUsers",
                column: "NormalizedEmail");
            migrationBuilder.CreateIndex(
                name: "UserNameIndex",
                table: "AspNetUsers",
                column: "NormalizedUserName");
            migrationBuilder.CreateIndex(
                name: "RoleNameIndex",
                table: "AspNetRoles",
                column: "NormalizedName");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable("FuncFerias");
            migrationBuilder.DropTable("FuncInfFerias");
            migrationBuilder.DropTable("AspNetRoleClaims");
            migrationBuilder.DropTable("AspNetUserClaims");
            migrationBuilder.DropTable("AspNetUserLogins");
            migrationBuilder.DropTable("AspNetUserRoles");
            migrationBuilder.DropTable("Funcionario");
            migrationBuilder.DropTable("AspNetRoles");
            migrationBuilder.DropTable("Departamento");
            migrationBuilder.DropTable("Empresa");
            migrationBuilder.DropTable("AspNetUsers");
        }
    }
}
