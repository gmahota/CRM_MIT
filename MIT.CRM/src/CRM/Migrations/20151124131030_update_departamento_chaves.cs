using System;
using System.Collections.Generic;
using Microsoft.Data.Entity.Migrations;

namespace CRM.Migrations
{
    public partial class update_departamento_chaves : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(name: "FK_Funcionario_Departamento_departamentoId", table: "Funcionario");
            migrationBuilder.DropPrimaryKey(name: "PK_Departamento", table: "Departamento");
            migrationBuilder.AlterColumn<Guid>(
                name: "departamentoId",
                table: "Funcionario",
                nullable: false);
            migrationBuilder.AlterColumn<string>(
                name: "departamento",
                table: "Departamento",
                nullable: true);
            migrationBuilder.AddColumn<Guid>(
                name: "Id",
                table: "Departamento",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));
            migrationBuilder.AddPrimaryKey(
                name: "PK_Departamento",
                table: "Departamento",
                column: "Id");
            migrationBuilder.AddForeignKey(
                name: "FK_Funcionario_Departamento_departamentoId",
                table: "Funcionario",
                column: "departamentoId",
                principalTable: "Departamento",
                principalColumn: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(name: "FK_Funcionario_Departamento_departamentoId", table: "Funcionario");
            migrationBuilder.DropPrimaryKey(name: "PK_Departamento", table: "Departamento");
            migrationBuilder.DropColumn(name: "Id", table: "Departamento");
            migrationBuilder.AlterColumn<string>(
                name: "departamentoId",
                table: "Funcionario",
                nullable: true);
            migrationBuilder.AlterColumn<string>(
                name: "departamento",
                table: "Departamento",
                nullable: false);
            migrationBuilder.AddPrimaryKey(
                name: "PK_Departamento",
                table: "Departamento",
                column: "departamento");
            migrationBuilder.AddForeignKey(
                name: "FK_Funcionario_Departamento_departamentoId",
                table: "Funcionario",
                column: "departamentoId",
                principalTable: "Departamento",
                principalColumn: "departamento");
        }
    }
}
