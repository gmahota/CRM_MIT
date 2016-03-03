using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;


namespace MIT.Data.Model
{
    public class Moeda
    {
        public string moedaId { get; set; }
        
        [Required]
        public string descricao { get; set; }
        public double compra { get; set; }
        public double venda { get; set; }
        public DateTime? data { get; set; }
        public string codigoIso { get; set; }
        
        public virtual ICollection<Pendente> listaPendentes { get; set; }

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
    }

    public class Entidade
    {
        public string tipoEntidade { get; set; }
        public string entidade { get; set; }
        public string nome { get; set; }
        public string fac_Mor { get; set; }
        public string fac_Local { get; set; }
        public string numContrib { get; set; }
        public string pais { get; set; }
        public string fac_Tel { get; set; }
        public string moeda { get; set; }
        public bool enviaCobranca { get; set; }
        public double valorPendente { get; set; }
        public double valorDebitoTotal { get; set; }
        public double valorCreditoTotal { get; set; }

        public List<Documentos_Pendentes> documentosPendentes { get; set; }
        public List<Contactos> contactos { get; set; }
    }

    public class Contactos
    {
        public string Nome { get; set; }
        public string PrimeiroNome { get; set; }
        public string UltimoNome { get; set; }
        public string Titulo { get; set; }
        public string Email { get; set; }
        public string EmailAssist { get; set; }
        public string tipoContacto { get; set; }
        public string telefone { get; set; }
    }

    public class Report
    {
        public string empresa { get; set; }
        public string nomeEmpresa { get; set; }
        public string tipoEntidade { get; set; }
        public string entidade { get; set; }
        public string caminho { get; set; }
        public string to { get; set; }
        public string cc { get; set; }

    }
}