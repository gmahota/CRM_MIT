using System;
using System.Collections.Generic;
using Microsoft.Data.Entity.Migrations;

namespace CRM.Migrations
{
    public partial class update_telemovel_funcionario : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "telefoneAlternativo",
                table: "Funcionario",
                nullable: true);
            migrationBuilder.AddColumn<string>(
                name: "telemovel",
                table: "Funcionario",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(name: "telefoneAlternativo", table: "Funcionario");
            migrationBuilder.DropColumn(name: "telemovel", table: "Funcionario");
        }
    }
}
