using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace MIT.Data.Model
{
    #region Contas Correntes
    public class Pendente
    {   
        [Key]
        public Guid id { get; set; }
                
        /// <summary>
        /// Dados Entidade 
        /// </summary>
        public string tipoEntidade { get; set; }
        public string entidade { get; set; }
        
        /// <summary>
        /// Dados do Documento
        /// </summary>
        public string documento { get; set; }
        
        public string moedaId { get; set; }
        public double cambio { get; set; }
        public double valorTotal { get; set; }
        public double valorPendente { get; set; }
        public double valorTotalMT { get; set; }
        public double valorPendenteMT { get; set; }
        public double contraValor { get; set; }

        public DateTime dataCriacao  {get;set;}
        public DateTime dataVencimento {get;set;}
         
        public string tipoDoc { get; set; }   
        public Int32 numDoc {get;set;}
        public string serie {get;set;}
        public string referencia {get;set;}

        public double valorActualizacao { get; set; }

        public virtual Moeda moeda { get; set; }


        public Pendente() { }

        public Pendente(ref string tipoEntidade, ref string entidade, ref string documento, ref string moeda, ref double cambio, ref double valorTotal, ref double valorPendente)
        {
            this.tipoEntidade = tipoEntidade;
            this.entidade = entidade;
            this.documento = documento;
            this.moedaId = moeda;
            this.cambio = cambio;
            this.valorTotal = valorTotal;
            this.valorPendente = valorPendente;
            this.valorActualizacao = valorActualizacao;

            this.valorTotalMT = valorTotal * cambio;
            this.valorPendenteMT = valorPendente * cambio;
        }
        
        
    }

    public class Extracto
    {

    }
    
    public class Documento
    {
        public Guid id {get;set;}
        public string numDoc {get;set;}
        //Prencido com H

        public string tipoRegisto {get;set;}
        // TipoDoc = "HON","AVE"
        public string tipoDoc {get;set;}
        public string codigoEntidade {get;set;}
        public string nomeEntidade {get;set;}
        public string moradaEntidade {get;set;}
        public string localidadeEntidade {get;set;}
        public string codigoPostaEntidade {get;set;}
        public string localidadePostalEntidade {get;set;}
        public string nifEntidade {get;set;}
        public string paisEntidade {get;set;}

        public string condicaoPag {get;set;}//= "1";

        public string modPag {get;set;} //= "1";
        public string codMoedaErp {get;set;} //= "MT";
        public int tipoMercado {get;set;} //= 0 ;
        public string segmentoErp {get;set;} 
        public string serie {get;set;}
        public System.DateTime dataEmissao {get;set;}
        public System.DateTime dataVencimento {get;set;}
        public string numeroDocumentoRef {get;set;}
        public double valorCambio {get;set;}
        public double percentagemDescontoEntidade {get;set;}
        public decimal percentagemDescontoFinanceiro {get;set;}

        public List<Documento_Item> documento_item = new List<Documento_Item>();

        public string documentoGerado;

        
    }

    public class DocumentoVenda : Documento
    {

    }

    public class DocumentoCompra : Documento
    {

    }

    public class Documentos_Pendentes
    {
        public string modulo { get; set; }
        public string tipoEntidade { get; set; }
        public string entidade { get; set; }
        public string tipoDoc { get; set; }
        public string numDoc { get; set; }
        public int numDocInt { get; set; }
        public Nullable<System.DateTime> dataDoc { get; set; }
        public Nullable<System.DateTime> dataVenc { get; set; }
        public Nullable<double> valorTotal { get; set; }
        public Nullable<double> valorPendente { get; set; }
        public string moeda { get; set; }
        public Nullable<double> cambio { get; set; }
        public Nullable<short> numAvisos { get; set; }
        public short numPrestacao { get; set; }
        public string serie { get; set; }
        public string conta { get; set; }

        public Entidade Entidade { get; set; }
    }

    public class DocumentoContaCorrente : Documento
    {

    }

    public class Documento_Item
    {

        //Prenchido com L

        public string tipoRegisto { get; set; }
        public string artigo { get; set; }

        public string descricao { get; set; }
        //Tipo Servico
        public string tipoArtigo { get; set; }
        public string taxaIva { get; set; }
        public bool movStock  { get; set; }
        public bool sujeitoDevolucao { get; set; }
        public string codUnidades { get; set; } //= "UN";
        public double precoUnitario { get; set; }
        public double quantidade { get; set; }
        public decimal descontoLinha { get; set; }
        public string codArmazem { get; set; }
        public string codLocalizacao;

        public string documentoId {get;set;}


        public virtual Documento documento { get; set; }
        
    }

    #endregion

    #region Movimentos Bancos
    public class Banco
    {
        [Key]
        public string bancoId { get; set; }

        [Required]
        public string descricao { get; set; }

        public string formatoId { get; set; }

        public virtual ICollection<ContaBancaria> listaContaBancaria { get; set; }


        public Banco()
        {
        }

        public Banco(string banco, string descricao, string formato)
        {
            this.bancoId = banco;
            this.descricao = descricao;
            this.formatoId = formato;
        }
    }

    public class ContaBancaria
    {
        [Key]
	    public string conta { get; set; }
        
        [Required]
        public string numConta { get; set; }
        
        [Required]
        public string descricao { get; set; }

        [Key]
	    public string bancoId { get; set; }

        [Key]
	    public string moedaId { get; set; }

        public Banco banco { get; set; }

        public Moeda moeda { get; set; }

        public virtual ICollection<CabecExtractoBancario> listaCabecExtractoBancario { get; set; }

	    public ContaBancaria()
	    {

	    }

	    public ContaBancaria(string conta, string numconta, string banco, string descricao, string moeda)
	    {
		    this.conta = conta;
		    this.numConta = numconta;
		    this.bancoId = banco;
		    this.descricao = descricao;
		    this.moedaId = moeda;
	    }

    }

    public class FormatoBancario
    {
        [Key]
        public string Id { get; set; }
        public string descricao { get; set; }
        public string separadorDecimal { get; set; }
        public string separadorMilhares { get; set; }

        public string separadorDatas { get; set; }

        public virtual ICollection<LinhasFormatoBancario> linhasFormatosBancarios { get; set; }

        public FormatoBancario()
        {
        }

        public FormatoBancario(string formato, string descricao, string separadorDecimal, string separadorMilhares, string separadorDatas)
        {
            this.Id = formato;
            this.descricao = descricao;
            this.separadorMilhares = separadorMilhares;
            this.separadorDecimal = separadorDecimal;
            this.separadorDatas = separadorDatas;

        }
    }

    public class LinhasFormatoBancario
    {
        public string formatoBancarioId { get; set; }
        public string tipoItem { get; set; }
        public string campo { get; set; }
        public int coluna { get; set; }

        public int linha { get; set; }

        public string formatoEspecial { get; set; }

        public FormatoBancario FormatoBancario { get; set; }

        public LinhasFormatoBancario()
        {
        }

        public LinhasFormatoBancario(string formato, string tipoItem, string campo, int coluna, int linha, string formatoEspecial)
        {
            this.formatoBancarioId = formato;
            this.tipoItem = tipoItem;
            this.campo = campo;
            this.coluna = coluna;
            this.linha = linha;
            this.formatoEspecial = formatoEspecial;
        }
    }

    public class CabecExtractoBancario
    {

        public CabecExtractoBancario()
        {
            // TODO: Complete member initialization 
        }

        public string contaId { get; set; }
        public string numeroConta { get; set; }
        public string numeroExtracto { get; set; }
        public string origem { get; set; }
        public System.DateTime dataInicial { get; set; }
        public System.DateTime dataFinal { get; set; }

        [Key()]
        public Guid id { get; set; }

        public virtual ContaBancaria conta { get; set; }

        public virtual ICollection<LinhasExtractoBancario> linhasExtractoBancario { get; set; }

        public CabecExtractoBancario(ref string conta, ref string NumeroConta, ref string NumeroExtracto, ref string Origem, ref System.DateTime DataInicial, ref System.DateTime DataFinal, ref Guid id)
        {
            this.contaId = conta;
            this.numeroConta = NumeroConta;
            this.numeroExtracto = NumeroExtracto;
            this.origem = Origem;
            this.dataFinal = DataFinal;
            this.dataInicial = DataInicial;
            this.id = id;
        }

    }

    public class LinhasExtractoBancario
    {
        public Guid id { get; set; }
        public Guid cabecExtractoBancarioId { get; set; }
        public System.DateTime dataMovimento { get; set; }
        public System.DateTime dataValor { get; set; }
        public string movimento { get; set; }
        public string natureza { get; set; }
        public string numero { get; set; }
        public string obs { get; set; }
        public double valorMov { get; set; }
        public double valorConta { get; set; }
        public string moedaMov { get; set; }
        public string moedaConta { get; set; }
        public bool emEstadoEdicao { get; set; }

        public virtual CabecExtractoBancario cabecExtactoBancario { get; set; }

        public LinhasExtractoBancario() { }

        public LinhasExtractoBancario(ref Guid Id, ref Guid IdCabecExtractoBancario, ref System.DateTime DataMovimento, ref System.DateTime DataValor, ref string Movimento, ref string Natureza, ref string Numero, ref string Obs, ref double ValorMov, ref double ValorConta,
        ref string MoedaMov, ref string MoedaConta, ref bool EmEstadoEdicao)
        {
            this.id = Id;
            this.cabecExtractoBancarioId = IdCabecExtractoBancario;
            this.dataMovimento = DataMovimento;
            this.dataValor = DataValor;
            this.movimento = Movimento;
            this.natureza = Natureza;
            this.numero = Numero;
            this.obs = Obs;
            this.valorConta = ValorConta;
            this.valorMov = ValorMov;
            this.valorConta = ValorConta;
            this.moedaMov = MoedaMov;
            this.moedaConta = MoedaConta;
            this.emEstadoEdicao = EmEstadoEdicao;

        }

        public double daValorDebito()
        {
            if (natureza == "D")
            {
                return valorMov;
            }
            else
            {
                return 0;
            }

        }
        public double daValorCredito()
        {
            if (natureza == "C")
            {
                return valorMov;
            }
            else
            {
                return 0;
            }

        }

    }

    public class HistoricoExpPS2
    {

        public string Opcoes { get; set; }
        public int Sequencia { get; set; }
        public string IdTEServicosBancarios { get; set; }
        public double ValorTotal { get; set; }
        public int TotalRegistosExportados { get; set; }
        public string UltimoLogin { get; set; }
        public System.DateTime DataExportacao { get; set; }
        public int IdExportacao { get; set; }

        public HistoricoExpPS2(ref string Opcoes, ref int Sequencia, ref string IdTEServicosBancarios, ref double ValorTotal, ref int TotalRegistosExportados, ref string UltimoLogin, ref System.DateTime DataExportacao, ref int IdExportacao)
        {
            this.Opcoes = Opcoes;
            this.Sequencia = Sequencia;
            this.IdTEServicosBancarios = IdTEServicosBancarios;
            this.ValorTotal = ValorTotal;
            this.TotalRegistosExportados = TotalRegistosExportados;
            this.DataExportacao = DataExportacao;
            this.UltimoLogin = UltimoLogin;
            this.IdExportacao = IdExportacao;
        }
    }

    public class MovimentosBancos
    {
        [Key()]
        public string id { get; set; }

        public string conta { get; set; }
        public string movim { get; set; }
        public double valor { get; set; }
        public string entidade { get; set; }
        public string tipoEntidade { get; set; }
        public string nomeEntidade { get; set; }

        public System.DateTime dataMovimento { get; set; }
        public string Observacao { get; set; }

        public string IdExportacaoPS2 { get; set; }
        
        public string NibExportarPS2 { get; set; }
        public string referencia { get; set; }

        public string serieOriginal { get; set; }
        public string tipoDocOriginal { get; set; }
        public int numDocOriginal { get; set; }
        
        public string moedaIso;
        
        public MovimentosBancos(ref string Conta, ref string Movim, double Valor, ref string Entidade, string TipoEntidade, ref System.DateTime DtMov, ref string Obsv, ref string IdExportacaoPS2, string id, string NibExportarPS2,
        string SerieOriginal, string TipoDocOriginal, int NumDocOriginal, ref string Referencia, ref string MoedaIso)
        {
            this.conta = Conta;
            this.movim = Movim;
            this.valor = Valor;
            this.entidade = Entidade;
            this.dataMovimento = DtMov;
            this.Observacao = Obsv;
            this.tipoEntidade = TipoEntidade;
            this.IdExportacaoPS2 = IdExportacaoPS2;
            this.id = id;
            this.NibExportarPS2 = NibExportarPS2;
            this.serieOriginal = SerieOriginal;
            this.tipoDocOriginal = TipoDocOriginal;
            this.numDocOriginal = NumDocOriginal;
            this.referencia =Referencia;
            this.moedaIso = MoedaIso;

        }

        
    }

    public class EntidadeExportacao
    {

        public string TipoEntidade { get; set; }
        public string Entidade { get; set; }
        public string Nome { get; set; }
        public string NrConta { get; set; }
        public string NIB { get; set; }
        public double Valor { get; set; }
        public string Banco { get; set; }
        public string Email { get; set; }

        public IEnumerable<MovimentosBancos> listaMovimentos { get; set; }

        public EntidadeExportacao(ref string TipoEntidade, ref string Entidade, ref string NrConta, ref string NIB, ref double Valor, ref string Banco, ref string Email, ref string Nome)
        {
            this.TipoEntidade = TipoEntidade;
            this.Entidade = Entidade;
            this.NrConta = NrConta;
            this.NIB = NIB;
            this.Valor = Valor;
            this.Banco = Banco;
            this.Email = Email;
            this.Nome = Nome;
        }
    }

    #endregion

    #region Facturacao
    public class CabecDoc
    {


        public string numDoc;
        //Prencido com H

        public string tipoRegisto;
        // TipoDoc = "HON","AVE"
        public string tipoDoc;
        public string codigoEntidade;
        public string nomeEntidade;
        public string moradaEntidade;
        public string localidadeEntidade;
        public string codigoPostaEntidade;
        public string localidadePostalEntidade;
        public string nifEntidade;
        public string paisEntidade;

        public string condicaoPag = "1";

        public string modPag = "1";
        public string codMoedaErp = "MT";
        public int tipoMercado = 0;
        public string segmentoErp;
        public string serie;
        public System.DateTime dataEmissao;
        public System.DateTime dataVencimento;
        public string numeroDocumentoRef;
        public double valorCambio;
        public double percentagemDescontoEntidade;

        public decimal percentagemDescontoFinanceiro;

        public List<LinhasDoc> linhasdoc = new List<LinhasDoc>();

        public string documentoGerado;

        public string condicoesPag
        {
            get { return this.condicaoPag; }

            set
            {
                if (string.IsNullOrEmpty(value))
                {
                    this.condicaoPag = "1";

                }
                else
                {
                    this.condicaoPag = value;
                }

            }
        }

        public string modoPagamento
        {
            get { return this.modPag; }

            set
            {
                if (string.IsNullOrEmpty(value))
                {
                    this.modPag = "1";

                }
                else
                {
                    this.modPag = value;
                }

            }
        }

        public string codigoMoeda
        {
            get { return this.codMoedaErp; }

            set
            {
                if (string.IsNullOrEmpty(value))
                {
                    this.codMoedaErp = "MT";

                }
                else
                {
                    this.modPag = value;
                }

            }
        }

        public int tipoMercadoErp
        {
            get { return this.tipoMercado; }

            set
            {
                this.tipoMercado = value;
            }
        }

        public System.DateTime data_Emissao
        {
            get { return this.dataEmissao; }


            set { }
        }

        public double valor_Cambio
        {
            get { return this.valorCambio; }

            set
            {
                
                    this.valorCambio = value;
                

            }
        }

        public string paraString()
        {
            return "O Relatorio " + numeroDocumentoRef + " gerou o " + tipoDoc + " - " + numDoc + "/" + serie;
        }

    }

    public class LinhasDoc
    {

        //Prenchido com L

        public string tipoRegisto { get; set; }
        public string artigo { get; set; }

        public string descricao { get; set; }
        //Tipo Servico
        public string tipoArtigo { get; set; }
        public string taxaIva { get; set; }
        public bool movStock  { get; set; }
        public bool sujeitoDevolucao { get; set; }
        public string codUnidades { get; set; } //= "UN";
        public double precoUnitario { get; set; }
        public double quantidade { get; set; }
        public decimal descontoLinha { get; set; }
        public string codArmazem { get; set; }
        public string codLocalizacao;


        public virtual CabecDoc cabecDoc { get; set; }
        
    }
    #endregion

    #region Inventario
    
    #endregion
}