using System;
using System.Collections.Generic;
using Microsoft.Data.Entity.Migrations;
using Microsoft.Data.Entity.Metadata;

namespace CRM.Migrations
{
    public partial class add_tabela_ferias : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
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
                        principalColumn: "id");
                });
            migrationBuilder.CreateTable(
                name: "Ferias_Itens",
                columns: table => new
                {
                    id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    ano = table.Column<short>(nullable: false),
                    dataFeria = table.Column<DateTime>(nullable: false),
                    estadoGozo = table.Column<bool>(nullable: false),
                    feriasId = table.Column<int>(nullable: false),
                    funcionarioId = table.Column<int>(nullable: false),
                    originouFalta = table.Column<bool>(nullable: false),
                    originouFaltaSubAlim = table.Column<bool>(nullable: false),
                    tipoMarcacao = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Ferias_Itens", x => x.id);
                    table.ForeignKey(
                        name: "FK_Ferias_Itens_Ferias_feriasId",
                        column: x => x.feriasId,
                        principalTable: "Ferias",
                        principalColumn: "id");
                    table.ForeignKey(
                        name: "FK_Ferias_Itens_Funcionario_funcionarioId",
                        column: x => x.funcionarioId,
                        principalTable: "Funcionario",
                        principalColumn: "id");
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable("Ferias_Itens");
            migrationBuilder.DropTable("Ferias");
        }
    }
}
