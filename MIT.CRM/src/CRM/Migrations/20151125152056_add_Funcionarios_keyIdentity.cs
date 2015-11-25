using System;
using System.Collections.Generic;
using Microsoft.Data.Entity.Migrations;

namespace CRM.Migrations
{
    public partial class add_Funcionarios_keyIdentity : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(name: "FK_FuncFerias_Funcionario_funcionarioId", table: "FuncFerias");
            migrationBuilder.DropForeignKey(name: "FK_FuncInfFerias_Funcionario_funcionarioId", table: "FuncInfFerias");
            migrationBuilder.DropPrimaryKey(name: "PK_Funcionario", table: "Funcionario");
            migrationBuilder.AlterColumn<string>(
                name: "codigo",
                table: "Funcionario",
                nullable: true);
            migrationBuilder.AddColumn<Guid>(
                name: "id",
                table: "Funcionario",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));
            migrationBuilder.AddPrimaryKey(
                name: "PK_Funcionario",
                table: "Funcionario",
                column: "id");
            migrationBuilder.AlterColumn<Guid>(
                name: "funcionarioId",
                table: "FuncInfFerias",
                nullable: false);
            migrationBuilder.AlterColumn<Guid>(
                name: "funcionarioId",
                table: "FuncFerias",
                nullable: false);
            migrationBuilder.AddForeignKey(
                name: "FK_FuncFerias_Funcionario_funcionarioId",
                table: "FuncFerias",
                column: "funcionarioId",
                principalTable: "Funcionario",
                principalColumn: "id");
            migrationBuilder.AddForeignKey(
                name: "FK_FuncInfFerias_Funcionario_funcionarioId",
                table: "FuncInfFerias",
                column: "funcionarioId",
                principalTable: "Funcionario",
                principalColumn: "id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(name: "FK_FuncFerias_Funcionario_funcionarioId", table: "FuncFerias");
            migrationBuilder.DropForeignKey(name: "FK_FuncInfFerias_Funcionario_funcionarioId", table: "FuncInfFerias");
            migrationBuilder.DropPrimaryKey(name: "PK_Funcionario", table: "Funcionario");
            migrationBuilder.DropColumn(name: "id", table: "Funcionario");
            migrationBuilder.AlterColumn<string>(
                name: "codigo",
                table: "Funcionario",
                nullable: false);
            migrationBuilder.AddPrimaryKey(
                name: "PK_Funcionario",
                table: "Funcionario",
                column: "codigo");
            migrationBuilder.AlterColumn<string>(
                name: "funcionarioId",
                table: "FuncInfFerias",
                nullable: true);
            migrationBuilder.AlterColumn<string>(
                name: "funcionarioId",
                table: "FuncFerias",
                nullable: true);
            migrationBuilder.AddForeignKey(
                name: "FK_FuncFerias_Funcionario_funcionarioId",
                table: "FuncFerias",
                column: "funcionarioId",
                principalTable: "Funcionario",
                principalColumn: "codigo");
            migrationBuilder.AddForeignKey(
                name: "FK_FuncInfFerias_Funcionario_funcionarioId",
                table: "FuncInfFerias",
                column: "funcionarioId",
                principalTable: "Funcionario",
                principalColumn: "codigo");
        }
    }
}
