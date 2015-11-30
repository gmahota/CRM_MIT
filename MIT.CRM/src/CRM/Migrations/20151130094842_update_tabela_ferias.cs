using System;
using System.Collections.Generic;
using Microsoft.Data.Entity.Migrations;

namespace CRM.Migrations
{
    public partial class update_tabela_ferias : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<int>(
                name: "feriasId",
                table: "Ferias_Itens",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<int>(
                name: "feriasId",
                table: "Ferias_Itens",
                nullable: false);
        }
    }
}
