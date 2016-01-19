using System;
using System.Collections.Generic;
using Microsoft.Data.Entity.Migrations;
using Microsoft.Data.Entity.Metadata;

namespace MIT.CRM.Migrations
{
    public partial class repotstable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(name: "FK_IdentityRoleClaim<string>_IdentityRole_RoleId", table: "AspNetRoleClaims");
            migrationBuilder.DropForeignKey(name: "FK_IdentityUserClaim<string>_ApplicationUser_UserId", table: "AspNetUserClaims");
            migrationBuilder.DropForeignKey(name: "FK_IdentityUserLogin<string>_ApplicationUser_UserId", table: "AspNetUserLogins");
            migrationBuilder.DropForeignKey(name: "FK_IdentityUserRole<string>_IdentityRole_RoleId", table: "AspNetUserRoles");
            migrationBuilder.DropForeignKey(name: "FK_IdentityUserRole<string>_ApplicationUser_UserId", table: "AspNetUserRoles");
            migrationBuilder.DropForeignKey(name: "FK_Ferias_Funcionario_funcionarioId", table: "Ferias");
            migrationBuilder.DropForeignKey(name: "FK_Ferias_Itens_Funcionario_funcionarioId", table: "Ferias_Itens");
            migrationBuilder.DropForeignKey(name: "FK_FuncFerias_Funcionario_funcionarioId", table: "FuncFerias");
            migrationBuilder.DropForeignKey(name: "FK_FuncInfFerias_Funcionario_funcionarioId", table: "FuncInfFerias");
            migrationBuilder.DropForeignKey(name: "FK_Funcionario_Departamento_departamentoId", table: "Funcionario");
            migrationBuilder.DropForeignKey(name: "FK_Historio_Ferias_Item_Ferias_Itens_ferias_item_id", table: "Historio_Ferias_Item");
            migrationBuilder.CreateTable(
                name: "Contact",
                columns: table => new
                {
                    id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    celphone = table.Column<string>(nullable: true),
                    contactId = table.Column<string>(nullable: false),
                    email = table.Column<string>(nullable: true),
                    emailAlt = table.Column<string>(nullable: true),
                    firstName = table.Column<string>(nullable: true),
                    lastName = table.Column<string>(nullable: true),
                    name = table.Column<string>(nullable: true),
                    telphone = table.Column<string>(nullable: true),
                    title = table.Column<string>(nullable: true),
                    @type = table.Column<string>(name: "type", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Contact", x => x.id);
                    table.UniqueConstraint("AK_Contact_contactId", x => x.contactId);
                });
            migrationBuilder.CreateTable(
                name: "Contact_Entity",
                columns: table => new
                {
                    id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    name = table.Column<string>(nullable: true),
                    @type = table.Column<string>(name: "type", nullable: true),
                    value = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Contact_Entity", x => x.id);
                });
            migrationBuilder.CreateTable(
                name: "Entity",
                columns: table => new
                {
                    id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    entidade = table.Column<string>(nullable: true),
                    enviaCobranca = table.Column<bool>(nullable: false),
                    fac_Local = table.Column<string>(nullable: true),
                    fac_Mor = table.Column<string>(nullable: true),
                    fac_Tel = table.Column<string>(nullable: true),
                    moeda = table.Column<string>(nullable: true),
                    nome = table.Column<string>(nullable: true),
                    numContrib = table.Column<string>(nullable: true),
                    pais = table.Column<string>(nullable: true),
                    @type = table.Column<string>(name: "type", nullable: true),
                    valorCreditoTotal = table.Column<double>(nullable: false),
                    valorDebitoTotal = table.Column<double>(nullable: false),
                    valorPendente = table.Column<double>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Entity", x => x.id);
                });
            migrationBuilder.CreateTable(
                name: "Report",
                columns: table => new
                {
                    id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    caminho = table.Column<string>(nullable: true),
                    cc = table.Column<string>(nullable: true),
                    empresa = table.Column<string>(nullable: true),
                    entidade = table.Column<string>(nullable: true),
                    nomeEmpresa = table.Column<string>(nullable: true),
                    tipoEntidade = table.Column<string>(nullable: true),
                    to = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Report", x => x.id);
                });
            migrationBuilder.CreateTable(
                name: "Contact_Item",
                columns: table => new
                {
                    id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    contactId = table.Column<string>(nullable: true),
                    name = table.Column<string>(nullable: true),
                    @type = table.Column<string>(name: "type", nullable: true),
                    value = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Contact_Item", x => x.id);
                    table.ForeignKey(
                        name: "FK_Contact_Item_Contact_contactId",
                        column: x => x.contactId,
                        principalTable: "Contact",
                        principalColumn: "contactId",
                        onDelete: ReferentialAction.Restrict);
                });
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
            migrationBuilder.DropForeignKey(name: "FK_Ferias_Funcionario_funcionarioId", table: "Ferias");
            migrationBuilder.DropForeignKey(name: "FK_Ferias_Itens_Funcionario_funcionarioId", table: "Ferias_Itens");
            migrationBuilder.DropForeignKey(name: "FK_FuncFerias_Funcionario_funcionarioId", table: "FuncFerias");
            migrationBuilder.DropForeignKey(name: "FK_FuncInfFerias_Funcionario_funcionarioId", table: "FuncInfFerias");
            migrationBuilder.DropForeignKey(name: "FK_Funcionario_Departamento_departamentoId", table: "Funcionario");
            migrationBuilder.DropForeignKey(name: "FK_Historio_Ferias_Item_Ferias_Itens_ferias_item_id", table: "Historio_Ferias_Item");
            migrationBuilder.DropTable("Contact_Entity");
            migrationBuilder.DropTable("Contact_Item");
            migrationBuilder.DropTable("Entity");
            migrationBuilder.DropTable("Report");
            migrationBuilder.DropTable("Contact");
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
