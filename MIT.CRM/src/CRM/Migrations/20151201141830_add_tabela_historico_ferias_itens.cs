using System;
using System.Collections.Generic;
using Microsoft.Data.Entity.Migrations;
using Microsoft.Data.Entity.Metadata;

namespace CRM.Migrations
{
    public partial class add_tabela_historico_ferias_itens : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Historio_Ferias_Item",
                columns: table => new
                {
                    id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
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
                        principalColumn: "id");
                    table.ForeignKey(
                        name: "FK_Historio_Ferias_Item_ApplicationUser_utilizadorId",
                        column: x => x.utilizadorId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id");
                });
            migrationBuilder.AddColumn<string>(
                name: "estado",
                table: "Ferias_Itens",
                nullable: true);
            migrationBuilder.AddColumn<string>(
                name: "tipo",
                table: "Ferias_Itens",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(name: "estado", table: "Ferias_Itens");
            migrationBuilder.DropColumn(name: "tipo", table: "Ferias_Itens");
            migrationBuilder.DropTable("Historio_Ferias_Item");
        }
    }
}
