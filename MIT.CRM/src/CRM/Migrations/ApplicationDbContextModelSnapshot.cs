using System;
using Microsoft.Data.Entity;
using Microsoft.Data.Entity.Infrastructure;
using Microsoft.Data.Entity.Metadata;
using Microsoft.Data.Entity.Migrations;
using CRM.Models;

namespace CRM.Migrations
{
    [DbContext(typeof(ApplicationDbContext))]
    partial class ApplicationDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
            modelBuilder
                .Annotation("ProductVersion", "7.0.0-beta8-15964")
                .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("CRM.Models.ApplicationUser", b =>
                {
                    b.Property<string>("Id");

                    b.Property<int>("AccessFailedCount");

                    b.Property<string>("ConcurrencyStamp")
                        .IsConcurrencyToken();

                    b.Property<string>("Email")
                        .Annotation("MaxLength", 256);

                    b.Property<bool>("EmailConfirmed");

                    b.Property<bool>("LockoutEnabled");

                    b.Property<DateTimeOffset?>("LockoutEnd");

                    b.Property<string>("NormalizedEmail")
                        .Annotation("MaxLength", 256);

                    b.Property<string>("NormalizedUserName")
                        .Annotation("MaxLength", 256);

                    b.Property<string>("PasswordHash");

                    b.Property<string>("PhoneNumber");

                    b.Property<bool>("PhoneNumberConfirmed");

                    b.Property<string>("SecurityStamp");

                    b.Property<bool>("TwoFactorEnabled");

                    b.Property<string>("UserName")
                        .Annotation("MaxLength", 256);

                    b.HasKey("Id");

                    b.Index("NormalizedEmail")
                        .Annotation("Relational:Name", "EmailIndex");

                    b.Index("NormalizedUserName")
                        .Annotation("Relational:Name", "UserNameIndex");

                    b.Annotation("Relational:TableName", "AspNetUsers");
                });

            modelBuilder.Entity("CRM.Models.Departamento", b =>
                {
                    b.Property<string>("departamento");

                    b.Property<string>("descricao");

                    b.Property<string>("empresaId");

                    b.Property<string>("responsavelId");

                    b.HasKey("departamento");
                });

            modelBuilder.Entity("CRM.Models.Empresa", b =>
                {
                    b.Property<string>("codigo");

                    b.Property<byte[]>("LogoTipo");

                    b.Property<string>("categoria");

                    b.Property<string>("codEmpresaPri");

                    b.Property<string>("conexao");

                    b.Property<string>("credentials");

                    b.Property<string>("email");

                    b.Property<bool?>("empresaPrimavera");

                    b.Property<bool?>("enableSsl");

                    b.Property<string>("host");

                    b.Property<string>("localidadeEmpresa");

                    b.Property<string>("morada");

                    b.Property<string>("moradaEmpresa");

                    b.Property<string>("nome");

                    b.Property<string>("nomeEmpresa");

                    b.Property<string>("nuit");

                    b.Property<string>("nuitEmpresa");

                    b.Property<string>("passwordUtilizadorPrimavera");

                    b.Property<int?>("port");

                    b.Property<string>("telefoneEmpresa");

                    b.Property<string>("tipoEmpresa");

                    b.Property<bool?>("useDefaultCredentials");

                    b.Property<string>("utilizadorPrimavera");

                    b.HasKey("codigo");
                });

            modelBuilder.Entity("CRM.Models.FuncFerias", b =>
                {
                    b.Property<int>("id")
                        .ValueGeneratedOnAdd();

                    b.Property<short>("ano");

                    b.Property<DateTime>("dataFeria");

                    b.Property<bool>("estadoGozo");

                    b.Property<string>("funcionarioId");

                    b.Property<bool>("originouFalta");

                    b.Property<bool>("originouFaltaSubAlim");

                    b.Property<bool>("tipoMarcacao");

                    b.HasKey("id");
                });

            modelBuilder.Entity("CRM.Models.FuncInfFerias", b =>
                {
                    b.Property<int>("id")
                        .ValueGeneratedOnAdd();

                    b.Property<short>("ano");

                    b.Property<double>("diasAdicionais");

                    b.Property<double>("diasAnoAnterior");

                    b.Property<double>("diasDireito");

                    b.Property<double>("diasFeriasJaPagas");

                    b.Property<double>("diasJaGozados");

                    b.Property<double>("diasPorGozar");

                    b.Property<double>("diasPorMarcar");

                    b.Property<bool>("funcSemFerias");

                    b.Property<string>("funcionarioId");

                    b.Property<double>("totalDias");

                    b.HasKey("id");
                });

            modelBuilder.Entity("CRM.Models.Funcionario", b =>
                {
                    b.Property<string>("codigo");

                    b.Property<string>("categoria");

                    b.Property<string>("classificacao");

                    b.Property<DateTime?>("dataAdmissao");

                    b.Property<DateTime?>("dataClassificacao");

                    b.Property<DateTime?>("dataFimContrato");

                    b.Property<DateTime?>("dataNascimento");

                    b.Property<DateTime?>("dataReadmissao");

                    b.Property<string>("departamentoId");

                    b.Property<string>("distrito");

                    b.Property<string>("email");

                    b.Property<string>("empresaId");

                    b.Property<string>("estadoCivil");

                    b.Property<string>("habilitacao");

                    b.Property<string>("localidade");

                    b.Property<string>("nacionalidade");

                    b.Property<string>("naturalidade");

                    b.Property<string>("nome");

                    b.Property<string>("profissao");

                    b.Property<string>("sexo");

                    b.Property<string>("telefone");

                    b.Property<string>("utilizadorId");

                    b.HasKey("codigo");
                });

            modelBuilder.Entity("Microsoft.AspNet.Identity.EntityFramework.IdentityRole", b =>
                {
                    b.Property<string>("Id");

                    b.Property<string>("ConcurrencyStamp")
                        .IsConcurrencyToken();

                    b.Property<string>("Name")
                        .Annotation("MaxLength", 256);

                    b.Property<string>("NormalizedName")
                        .Annotation("MaxLength", 256);

                    b.HasKey("Id");

                    b.Index("NormalizedName")
                        .Annotation("Relational:Name", "RoleNameIndex");

                    b.Annotation("Relational:TableName", "AspNetRoles");
                });

            modelBuilder.Entity("Microsoft.AspNet.Identity.EntityFramework.IdentityRoleClaim<string>", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("ClaimType");

                    b.Property<string>("ClaimValue");

                    b.Property<string>("RoleId");

                    b.HasKey("Id");

                    b.Annotation("Relational:TableName", "AspNetRoleClaims");
                });

            modelBuilder.Entity("Microsoft.AspNet.Identity.EntityFramework.IdentityUserClaim<string>", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("ClaimType");

                    b.Property<string>("ClaimValue");

                    b.Property<string>("UserId");

                    b.HasKey("Id");

                    b.Annotation("Relational:TableName", "AspNetUserClaims");
                });

            modelBuilder.Entity("Microsoft.AspNet.Identity.EntityFramework.IdentityUserLogin<string>", b =>
                {
                    b.Property<string>("LoginProvider");

                    b.Property<string>("ProviderKey");

                    b.Property<string>("ProviderDisplayName");

                    b.Property<string>("UserId");

                    b.HasKey("LoginProvider", "ProviderKey");

                    b.Annotation("Relational:TableName", "AspNetUserLogins");
                });

            modelBuilder.Entity("Microsoft.AspNet.Identity.EntityFramework.IdentityUserRole<string>", b =>
                {
                    b.Property<string>("UserId");

                    b.Property<string>("RoleId");

                    b.HasKey("UserId", "RoleId");

                    b.Annotation("Relational:TableName", "AspNetUserRoles");
                });

            modelBuilder.Entity("CRM.Models.Departamento", b =>
                {
                    b.HasOne("CRM.Models.Empresa")
                        .WithMany()
                        .ForeignKey("empresaId");

                    b.HasOne("CRM.Models.ApplicationUser")
                        .WithOne()
                        .ForeignKey("CRM.Models.Departamento", "responsavelId");
                });

            modelBuilder.Entity("CRM.Models.FuncFerias", b =>
                {
                    b.HasOne("CRM.Models.Funcionario")
                        .WithMany()
                        .ForeignKey("funcionarioId");
                });

            modelBuilder.Entity("CRM.Models.FuncInfFerias", b =>
                {
                    b.HasOne("CRM.Models.Funcionario")
                        .WithMany()
                        .ForeignKey("funcionarioId");
                });

            modelBuilder.Entity("CRM.Models.Funcionario", b =>
                {
                    b.HasOne("CRM.Models.Departamento")
                        .WithMany()
                        .ForeignKey("departamentoId");

                    b.HasOne("CRM.Models.Empresa")
                        .WithMany()
                        .ForeignKey("empresaId");

                    b.HasOne("CRM.Models.ApplicationUser")
                        .WithOne()
                        .ForeignKey("CRM.Models.Funcionario", "utilizadorId");
                });

            modelBuilder.Entity("Microsoft.AspNet.Identity.EntityFramework.IdentityRoleClaim<string>", b =>
                {
                    b.HasOne("Microsoft.AspNet.Identity.EntityFramework.IdentityRole")
                        .WithMany()
                        .ForeignKey("RoleId");
                });

            modelBuilder.Entity("Microsoft.AspNet.Identity.EntityFramework.IdentityUserClaim<string>", b =>
                {
                    b.HasOne("CRM.Models.ApplicationUser")
                        .WithMany()
                        .ForeignKey("UserId");
                });

            modelBuilder.Entity("Microsoft.AspNet.Identity.EntityFramework.IdentityUserLogin<string>", b =>
                {
                    b.HasOne("CRM.Models.ApplicationUser")
                        .WithMany()
                        .ForeignKey("UserId");
                });

            modelBuilder.Entity("Microsoft.AspNet.Identity.EntityFramework.IdentityUserRole<string>", b =>
                {
                    b.HasOne("Microsoft.AspNet.Identity.EntityFramework.IdentityRole")
                        .WithMany()
                        .ForeignKey("RoleId");

                    b.HasOne("CRM.Models.ApplicationUser")
                        .WithMany()
                        .ForeignKey("UserId");
                });
        }
    }
}
