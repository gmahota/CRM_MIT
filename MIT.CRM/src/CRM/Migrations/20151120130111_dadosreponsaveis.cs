using System;
using System.Collections.Generic;
using Microsoft.Data.Entity.Migrations;

namespace CRM.Migrations
{
    public partial class dadosreponsaveis : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "utilizadorId",
                table: "Funcionario",
                nullable: true);
            migrationBuilder.AddColumn<string>(
                name: "responsavelId",
                table: "Departamento",
                nullable: true);
            migrationBuilder.AddForeignKey(
                name: "FK_Departamento_ApplicationUser_responsavelId",
                table: "Departamento",
                column: "responsavelId",
                principalTable: "AspNetUsers",
                principalColumn: "Id");
            migrationBuilder.AddForeignKey(
                name: "FK_Funcionario_ApplicationUser_utilizadorId",
                table: "Funcionario",
                column: "utilizadorId",
                principalTable: "AspNetUsers",
                principalColumn: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(name: "FK_Departamento_ApplicationUser_responsavelId", table: "Departamento");
            migrationBuilder.DropForeignKey(name: "FK_Funcionario_ApplicationUser_utilizadorId", table: "Funcionario");
            migrationBuilder.DropColumn(name: "utilizadorId", table: "Funcionario");
            migrationBuilder.DropColumn(name: "responsavelId", table: "Departamento");
        }
    }
}
