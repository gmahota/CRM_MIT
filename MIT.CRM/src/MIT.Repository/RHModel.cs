using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace MIT.Repository
{
    public class Funcionario
    {
        public int id { get; set; }

        [StringLength(250)]
        public string codigo { get; set; }

        [StringLength(250)]
        public string nome { get; set; }

        [StringLength(25)]
        [Display(Name = "Alcunha")]
        public string nomeAbreviado { get; set; }

        [StringLength(50)]
        public string localidade { get; set; }

        [StringLength(50)]
        public string naturalidade { get; set; }

        [StringLength(50)]
        public string distrito { get; set; }

        [StringLength(50)]
        public string nacionalidade { get; set; }

        [StringLength(20)]
        public string telefone { get; set; }

        [StringLength(30)]
        public string estadoCivil { get; set; }

        [DefaultValue("true")]
        public bool sexo { get; set; }

        public DateTime? dataNascimento { get; set; }

        public DateTime? dataAdmissao { get; set; }

        public DateTime? dataReadmissao { get; set; }

        public DateTime? dataFimContrato { get; set; }

        [StringLength(30)]
        public string categoria { get; set; }

        [StringLength(30)]
        public string profissao { get; set; }

        [StringLength(30)]
        public string classificacao { get; set; }

        public DateTime? dataClassificacao { get; set; }

        [StringLength(30)]
        public string habilitacao { get; set; }

        [StringLength(50)]
        public string email { get; set; }

        [StringLength(30)]
        public string telemovel { get; set; }

        [StringLength(30)]
        public string telefoneAlternativo { get; set; }

        [DefaultValue("true")]
        public bool activo { get; set; }

        public int departamentoId { get; set; }

        [StringLength(20)]
        public string departamentoCodigo { get; set; }

        public string empresaId { get; set; }

        public int? avatarId { get; set; }

        [StringLength(25)]
        public string cargo { get; set; }

        public string utilizadorId { get; set; }

        [ForeignKey("departamentoId")]
        public virtual Departamento departamento { get; set; }

        [ForeignKey("empresaId")]
        public virtual Empresa empresa { get; set; }

        public List<FuncInfFerias> funcInfFerias { get; set; }

        public List<Ferias> ferias { get; set; }

        public List<Ferias_Itens> ferias_item { get; set; }

        [ForeignKey("utilizadorId")]
        public virtual ApplicationUser utilizador { get; set; }

        [ForeignKey("avatarId")]
        public virtual FileDescription avatar { get; set; }

        public virtual List<Responsalvel_Departamento> listaResponsavelDepartamentos { get; set; }

    }

    public class FuncInfFerias
    {
        public int id { get; set; }
        
        public short ano { get; set; }

        public int funcionarioId { get; set; }

        public double diasDireito { get; set; }

        public double diasAdicionais { get; set; }

        public double diasAnoAnterior { get; set; }

        public double totalDias { get; set; }

        public double diasPorGozar { get; set; }

        public double diasJaGozados { get; set; }

        public double diasPorMarcar { get; set; }

        public double diasFeriasJaPagas { get; set; }

        public bool funcSemFerias { get; set; }

        [StringLength(250)]
        public string notas { get; set; }

        [ForeignKey("funcionarioId")]
        public virtual Funcionario funcionario { get; set; }

        public virtual List<Ferias_Itens> ferias_Itens { get; set; }
        
    }

    public class FuncFerias
    {
        public int id { get; set; }

        public short ano { get; set; }
        
        public int funcionarioId { get; set; }

        [Required]
        public DateTime dataFeria { get; set; }
        public bool estadoGozo { get; set; }
        public bool originouFalta { get; set; }
        public int tipoMarcacao { get; set; }
        public bool originouFaltaSubAlim { get; set; }

        [ForeignKey("funcionarioId")]
        public virtual Funcionario funcionario { get; set; }
    }

    public class Departamento
    {
        public int Id { get; set; }

        [Required]
        public string departamento { get; set; }
        
        public string empresaId { get; set; }

        [Required]
        [StringLength(30)]
        public string descricao { get; set; }

        [Display(Name = "Responsavel")]
        public string responsavelId { get; set; }

        [DefaultValue(true)]
        public bool activo { get; set; }
        
        [ForeignKey("responsavelId")]
        public virtual ApplicationUser responsavel { get; set; }

        [ForeignKey("empresaId")]
        public virtual Empresa empresa { get; set; }
        
        public virtual List<Funcionario> listaFuncionarios { get; set; }

        public virtual List<Responsalvel_Departamento> listaResponsavelDepartamentos { get; set; }

        //public ICollection<ApplicationUser> listResponsible { get; set; }
    }

    public class Responsalvel_Departamento
    {
        [Key]
        public int Id { get; set; }

        public int? funcionarioId { get; set; }

        public int? departamentoId { get; set; }

        public virtual Funcionario funcionario { get; set; }

        public virtual Departamento departamento { get; set; }
    }

    public class Ferias
    {
        public int id { get; set; }
        public DateTime dataInicio { get; set; }
        public DateTime dataFim { get; set; }
        public int funcionarioId { get; set; }

        public virtual List <Ferias_Itens> itens {get;set;}

        [ForeignKey("funcionarioId")]
        public virtual Funcionario funcionario { get; set; }

    }

    public class Ferias_Itens
    {
        public int id { get; set; }

        public short ano { get; set; }

        public int funcionarioId { get; set; }

        public int? feriasId { get; set; }

        public int? feriasInfFeriasId { get; set; }

        [Required]
        public DateTime dataFeria { get; set; }

        public bool estadoGozo { get; set; }
        public bool originouFalta { get; set; }
        public int tipoMarcacao { get; set; }
        public bool originouFaltaSubAlim { get; set; }

        [StringLength(10)]
        public string estado { get; set; }

        [StringLength(10)]
        public string tipo { get; set; }

        [StringLength(250)]
        public string notas { get; set; }

        [ForeignKey("feriasId")]
        public Ferias ferias { get; set; }

        [ForeignKey("funcionarioId")]
        public virtual Funcionario funcionario { get; set; }

        [ForeignKey("feriasInfFeriasId")]
        public virtual FuncInfFerias funcInfFerias { get; set; }

        public virtual List<Historio_Ferias_Item> historio_Ferias_Itens { get; set; }

        public string daString_Data_Feria()
        {
            return dataFeria.ToString("dd/MM/yyyy");
        }
    }

    public class Historio_Ferias_Item
    {
        public int id { get; set; }

        public int ferias_item_id { get; set; }

        [StringLength(20)]
        public string estado { get; set; }

        [StringLength(250)]
        public string notas { get; set; }

        public string utilizadorId { get; set; }

        public DateTime data { get; set; }

        [ForeignKey("utilizadorId")]
        public virtual ApplicationUser utilizador { get; set; }

        [ForeignKey("ferias_item_id")]
        public virtual Ferias_Itens ferias_Itens { get; set; }
    }
}
