using System;
using System.Collections.Generic;
using Microsoft.Data.Entity.Migrations;

namespace MIT.CRM.Migrations
{
    public partial class campos_no_funcionario_cargo : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(name: "FK_IdentityRoleClaim<string>_IdentityRole_RoleId", table: "AspNetRoleClaims");
            migrationBuilder.DropForeignKey(name: "FK_IdentityUserClaim<string>_ApplicationUser_UserId", table: "AspNetUserClaims");
            migrationBuilder.DropForeignKey(name: "FK_IdentityUserLogin<string>_ApplicationUser_UserId", table: "AspNetUserLogins");
            migrationBuilder.DropForeignKey(name: "FK_IdentityUserRole<string>_IdentityRole_RoleId", table: "AspNetUserRoles");
            migrationBuilder.DropForeignKey(name: "FK_IdentityUserRole<string>_ApplicationUser_UserId", table: "AspNetUserRoles");
            migrationBuilder.DropForeignKey(name: "FK_CompanyModule_Module_moduleId", table: "CompanyModule");
            migrationBuilder.DropForeignKey(name: "FK_Contact_Item_Contact_contactId", table: "Contact_Item");
            migrationBuilder.DropForeignKey(name: "FK_Ferias_Funcionario_funcionarioId", table: "Ferias");
            migrationBuilder.DropForeignKey(name: "FK_Ferias_Itens_Funcionario_funcionarioId", table: "Ferias_Itens");
            migrationBuilder.DropForeignKey(name: "FK_FuncFerias_Funcionario_funcionarioId", table: "FuncFerias");
            migrationBuilder.DropForeignKey(name: "FK_FuncInfFerias_Funcionario_funcionarioId", table: "FuncInfFerias");
            migrationBuilder.DropForeignKey(name: "FK_Funcionario_Departamento_departamentoId", table: "Funcionario");
            migrationBuilder.DropForeignKey(name: "FK_Historio_Ferias_Item_Ferias_Itens_ferias_item_id", table: "Historio_Ferias_Item");
            migrationBuilder.AddColumn<string>(
                name: "cargo",
                table: "Funcionario",
                nullable: true);
            migrationBuilder.AddColumn<string>(
                name: "nomeAbreviado",
                table: "Funcionario",
                nullable: true);
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
            migrationBuilder.AddForeignKey(
                name: "FK_CompanyModule_Module_moduleId",
                table: "CompanyModule",
                column: "moduleId",
                principalTable: "Module",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);
            migrationBuilder.AddForeignKey(
                name: "FK_Contact_Item_Contact_contactId",
                table: "Contact_Item",
                column: "contactId",
                principalTable: "Contact",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);
            migrationBuilder.AddForeignKey(
                name: "FK_Ferias_Funcionario_funcionarioId",
                table: "Ferias",
                column: "funcionarioId",
                principalTable: "Funcionario",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);
            migrationBuilder.AddForeignKey(
                name: "FK_Ferias_Itens_Funcionario_funcionarioId",
                table: "Ferias_Itens",
                column: "funcionarioId",
                principalTable: "Funcionario",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);
            migrationBuilder.AddForeignKey(
                name: "FK_FuncFerias_Funcionario_funcionarioId",
                table: "FuncFerias",
                column: "funcionarioId",
                principalTable: "Funcionario",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);
            migrationBuilder.AddForeignKey(
                name: "FK_FuncInfFerias_Funcionario_funcionarioId",
                table: "FuncInfFerias",
                column: "funcionarioId",
                principalTable: "Funcionario",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);
            migrationBuilder.AddForeignKey(
                name: "FK_Funcionario_Departamento_departamentoId",
                table: "Funcionario",
                column: "departamentoId",
                principalTable: "Departamento",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
            migrationBuilder.AddForeignKey(
                name: "FK_Historio_Ferias_Item_Ferias_Itens_ferias_item_id",
                table: "Historio_Ferias_Item",
                column: "ferias_item_id",
                principalTable: "Ferias_Itens",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(name: "FK_IdentityRoleClaim<string>_IdentityRole_RoleId", table: "AspNetRoleClaims");
            migrationBuilder.DropForeignKey(name: "FK_IdentityUserClaim<string>_ApplicationUser_UserId", table: "AspNetUserClaims");
            migrationBuilder.DropForeignKey(name: "FK_IdentityUserLogin<string>_ApplicationUser_UserId", table: "AspNetUserLogins");
            migrationBuilder.DropForeignKey(name: "FK_IdentityUserRole<string>_IdentityRole_RoleId", table: "AspNetUserRoles");
            migrationBuilder.DropForeignKey(name: "FK_IdentityUserRole<string>_ApplicationUser_UserId", table: "AspNetUserRoles");
            migrationBuilder.DropForeignKey(name: "FK_CompanyModule_Module_moduleId", table: "CompanyModule");
            migrationBuilder.DropForeignKey(name: "FK_Contact_Item_Contact_contactId", table: "Contact_Item");
            migrationBuilder.DropForeignKey(name: "FK_Ferias_Funcionario_funcionarioId", table: "Ferias");
            migrationBuilder.DropForeignKey(name: "FK_Ferias_Itens_Funcionario_funcionarioId", table: "Ferias_Itens");
            migrationBuilder.DropForeignKey(name: "FK_FuncFerias_Funcionario_funcionarioId", table: "FuncFerias");
            migrationBuilder.DropForeignKey(name: "FK_FuncInfFerias_Funcionario_funcionarioId", table: "FuncInfFerias");
            migrationBuilder.DropForeignKey(name: "FK_Funcionario_Departamento_departamentoId", table: "Funcionario");
            migrationBuilder.DropForeignKey(name: "FK_Historio_Ferias_Item_Ferias_Itens_ferias_item_id", table: "Historio_Ferias_Item");
            migrationBuilder.DropColumn(name: "cargo", table: "Funcionario");
            migrationBuilder.DropColumn(name: "nomeAbreviado", table: "Funcionario");
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
            migrationBuilder.AddForeignKey(
                name: "FK_CompanyModule_Module_moduleId",
                table: "CompanyModule",
                column: "moduleId",
                principalTable: "Module",
                principalColumn: "id",
                onDelete: ReferentialAction.Restrict);
            migrationBuilder.AddForeignKey(
                name: "FK_Contact_Item_Contact_contactId",
                table: "Contact_Item",
                column: "contactId",
                principalTable: "Contact",
                principalColumn: "id",
                onDelete: ReferentialAction.Restrict);
            migrationBuilder.AddForeignKey(
                name: "FK_Ferias_Funcionario_funcionarioId",
                table: "Ferias",
                column: "funcionarioId",
                principalTable: "Funcionario",
                principalColumn: "id",
                onDelete: ReferentialAction.Restrict);
            migrationBuilder.AddForeignKey(
                name: "FK_Ferias_Itens_Funcionario_funcionarioId",
                table: "Ferias_Itens",
                column: "funcionarioId",
                principalTable: "Funcionario",
                principalColumn: "id",
                onDelete: ReferentialAction.Restrict);
            migrationBuilder.AddForeignKey(
                name: "FK_FuncFerias_Funcionario_funcionarioId",
                table: "FuncFerias",
                column: "funcionarioId",
                principalTable: "Funcionario",
                principalColumn: "id",
                onDelete: ReferentialAction.Restrict);
            migrationBuilder.AddForeignKey(
                name: "FK_FuncInfFerias_Funcionario_funcionarioId",
                table: "FuncInfFerias",
                column: "funcionarioId",
                principalTable: "Funcionario",
                principalColumn: "id",
                onDelete: ReferentialAction.Restrict);
            migrationBuilder.AddForeignKey(
                name: "FK_Funcionario_Departamento_departamentoId",
                table: "Funcionario",
                column: "departamentoId",
                principalTable: "Departamento",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
            migrationBuilder.AddForeignKey(
                name: "FK_Historio_Ferias_Item_Ferias_Itens_ferias_item_id",
                table: "Historio_Ferias_Item",
                column: "ferias_item_id",
                principalTable: "Ferias_Itens",
                principalColumn: "id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
