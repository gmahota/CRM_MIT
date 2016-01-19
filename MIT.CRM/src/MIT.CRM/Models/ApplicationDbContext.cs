using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.Data.Entity;
using MIT.Data;

namespace MIT.CRM.Models
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        #region RH DbSet
        public DbSet<Funcionario> Funcionarios { get; set; }
        public DbSet<FuncFerias> FuncionariosFerias { get; set; }
        public DbSet<FuncInfFerias> FuncInfFerias { get; set; }
        public DbSet<Departamento> Departamentos { get; set; }
        public DbSet<Ferias> Ferias { get; set; }
        public DbSet<Ferias_Itens> Ferias_Itens { get; set; }
        public DbSet<Historio_Ferias_Item> Historio_Ferias_Item { get; set; }
        #endregion

        #region CRM
        public DbSet<Entity> Entity { get; set; }
        public DbSet<Contact> Contact { get; set; }
        public DbSet<Contact_Item> Contact_Item { get; set; }
        public DbSet<Contact_Entity> Contact_Entity { get; set; }
        #endregion

        #region General 
        public DbSet<Empresa> Empresas { get; set; }
        public DbSet<Module> Module { get; set; }
        public DbSet<Report> Report { get; set; }
        #endregion

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            // Customize the ASP.NET Identity model and override the defaults if needed.
            // For example, you can rename the ASP.NET Identity table names and more.
            // Add your customizations after calling base.OnModelCreating(builder);
        }
    }

    public class IdentityDbContextOptions
    {
        public string DefaultAdminUserName { get; set; }

        public string DefaultAdminPassword { get; set; }
    }
}

