using System;
using Microsoft.Data.Entity;
using Microsoft.Data.Entity.Infrastructure;
using Microsoft.Data.Entity.Metadata;
using Microsoft.Data.Entity.Migrations;
using MIT.CRM.Models;

namespace MIT.CRM.Migrations
{
    [DbContext(typeof(ApplicationDbContext))]
    [Migration("20160223122751_Chave_Primaria_Ferias_itens_Func_inf_ferias")]
    partial class Chave_Primaria_Ferias_itens_Func_inf_ferias
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
            modelBuilder
                .HasAnnotation("ProductVersion", "7.0.0-rc1-16348")
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("Microsoft.AspNet.Identity.EntityFramework.IdentityRole", b =>
                {
                    b.Property<string>("Id");

                    b.Property<string>("ConcurrencyStamp")
                        .IsConcurrencyToken();

                    b.Property<string>("Name")
                        .HasAnnotation("MaxLength", 256);

                    b.Property<string>("NormalizedName")
                        .HasAnnotation("MaxLength", 256);

                    b.HasKey("Id");

                    b.HasIndex("NormalizedName")
                        .HasAnnotation("Relational:Name", "RoleNameIndex");

                    b.HasAnnotation("Relational:TableName", "AspNetRoles");
                });

            modelBuilder.Entity("Microsoft.AspNet.Identity.EntityFramework.IdentityRoleClaim<string>", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("ClaimType");

                    b.Property<string>("ClaimValue");

                    b.Property<string>("RoleId")
                        .IsRequired();

                    b.HasKey("Id");

                    b.HasAnnotation("Relational:TableName", "AspNetRoleClaims");
                });

            modelBuilder.Entity("Microsoft.AspNet.Identity.EntityFramework.IdentityUserClaim<string>", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("ClaimType");

                    b.Property<string>("ClaimValue");

                    b.Property<string>("UserId")
                        .IsRequired();

                    b.HasKey("Id");

                    b.HasAnnotation("Relational:TableName", "AspNetUserClaims");
                });

            modelBuilder.Entity("Microsoft.AspNet.Identity.EntityFramework.IdentityUserLogin<string>", b =>
                {
                    b.Property<string>("LoginProvider");

                    b.Property<string>("ProviderKey");

                    b.Property<string>("ProviderDisplayName");

                    b.Property<string>("UserId")
                        .IsRequired();

                    b.HasKey("LoginProvider", "ProviderKey");

                    b.HasAnnotation("Relational:TableName", "AspNetUserLogins");
                });

            modelBuilder.Entity("Microsoft.AspNet.Identity.EntityFramework.IdentityUserRole<string>", b =>
                {
                    b.Property<string>("UserId");

                    b.Property<string>("RoleId");

                    b.HasKey("UserId", "RoleId");

                    b.HasAnnotation("Relational:TableName", "AspNetUserRoles");
                });

            modelBuilder.Entity("MIT.Data.ApplicationUser", b =>
                {
                    b.Property<string>("Id");

                    b.Property<int>("AccessFailedCount");

                    b.Property<string>("ConcurrencyStamp")
                        .IsConcurrencyToken();

                    b.Property<string>("Email")
                        .HasAnnotation("MaxLength", 256);

                    b.Property<bool>("EmailConfirmed");

                    b.Property<bool>("LockoutEnabled");

                    b.Property<DateTimeOffset?>("LockoutEnd");

                    b.Property<string>("NormalizedEmail")
                        .HasAnnotation("MaxLength", 256);

                    b.Property<string>("NormalizedUserName")
                        .HasAnnotation("MaxLength", 256);

                    b.Property<string>("PasswordHash");

                    b.Property<string>("PhoneNumber");

                    b.Property<bool>("PhoneNumberConfirmed");

                    b.Property<string>("SecurityStamp");

                    b.Property<bool>("TwoFactorEnabled");

                    b.Property<string>("UserName")
                        .HasAnnotation("MaxLength", 256);

                    b.HasKey("Id");

                    b.HasIndex("NormalizedEmail")
                        .HasAnnotation("Relational:Name", "EmailIndex");

                    b.HasIndex("NormalizedUserName")
                        .HasAnnotation("Relational:Name", "UserNameIndex");

                    b.HasAnnotation("Relational:TableName", "AspNetUsers");
                });

            modelBuilder.Entity("MIT.Data.CompanyModule", b =>
                {
                    b.Property<int>("companyId");

                    b.Property<int>("moduleId");

                    b.Property<string>("companycodigo");

                    b.HasKey("companyId", "moduleId");
                });

            modelBuilder.Entity("MIT.Data.Contact", b =>
                {
                    b.Property<int>("id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("celphone");

                    b.Property<string>("email");

                    b.Property<string>("emailAlt");

                    b.Property<string>("firstName");

                    b.Property<string>("lastName");

                    b.Property<string>("name");

                    b.Property<string>("telphone");

                    b.Property<string>("title");

                    b.Property<string>("type");

                    b.HasKey("id");
                });

            modelBuilder.Entity("MIT.Data.Contact_Entity", b =>
                {
                    b.Property<int>("id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("name");

                    b.Property<string>("type");

                    b.Property<string>("value");

                    b.HasKey("id");
                });

            modelBuilder.Entity("MIT.Data.Contact_Item", b =>
                {
                    b.Property<int>("id")
                        .ValueGeneratedOnAdd();

                    b.Property<int>("contactId");

                    b.Property<string>("name");

                    b.Property<string>("type");

                    b.Property<string>("value");

                    b.HasKey("id");
                });

            modelBuilder.Entity("MIT.Data.Departamento", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<bool>("activo");

                    b.Property<string>("departamento")
                        .IsRequired();

                    b.Property<string>("descricao")
                        .IsRequired();

                    b.Property<string>("empresaId");

                    b.Property<string>("responsavelId");

                    b.HasKey("Id");
                });

            modelBuilder.Entity("MIT.Data.Empresa", b =>
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

            modelBuilder.Entity("MIT.Data.Entity", b =>
                {
                    b.Property<int>("id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("entidade");

                    b.Property<bool>("enviaCobranca");

                    b.Property<string>("fac_Local");

                    b.Property<string>("fac_Mor");

                    b.Property<string>("fac_Tel");

                    b.Property<string>("moeda");

                    b.Property<string>("nome");

                    b.Property<string>("numContrib");

                    b.Property<string>("pais");

                    b.Property<string>("type");

                    b.Property<double>("valorCreditoTotal");

                    b.Property<double>("valorDebitoTotal");

                    b.Property<double>("valorPendente");

                    b.HasKey("id");
                });

            modelBuilder.Entity("MIT.Data.Ferias", b =>
                {
                    b.Property<int>("id")
                        .ValueGeneratedOnAdd();

                    b.Property<DateTime>("dataFim");

                    b.Property<DateTime>("dataInicio");

                    b.Property<int>("funcionarioId");

                    b.HasKey("id");
                });

            modelBuilder.Entity("MIT.Data.Ferias_Itens", b =>
                {
                    b.Property<int>("id")
                        .ValueGeneratedOnAdd();

                    b.Property<short>("ano");

                    b.Property<DateTime>("dataFeria");

                    b.Property<string>("estado");

                    b.Property<bool>("estadoGozo");

                    b.Property<int?>("feriasId");

                    b.Property<int?>("feriasInfFeriasId");

                    b.Property<int>("funcionarioId");

                    b.Property<bool>("originouFalta");

                    b.Property<bool>("originouFaltaSubAlim");

                    b.Property<string>("tipo");

                    b.Property<int>("tipoMarcacao");

                    b.HasKey("id");
                });

            modelBuilder.Entity("MIT.Data.FileDescription", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("ContentType");

                    b.Property<DateTime>("CreatedTimestamp");

                    b.Property<string>("Description");

                    b.Property<string>("FileName");

                    b.Property<DateTime>("UpdatedTimestamp");

                    b.HasKey("Id");
                });

            modelBuilder.Entity("MIT.Data.FuncFerias", b =>
                {
                    b.Property<int>("id")
                        .ValueGeneratedOnAdd();

                    b.Property<short>("ano");

                    b.Property<DateTime>("dataFeria");

                    b.Property<bool>("estadoGozo");

                    b.Property<int>("funcionarioId");

                    b.Property<bool>("originouFalta");

                    b.Property<bool>("originouFaltaSubAlim");

                    b.Property<int>("tipoMarcacao");

                    b.HasKey("id");
                });

            modelBuilder.Entity("MIT.Data.FuncInfFerias", b =>
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

                    b.Property<int>("funcionarioId");

                    b.Property<double>("totalDias");

                    b.HasKey("id");
                });

            modelBuilder.Entity("MIT.Data.Funcionario", b =>
                {
                    b.Property<int>("id")
                        .ValueGeneratedOnAdd();

                    b.Property<bool>("activo");

                    b.Property<int?>("avatarId");

                    b.Property<string>("cargo")
                        .HasAnnotation("MaxLength", 25);

                    b.Property<string>("categoria");

                    b.Property<string>("classificacao");

                    b.Property<string>("codigo");

                    b.Property<DateTime?>("dataAdmissao");

                    b.Property<DateTime?>("dataClassificacao");

                    b.Property<DateTime?>("dataFimContrato");

                    b.Property<DateTime?>("dataNascimento");

                    b.Property<DateTime?>("dataReadmissao");

                    b.Property<int>("departamentoId");

                    b.Property<string>("distrito");

                    b.Property<string>("email");

                    b.Property<string>("empresaId");

                    b.Property<string>("estadoCivil");

                    b.Property<string>("habilitacao");

                    b.Property<string>("localidade");

                    b.Property<string>("nacionalidade");

                    b.Property<string>("naturalidade");

                    b.Property<string>("nome");

                    b.Property<string>("nomeAbreviado")
                        .HasAnnotation("MaxLength", 25);

                    b.Property<string>("profissao");

                    b.Property<string>("sexo");

                    b.Property<string>("telefone");

                    b.Property<string>("telefoneAlternativo");

                    b.Property<string>("telemovel");

                    b.Property<string>("utilizadorId");

                    b.HasKey("id");
                });

            modelBuilder.Entity("MIT.Data.Historio_Ferias_Item", b =>
                {
                    b.Property<int>("id")
                        .ValueGeneratedOnAdd();

                    b.Property<DateTime>("data");

                    b.Property<string>("estado");

                    b.Property<int>("ferias_item_id");

                    b.Property<string>("utilizadorId");

                    b.HasKey("id");
                });

            modelBuilder.Entity("MIT.Data.Module", b =>
                {
                    b.Property<int>("id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("description")
                        .IsRequired()
                        .HasAnnotation("MaxLength", 25);

                    b.Property<int?>("parentId");

                    b.HasKey("id");
                });

            modelBuilder.Entity("MIT.Data.Report", b =>
                {
                    b.Property<int>("id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("caminho");

                    b.Property<string>("cc");

                    b.Property<string>("empresa");

                    b.Property<string>("entidade");

                    b.Property<string>("nomeEmpresa");

                    b.Property<string>("tipoEntidade");

                    b.Property<string>("to");

                    b.HasKey("id");
                });

            modelBuilder.Entity("Microsoft.AspNet.Identity.EntityFramework.IdentityRoleClaim<string>", b =>
                {
                    b.HasOne("Microsoft.AspNet.Identity.EntityFramework.IdentityRole")
                        .WithMany()
                        .HasForeignKey("RoleId");
                });

            modelBuilder.Entity("Microsoft.AspNet.Identity.EntityFramework.IdentityUserClaim<string>", b =>
                {
                    b.HasOne("MIT.Data.ApplicationUser")
                        .WithMany()
                        .HasForeignKey("UserId");
                });

            modelBuilder.Entity("Microsoft.AspNet.Identity.EntityFramework.IdentityUserLogin<string>", b =>
                {
                    b.HasOne("MIT.Data.ApplicationUser")
                        .WithMany()
                        .HasForeignKey("UserId");
                });

            modelBuilder.Entity("Microsoft.AspNet.Identity.EntityFramework.IdentityUserRole<string>", b =>
                {
                    b.HasOne("Microsoft.AspNet.Identity.EntityFramework.IdentityRole")
                        .WithMany()
                        .HasForeignKey("RoleId");

                    b.HasOne("MIT.Data.ApplicationUser")
                        .WithMany()
                        .HasForeignKey("UserId");
                });

            modelBuilder.Entity("MIT.Data.CompanyModule", b =>
                {
                    b.HasOne("MIT.Data.Empresa")
                        .WithMany()
                        .HasForeignKey("companycodigo");

                    b.HasOne("MIT.Data.Module")
                        .WithMany()
                        .HasForeignKey("moduleId");
                });

            modelBuilder.Entity("MIT.Data.Contact_Item", b =>
                {
                    b.HasOne("MIT.Data.Contact")
                        .WithMany()
                        .HasForeignKey("contactId");
                });

            modelBuilder.Entity("MIT.Data.Departamento", b =>
                {
                    b.HasOne("MIT.Data.Empresa")
                        .WithMany()
                        .HasForeignKey("empresaId");

                    b.HasOne("MIT.Data.ApplicationUser")
                        .WithOne()
                        .HasForeignKey("MIT.Data.Departamento", "responsavelId");
                });

            modelBuilder.Entity("MIT.Data.Ferias", b =>
                {
                    b.HasOne("MIT.Data.Funcionario")
                        .WithMany()
                        .HasForeignKey("funcionarioId");
                });

            modelBuilder.Entity("MIT.Data.Ferias_Itens", b =>
                {
                    b.HasOne("MIT.Data.Ferias")
                        .WithMany()
                        .HasForeignKey("feriasId");

                    b.HasOne("MIT.Data.FuncInfFerias")
                        .WithMany()
                        .HasForeignKey("feriasInfFeriasId");

                    b.HasOne("MIT.Data.Funcionario")
                        .WithMany()
                        .HasForeignKey("funcionarioId");
                });

            modelBuilder.Entity("MIT.Data.FuncFerias", b =>
                {
                    b.HasOne("MIT.Data.Funcionario")
                        .WithMany()
                        .HasForeignKey("funcionarioId");
                });

            modelBuilder.Entity("MIT.Data.FuncInfFerias", b =>
                {
                    b.HasOne("MIT.Data.Funcionario")
                        .WithMany()
                        .HasForeignKey("funcionarioId");
                });

            modelBuilder.Entity("MIT.Data.Funcionario", b =>
                {
                    b.HasOne("MIT.Data.FileDescription")
                        .WithMany()
                        .HasForeignKey("avatarId");

                    b.HasOne("MIT.Data.Departamento")
                        .WithMany()
                        .HasForeignKey("departamentoId");

                    b.HasOne("MIT.Data.Empresa")
                        .WithMany()
                        .HasForeignKey("empresaId");

                    b.HasOne("MIT.Data.ApplicationUser")
                        .WithOne()
                        .HasForeignKey("MIT.Data.Funcionario", "utilizadorId");
                });

            modelBuilder.Entity("MIT.Data.Historio_Ferias_Item", b =>
                {
                    b.HasOne("MIT.Data.Ferias_Itens")
                        .WithMany()
                        .HasForeignKey("ferias_item_id");

                    b.HasOne("MIT.Data.ApplicationUser")
                        .WithMany()
                        .HasForeignKey("utilizadorId");
                });

            modelBuilder.Entity("MIT.Data.Module", b =>
                {
                    b.HasOne("MIT.Data.Module")
                        .WithMany()
                        .HasForeignKey("parentId");
                });
        }
    }
}
