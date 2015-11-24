using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace CRM.Models
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

        public virtual List <Departamento> departamento { get; set; }
    }
}
