using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace MIT.Repository
{
    public class Empresa
    {
        [Key]
        public string codigo { get; set; }
        public string nome { get; set; }
        public string nuit { get; set; }
        public string morada { get; set; }
        public string categoria { get; set; }
        public byte[] LogoTipo { get; set; }

        //Integração Primavera
        public string codEmpresaPri { get; set; }
        public string nomeEmpresa { get; set; }
        public Nullable<bool> empresaPrimavera { get; set; }
        public string tipoEmpresa { get; set; }
        public string moradaEmpresa { get; set; }
        public string localidadeEmpresa { get; set; }
        public string telefoneEmpresa { get; set; }
        public string nuitEmpresa { get; set; }
        public string utilizadorPrimavera { get; set; }
        public string passwordUtilizadorPrimavera { get; set; }

        //Dados Mail Server
        public string conexao { get; set; }
        public Nullable<bool> useDefaultCredentials { get; set; }
        public string credentials { get; set; }
        public Nullable<int> port { get; set; }
        public Nullable<bool> enableSsl { get; set; }
        public string host { get; set; }
        public string email { get; set; }

        public ICollection<Departamento> departamentosList { get; set; }
        public ICollection<CompanyModule> companyModules { get; set; }
    }

    public class Moeda
    {
        [Key]
        public string moedaId { get; set; }

        [Required]
        public string descricao { get; set; }
        public double compra { get; set; }
        public double venda { get; set; }
        public DateTime? data { get; set; }
        public string codigoIso { get; set; }
        
        public Moeda() { }

        public Moeda(ref string moeda, ref string descricao, ref double compra, ref double venda, ref DateTime data)
        {
            this.moedaId = moeda;
            this.descricao = descricao;
            this.compra = compra;
            this.venda = venda;
            this.data = data;

        }
    }

    public class Module
    {
        [Key]
        public int id { get; set; }

        [StringLength(25)]
        [Required, Display(Name = "Descrição")]
        public string description { get; set; }


        public int? parentId { get; set; }
        
        public Module parent { get; set; }
        public ICollection<Module> childList { get; set; }

        public ICollection<CompanyModule> companyModules { get; set; } 
         
    }

    public class CompanyModule
    {
        public int moduleId { get; set; }
        public Module module { get; set; }

        public int companyId { get; set; }
        public Empresa company { get; set; }
    }

    public class FileDescription
    {
        [Key]
        public int Id { get; set; }
        public string FileName { get; set; }
        public string Description { get; set; }
        public DateTime CreatedTimestamp { get; set; }
        public DateTime UpdatedTimestamp { get; set; }
        public string ContentType { get; set; }
        
        
    }
}
